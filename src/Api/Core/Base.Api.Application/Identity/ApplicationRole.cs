using Microsoft.AspNetCore.Identity;

namespace Base.Api.Application.Identity;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationRole : IdentityRole<int>
{
}