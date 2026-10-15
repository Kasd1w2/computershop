using Microsoft.AspNetCore.Mvc;
using ComputeShopApi.Models;
using ComputeShopApi.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Components;

namespace ComputeShopApi.Controllers
{
	[Route("computers")]
	[ApiController]
	public class ComputerController : ControllerBase
	{
		public ComputerShopContext context = new();

		[HttpGet("getAllComputer")]
		public object GetAllComputer()
		{
			var computers = context.Computers.ToList();
			if (computers.Count > 0)
			{
				return StatusCode(200, new { message = "Sikeres lekérdezés.", result = computers });
			}
			else
			{
				return StatusCode(404, new { message = "Üres a tábla.", result = computers });
			}
		}

		[HttpGet("getComputerById/{id}")]
		public object GetComputerById(Guid id)
		{
			var computer = context.Computers.FirstOrDefault(c => c.Id == id);
			if (computer != null)
			{
				return StatusCode(200, new { message = "Sikeres lekérdezés.", result = computer });
			}
			else
			{
				return StatusCode(404, new { message = "Nincs ilyen számítógép az adatbázisban.", result = computer });
			}
		}

		[HttpPost("addNewComputer")]
		public object AddNewComputer(AddNewComputerDto newComputer)
		{
			var computer = new Computer
			{
				Id = Guid.NewGuid(),
				Brand = newComputer.Brand,
				Type = newComputer.Type,
				Display = newComputer.Display,
				Memory = newComputer.Memory,
				CreatedTime = DateTime.Now,
				Os = newComputer.Os
			};

			if (computer != null)
			{
				context.Computers.Add(computer);
				context.SaveChanges();
				return StatusCode(201, new { message = "Sikeres hozzáadás.", result = computer });
			}
			else
			{
				return StatusCode(400, new { message = "Nem sikerült hozzáadni", result = computer });
			}
		}

		[HttpPut("updateComputer/{id}")]
		public object UpdateComputer([FromQuery]Guid Id, UpdateComputerDto updatedComputer)
		{
			var computer = context.Computers.FirstOrDefault(c => c.Id == Id);
			if (computer != null)
			{
				computer.Brand = updatedComputer.Brand;
				computer.Type = updatedComputer.Type;
				computer.Display = double.Parse(updatedComputer.Display);
				computer.Memory = updatedComputer.Memory;
				computer.OsId = updatedComputer.OsId;
				context.SaveChanges();
				return StatusCode(200, new { message = "Sikeres frissítés.", result = computer });
			}
			else
			{
				return StatusCode(404, new { message = "Nincs ilyen számítógép az adatbázisban.", result = computer });
			}
		}
}
