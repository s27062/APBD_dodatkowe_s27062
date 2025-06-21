using System.ComponentModel.DataAnnotations;
using APBD_dodatkowe_s27062.Models;

namespace APBD_dodatkowe_s27062.DTOs;

public class CreateWydarzenieDTO
{
    [Required]
    [MaxLength(50)]
    public string Tytul { get; set; } = string.Empty;
    
    [MaxLength(300)]
    public string? Opis { get; set; }

    [Required]
    public DateTime Data { get; set; }
    
    [Required]
    public int MaxUczestnikow { get; set; }
}

public class GetWydarzenieDTO
{
    public int Id { get; set; }
    public string Tytul { get; set; } = string.Empty;
    public string? Opis { get; set; }
    public DateTime Data { get; set; }
    public int MaxUczestnikow { get; set; }
    public int ZarejestrowaniUczestnicy { get; set; }
    public int WolneMiejsca { get; set; }
    public IEnumerable<GetPrelegentDTO>? Prelegenci { get; set; }
}

public class GetPrelegentDTO
{
    public int Id { get; set; }
    public string Imie { get; set; } = string.Empty;
    public string Nazwisko { get; set; } = string.Empty;
    public string? Telefon { get; set; }
}

public class GetUczestnikWydarzeniaDTO
{
    public int Id { get; set; }
    public string Imie { get; set; } = string.Empty;
    public string Nazwisko { get; set; } = string.Empty;
    public string? Email { get; set; }
    public IEnumerable<GetWydarzenieDTO>? Wydarzenia { get; set; }
}