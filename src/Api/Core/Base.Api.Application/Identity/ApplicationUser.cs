using Base.Api.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Base.Api.Application.Identity;

public class ApplicationUser : IdentityUser<int>
{
    public List<Category> Categories { get; set; }
    public List<Note> Notes { get; set; }
}
