namespace Base.Api.Application.Models.Dtos;

public class NoteDto
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public bool IsPublic { get; set; }
    public string Username { get; set; }
}