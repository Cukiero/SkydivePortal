using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using SkydivePortal.Models;
using Microsoft.EntityFrameworkCore;

namespace SkydivePortal.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();
            if (context.PagePosts.Any())
            {
                return;   
            }
            var PagePosts = new PagePost[]
            {
                new PagePost
                {
                    Title="Pierwszy post na portalu",
                    Text="Witam wszystkich zebranych",
                    Date= new DateTime(2018,1,10,10,20,20)
                },
                new PagePost
                {
                    Title="Drugi post na portalu",
                    Text="Witam wszystkich zebranych",
                    Date= new DateTime(2018,1,12,10,20,20)
                },
                new PagePost
                {
                    Title="Trzeci post na portalu",
                    Text="No siema",
                    Date= new DateTime(2018,1,16,10,20,20)
                },
                new PagePost
                {
                    Title="Czwarty post na portalu",
                    Text="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse sodales nulla nisi, sit amet efficitur nisl tristique non. Donec ut dui faucibus nulla aliquam pulvinar vel in turpis. Proin malesuada malesuada tincidunt. Phasellus molestie erat nec arcu blandit mollis. Cras vitae nisl rutrum, tempor mauris at, suscipit libero. Nulla fermentum diam at purus imperdiet, nec efficitur massa imperdiet. Sed posuere ligula porttitor nibh tempus posuere. Nulla molestie dignissim lorem, vel semper enim aliquet nec. Donec convallis tellus ac venenatis tempor. Nulla vitae tellus cursus, feugiat nisl eget, bibendum quam. /n Etiam elementum hendrerit massa a consequat. Vestibulum iaculis sem quis turpis pharetra, eget ultrices elit maximus. Mauris ultricies, dui non bibendum consequat, urna est eleifend est, fringilla venenatis nunc lorem sed turpis. Vestibulum fermentum auctor justo, non pulvinar massa. Interdum et malesuada fames ac ante ipsum primis in faucibus. Sed a neque interdum, cursus lacus eu, luctus velit. Suspendisse lectus ante, consequat et sollicitudin ac, pretium vitae nunc. Duis finibus erat in risus ullamcorper, ut facilisis enim convallis. Phasellus sodales lorem neque, ac efficitur ipsum tempor porta. Aenean sed lacinia ipsum, fermentum sodales nisl. Sed a libero et nunc finibus semper at ac enim. Nulla finibus nisl in arcu porttitor fringilla. Quisque fringilla purus at lectus egestas, id pharetra nisl interdum.",
                    Date= new DateTime(2018,1,20,20,20,20)
                }

            };
            foreach (PagePost pp in PagePosts)
            {
                context.PagePosts.Add(pp);
            }

            var countries = new Country[]
            {
                new Country
                {
                    name = "Poland"
                }
            };

            foreach (Country c in countries)
            {
                context.Countries.Add(c);
            }

            var regions = new Region[]
            {
                new Region
                {
                    Name = "Mazowieckie",
                    Country = countries[0]
                },
                new Region
                {
                    Name = "Warmińsko-mazurskie",
                    Country = countries[0]
                }
            };

            foreach (Region r in regions)
            {
                context.Regions.Add(r);
            }

            var dropzones = new Dropzone[]
            {
                new Dropzone
                {
                    Name = "SkyDive Warszawa",
                    Region = regions[0],
                    Address = "05-190 Nasielsk, Lotnisko Chrcynno"
                },
                new Dropzone
                {
                    Name = "Strefa Baltic",
                    Region = regions[1],
                    Address = "82-300 Elbląg, ul. Lotnicza 8b"
                }
            };

            foreach (Dropzone d in dropzones)
            {
                var roles = new ApplicationRole[]
                {
                    new ApplicationRole
                    {
                        Dropzone = d,
                        Name = Role.Admin
                    },
                    new ApplicationRole
                    {
                        Dropzone = d,
                        Name = Role.Moderator
                    }

                    
                };
                foreach (ApplicationRole ar in roles)
                {
                    context.ApplicationRoles.Add(ar);
                }
                context.Dropzones.Add(d);
            }

            var role = new ApplicationRole()
            {
                Name = Role.Master
            };
            context.ApplicationRoles.Add(role);

            context.SaveChanges();
        }
    }
}
