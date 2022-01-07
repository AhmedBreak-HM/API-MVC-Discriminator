using API_MVC_Discriminator.Persistence.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_MVC_Discriminator.Persistence.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<UserApplication> UserApplication { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityRole>().HasData(new IdentityRole[] {
                    new IdentityRole{ Id="1", Name="Admin",NormalizedName="ADMIN"},
                    new IdentityRole{ Id="2", Name="Moderator", NormalizedName="MODERATOR"},
                    new IdentityRole{ Id="3", Name="Member", NormalizedName="MEMBER"},
                    new IdentityRole{ Id="4", Name="VIP", NormalizedName="VIP"}});
        }

    }
}
