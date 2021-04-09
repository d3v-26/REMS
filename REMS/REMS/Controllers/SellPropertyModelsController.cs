using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using REMS.Data;
using REMS.Models;
using REMS.ViewModels;

namespace REMS.Controllers
{
    public class SellPropertyModelsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IWebHostEnvironment _hostingEnvironment;
        
        public SellPropertyModelsController(ApplicationDbContext context, UserManager<IdentityUser> userManager, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _hostingEnvironment = hostingEnvironment;
        }
        [Authorize(Roles = "Admin, Seller")]
        // GET: SellPropertyModels
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            string name = await _userManager.GetUserNameAsync(user);
            var models = await _context.SellPropertyModel.Where(x => x.owner == name).ToListAsync();
            if(models.Count > 0)
            {
                ViewBag.empty = false;
            }
            else
            {
                ViewBag.empty = true;
            }
            return View(models);
        }
        [Authorize(Roles = "Admin, Seller")]
        // GET: SellPropertyModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
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
            ViewBag.isSold = false;
            var comments = _context.CommentModel.Where(x => x.propname == sellPropertyModel.name).ToList();
            var bids = _context.BidModel.Where(x => x.propname == sellPropertyModel.name).ToList();
            var p = _context.PropertyModel.FirstOrDefault(x => x.name == sellPropertyModel.name);
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
                ViewBag.hasBids = true;
            }
            else
            {
                ViewBag.hasBids = false;
            }
            dynamic model = new ExpandoObject();
            model.property = sellPropertyModel;
            model.comments = comments;
            if (p.propertyDetail == PropertyDetail.Unsold)
            {
                model.bids = bids;
            }
            else
            {
                ViewBag.isSold = true;
                var winner = _context.AuctionModel.FirstOrDefault(x => x.propname == sellPropertyModel.name);
                ViewBag.winner = winner.custname;
            }
            return View(model);
        }
        [Authorize(Roles = "Admin, Seller")]
        public async Task<IActionResult> Comments(int id, string comment)
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
            return Redirect("Details/" + id.ToString());
        }
        [Authorize(Roles = "Admin, Seller")]
        public async Task<IActionResult> Bid(int id, int bidId)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                string name = await _userManager.GetUserNameAsync(user);
                var prop = _context.SellPropertyModel.FirstOrDefault(x => x.id == id);
                var property = _context.PropertyModel.FirstOrDefault(x => x.name == prop.name);
                var bid = _context.BidModel.FirstOrDefault(x => x.id == bidId);
                property.propertyDetail = PropertyDetail.Sold;
                _context.Update(property);
                AuctionModel a = new AuctionModel
                {
                    custname = bid.customername,
                    propname = prop.name,
                    bid = bidId
                };
                _context.AuctionModel.Add(a);
            }
            await _context.SaveChangesAsync();
            return Redirect("Details/" + id.ToString());
        }
        [Authorize(Roles = "Admin, Seller")]
        // GET: SellPropertyModels/Create
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "Admin, Seller")]
        // POST: SellPropertyModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SellPropertyViewModel sellPropertyViewModel)
        {

            string pathForPic = String.Empty;
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                string name = await _userManager.GetUserNameAsync(user);
                if (sellPropertyViewModel.image != null)
                {
                    string uploadFolder = Path.Combine(_hostingEnvironment.WebRootPath, "Photos");
                    pathForPic = Guid.NewGuid().ToString() + "_" + sellPropertyViewModel.image.FileName;
                    string filePath = Path.Combine(uploadFolder, pathForPic);
                    sellPropertyViewModel.image.CopyTo(new FileStream(filePath, FileMode.Create));
                    SellPropertyModel r = new SellPropertyModel
                    {
                        name = sellPropertyViewModel.name,
                        image = pathForPic,
                        address = sellPropertyViewModel.address,
                        description = sellPropertyViewModel.description,
                        owner = name,
                        startprice = sellPropertyViewModel.startprice,
                        propertyType = sellPropertyViewModel.propertyType                       
                    };
                    _context.Add(r);

                    PropertyModel p = new PropertyModel
                    {
                        address = r.address,
                        description = r.description,
                        image = r.image,
                        name = r.name,
                        owner = r.owner,
                        price = r.startprice,
                        propertyDetail = PropertyDetail.Unsold,
                        propertyMode = PropertyMode.Sell,
                        propertyStatus = PropertyStatus.Pending,
                        propertyType = r.propertyType
                    };
                    _context.PropertyModel.Add(p);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sellPropertyViewModel);            
        }
        [Authorize(Roles = "Admin, Seller")]
        // GET: SellPropertyModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sellPropertyModel = await _context.SellPropertyModel.FindAsync(id);
            if (sellPropertyModel == null)
            {
                return NotFound();
            }

            ViewBag.path = sellPropertyModel.image;
            SellPropertyViewModel sellPropertyViewModel = new SellPropertyViewModel
            {
                address = sellPropertyModel.address,
                description = sellPropertyModel.description,
                name = sellPropertyModel.name,
                startprice = sellPropertyModel.startprice,
                propertyType = sellPropertyModel.propertyType,
                image = null
            };
            return View(sellPropertyViewModel);
        }
        [Authorize(Roles = "Admin, Seller")]
        // POST: SellPropertyModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SellPropertyViewModel sellPropertyViewModel)
        {
            if (id != sellPropertyViewModel.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string uploadFolder = Path.Combine(_hostingEnvironment.WebRootPath, "Photos");
                    /*RentPropertyModel oldModel = _context.RentPropertyModel.FirstOrDefault(x => x.id == sellPropertyViewModel.id);
                    string oldPic = Path.Combine(uploadFolder, oldModel.image);*/
                    /*if (System.IO.File.Exists(oldPic))
                    {
                        System.IO.File.Delete(oldPic);
                    }*/
                    var user = await _userManager.GetUserAsync(HttpContext.User);
                    string name = await _userManager.GetUserNameAsync(user);
                    string pathForPic = Guid.NewGuid().ToString() + "_" + sellPropertyViewModel.image.FileName;
                    string filePath = Path.Combine(uploadFolder, pathForPic);
                    sellPropertyViewModel.image.CopyTo(new FileStream(filePath, FileMode.Create));
                    var s = _context.SellPropertyModel.FirstOrDefault(x => x.id == id);
                    s.address = sellPropertyViewModel.address;
                    s.description = sellPropertyViewModel.description;
                    s.image = pathForPic;
                    s.name = sellPropertyViewModel.name;
                    s.owner = name;
                    s.propertyType = sellPropertyViewModel.propertyType;
                    s.startprice = sellPropertyViewModel.startprice;
                    
                    PropertyModel p = _context.PropertyModel.FirstOrDefault(x => x.name == s.name);
                    p.address = s.address;
                    p.description = s.description;
                    p.image = s.image;
                    p.name = s.name;
                    p.owner = s.owner;
                    p.price = s.startprice;
                    p.propertyType = s.propertyType;

                    _context.PropertyModel.Update(p);
                    _context.SellPropertyModel.Update(s);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SellPropertyModelExists(sellPropertyViewModel.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(sellPropertyViewModel);
        }
        [Authorize(Roles = "Admin, Seller")]
        // GET: SellPropertyModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
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

            return View(sellPropertyModel);
        }
        [Authorize(Roles = "Admin, Seller")]
        // POST: SellPropertyModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sellPropertyModel = await _context.SellPropertyModel.FindAsync(id);
            string file = sellPropertyModel.image;
            string uploadFolder = Path.Combine(_hostingEnvironment.WebRootPath, "Photos");
            string fileToDelete = Path.Combine(uploadFolder, file);
            /*if (System.IO.File.Exists(fileToDelete))
            {
                System.IO.File.Delete(fileToDelete);                
            }*/
            _context.SellPropertyModel.Remove(sellPropertyModel);
            var comments = _context.CommentModel.Where(x => x.propname == sellPropertyModel.name).ToList();
            foreach (var comment in comments)
            {
                _context.CommentModel.Remove(comment);
            }
            var bids = _context.BidModel.Where(x => x.propname == sellPropertyModel.name).ToList();
            foreach (var bid in bids)
            {
                _context.BidModel.Remove(bid);
            }
            var p = _context.PropertyModel.FirstOrDefault(x => x.name == sellPropertyModel.name);
            _context.PropertyModel.Remove(p);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SellPropertyModelExists(int id)
        {
            return _context.SellPropertyModel.Any(e => e.id == id);
        }
    }
}
