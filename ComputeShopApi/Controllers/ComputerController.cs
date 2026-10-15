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
				return StatusCode(404, new { message = "Sikertelen lekérdezés.", result = computers });
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
				return StatusCode(404, new { message = "Sikertelen lekérdezés.", result = computer });
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
				return StatusCode(400, new { message = "Sikertelen hozzáadás.", result = computer });
			}
		}

		[HttpPut("updateComputer/{id}")]
		public object UpdateComputer([FromQuery] Guid Id, UpdateComputerDto updatedComputer)
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
				return StatusCode(404, new { message = "Sikertelen frissítés.", result = computer });
			}
		}

		[HttpDelete("deleteComputer/{id}")]
		public object DeleteComputer(Guid id)
		{
			var computer = context.Computers.Find(id);
			if (computer != null)
			{
				context.Computers.Remove(computer);
				context.SaveChanges();
				return StatusCode(204);
			}
			else
			{
				return StatusCode(404);
			}
		}
		[HttpPut]
		public object UpdateOsystem([FromQuery] Guid Id, [FromBody] UpdateComputerDto updatedComputer)
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
				return StatusCode(404, new { message = "Sikertelen frissítés.", result = computer });
			}
		}
		[HttpGet("allOsWithComputers")]
		public object GetAllOsWithComputers()
		{
			var osWithComputers = context.Osystems.Include(os => os.Computers).ToList();
			if (osWithComputers.Count > 0)
			{
				return StatusCode(200, new { message = "Sikeres lekérdezés.", result = osWithComputers });
			}
			else
			{
				return StatusCode(404, new { message = "Sikertelen lekérdezés.", result = osWithComputers });
			}
		}
	}
}