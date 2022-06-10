using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;
using WineDocumentation.Core.Domain;

namespace WineDocumentation.Infrastructure.EF
{

    public class ApplicationContextDbFactory : IDesignTimeDbContextFactory<WineDocumentationContex>
    {
        WineDocumentationContex IDesignTimeDbContextFactory<WineDocumentationContex>.CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<WineDocumentationContex>();
            optionsBuilder.UseNpgsql<WineDocumentationContex>("User ID=lvwmqcdxbgfkle;Password=cf9d6a5ba4d102169cdbbc3191a676e5625514baf11b51dcfa0e7817c19cd471;Host=ec2-54-155-208-5.eu-west-1.compute.amazonaws.com;Port=5432;Database=dlo59e8o3qoav;Pooling=true;Use SSL Stream=True;SSL Mode=Require;TrustServerCertificate=True;");

            return new WineDocumentationContex(optionsBuilder.Options);
        }
    }

    public class WineDocumentationContex : DbContext
    {
        // private string _connectionString = "Server=ec2-54-155-208-5.eu-west-1.compute.amazonaws.com;Port=5432;User Id=lvwmqcdxbgfkle;Password=cf9d6a5ba4d102169cdbbc3191a676e5625514baf11b51dcfa0e7817c19cd471;";
        public DbSet<User> Users { get; set; }
        public DbSet<Wine> Wines { get; set; }
        public DbSet<Species> Species { get; set; }
        public DbSet<Score> Scores { get; set; }

        public WineDocumentationContex(DbContextOptions<WineDocumentationContex> options) 
            : base(options)
        {
           
        }

       

        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     // if(_settings.InMemory)
        //     // {
        //     //     optionsBuilder.UseInMemoryDatabase();

        //     //     return;    
        //     // }

        //     optionsBuilder.UseNpgsql(_connectionString); // postgresql
        //     // optionsBuilder.UseSqlServer(_settings.ConnectionString); // mssql
        //     base.OnConfiguring(optionsBuilder);
        // }

        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     var itemBuilder = modelBuilder.Entity<Wine>();
        //     itemBuilder.HasKey(x => x.Id); 

        //     base.OnModelCreating(modelBuilder);
        // }
    }
}