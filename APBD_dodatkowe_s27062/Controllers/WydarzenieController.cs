using APBD_dodatkowe_s27062.DTOs;
using APBD_dodatkowe_s27062.Exceptions;
using APBD_dodatkowe_s27062.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace APBD_dodatkowe_s27062.Controllers;


[ApiController]
[Route("api/[Controller]")]
public class WydarzeniaController (IDbService dbService) : ControllerBase
{
    [HttpPost("dodaj")]
    public async Task<IActionResult> DodajWydarzenie([FromBody] CreateWydarzenieDTO request)
    {
        try
        {
            var wydarzenie = await dbService.UtworzWydarzenie(request);
            return Created($"wydarzenie/{wydarzenie.Id}", wydarzenie);
        }
        catch (BadDateException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (BadMaxUczestnikException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("{wydarzenieId}/dodaj-prelegenta/{prelegentId}")]
    public async Task<IActionResult> DodajPrelegentaDoWydarzenia(int wydarzenieId, int prelegentId)
    {
        try
        {
            var wiadomosc = await dbService.DodajPrelegentaDoWydarzenia(wydarzenieId, prelegentId);
            return Ok(wiadomosc);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (PrelegentZajetyException ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpPost("{wydarzenieId}/zarejestruj-uczestnika/{uczestnikId}")]
    public async Task<IActionResult> ZarejestrujUczestnikaNaWydarzenie(int wydarzenieId, int uczestnikId)
    {
        try
        {
            var wiadomosc = await dbService.ZarejestrujUczestnikaNaWydarzenie(wydarzenieId, uczestnikId);
            return Ok(wiadomosc);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (WydarzenieFullException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (UczestnikZarejestrowanyException ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpPost("{wydarzenieId}/anuluj-rejestracje/{uczestnikId}")]
    public async Task<IActionResult> AnulujRejestracjeUczestnika(int wydarzenieId, int uczestnikId)
    {
        try
        {
            var wiadomosc = await dbService.AnulujRejestracjeUczestnika(wydarzenieId, uczestnikId);
            return Ok(wiadomosc);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (ZaPozneOdwolanieException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (UczestnikZarejestrowanyException ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpGet("nadchodzace")]
    public async Task<IActionResult> GetNadchodzaceWydarzenia()
    {
        return Ok(await dbService.GetNadchodzaceWydarzenia());
    }
    
    [HttpGet("uczestnik/{uczestnikId}")]
    public async Task<IActionResult> GetWydarzeniaUczestnika(int uczestnikId)
    {
        try
        {
            return Ok(await dbService.GetWydarzeniaUczestnika(uczestnikId));
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}