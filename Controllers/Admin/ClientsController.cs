using IsolaattiSoftwareWebsite.Dto;
using IsolaattiSoftwareWebsite.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IsolaattiSoftwareWebsite.Controllers.Admin;

[ApiController]
[Route("/api/clients")]
public class Clients : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;

    public Clients(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    [Route("listClientAccounts")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> GetClients()
    {
        return Ok(_userManager.Users.Select(user => new ClientAccount
        {
            Id = user.Id,
            Email = user.Email,
            Name = user.UserName
        }));
    }
}