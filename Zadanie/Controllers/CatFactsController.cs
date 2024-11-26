using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Zadanie.Services;

namespace Zadanie.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class CatFactsController : ControllerBase
	{
		private readonly CatFactService _catFactService;
		
		public CatFactsController(CatFactService catFactService)
		{
			_catFactService = catFactService;
		}
		
		// https://localhost:5001/api/catfacts/fetch-fact
		[HttpGet("fetch-fact")]
		public async Task<IActionResult> FetchData()
		{
			var fact = await _catFactService.GetFactAsync("https://catfact.ninja/fact");
			
			if(fact == null) return BadRequest("Failed to fetch cat fact");
			
			return Ok(fact);
		}
	}
}