using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Services;
using WebApi.Dto;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeTaskController : ControllerBase
    {
        private readonly HomeTaskService _hometaskService;

        public HomeTaskController(HomeTaskService hometaskService)
        {
            _hometaskService = hometaskService;
        }

        // GET: api/HomeTask
        [HttpGet]
        public ActionResult<IEnumerable<HomeTaskDto>> Get()
        {
            return Ok(_hometaskService.GetAllHomeTasks().Select(homeTask => HomeTaskDto.FromModel(homeTask)));
        }
        // POST api/CreateHomeTask
        [HttpPost]
        public ActionResult CreateHomeTask([FromBody] HomeTaskDto value)
        {
            var updateResult = _hometaskService.CreateHomeTask(value.ToModel());
            if (updateResult.HasErrors)
            {
                return BadRequest(updateResult.Errors);
            }
            return Accepted(updateResult);
        }


        // GET api/HomeTask/5
        [HttpGet("{id}")]
        public ActionResult<HomeTaskDto> Get(int id)
        {
            var hometaskService = _hometaskService.GetHomeTaskById(id);

            if (hometaskService == null)
            {
                return NotFound();
            }

            return Ok(HomeTaskDto.FromModel(hometaskService));
        }
        
        // PUT api/HomeTask/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] HomeTaskDto value)
        {
            var result = _hometaskService.UpdateHomeTask(value.ToModel());
            if (result.HasErrors)
            {
                return BadRequest(result.Errors);
            }
            return Accepted();
        }

        // DELETE api/HomeTaks/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _hometaskService.DeleteHomeTask(id);
            return Accepted();
        }
    }
}
