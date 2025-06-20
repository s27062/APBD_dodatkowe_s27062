using APBD_dodatkowe_s27062.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD_dodatkowe_s27062.Data;

public class MyDbContext : DbContext
{
    
    public DbSet<Uczestnik> Uczestnicy { get; set; }
    public DbSet<Prelegent> Prelegenci { get; set; }
    public DbSet<Wydarzenie> Wydarzenia { get; set; }
    public DbSet<UczestnikWydarzenie> UczestnicyWydarzenia { get; set; }
    public DbSet<PrelegentWydarzenie> PrelegenciWydarzenia { get; set; }

    public MyDbContext(DbContextOptions options) : base(options)
    {
        
    }
}