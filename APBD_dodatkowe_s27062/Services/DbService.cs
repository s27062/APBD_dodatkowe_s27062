using APBD_dodatkowe_s27062.Data;
using APBD_dodatkowe_s27062.DTOs;
using APBD_dodatkowe_s27062.Exceptions;
using APBD_dodatkowe_s27062.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD_dodatkowe_s27062.Services;

public interface IDbService
{
    public Task<GetWydarzenieDTO> UtworzWydarzenie(CreateWydarzenieDTO request);
    public Task<string> DodajPrelegentaDoWydarzenia(int wydarzenieId, int prelegentId);
    public Task<string> ZarejestrujUczestnikaNaWydarzenie(int wydarzenieId, int uczestnikId);
    public Task<string> AnulujRejestracjeUczestnika(int wydarzenieId, int uczestnikId);
    public Task<ICollection<GetWydarzenieDTO>> GetNadchodzaceWydarzenia();
    public Task<GetUczestnikWydarzeniaDTO> GetWydarzeniaUczestnika(int uczestnikId);
}

public class DbService(MyDbContext data) : IDbService
{
    public async Task<GetWydarzenieDTO> UtworzWydarzenie(CreateWydarzenieDTO request)
    {
        if (request.Data <= DateTime.Now)
        {
            throw new BadDateException("Data wydarzenia musi być w przyszłości!");
        }

        if (request.MaxUczestnikow < 1)
        {
            throw new BadMaxUczestnikException("Maksymalna liczba uczestników nie może być mniejsza niż 1!");
        }

        var wydarzenie = new Wydarzenie
        {
            Tytul = request.Tytul,
            Opis = request.Opis,
            Data = request.Data,
            MaxUczestnikow = request.MaxUczestnikow
        };
        
        await data.Wydarzenia.AddAsync(wydarzenie);
        await data.SaveChangesAsync();

        return new GetWydarzenieDTO()
        {
            Id = wydarzenie.Id,
            Tytul = wydarzenie.Tytul,
            Opis = wydarzenie.Opis,
            Data = wydarzenie.Data,
            MaxUczestnikow = wydarzenie.MaxUczestnikow,
            ZarejestrowaniUczestnicy = 0,
            WolneMiejsca = wydarzenie.MaxUczestnikow,
            Prelegenci = new List<GetPrelegentDTO>()
        };
    }

    public async Task<string> DodajPrelegentaDoWydarzenia(int wydarzenieId, int prelegentId)
    {
        var wydarzenie = await data.Wydarzenia.FindAsync(wydarzenieId) ?? throw new NotFoundException($"Nie znaleziono wydarzenia {wydarzenieId}!");
        var prelegent = await data.Prelegenci.FindAsync(prelegentId) ?? throw new NotFoundException($"Nie znaleziono prelegenta {prelegentId}!");
        
        bool prelegentZajety = await data.PrelegenciWydarzenia
            .Include(pw => pw.Wydarzenie)
            .AnyAsync(pw => pw.Wydarzenie.Data.Date == wydarzenie.Data.Date && pw.Prelegent == prelegent);

        if (prelegentZajety)
        {
            throw new PrelegentZajetyException($"Prelegent {prelegentId} jest już przypisany do innego wydarzenia w tym samym terminie!");
        }

        data.PrelegenciWydarzenia.Add(new PrelegentWydarzenie
        {
            WydarzenieId = wydarzenieId,
            PrelegentId = prelegentId,
        });
        await data.SaveChangesAsync();
        
        return $"Prelegent {prelegentId} pomyślnie dodany do wydarzenia {wydarzenieId}.";
    }

    public async Task<string> ZarejestrujUczestnikaNaWydarzenie(int wydarzenieId, int uczestnikId)
    {
        var wydarzenie = await data.Wydarzenia.FindAsync(wydarzenieId) ?? throw new NotFoundException($"Nie znaleziono wydarzenia {wydarzenieId}!");
        var uczestnik = await data.Uczestnicy.FindAsync(uczestnikId) ?? throw new NotFoundException($"Nie znaleziono uczestnika {uczestnikId}!");
        
        int miejsca = data.Wydarzenia.Where(w => w.Id == wydarzenie.Id).Select(w => new GetWydarzenieDTO
        {
            WolneMiejsca = w.MaxUczestnikow - w.UczestnicyWydarzenia.Count
        }).FirstOrDefaultAsync().Result.WolneMiejsca;
        
        if (miejsca <= 0)
        {
            throw new WydarzenieFullException($"Wydarzenie {wydarzenieId} nie ma już wolnych miejsc!");
        }
        
        bool uczestnikZarejestrowany = data.UczestnicyWydarzenia.Any(pw => pw.UczestnikId == uczestnik.Id && pw.WydarzenieId == wydarzenie.Id);

        if (uczestnikZarejestrowany)
        {
            throw new UczestnikZarejestrowanyException(
                $"Uczestnik {uczestnikId} jest już zarejestrowany na wydarzenie {wydarzenieId}!");
        }

        data.UczestnicyWydarzenia.Add(new UczestnikWydarzenie
        {
            WydarzenieId = wydarzenie.Id,
            UczestnikId = uczestnik.Id,
        });
        await data.SaveChangesAsync();
        
        return $"Uczestnik {uczestnikId} pomyślnie zarejestrowany na wydarzenie {wydarzenieId}.";
    }

