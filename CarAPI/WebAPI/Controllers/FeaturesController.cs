using Microsoft.AspNetCore.Mvc;
using WebAPI.DTOs.Feature;
using WebAPI.Services.Contracts;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeaturesController : ControllerBase
    {
        private readonly IFeatureService _featureService;

        public FeaturesController(IFeatureService featureService)
        {
            _featureService = featureService;
        }

        // GET: api/Features
        [HttpGet]
        public IActionResult GetAllFeatures()
        {
            var features = _featureService.GetAllFeaturesAsync();
            return Ok(features);
        }

        // GET: api/Features/5
        [HttpGet("{id}")]
        public IActionResult GetFeatureById(int id)
        {
            var feature = _featureService.GetFeatureByIdAsync(id);
            if (feature == null)
                return NotFound();

            return Ok(feature);
        }

        // POST: api/Features
        [HttpPost]
        public async Task<IActionResult> CreateFeature([FromBody] FeatureRequest featureRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdFeature = await _featureService.CreateFeatureAsync(featureRequest);
            return CreatedAtAction("The Feature Is Created.", createdFeature);
        }

        // POST: api/Features/list
        [HttpPost("list")]
        public async Task<IActionResult> CreateFeatures([FromBody] IEnumerable<FeatureRequest> featureRequests)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdFeatures = await _featureService.CreateFeatureAsync(featureRequests);
            return Ok(createdFeatures);
        }

        // PUT: api/Features/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFeature(int id, [FromBody] FeatureRequest featureRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _featureService.UpdateFeatureAsync(id, featureRequest);
            return NoContent();
        }

        // DELETE: api/Features/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeature(int id)
        {
            await _featureService.DeleteFeatureAsync(id);
            return NoContent();
        }
    }
}
