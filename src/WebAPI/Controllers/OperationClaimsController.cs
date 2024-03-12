using Business.Features.OperationClaims.Dtos;
using Business.Features.OperationClaims.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OperationClaimsController : Controller
{
    private readonly IOperationClaimService _service;

    public OperationClaimsController(IOperationClaimService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOperationClaimDto dto)
    {
        await _service.CreateAsync(dto);
        return Ok();
    }
}
