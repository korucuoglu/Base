using Base.Api.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Base.Api.Application.Identity;

public class ApplicationUser : IdentityUser<int>
{
    public virtual ICollection<Category> Categories { get; set; }
    public virtual ICollection<Note> Notes { get; set; }
}