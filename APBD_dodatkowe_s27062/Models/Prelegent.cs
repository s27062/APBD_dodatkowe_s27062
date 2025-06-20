using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APBD_dodatkowe_s27062.Models;

[Table("Prelegent")]
public class Prelegent
{
    [Column("ID")]
    [Key]
    public int Id { get; set; }
    
    [MaxLength(50)]
    public string Imie { get; set; } = null!;
    
    [MaxLength(50)]
    public string Nazwisko { get; set; } = null!;
    
    [MaxLength(20)]
    public string? Telefon { get; set; }
    
    public virtual ICollection<PrelegentWydarzenie> PrelegenciWydarzenia { get; set; } = null!;
}