using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APBD_dodatkowe_s27062.Models;

[Table("Uczestnik")]
public class Uczestnik
{
    [Column("ID")]
    [Key]
    public int Id { get; set; }
    
    [MaxLength(50)]
    public string Imie { get; set; } = null!;
    
    [MaxLength(50)]
    public string Nazwisko { get; set; } = null!;
    
    [MaxLength(100)]
    public string? Email { get; set; }
    
    public virtual ICollection<UczestnikWydarzenie> UczestnicyWydarzenia { get; set; } = null!;
}