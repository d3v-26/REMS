using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using REMS.Data;
using REMS.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace REMS.Controllers
{
    public class PropertyController : Controller
    {
        private ApplicationDbContext _context;
        private UserManager<IdentityUser> _userManager;

        public PropertyController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            string name = await _userManager.GetUserNameAsync(user);
            var auctions = _context.AuctionModel.Where(x => x.custname == name).ToList();
            List<PropertyModel> props = new List<PropertyModel>();
            foreach(var a in auctions)
            {
                var p = _context.PropertyModel.FirstOrDefault(x => x.name == a.propname);
                if(p.propertyMode == PropertyMode.Rent)
                {
                    var r = _context.RentPropertyModel.FirstOrDefault(x => x.name == p.name);
                    p.id = r.id;
                }
                else
                {
                    var s = _context.SellPropertyModel.FirstOrDefault(x => x.name == p.name);
                    p.id = s.id;
                }
                props.Add(p);
            }
            if(props.Count == 0)
            {
                ViewBag.notempty = false;
            }
            else
            {
                ViewBag.notempty = true;
            }
            return View(props);
        }
        public IActionResult Rent(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;
            List<RentPropertyModel> properties = new List<RentPropertyModel>();
            var model = _context.PropertyModel.Where(x => x.propertyStatus == PropertyStatus.Verified && x.propertyDetail == PropertyDetail.Unsold && x.propertyMode == PropertyMode.Rent).ToList();
            foreach(var m in model)
            {
                var rp = _context.RentPropertyModel.FirstOrDefault(x => x.name == m.name);
                RentPropertyModel r = new RentPropertyModel
                {
                    id = rp.id,
                    address = m.address,
                    description = m.description,
                    image = m.image,
                    name = m.name,
                    owner = m.owner,
                    price = m.price,
                    propertyType = m.propertyType
                };
                properties.Add(r);
            }
            if (!String.IsNullOrEmpty(searchString))
            {
                properties = properties.Where(x=>x.name.Contains(searchString) || x.address.Contains(searchString)).ToList();
                ViewBag.visible = true;
            }
            else
            {
                ViewBag.visible = false;
            }
            return View(properties);
        }
        
        public IActionResult Sell(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;
            List<SellPropertyModel> properties = new List<SellPropertyModel>();
            var model = _context.PropertyModel.Where(x => x.propertyDetail == PropertyDetail.Unsold && x.propertyMode == PropertyMode.Sell && x.propertyStatus == PropertyStatus.Verified).ToList();
            foreach(var m in model)
            {
                var sp = _context.SellPropertyModel.FirstOrDefault(x => x.name == m.name);
                SellPropertyModel s = new SellPropertyModel
                {
                    id = sp.id,
                    address = m.address,
                    description = m.description,
                    image = m.image,
                    name = m.name,
                    owner = m.owner,
                    propertyType = m.propertyType,
                    startprice = m.price
                };
                properties.Add(s);
            }
            if (!String.IsNullOrEmpty(searchString))
            {
                properties = properties.Where(x => x.name.Contains(searchString) || x.address.Contains(searchString)).ToList();
                ViewBag.visible = true;
            }
            else
            {
                ViewBag.visible = false;
            }
            return View(properties);
        }
        [Authorize(Roles = "Admin, Seller, Customer")]
        public async Task<IActionResult> RComments(int id, string comment)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                string name = await _userManager.GetUserNameAsync(user);
                var p = _context.RentPropertyModel.FirstOrDefault(x => x.id == id);
                CommentModel commentModel = new CommentModel
                {
                    comment = comment,
                    name = name,
                    propname = p.name
                };
                _context.CommentModel.Add(commentModel);
            }
            await _context.SaveChangesAsync();
            return Redirect("RentDetails/" + id.ToString());
        }
        [Authorize(Roles = "Admin, Seller, Customer")]
        public async Task<IActionResult> SComments(int id, string comment)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                string name = await _userManager.GetUserNameAsync(user);
                var p = _context.SellPropertyModel.FirstOrDefault(x => x.id == id);
                CommentModel commentModel = new CommentModel
                {
                    comment = comment,
                    name = name,
                    propname = p.name
                };
                _context.CommentModel.Add(commentModel);
            }
            await _context.SaveChangesAsync();
            return Redirect("SellDetails/" + id.ToString());
        }
        public async Task<IActionResult> RentDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rentPropertyModel = await _context.RentPropertyModel.FirstOrDefaultAsync(m => m.id == id);
            if (rentPropertyModel == null)
            {
                return NotFound();
            }
            ViewBag.path = rentPropertyModel.image;
            ViewBag.id = rentPropertyModel.id;
            var user = await _userManager.GetUserAsync(HttpContext.User);
            string name = await _userManager.GetUserNameAsync(user);
            var comments = _context.CommentModel.Where(x => x.propname == rentPropertyModel.name).ToList();
            var bid = _context.BidModel.Where(x => x.propname == rentPropertyModel.name && x.customername == name).ToList();
            dynamic model = new ExpandoObject();
            model.property = rentPropertyModel;
            model.comments = comments;
            if (comments.Count > 0)
            {
                ViewBag.show = true;
            }
            else
            {
                ViewBag.show = false;
            }
            if (bid.Count > 0)
            {
                model.bids = bid.ElementAt(0);
                ViewBag.isApplied = true;
                var b = bid.ElementAt(0);
                var a = _context.AuctionModel.FirstOrDefault(x => x.bid == b.id);
                if(a!=null && a.custname == name && a.propname == rentPropertyModel.name)
                {
                    ViewBag.won = true;
                }
                else
                {
                    ViewBag.won = false;
                }
            }
            else
            {
                ViewBag.isApplied = false;
            }
            return View(model);
        }

        public async Task<IActionResult> SellDetails(int? id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            string name = await _userManager.GetUserNameAsync(user);
            if (id == null)
            {
                return NotFound();
            }

            var sellPropertyModel = await _context.SellPropertyModel
                .FirstOrDefaultAsync(m => m.id == id);
            if (sellPropertyModel == null)
            {
                return NotFound();
            }
            ViewBag.path = sellPropertyModel.image;
            ViewBag.id = sellPropertyModel.id;
            var comments = _context.CommentModel.Where(x => x.propname == sellPropertyModel.name).ToList();
            var bids = _context.BidModel.Where(x => x.propname == sellPropertyModel.name && x.customername == name).ToList();
            var p = _context.PropertyModel.FirstOrDefault(x => x.name == sellPropertyModel.name);
            dynamic model = new ExpandoObject();
            model.property = sellPropertyModel;
            model.comments = comments;
            if (comments.Count > 0)
            {
                ViewBag.show = true;
            }
            else
            {
                ViewBag.show = false;
            }
            if (bids.Count > 0)
            {
                model.bids = bids.ElementAt(0);
                ViewBag.isApplied = true;
                var b = bids.ElementAt(0);
                var a = _context.AuctionModel.FirstOrDefault(x => x.bid == b.id);
                if (a != null && a.custname == name && a.propname == sellPropertyModel.name)
                {
                    ViewBag.won = true;
                }
                else
                {
                    ViewBag.won = false;
                }
            }
            else
            {
                ViewBag.isApplied = false;                
            }
            return View(model);
        }
        [Authorize(Roles = "Admin, Seller, Customer")]
        public async Task<IActionResult> RentBid(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = await _userManager.GetUserAsync(HttpContext.User);
            string name = await _userManager.GetUserNameAsync(user);
            var rentPropertyModel = await _context.RentPropertyModel.FirstOrDefaultAsync(m => m.id == id);
            if (rentPropertyModel == null)
            {
                return NotFound();
            }
            BidModel b = new BidModel
            {
                bid = rentPropertyModel.price,
                customername = name,
                propname = rentPropertyModel.name
            };
            _context.BidModel.Add(b);
            _context.SaveChanges();
            return Redirect("~/Property/RentDetails/"+ id.ToString());
        }
        [Authorize(Roles = "Admin, Seller, Customer")]
        public async Task<IActionResult> Bid(int? id, int bid)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            string name = await _userManager.GetUserNameAsync(user);
            if (id == null)
            {
                return NotFound();
            }

            var sellPropertyModel = await _context.SellPropertyModel
                .FirstOrDefaultAsync(m => m.id == id);
            if (sellPropertyModel == null)
            {
                return NotFound();
            }

            BidModel b = new BidModel
            {
                bid = bid,
                customername = name,
                propname = sellPropertyModel.name
            };
            _context.BidModel.Add(b);
            _context.SaveChanges();
            return Redirect("~/Property/SellDetails/" + id.ToString());
        }

        [Authorize(Roles = "Admin,Customer")]
        public IActionResult Convert()
        {
            return View();
        }

        [Authorize(Roles = "Admin,Customer")]
        public async Task<IActionResult> ConvertNow()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            await _userManager.AddToRoleAsync(user, "Seller");
            await _userManager.RemoveFromRoleAsync(user, "Customer");
            await _userManager.UpdateAsync(user);
            return View();
        }
    }
}
