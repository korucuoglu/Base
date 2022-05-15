using System;

namespace Base.Api.Application.Models.Entities;

public class ArticleRawSqlQueryModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public bool IsPublic { get; set; }
    public string Username { get; set; }
    public DateTime CreatedDate { get; set; }
}