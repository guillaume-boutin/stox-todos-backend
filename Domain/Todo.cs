namespace Domain;

public class Todo
{
    public int Id { get; set; } = default;

    public string Title { get; set; } = String.Empty;

    public bool Done { get; set; } = false;
}
