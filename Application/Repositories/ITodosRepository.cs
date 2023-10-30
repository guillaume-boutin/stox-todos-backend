using Domain;

namespace Application.Repositories;

public interface ITodosRepository
{
    Task<List<Todo>> List();

    Task<Todo?> Get(int id);

    Task Create(Todo todo);

    Task Update(Todo todo);

    Task Delete(Todo todo);
}
