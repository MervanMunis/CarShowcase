using Microsoft.AspNetCore.Mvc;
using WebAPI.DTOs.Brand;
using WebAPI.Services.Contracts;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandService _brandService;

        public BrandsController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpGet]
        public IActionResult GetAllBrands()
        {
            var brands = _brandService.GetAllBrandsAsync();
            return Ok(brands);
        }

        [HttpGet("{id}")]
        public IActionResult GetBrandById(int id)
        {
            var brand = _brandService.GetBrandByIdAsync(id);
            if (brand == null) return NotFound();
            return Ok(brand);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBrand([FromBody] BrandRequest brandRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdBrand = await _brandService.CreateBrandAsync(brandRequest);

            return CreatedAtAction("The Brand Is Creaded.", createdBrand);
        }

        [HttpPost("list")]
        public async Task<IActionResult> CreateBrands([FromBody] IEnumerable<BrandRequest> brandRequests)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdBrands = await _brandService.CreateBrandAsync(brandRequests);

            return Ok(createdBrands);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBrand(int id, [FromBody] BrandRequest brandRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _brandService.UpdateBrandAsync(id, brandRequest);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            await _brandService.DeleteBrandAsync(id);
            return NoContent();
        }
    }
}
