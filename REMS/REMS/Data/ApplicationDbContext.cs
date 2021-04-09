using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using REMS.Models;

namespace REMS.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }        
        public DbSet<REMS.Models.RentPropertyModel> RentPropertyModel { get; set; }
        public DbSet<REMS.Models.SellPropertyModel> SellPropertyModel { get; set; }
        public DbSet<REMS.Models.CommentModel> CommentModel { get; set; }
        public DbSet<REMS.Models.PropertyModel> PropertyModel { get; set; }
        public DbSet<REMS.Models.BidModel> BidModel { get; set; }
        public DbSet<REMS.Models.AuctionModel> AuctionModel { get; set; }
        public DbSet<REMS.Models.AgentModel> AgentModel { get; set; }
    }
}
