using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class RonaldsYRonaldsContext(DbContextOptions<RonaldsYRonaldsContext> options) : IdentityDbContext<RonaldsYRonalds.Data.ApplicationUser>(options)
{
}
