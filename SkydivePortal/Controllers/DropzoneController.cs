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
using SkydivePortal.Models.DropzoneViewModels;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;


namespace SkydivePortal.Controllers
{
    public class DropzoneController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DropzoneController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var model = new DropzonesViewModel()
            {
                Regions = await _context.Regions.ToListAsync(),
                Dropzones = await _context.Dropzones.ToListAsync()
            };
            return View("Index", model);
        }

        public async Task<IActionResult> DzPosts (int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var dropzone = await _context.Dropzones.SingleOrDefaultAsync(d => d.Id == id);
            if (dropzone == null)
            {
                return RedirectToAction("Index");
            }
            var model = new DzPostsViewModel()
            {
                ApplicationUser = user,
                Dropzone = dropzone
            };

            model.NewPost = new Dropzone_Post()
            {
                DropzoneId = dropzone.Id,
                Dropzone = dropzone
            };

            model.Dropzone_Posts = await _context.Dropzone_Posts.Where(d => d.DropzoneId == id).OrderByDescending(d => d.Date).ToListAsync();
            model.DzEvents = new DzEventsViewModel();
            var dzEventsList = await _context.Dropzone_Events.Where(d => d.DropzoneId == id).OrderBy(d => d.Date).ToListAsync();


            int nextEventid = dzEventsList.Last().Id;
            foreach (var item in dzEventsList)
            {
                if (DateTime.Now < item.Date)
                {
                    nextEventid = item.Id;
                    break;
                }
            }

            model.DzEvents.Dropzone_Events = dzEventsList;

            ViewData["nextEvent"] = nextEventid;
            model.DzEvents.newEvent = new Dropzone_Event()
            {
                Date = DateTime.Now,
                DropzoneId = dropzone.Id,
                Dropzone = dropzone
            };

            ViewData["isAllowed"] = "false";


            if (user != null)
            {
                var userRoles = await _context.ApplicationUserRoles.Where(ar => ar.ApplicationUserId == user.Id).Include(ar => ar.ApplicationRole).ToListAsync();
                foreach (var item in userRoles)
                {
                    if(item.ApplicationRole != null)
                    {
                        if (item.ApplicationRole.Name == Role.Master)
                        {
                            ViewData["isAllowed"] = "true";
                        }
                        else if (item.ApplicationRole.DropzoneId == id &&
                           (item.ApplicationRole.Name == Role.Admin || (item.ApplicationRole.Name == Role.Moderator)))
                        {
                            ViewData["isAllowed"] = "true";
                        }
                    }
                    
                }
            }


            return View("DzPosts", model);
        }

        [HttpPost]
        public async Task<IActionResult> AddDzPost([Bind("DropzoneId,Title,Text")] Dropzone_Post newPost)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                if (ModelState.IsValid)
                {
                    newPost.Date = DateTime.Now;
                    var dropzone = await _context.Dropzones.SingleOrDefaultAsync(d => d.Id == newPost.DropzoneId);
                    if (dropzone != null)
                    {
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
                                else if (item.ApplicationRole.DropzoneId == dropzone.Id &&
                                   (item.ApplicationRole.Name == Role.Admin || (item.ApplicationRole.Name == Role.Moderator)))
                                {
                                    _context.Add(newPost);
                                    await _context.SaveChangesAsync();
                                    break;
                                }
                            }

                        }
                        return RedirectToAction("DzPosts", new { id = dropzone.Id });
                    }
                    
                }

            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DzUserPosts(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var dropzone = await _context.Dropzones.SingleOrDefaultAsync(d => d.Id == id);
            if(dropzone == null)
            {
                return RedirectToAction("Index");
            }
            var model = new DzUserPostsViewModel()
            {
                ApplicationUser = user,
                Dropzone = dropzone
            };

            model.NewPost = new Dropzone_User_Post()
            {
                DropzoneId = dropzone.Id,
                Dropzone = dropzone
            };

            model.NewPostComment = new Dropzone_User_Post_Comment();

            model.Dropzone_User_Posts = await _context.Dropzone_User_Posts.Where(d => d.DropzoneId == id).Include(d => d.ApplicationUser).OrderByDescending(d => d.Date).ToListAsync();
            foreach(var item in model.Dropzone_User_Posts)
            {
                item.Dropzone_User_Post_Comments = await _context.Dropzone_User_Post_Comments.Where(c => c.Dropzone_User_PostId == item.Id).Include(c => c.ApplicationUser).OrderBy(c => c.Date).ToListAsync();
            }
            model.DzEvents = new DzEventsViewModel();
            var dzEventsList = await _context.Dropzone_Events.Where(d => d.DropzoneId == id).OrderBy(d => d.Date).ToListAsync();


            int nextEventid = dzEventsList.Last().Id;
            foreach(var item in dzEventsList)
            {
                if(DateTime.Now < item.Date)
                {
                    nextEventid = item.Id;
                    break;
                }
            }

            model.DzEvents.Dropzone_Events = dzEventsList;

            ViewData["nextEvent"] = nextEventid;

            model.DzEvents.newEvent = new Dropzone_Event()
            {
                Date = DateTime.Now,
                DropzoneId = dropzone.Id,
                Dropzone = dropzone
            };


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
                        else if (item.ApplicationRole.DropzoneId == id &&
                           (item.ApplicationRole.Name == Role.Admin || (item.ApplicationRole.Name == Role.Moderator)))
                        {
                            ViewData["isAllowed"] = "true";
                        }
                    }

                }
            }


            return View("DzUserPosts", model);
        }


        [HttpPost]
        public async Task<IActionResult> AddDzUserPost([Bind("DropzoneId,Text")] Dropzone_User_Post newPost)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                if (ModelState.IsValid)
                {
                    newPost.Date = DateTime.Now;
                    newPost.ApplicationUser = user;
                    var dropzone = await _context.Dropzones.SingleOrDefaultAsync(d => d.Id == newPost.DropzoneId);
                    if(dropzone != null)
                    {
                        _context.Add(newPost);
                        await _context.SaveChangesAsync();
                        return RedirectToAction("DzUserPosts", new { id = dropzone.Id });
                    }
                }

            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AddDzUserPostComment ([Bind("Dropzone_User_PostId,Text")] Dropzone_User_Post_Comment newPostComment)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                if (ModelState.IsValid)
                {
                    newPostComment.Date = DateTime.Now;
                    newPostComment.ApplicationUser = user;
                    var post = await _context.Dropzone_User_Posts.SingleOrDefaultAsync(du => du.Id == newPostComment.Dropzone_User_PostId);
                    if (post != null)
                    {
                        _context.Add(newPostComment);
                        await _context.SaveChangesAsync();
                        return RedirectToAction("DzUserPosts", new { id = post.DropzoneId });
                    }
                }

            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AddDzEvent([Bind("DropzoneId,Name,Date,Description")] Dropzone_Event newEvent)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                if (ModelState.IsValid)
                {
                    var dropzone = await _context.Dropzones.SingleOrDefaultAsync(d => d.Id == newEvent.DropzoneId);
                    if (dropzone != null)
                    {
                        var userRoles = await _context.ApplicationUserRoles.Where(ar => ar.ApplicationUserId == user.Id).Include(ar => ar.ApplicationRole).ToListAsync();
                        foreach (var item in userRoles)
                        {
                            if (item.ApplicationRole != null)
                            {
                                if (item.ApplicationRole.Name == Role.Master)
                                {
                                    _context.Add(newEvent);
                                    await _context.SaveChangesAsync();
                                    break;
                                }
                                else if (item.ApplicationRole.DropzoneId == dropzone.Id &&
                                   (item.ApplicationRole.Name == Role.Admin || (item.ApplicationRole.Name == Role.Moderator)))
                                {
                                    _context.Add(newEvent);
                                    await _context.SaveChangesAsync();
                                    break;
                                }
                            }

                        }
                        return RedirectToAction("DzPosts", new { id = dropzone.Id });
                    }

                }

            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DzInfo(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var dropzone = await _context.Dropzones.Include(d => d.Region).SingleOrDefaultAsync(d => d.Id == id);
            if (dropzone == null)
            {
                return RedirectToAction("Index");
            }
            var model = new DzInfoViewModel();

            model.Dropzone = dropzone;

            model.DzEvents = new DzEventsViewModel();
            var dzEventsList = await _context.Dropzone_Events.Where(d => d.DropzoneId == id).OrderBy(d => d.Date).ToListAsync();


            int nextEventid = dzEventsList.Last().Id;
            foreach (var item in dzEventsList)
            {
                if (DateTime.Now < item.Date)
                {
                    nextEventid = item.Id;
                    break;
                }
            }

            model.DzEvents.Dropzone_Events = dzEventsList;

            ViewData["nextEvent"] = nextEventid;

            model.DzEvents.newEvent = new Dropzone_Event()
            {
                Date = DateTime.Now,
                DropzoneId = dropzone.Id,
                Dropzone = dropzone
            };

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
                        else if (item.ApplicationRole.DropzoneId == id &&
                           (item.ApplicationRole.Name == Role.Admin || (item.ApplicationRole.Name == Role.Moderator)))
                        {
                            ViewData["isAllowed"] = "true";
                        }
                    }

                }
            }


            return View("DzInfo", model);
        }

        /*Remove actions*/

        public async Task<IActionResult> RemoveDzPost(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var dzPost = await _context.Dropzone_Posts.Include(dp => dp.Dropzone).SingleOrDefaultAsync(dp => dp.Id == id);
                if(dzPost != null)
                {
                    var dropzone = dzPost.Dropzone;
                    if (dropzone != null)
                    {
                        var userRoles = await _context.ApplicationUserRoles.Where(ar => ar.ApplicationUserId == user.Id).Include(ar => ar.ApplicationRole).ToListAsync();
                        foreach (var item in userRoles)
                        {
                            if (item.ApplicationRole != null)
                            {
                                if (item.ApplicationRole.Name == Role.Master)
                                {
                                    _context.Dropzone_Posts.Remove(dzPost);
                                    await _context.SaveChangesAsync();
                                    break;
                                }
                                else if (item.ApplicationRole.DropzoneId == dropzone.Id &&
                                    (item.ApplicationRole.Name == Role.Admin || (item.ApplicationRole.Name == Role.Moderator)))
                                {
                                    _context.Dropzone_Posts.Remove(dzPost);
                                    await _context.SaveChangesAsync();
                                    break;
                                }
                            }

                        }
                        return RedirectToAction("DzPosts", new { id = dropzone.Id });
                    }
                }
                
                

            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> RemoveDzUserPost(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var dzUserPost = await _context.Dropzone_User_Posts.Include(dp => dp.Dropzone).Include(dp => dp.Dropzone_User_Post_Comments).SingleOrDefaultAsync(dp => dp.Id == id);
                if (dzUserPost != null)
                {
                    var dropzone = dzUserPost.Dropzone;
                    if (dropzone != null)
                    {
                        bool remove = false;

                        if(dzUserPost.ApplicationUserId == user.Id)
                        {
                            remove = true;
                        }

                        var userRoles = await _context.ApplicationUserRoles.Where(ar => ar.ApplicationUserId == user.Id).Include(ar => ar.ApplicationRole).ToListAsync();
                        foreach (var item in userRoles)
                        {
                            if (item.ApplicationRole != null)
                            {
                                if (item.ApplicationRole.Name == Role.Master)
                                {
                                    remove = true;
                                    break;
                                }
                                else if (item.ApplicationRole.DropzoneId == dropzone.Id &&
                                    (item.ApplicationRole.Name == Role.Admin || (item.ApplicationRole.Name == Role.Moderator)))
                                {
                                    remove = true;
                                    break;
                                } 
                            }
                        }

                        if(remove == true)
                        {
                            foreach (var comment in dzUserPost.Dropzone_User_Post_Comments)
                            {
                                _context.Dropzone_User_Post_Comments.Remove(comment);
                            }
                            _context.Dropzone_User_Posts.Remove(dzUserPost);
                            await _context.SaveChangesAsync();
                        }

                        return RedirectToAction("DzUserPosts", new { id = dropzone.Id });
                    }
                }



            }
            return RedirectToAction("Index");
        }

        public void RemoveDzUserPostComment(int id)
        {
            var userid = _userManager.GetUserId(User);
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userid);

            if (user != null)
            {
                var dzUserPostComment = _context.Dropzone_User_Post_Comments.Include(pc => pc.Dropzone_User_Post).SingleOrDefault(pc => pc.Id == id);
                if (dzUserPostComment != null)
                {
                    var dropzone = _context.Dropzones.SingleOrDefault(d => d.Id == dzUserPostComment.Dropzone_User_Post.DropzoneId);
                    if (dropzone != null)
                    {
                        bool remove = false;

                        if(dzUserPostComment.ApplicationUserId == user.Id)
                        {
                            remove = true;
                        }

                        var userRoles = _context.ApplicationUserRoles.Where(ar => ar.ApplicationUserId == user.Id).Include(ar => ar.ApplicationRole).ToList();
                        foreach (var item in userRoles)
                        {
                            if (item.ApplicationRole != null)
                            {
                                if (item.ApplicationRole.Name == Role.Master)
                                {
                                    remove = true;
                                    break;
                                }
                                else if (item.ApplicationRole.DropzoneId == dropzone.Id &&
                                    (item.ApplicationRole.Name == Role.Admin || (item.ApplicationRole.Name == Role.Moderator)))
                                {
                                    remove = true;
                                    break;
                                }
                            }

                        }

                        if(remove == true)
                        {
                            _context.Dropzone_User_Post_Comments.Remove(dzUserPostComment);
                            _context.SaveChanges();
                        }
                        
                    }
                }



            }
            
        }
        public async Task<IActionResult> RemoveDzEvent(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var dzEvent = await _context.Dropzone_Events.Include(dp => dp.Dropzone).SingleOrDefaultAsync(de => de.Id == id);
                if (dzEvent != null)
                {
                    var dropzone = dzEvent.Dropzone;
                    if (dropzone != null)
                    {
                        bool remove = false;

                        var userRoles = await _context.ApplicationUserRoles.Where(ar => ar.ApplicationUserId == user.Id).Include(ar => ar.ApplicationRole).ToListAsync();
                        foreach (var item in userRoles)
                        {
                            if (item.ApplicationRole != null)
                            {
                                if (item.ApplicationRole.Name == Role.Master)
                                {
                                    remove = true;
                                    break;
                                }
                                else if (item.ApplicationRole.DropzoneId == dropzone.Id &&
                                    (item.ApplicationRole.Name == Role.Admin || (item.ApplicationRole.Name == Role.Moderator)))
                                {
                                    remove = true;
                                    break;
                                }
                            }

                        }

                        if(remove == true)
                        {
                            _context.Dropzone_Events.Remove(dzEvent);
                            await _context.SaveChangesAsync();
                        }

                        return RedirectToAction("DzPosts", new { id = dropzone.Id });
                    }
                }



            }
            return RedirectToAction("Index");
        }

        /* comments refresh function */

        [HttpGet]
        public IActionResult GetPostComments(int postid)
        {
            var userid = _userManager.GetUserId(User);
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userid);
            var post = _context.Dropzone_User_Posts.Include(d => d.ApplicationUser).SingleOrDefault(d => d.Id == postid);


            ViewData["isAllowed"] = "false";

            if (user != null)
            {
                var userRoles = _context.ApplicationUserRoles.Where(ar => ar.ApplicationUserId == user.Id).Include(ar => ar.ApplicationRole).ToList();
                foreach (var item in userRoles)
                {
                    if (item.ApplicationRole != null)
                    {
                        if (item.ApplicationRole.Name == Role.Master)
                        {
                            ViewData["isAllowed"] = "true";
                        }
                        else if (item.ApplicationRole.DropzoneId == post.DropzoneId &&
                           (item.ApplicationRole.Name == Role.Admin || (item.ApplicationRole.Name == Role.Moderator)))
                        {
                            ViewData["isAllowed"] = "true";
                        }
                    }

                }
            }

            var model = new PostCommentsViewModel()
            {
                ApplicationUser = user,
                Dropzone_User_Post_Comments = null
            };
            if (post != null)
            {
                model.Dropzone_User_Post_Comments = _context.Dropzone_User_Post_Comments.Where(c => c.Dropzone_User_PostId == post.Id).Include(c => c.ApplicationUser).OrderBy(c => c.Date).ToList();
            }
            return PartialView("~/Views/Dropzone/_DzPostComments.cshtml", model);
        }

    }
}