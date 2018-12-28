using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace APIEmbedded.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Auth")]
    public class TrainersController : ControllerBase
    {

        [HttpGet]
        [Authorize]
        [Route("/api/trainers/listTrainers")]
        public async Task<IActionResult> Trainers()
        {
            var trainers = new {trainers = new[] {"Jean-louis", "michel", "roger", "Beber", "Cécilia", "Jean-michel"}};
            var res = JsonConvert.SerializeObject(trainers);
            return Ok(res);
        }
    }
}