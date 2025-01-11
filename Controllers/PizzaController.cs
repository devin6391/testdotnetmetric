using ContosoPizza.Models;
using ContosoPizza.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using OpenTelemetry.Metrics;

namespace ContosoPizza.Controllers;

[ApiController]
[Route("[controller]")]
public class PizzaController : ControllerBase
{
    private static readonly Meter Meter = new Meter("PizzaController", "1.0");
    private static readonly Counter<long> RequestCount = Meter.CreateCounter<long>("pizza_requests_total");
    private static readonly Histogram<double> RequestDuration = Meter.CreateHistogram<double>("pizza_request_duration_seconds");
    private static readonly Counter<long> ErrorCount = Meter.CreateCounter<long>("pizza_errors_total");

    public PizzaController()
    {
    }

    // GET all action
    [HttpGet]
    public ActionResult<List<Pizza>> GetAll()
    {
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        RequestCount.Add(1);
        try
        {
            var result = PizzaService.GetAll();
            return result;
        }
        finally
        {
            stopwatch.Stop();
            RequestDuration.Record(stopwatch.Elapsed.TotalSeconds);
        }
    }

    // GET by Id action
    [HttpGet("{id}")]
    public ActionResult<Pizza> Get(int id)
    {
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        RequestCount.Add(1);
        try
        {
            var pizza = PizzaService.Get(id);

            if (pizza == null)
            {
                ErrorCount.Add(1);
                return NotFound();
            }

            return pizza;
        }
        finally
        {
            stopwatch.Stop();
            RequestDuration.Record(stopwatch.Elapsed.TotalSeconds);
        }
    }

    // POST action
    [HttpPost]
    public IActionResult Create(Pizza pizza)
    {
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        RequestCount.Add(1);
        try
        {
            PizzaService.Add(pizza);
            return CreatedAtAction(nameof(Get), new { id = pizza.Id }, pizza);
        }
        finally
        {
            stopwatch.Stop();
            RequestDuration.Record(stopwatch.Elapsed.TotalSeconds);
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