using DigiKala.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace DigiKala.DataAccessLayer.Context
{
    public class DigiKalaDbContext : DbContext
    {
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermissions> RolePermissions { get; set; }
        public DbSet<User> Users { get; set; }

        public DigiKalaDbContext([NotNullAttribute] DbContextOptions<DigiKalaDbContext> options)
            : base(options)
        {
        }
    }
}
