using Api.Contracts.Todos;
using Domain;
using Mapster;

namespace Api.Mappings;

public class TodoMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<SaveTodoRequest, Todo>();
    }
}
