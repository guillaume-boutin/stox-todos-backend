using Application.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class TodosRepository : ITodosRepository
{
    private readonly PostgresContext _db;

    public TodosRepository(PostgresContext db)
    {
        _db = db;
    }

    public async Task<List<Todo>> List()
    {
        var query = _db.Todos.AsQueryable();
        return await query.OrderBy(x => x.Id).ToListAsync();
    }

    public async Task<Todo?> Get(int id)
    {
        return await _db.Todos.FindAsync(id);
    }

    public async Task Create(Todo todo)
    {
        _db.Todos.Add(todo);
        await _db.SaveChangesAsync();
    }

    public async Task Update(Todo todo)
    {
        _db.Todos.Update(todo);
        await _db.SaveChangesAsync();
    }

    public async Task Delete(Todo todo)
    {
        _db.Todos.Remove(todo);
        await _db.SaveChangesAsync();
    }
}
