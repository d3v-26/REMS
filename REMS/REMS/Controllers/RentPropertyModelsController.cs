using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using REMS.Data;
using REMS.Models;
using REMS.ViewModels;

namespace REMS.Controllers
{
    public class RentPropertyModelsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public RentPropertyModelsController(ApplicationDbContext context, UserManager<IdentityUser> userManager, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
            _userManager = userManager;
        }
        [Authorize(Roles = "Admin, Seller")]
        // GET: RentPropertyModels
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            string name = await _userManager.GetUserNameAsync(user);
            var models = await _context.RentPropertyModel.Where(x => x.owner == name).ToListAsync();
            if (models.Count > 0)
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
        // GET: RentPropertyModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rentPropertyModel = await _context.RentPropertyModel
                .FirstOrDefaultAsync(m => m.id == id);
            if (rentPropertyModel == null)
            {
                return NotFound();
            }
            ViewBag.path = rentPropertyModel.image;
            ViewBag.id = rentPropertyModel.id;
            ViewBag.isSold = false;
            var comments = _context.CommentModel.Where(x => x.propname == rentPropertyModel.name).ToList();
            var bids = _context.BidModel.Where(x => x.propname == rentPropertyModel.name).ToList();
            var p = _context.PropertyModel.FirstOrDefault(x => x.name == rentPropertyModel.name);
            if(comments.Count > 0)
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
            model.property = rentPropertyModel;
            model.comments = comments;
            if(p.propertyDetail == PropertyDetail.Unsold)
            {
                model.bids = bids;
            }
            else
            {
                ViewBag.isSold = true;
                var winner = _context.AuctionModel.FirstOrDefault(x => x.propname == rentPropertyModel.name);
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
            return Redirect("Details/" + id.ToString());
        }
        [Authorize(Roles = "Admin, Seller")]
        public async Task<IActionResult> Bid(int id, int bidId)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                string name = await _userManager.GetUserNameAsync(user);
                var p = _context.RentPropertyModel.FirstOrDefault(x => x.id == id);
                var property = _context.PropertyModel.FirstOrDefault(x => x.name == p.name);
                var bid = _context.BidModel.FirstOrDefault(x => x.id == bidId);
                property.propertyDetail = PropertyDetail.Sold;
                _context.Update(property);
                AuctionModel a = new AuctionModel
                {
                    custname = bid.customername,
                    propname = p.name,
                    bid = bidId
                };
                _context.AuctionModel.Add(a);
            }
            await _context.SaveChangesAsync();
            return Redirect("Details/" + id.ToString());
        }
        [Authorize(Roles = "Admin, Seller")]
        // GET: RentPropertyModels/Create
        public IActionResult Create()
        {
            return View();
        }
        [Authorize]
        // POST: RentPropertyModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RentPropertyViewModel rentPropertyViewModel)
        {
            string pathForPic = String.Empty;
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                string name = await _userManager.GetUserNameAsync(user);
                if (rentPropertyViewModel.image != null)
                {
                    string uploadFolder = Path.Combine(_hostingEnvironment.WebRootPath, "Photos");
                    pathForPic = Guid.NewGuid().ToString() + "_" + rentPropertyViewModel.image.FileName;
                    string filePath = Path.Combine(uploadFolder, pathForPic);
                    rentPropertyViewModel.image.CopyTo(new FileStream(filePath, FileMode.Create));
                    RentPropertyModel r = new RentPropertyModel
                    {
                        name = rentPropertyViewModel.name,
                        image = pathForPic,
                        address = rentPropertyViewModel.address,
                        description = rentPropertyViewModel.description,
                        owner = name,
                        price = rentPropertyViewModel.price,
                        propertyType = rentPropertyViewModel.propertyType
                    };
                    _context.Add(r);

                    PropertyModel p = new PropertyModel
                    {
                        address = r.address,
                        description = r.description,
                        image = r.image,
                        name = r.name,
                        owner = r.owner,
                        price = r.price,
                        propertyDetail = PropertyDetail.Unsold,
                        propertyMode = PropertyMode.Rent,
                        propertyStatus = PropertyStatus.Pending,
                        propertyType = r.propertyType
                    };
                    _context.PropertyModel.Add(p);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rentPropertyViewModel);
        }
        [Authorize(Roles = "Admin, Seller")]
        // GET: RentPropertyModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rentPropertyModel = await _context.RentPropertyModel.FindAsync(id);
            if (rentPropertyModel == null)
            {
                return NotFound();
            }
            ViewBag.path = rentPropertyModel.image;                        
            RentPropertyViewModel rentPropertyViewModel = new RentPropertyViewModel
            {
                address = rentPropertyModel.address,
                description = rentPropertyModel.description,
                name = rentPropertyModel.name,
                price = rentPropertyModel.price,
                propertyType = rentPropertyModel.propertyType,
                image = null
            };
            return View(rentPropertyViewModel);
        }
        [Authorize(Roles = "Admin, Seller")]
        // POST: RentPropertyModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, RentPropertyViewModel rentPropertyViewModel)
        {
            if (id != rentPropertyViewModel.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string uploadFolder = Path.Combine(_hostingEnvironment.WebRootPath, "Photos");
                    var user = await _userManager.GetUserAsync(HttpContext.User);
                    string name = await _userManager.GetUserNameAsync(user);
                    string pathForPic = Guid.NewGuid().ToString() + "_" + rentPropertyViewModel.image.FileName;
                    string filePath = Path.Combine(uploadFolder, pathForPic);
                    rentPropertyViewModel.image.CopyTo(new FileStream(filePath, FileMode.Create));

                    RentPropertyModel rentPropertyModel = _context.RentPropertyModel.FirstOrDefault(x => x.id == id);
                    rentPropertyModel.address = rentPropertyViewModel.address;
                    rentPropertyModel.description = rentPropertyViewModel.description;
                    rentPropertyModel.image = pathForPic;
                    rentPropertyModel.name = rentPropertyViewModel.name;
                    rentPropertyModel.owner = name;
                    rentPropertyModel.price = rentPropertyViewModel.price;
                    rentPropertyModel.propertyType = rentPropertyViewModel.propertyType;
                    
                    PropertyModel p = _context.PropertyModel.FirstOrDefault(x => x.name == rentPropertyModel.name);
                    p.address = rentPropertyViewModel.address;
                    p.description = rentPropertyViewModel.description;
                    p.image = pathForPic;
                    p.name = rentPropertyViewModel.name;
                    p.owner = name;
                    p.price = rentPropertyViewModel.price;
                    p.propertyType = rentPropertyViewModel.propertyType;

                    _context.RentPropertyModel.Update(rentPropertyModel);
                    _context.PropertyModel.Update(p);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RentPropertyModelExists(rentPropertyViewModel.id))
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
            return View(rentPropertyViewModel);
        }
        [Authorize(Roles = "Admin, Seller")]
        // GET: RentPropertyModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rentPropertyModel = await _context.RentPropertyModel
                .FirstOrDefaultAsync(m => m.id == id);
            if (rentPropertyModel == null)
            {
                return NotFound();
            }

            return View(rentPropertyModel);
        }
        [Authorize(Roles = "Admin, Seller")]
        // POST: RentPropertyModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rentPropertyModel = await _context.RentPropertyModel.FindAsync(id);
            string file = rentPropertyModel.image;
            string uploadFolder = Path.Combine(_hostingEnvironment.WebRootPath, "Photos");
            string fileToDelete = Path.Combine(uploadFolder, file);
            /*if (System.IO.File.Exists(fileToDelete))
            {
                System.IO.File.Delete(fileToDelete);
            }*/
            _context.RentPropertyModel.Remove(rentPropertyModel);
            var comments = _context.CommentModel.Where(x => x.propname == rentPropertyModel.name).ToList();
            foreach(var comment in comments)
            {
                _context.CommentModel.Remove(comment);
            }
            var bids = _context.BidModel.Where(x => x.propname == rentPropertyModel.name).ToList();
            foreach(var bid in bids)
            {
                _context.BidModel.Remove(bid);
            }
            var p = _context.PropertyModel.FirstOrDefault(x => x.name == rentPropertyModel.name);
            _context.PropertyModel.Remove(p);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RentPropertyModelExists(int id)
        {
            return _context.RentPropertyModel.Any(e => e.id == id);
        }
    }
}
