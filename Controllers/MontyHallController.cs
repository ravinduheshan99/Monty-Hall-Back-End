using Microsoft.AspNetCore.Mvc;
using montyhall.Models;
using System;

namespace MontyHallSimulation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MontyHallController : ControllerBase
    {
        [HttpPost("simulate")]
        public IActionResult SimulateGames([FromBody] SimulationRequest request)
        {
            int winsBySwitching = 0;
            int winsByNotSwitching = 0;

            Random random = new Random();

            for (int i = 0; i < request.Simulations; i++)
            {
                int carDoor = random.Next(0, 3);
                int chosenDoor = random.Next(0, 3);

                bool winBySwitching = (carDoor != chosenDoor);

                if (request.SwitchDoor)
                {
                    if (winBySwitching) winsBySwitching++;
                }
                else
                {
                    if (!winBySwitching) winsByNotSwitching++;
                }
            }

            var result = new SimulationResult
            {
                WinsBySwitching = winsBySwitching,
                WinsByNotSwitching = winsByNotSwitching,
                TotalSimulations = request.Simulations
            };

            return Ok(result);
        }
    }
}
