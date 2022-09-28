﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DevIO.App.ViewModels;

namespace DevIO.App.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<DevIO.App.ViewModels.AddressViewModel> AddressViewModel { get; set; }
        public DbSet<DevIO.App.ViewModels.SupplierViewModel> SupplierViewModel { get; set; }
    }
}