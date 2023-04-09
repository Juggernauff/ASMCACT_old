using API.Context;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly ILogger<IdentityController> _logger;
        private readonly ApplicationContext _context;

        public IdentityController(ILogger<IdentityController> logger, ApplicationContext applicationContext)
        {
            _logger = logger;
            _context = applicationContext;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(User user)
        {
            /*_context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();*/

            if (user == null) return BadRequest(user);

            if (string.IsNullOrWhiteSpace(user.LastName)
                || string.IsNullOrWhiteSpace(user.FirstName)
                || string.IsNullOrWhiteSpace(user.PhoneNumber)) return BadRequest(user);

            if (await _context.Users
                .FirstOrDefaultAsync(u => u.PhoneNumber == user.PhoneNumber) != null) return BadRequest(user);

            await _context.Users.AddAsync(user);

            #if DEBUG
            Company? company = await _context.Companies.FirstOrDefaultAsync(c => c.FullName == "Тестовая организация");
            if (company == null) 
            {
                company = new Company("Тестовая организация");
                await _context.Companies.AddAsync(company);
            }

            CheckPoint? checkPoint = await _context.CheckPoints.FirstOrDefaultAsync(cp => cp.Name == "Тестовый пост");
            if (checkPoint == null)
            {
                checkPoint = new CheckPoint("Тестовый пост");
                await _context.CheckPoints.AddAsync(checkPoint);
            }

            user.Companies?.Add(company);
            company.CheckPoints?.Add(checkPoint);
            #endif

            _context.SaveChanges();

            User result = await _context.Users
                .FirstAsync(u => u.PhoneNumber == user.PhoneNumber);

            return CreatedAtAction(nameof(IdentityController.Register), result);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(User user)
        {
            User? result = await _context.Users
                .FirstOrDefaultAsync(u => u.PhoneNumber == user.PhoneNumber);

            if (result == null) return Unauthorized(result);

            // TODO: Добавить компании и посты в result.

            List<Company> companies = await _context.Companies
                .Where(c => c.Users.Contains(result))
                .ToListAsync();

            if (companies.Any())
            {
                result.Companies.AddRange(companies);

                List<CheckPoint> checkPoints = await _context.CheckPoints
                    .Where(cp => companies.Select(c => c.Id).Contains(cp.CompanyId))
                    .ToListAsync();
            }

            return result != null ? Ok(result) : Unauthorized(result);
        }
    }
}
