using System;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<ProxmoxHost> ProxmoxHosts => Set<ProxmoxHost>();
}
