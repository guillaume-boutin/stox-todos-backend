using Api.Contracts.Todos;
using Application.Errors;
using Application.Repositories;
using Domain;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TodosController : ControllerBase
{
    private readonly ITodosRepository _todosRepository;
    private readonly IMapper _mapper;

    public TodosController(ITodosRepository todosRepository, IMapper mapper)
    {
        _todosRepository = todosRepository;
        _mapper = mapper;
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
        if (todo is null) throw new NotFoundError();

        return Ok(todo);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] SaveTodoRequest request
    )
    {
        var validator = new SaveTodoRequestValidator();
        var validationResult = validator.Validate(request);
        if (!validationResult.IsValid) throw new ValidationError(validationResult.Errors);

        var todo = _mapper.Map<Todo>(request);
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
        if (todo is null) throw new NotFoundError();

        todo = _mapper.Map<SaveTodoRequest, Todo>(request, todo);
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
