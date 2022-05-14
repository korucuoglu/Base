namespace Base.Api.Application.Models.Entities;

public class NoteRawSqlQueryModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public bool IsPublic { get; set; }
    public string Username { get; set; }
}