    public async Task<string> AnulujRejestracjeUczestnika(int wydarzenieId, int uczestnikId)
    {
        var wydarzenie = await data.Wydarzenia.FindAsync(wydarzenieId) ?? throw new NotFoundException($"Nie znaleziono wydarzenia {wydarzenieId}!");
        var uczestnik = await data.Uczestnicy.FindAsync(uczestnikId) ?? throw new NotFoundException($"Nie znaleziono uczestnika {uczestnikId}!");
        
        var uczestnikZarejestrowany = data.UczestnicyWydarzenia.FirstOrDefault(pw => pw.UczestnikId == uczestnik.Id && pw.WydarzenieId == wydarzenie.Id);

        if (uczestnikZarejestrowany == null)
        {
            throw new UczestnikZarejestrowanyException($"Uczestnik {uczestnikId} nie jest zarejestrowany na wydarzenie {wydarzenieId}!");
        }

        if (wydarzenie.Data <= DateTime.Now.AddHours(24))
        {
            throw new ZaPozneOdwolanieException(
                "Anulowanie rejestracji jest możliwe tylko do 24h przed rozpoczęciem wydarzenia!");
        }
        
        data.UczestnicyWydarzenia.Remove(uczestnikZarejestrowany);
        await data.SaveChangesAsync();
        
        return $"Pomyślnie anulowano rejestrację uczestnika {uczestnikId} na wydarzenie {wydarzenieId}.";
    }

    public async Task<ICollection<GetWydarzenieDTO>> GetNadchodzaceWydarzenia()
    {
        return await data.Wydarzenia
            .Where(w => w.Data > DateTime.Now)
            .Select(w => new GetWydarzenieDTO
            {
                Id = w.Id,
                Tytul = w.Tytul,
                Opis = w.Opis,
                Data = w.Data,
                MaxUczestnikow = w.MaxUczestnikow,
                ZarejestrowaniUczestnicy = w.UczestnicyWydarzenia.Count,
                WolneMiejsca = w.MaxUczestnikow - w.UczestnicyWydarzenia.Count,
                Prelegenci = w.PrelegenciWydarzenia.Select(p => new GetPrelegentDTO
                {
                    Id = p.PrelegentId,
                    Imie = p.Prelegent.Imie,
                    Nazwisko = p.Prelegent.Nazwisko,
                    Telefon = p.Prelegent.Telefon
                }).ToList()
            }).ToListAsync();
    }

    public async Task<GetUczestnikWydarzeniaDTO> GetWydarzeniaUczestnika(int uczestnikId)
    {
        var uczestnik = await data.Uczestnicy.FindAsync(uczestnikId) ?? throw new NotFoundException($"Nie znaleziono uczestnika {uczestnikId}!");
        
        return await data.Uczestnicy
            .Where(u => u.Id == uczestnik.Id)
            .Select(u => new GetUczestnikWydarzeniaDTO()
            {
                Id = u.Id,
                Imie = u.Imie,
                Nazwisko = u.Nazwisko,
                Email = u.Email,
                Wydarzenia = u.UczestnicyWydarzenia.Select(uw => new GetWydarzenieDTO()
                {
                    Id = uw.Wydarzenie.Id,
                    Tytul = uw.Wydarzenie.Tytul,
                    Opis = uw.Wydarzenie.Opis,
                    Data = uw.Wydarzenie.Data,
                    MaxUczestnikow = uw.Wydarzenie.MaxUczestnikow,
                    ZarejestrowaniUczestnicy = uw.Wydarzenie.UczestnicyWydarzenia.Count,
                    WolneMiejsca = uw.Wydarzenie.MaxUczestnikow - uw.Wydarzenie.UczestnicyWydarzenia.Count,
                    Prelegenci = uw.Wydarzenie.PrelegenciWydarzenia.Select(p => new GetPrelegentDTO
                    {
                        Id = p.PrelegentId,
                        Imie = p.Prelegent.Imie,
                        Nazwisko = p.Prelegent.Nazwisko,
                        Telefon = p.Prelegent.Telefon
                    }).ToList()
                }).ToList()
            }).FirstAsync();
    }
}