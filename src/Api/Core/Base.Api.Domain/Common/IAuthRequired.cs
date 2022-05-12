namespace Base.Api.Domain.Common;

public interface IAuthRequired
{
    public int UserId { get; set; }
}