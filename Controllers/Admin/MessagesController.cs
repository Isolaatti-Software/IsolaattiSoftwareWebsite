using IsolaattiSoftwareWebsite.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IsolaattiSoftwareWebsite.Controllers.Admin;

[ApiController]
[Route("/api/admin/messages/")]
[Authorize(Roles = "admin")]
public class MessagesController : ControllerBase
{
    private readonly IFirstContactFormResponses _formResponsesService;

    public MessagesController(IFirstContactFormResponses formResponsesService)
    {
        _formResponsesService = formResponsesService;
    }
    
    [HttpGet]
    [Route("get")]
    public async Task<IActionResult> Index(string? lastId = null, int count = 30)
    {
        return Ok(await _formResponsesService.GetFirstContact(lastId, count));
    }
}