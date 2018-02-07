using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkydivePortal.Data;
using SkydivePortal.Models;
using SkydivePortal.Models.HomeViewModels;

namespace SkydivePortal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            ViewData["isAllowed"] = "false";

            if (user != null)
            {
                var userRoles = await _context.ApplicationUserRoles.Where(ar => ar.ApplicationUserId == user.Id).Include(ar => ar.ApplicationRole).ToListAsync();
                foreach (var item in userRoles)
                {
                    if (item.ApplicationRole != null)
                    {
                        if (item.ApplicationRole.Name == Role.Master)
                        {
                            ViewData["isAllowed"] = "true";
                        }
                    }

                }
            }

            var model = new PagePostsViewModel();

            model.NewPost = new PagePost();
            model.PagePosts = await _context.PagePosts.OrderByDescending(p => p.Date).AsNoTracking().ToListAsync();
            return View("Index", model);
        }


        [HttpPost]
        public async Task<IActionResult> AddPagePost([Bind("Title,Text")] PagePost newPost)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                if (ModelState.IsValid)
                {
                    newPost.Date = DateTime.Now;
                        var userRoles = await _context.ApplicationUserRoles.Where(ar => ar.ApplicationUserId == user.Id).Include(ar => ar.ApplicationRole).ToListAsync();
                        foreach (var item in userRoles)
                        {
                            if (item.ApplicationRole != null)
                            {
                                if (item.ApplicationRole.Name == Role.Master)
                                {
                                    _context.Add(newPost);
                                    await _context.SaveChangesAsync();
                                    break;
                                }
                            }

                        }

                }

            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> RemovePagePost (int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                if (ModelState.IsValid)
                {
                    var userRoles = await _context.ApplicationUserRoles.Where(ar => ar.ApplicationUserId == user.Id).Include(ar => ar.ApplicationRole).ToListAsync();
                    foreach (var item in userRoles)
                    {
                        if (item.ApplicationRole != null)
                        {
                            if (item.ApplicationRole.Name == Role.Master)
                            {
                                var post = await _context.PagePosts.SingleOrDefaultAsync(p => p.Id == id);
                                if (post != null)
                                {
                                    _context.PagePosts.Remove(post);
                                    await _context.SaveChangesAsync();
                                }
                                break;
                            }
                        }

                    }

                }

            }
            return RedirectToAction("Index");
        }
    }
}
