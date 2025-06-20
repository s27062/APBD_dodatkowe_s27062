using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APBD_dodatkowe_s27062.Models;

[Table("Wydarzenie")]
public class Wydarzenie
{
    [Column("ID")]
    [Key]
    public int Id { get; set; }
    
    [MaxLength(50)]
    public string Tytul { get; set; } = null!;
    
    [MaxLength(300)]
    public string? Opis { get; set; }

    public DateTime Data { get; set; }
    
    public int MaxUczestnikow { get; set; }
    
    public virtual ICollection<PrelegentWydarzenie> PrelegenciWydarzenia { get; set; } = null!;
    
    public virtual ICollection<UczestnikWydarzenie> UczestnicyWydarzenia { get; set; } = null!;
}