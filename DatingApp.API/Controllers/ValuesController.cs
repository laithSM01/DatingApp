using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DatingApp.API.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class ValuesController : Controller
    {
        private readonly DataContext _context;
        public ValuesController(DataContext context) 
        {
            _context = context;
        }
        [AllowAnonymous] 
    [HttpGet]
    public async Task<IActionResult> Getvalues() {//converted to acsyncronous
        var values = await _context.Values.ToListAsync();
        return Ok(values);
    }

     [AllowAnonymous] //get a single value Anonymously
        [HttpGet("{id}")]//root value
    public async Task<IActionResult> Getvalues(int id) {
        var values = await _context.Values.FirstOrDefaultAsync(x => 
        x.Id == id);
        return Ok(values);
    }

    }
}