namespace Base.Api.Application.Dtos.Notes;

public class NoteDto
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public bool IsPublic { get; set; }
}