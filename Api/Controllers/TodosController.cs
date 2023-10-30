using Api.Contracts.Todos;
using Application.Repositories;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TodosController : ControllerBase
{
    private readonly ITodosRepository _todosRepository;

    public TodosController(ITodosRepository todosRepository)
    {
        _todosRepository = todosRepository;
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        var todos = await _todosRepository.List();
        return Ok(todos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var todo = await _todosRepository.Get(id);
        if (todo is null) return NotFound();

        return Ok(todo);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] SaveTodoRequest request
    )
    {
        var todo = new Todo()
        {
            Title = request.Title
        };
        await _todosRepository.Create(todo);

        return CreatedAtAction(nameof(Get), new { id = todo.Id }, todo);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        [FromRoute] int id,
        [FromBody] SaveTodoRequest request
    )
    {
        var todo = await _todosRepository.Get(id);
        if (todo is null) return NotFound();

        todo.Title = request.Title;
        await _todosRepository.Update(todo);

        return Ok(todo);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var todo = await _todosRepository.Get(id);
        if (todo is null) return NotFound();

        await _todosRepository.Delete(todo);

        return NoContent();
    }
}
