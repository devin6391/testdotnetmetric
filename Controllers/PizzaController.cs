using ContosoPizza.Models;
using ContosoPizza.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace ContosoPizza.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PizzaController : ControllerBase
    {
        private readonly InstrumentationService _instrumentationService;
        private readonly ILogger<PizzaController> _logger;

        public PizzaController(InstrumentationService instrumentationService, ILogger<PizzaController> logger)
        {
            _instrumentationService = instrumentationService;
            _logger = logger;
        }

        // GET all action
        [HttpGet]
        public ActionResult<List<Pizza>> GetAll()
        {
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            _logger.LogInformation("Recording RequestCount metric");
            _instrumentationService.RequestCount.Add(1);
            try
            {
                var result = PizzaService.GetAll();
                return result;
            }
            finally
            {
                stopwatch.Stop();
                _logger.LogInformation("Recording RequestDuration metric");
                _instrumentationService.RequestDuration.Record(stopwatch.Elapsed.TotalSeconds);
            }
        }

        // GET by Id action
        [HttpGet("{id}")]
        public ActionResult<Pizza> Get(int id)
        {
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            _logger.LogInformation("Recording RequestCount metric");
            _instrumentationService.RequestCount.Add(1);
            try
            {
                var pizza = PizzaService.Get(id);

                if (pizza == null)
                {
                    _logger.LogInformation("Recording ErrorCount metric");
                    _instrumentationService.ErrorCount.Add(1);
                    return NotFound();
                }

                return pizza;
            }
            finally
            {
                stopwatch.Stop();
                _logger.LogInformation("Recording RequestDuration metric");
                _instrumentationService.RequestDuration.Record(stopwatch.Elapsed.TotalSeconds);
            }
        }

        // POST action
        [HttpPost]
        public IActionResult Create(Pizza pizza)
        {
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            _logger.LogInformation("Recording RequestCount metric");
            _instrumentationService.RequestCount.Add(1);
            try
            {
                PizzaService.Add(pizza);
                return CreatedAtAction(nameof(Get), new { id = pizza.Id }, pizza);
            }
            finally
            {
                stopwatch.Stop();
                _logger.LogInformation("Recording RequestDuration metric");
                _instrumentationService.RequestDuration.Record(stopwatch.Elapsed.TotalSeconds);
            }
        }

        // PUT action
        [HttpPut("{id}")]
        public IActionResult Update(int id, Pizza pizza)
        {
            if (id != pizza.Id)
                return BadRequest();

            var existingPizza = PizzaService.Get(id);
            if (existingPizza is null)
                return NotFound();

            PizzaService.Update(pizza);

            return NoContent();
        }

        // DELETE action
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var pizza = PizzaService.Get(id);

            if (pizza is null)
                return NotFound();

            PizzaService.Delete(id);

            return NoContent();
        }
    }
}