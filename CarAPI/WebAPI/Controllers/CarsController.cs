using Microsoft.AspNetCore.Mvc;
using WebAPI.DTOs.Car;
using WebAPI.Services.Contracts;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarsController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet]
        public IActionResult GetAllCars()
        {
            var cars = _carService.GetAllCarsAsync();
            return Ok(cars);
        }

        [HttpGet("{id}")]
        public IActionResult GetCarById(int id)
        {
            var car = _carService.GetCarByIdAsync(id);
            if (car == null) return NotFound();
            return Ok(car);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCar([FromBody] CarRequest carRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdCar = await _carService.CreateCarAsync(carRequest);
            return CreatedAtAction("The Car Is Created.", createdCar);
        }

        [HttpPost("list")]
        public async Task<IActionResult> CreateCars([FromBody] IEnumerable<CarRequest> carRequests)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdCars = await _carService.CreateCarAsync(carRequests);
            return Ok(createdCars);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCar(int id, [FromBody] CarRequest carRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _carService.UpdateCarAsync(id, carRequest);
            return NoContent();
        }

        [HttpPut("{carId}/image")]
        public async Task<IActionResult> UpdateCarImage(int carId, IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
                return BadRequest("No file uploaded.");

            await _carService.UpdateCarImageByIdAsync(carId, imageFile);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            await _carService.DeleteCarAsync(id);
            return NoContent();
        }
    }
}
