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
using SkydivePortal.Models.ProfileViewModels;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace SkydivePortal.Controllers
{
    public class ProfileController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProfileController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var profileView = new ProfileViewModel();
            var user = await _userManager.GetUserAsync(User);
            if(user != null)
            {
                int newJumpNumber = 0;
                profileView.ApplicationUser = user;
                profileView.UserJumps = await _context.UserJumps.Where(u => u.ApplicationUserId == user.Id).OrderByDescending(u => u.Number).AsNoTracking().ToListAsync();
                foreach(var userJump in profileView.UserJumps)
                {
                    if(userJump.Number > newJumpNumber)
                    {
                        newJumpNumber = userJump.Number;
                    }
                }
                newJumpNumber++;
                profileView.UserJump = new UserJump()
                {
                    Number = newJumpNumber,
                    Date = DateTime.Now
                };
                return View(profileView);
            }

            ViewData["loggedIn"] = "no";

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddJump ([Bind("Number,Date,Height,Parachute,Plane,Note,Video")] UserJump userJump)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _userManager.GetUserAsync(User);
                    userJump.ApplicationUser = user;

                    if(user != null)
                    {
                        List<UserJump> jumpList = new List<UserJump>();
                        jumpList = await _context.UserJumps.Where(u => u.ApplicationUserId == user.Id).OrderBy(u => u.Number).ToListAsync();

                        if(jumpList.Exists(j => j.Number == userJump.Number))
                        {
                            int currentNumber = userJump.Number;
                            bool reachedItem = false;

                            foreach(var item in jumpList)
                            {
                                if (item.Number == currentNumber)
                                {
                                    reachedItem = true;
                                    item.Number = currentNumber + 1;
                                    _context.Update(item);
                                    currentNumber++;
                                }else if (reachedItem == true)
                                {
                                    break;
                                }
                                
                            }
                            _context.Add(userJump);
                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            _context.Add(userJump);
                            await _context.SaveChangesAsync();
                        }

                        
                    }    
                }
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> RemoveJump(int id)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                UserJump toDelete = await _context.UserJumps.SingleOrDefaultAsync(u => u.ApplicationUserId == user.Id && u.Id == id);
                if(toDelete != null)
                {
                    int currentNumber = toDelete.Number;
                    _context.Remove(toDelete);

                    List<UserJump> jumpList = new List<UserJump>();
                    jumpList = await _context.UserJumps.Where(u => u.ApplicationUserId == user.Id).OrderBy(u => u.Number).ToListAsync();

                    currentNumber++;
                    bool reachedItem = false;

                    foreach (var item in jumpList)
                    {
                        if (item.Number == currentNumber)
                        {
                            reachedItem = true;
                            item.Number = currentNumber - 1;
                            _context.Update(item);
                            currentNumber++;
                        }
                        else if (reachedItem == true)
                        {
                            break;
                        }

                    }

                    await _context.SaveChangesAsync();
                }

            }

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> EditJump([Bind("Id,Number,Date,Height,Parachute,Plane,Note,Video")] UserJump userJump)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                try
                {
                    var toEdit = await _context.UserJumps.SingleOrDefaultAsync(u => u.Id == userJump.Id);
                    if (toEdit != null && ModelState.IsValid)
                    {
                        if (toEdit.ApplicationUserId == user.Id)
                        {
                            if (toEdit.Number != userJump.Number)
                            {
                                int currentNumber = toEdit.Number;

                                List<UserJump> jumpList = new List<UserJump>();
                                jumpList = await _context.UserJumps.Where(u => u.ApplicationUserId == user.Id).OrderBy(u => u.Number).ToListAsync();

                                currentNumber++;
                                bool reachedItem = false;

                                foreach (var item in jumpList)
                                {
                                    if (item.Number == currentNumber)
                                    {
                                        reachedItem = true;
                                        item.Number--;
                                        _context.Update(item);
                                        currentNumber++;
                                    }
                                    else if (reachedItem == true)
                                    {
                                        break;
                                    }

                                }
                                currentNumber = userJump.Number;
                                reachedItem = false;

                                foreach (var item in jumpList)
                                {
                                    if (item.Id == toEdit.Id)
                                    {
                                        continue;
                                    }
                                    if (item.Number == currentNumber)
                                    {
                                        reachedItem = true;
                                        item.Number++;
                                        _context.Update(item);
                                        currentNumber++;
                                    }
                                    else if (reachedItem == true)
                                    {
                                        break;
                                    }

                                }

                            }
                            toEdit.Number = userJump.Number;
                            toEdit.Date = userJump.Date;
                            toEdit.Height = userJump.Height;
                            toEdit.Parachute = userJump.Parachute;
                            toEdit.Plane = userJump.Plane;
                            toEdit.Note = userJump.Note;
                            toEdit.Video = userJump.Video;

                            _context.Update(toEdit);
                            await _context.SaveChangesAsync();
                        }
                    }
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists " +
                        "see your system administrator.");
                }
            }


            return RedirectToAction(nameof(Index));
        }
    }
}