using AutoBit_WebInvoices.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly EmployeesRepository _dal;

    public EmployeesController(EmployeesRepository dal)
    {
        _dal = dal;
    }

    [HttpGet("GetAll")]
    public ActionResult<List<Employees>> LoadItems()
    {
        var list = _dal.SelectAll();
        return Ok(list);
    }
}

