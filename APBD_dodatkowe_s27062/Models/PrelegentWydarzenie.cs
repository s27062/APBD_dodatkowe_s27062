using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APBD_dodatkowe_s27062.Models;

[Table("Prelegent_Wydarzenie")]
[PrimaryKey(nameof(PrelegentId), nameof(WydarzenieId))]
public class PrelegentWydarzenie
{
    [Column("Prelegent_ID")]
    public int PrelegentId { get; set; }
    
    [Column("Wydarzenie_ID")]
    public int WydarzenieId { get; set; }
    
    [ForeignKey("PrelegentId")]
    public virtual Prelegent Prelegent { get; set; } = null!;
    
    [ForeignKey("WydarzenieId")]
    public virtual Wydarzenie Wydarzenie { get; set; } = null!;
}