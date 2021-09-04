using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace DigiKala.DataAccessLayer.Context
{
    public class DigiKalaDbContext : DbContext
    {
        public DigiKalaDbContext([NotNullAttribute] DbContextOptions<DigiKalaDbContext> options)
            : base(options)
        {
        }
    }
}
