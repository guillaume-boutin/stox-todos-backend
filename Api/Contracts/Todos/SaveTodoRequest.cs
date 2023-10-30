namespace Api.Contracts.Todos;

public class SaveTodoRequest
{
    public string Title { get; set; } = String.Empty;

    public bool Done { get; set; } = false;
}
