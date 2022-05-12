using Base.Api.Domain.Common;
using System;

namespace Base.Api.Domain.Entities;

public class Category: BaseEntity, IUpdateable, IAuthRequired
{
    public string Title { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public int ApplicationUserId { get; set; }
}
