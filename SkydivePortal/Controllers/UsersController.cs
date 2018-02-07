using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using SkydivePortal.Data;
using SkydivePortal.Models;
using SkydivePortal.Models.UsersViewModels;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace SkydivePortal.Controllers
{
    public class UsersController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.Include(u => u.Parachute).Include(u => u.Region).ThenInclude(r => r.Country).ToListAsync();
            var model = new List<UserViewModel>();
            foreach(var user in users)
            {
                var modelItem = new UserViewModel()
                {
                    ApplicationUser = user,
                    ApplicationUserRoles = await _context.ApplicationUserRoles.Where(ar => ar.ApplicationUserId == user.Id).Include(ar => ar.ApplicationRole).ThenInclude(a => a.Dropzone).ToListAsync()
                };
                model.Add(modelItem);
            }
            return View("Index", model);
        }
    }
}