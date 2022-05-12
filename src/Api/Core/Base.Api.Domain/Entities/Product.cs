using Base.Api.Domain.Common;
using System;

namespace Base.Api.Domain.Entities;

public class Product : BaseEntity, IUpdateable
{
    public string Name { get; set; }
    public string ShortName { get; set; }
    public string Image { get; set; }
    public string Url { get; set; }
    public DateTime? UpdatedDate { get; set; }
}