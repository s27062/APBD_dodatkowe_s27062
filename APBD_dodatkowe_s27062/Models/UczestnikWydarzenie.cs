using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APBD_dodatkowe_s27062.Models;

[Table("Uczestnik_Wydarzenie")]
[PrimaryKey(nameof(UczestnikId), nameof(WydarzenieId))]
public class UczestnikWydarzenie
{
    [Column("Uczestnik_ID")]
    public int UczestnikId { get; set; }
    
    [Column("Wydarzenie_ID")]
    public int WydarzenieId { get; set; }
    
    [ForeignKey("UczestnikId")]
    public virtual Uczestnik Uczestnik { get; set; } = null!;
    
    [ForeignKey("WydarzenieId")]
    public virtual Wydarzenie Wydarzenie { get; set; } = null!;
}