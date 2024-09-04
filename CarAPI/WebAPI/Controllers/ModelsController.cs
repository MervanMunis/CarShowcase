using Microsoft.AspNetCore.Mvc;
using WebAPI.DTOs.Model;
using WebAPI.Services.Contracts;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelsController : ControllerBase
    {
        private readonly IModelService _modelService;

        public ModelsController(IModelService modelService)
        {
            _modelService = modelService;
        }

        [HttpGet]
        public IActionResult GetAllModels()
        {
            var models = _modelService.GetAllModelsAsync();
            return Ok(models);
        }

        [HttpGet("{id}")]
        public IActionResult GetModelById(int id)
        {
            var model = _modelService.GetModelByIdAsync(id);
            if (model == null) return NotFound();
            return Ok(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateModel([FromBody] ModelRequest modelRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdModel = await _modelService.CreateModelAsync(modelRequest);
            return CreatedAtAction("The Model Is Created.", createdModel);
        }

        [HttpPost("list")]
        public async Task<IActionResult> CreateModels([FromBody] IEnumerable<ModelRequest> modelRequests)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _modelService.CreateModelAsync(modelRequests);
            return CreatedAtAction("The Models Are Created.", result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateModel(int id, [FromBody] ModelRequest modelRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _modelService.UpdateModelAsync(id, modelRequest);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModel(int id)
        {
            await _modelService.DeleteModelAsync(id);
            return NoContent();
        }
    }
}
