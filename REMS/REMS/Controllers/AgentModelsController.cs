using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using REMS.Data;
using REMS.Models;

namespace REMS.Controllers
{
    public class AgentModelsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<IdentityRole> _roleManger;


        public AgentModelsController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IEmailSender emailSender,
            RoleManager<IdentityRole> roleManager, 
            ApplicationDbContext context)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _roleManger = roleManager;
        }

        // GET: AgentModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.AgentModel.ToListAsync());
        }

        // GET: AgentModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agentModel = await _context.AgentModel
                .FirstOrDefaultAsync(m => m.id == id);
            if (agentModel == null)
            {
                return NotFound();
            }

            return View(agentModel);
        }

        // GET: AgentModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AgentModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,name,email")] AgentModel agentModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(agentModel);
                await _context.SaveChangesAsync();
                var user = new IdentityUser { UserName = agentModel.email, Email = agentModel.email };
                string password = GenerateRandomPassword();
                var result = await _userManager.CreateAsync(user, password);                
                string r = "Agent";
                await _userManager.AddToRoleAsync(user, r);
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var res = await _userManager.ConfirmEmailAsync(user, code);
                string message = "Your Username is your registered Email Address and Your password is " + password;
                if (result.Succeeded && res.Succeeded)
                {
                    await _emailSender.SendEmailAsync(agentModel.email, "Credentials", message);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(agentModel);
        }

        // GET: AgentModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agentModel = await _context.AgentModel.FindAsync(id);
            if (agentModel == null)
            {
                return NotFound();
            }
            return View(agentModel);
        }

        // POST: AgentModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,name,email")] AgentModel agentModel)
        {
            if (id != agentModel.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(agentModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AgentModelExists(agentModel.id))
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
            return View(agentModel);
        }

        // GET: AgentModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agentModel = await _context.AgentModel
                .FirstOrDefaultAsync(m => m.id == id);
            if (agentModel == null)
            {
                return NotFound();
            }

            return View(agentModel);
        }

        // POST: AgentModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var agentModel = await _context.AgentModel.FindAsync(id);
            _context.AgentModel.Remove(agentModel);
            await _context.SaveChangesAsync();
            var user = await _userManager.FindByNameAsync(agentModel.email);
            await _userManager.DeleteAsync(user);
            return RedirectToAction(nameof(Index));
        }

        private bool AgentModelExists(int id)
        {
            return _context.AgentModel.Any(e => e.id == id);
        }

        public static string GenerateRandomPassword()
        {
            int RequiredLength = 8;
            int RequiredUniqueChars = 4;
            bool RequireDigit = true;
            bool RequireLowercase = true;
            bool RequireNonAlphanumeric = true;
            bool RequireUppercase = true;

            string[] randomChars = new[] {
            "ABCDEFGHJKLMNOPQRSTUVWXYZ",    // uppercase 
            "abcdefghijkmnopqrstuvwxyz",    // lowercase
            "0123456789",                   // digits
            "!@$?_-"                        // non-alphanumeric
            };

            Random rand = new Random(Environment.TickCount);
            List<char> chars = new List<char>();

            if (RequireUppercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[0][rand.Next(0, randomChars[0].Length)]);

            if (RequireLowercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[1][rand.Next(0, randomChars[1].Length)]);

            if (RequireDigit)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[2][rand.Next(0, randomChars[2].Length)]);

            if (RequireNonAlphanumeric)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[3][rand.Next(0, randomChars[3].Length)]);

            for (int i = chars.Count; i < RequiredLength
                || chars.Distinct().Count() < RequiredUniqueChars; i++)
            {
                string rcs = randomChars[rand.Next(0, randomChars.Length)];
                chars.Insert(rand.Next(0, chars.Count),
                    rcs[rand.Next(0, rcs.Length)]);
            }

            return new string(chars.ToArray());
        }
    }
}
