using AspNetCoreTodo.Models;
using Microsoft.EntityFrameworkCore;


//Delete/re-write all of this if no solution is found to replicate the book example
public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
	public ApplicationDbContext(
		DbContextOptions<ApplicationDbContext> options)
		: base(options)
	{ 
	
	}

	public DbSet<TodoItem> Items { get; set; }

	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);
		// ...
	}
}