using Microsoft.AspNetCore.Identity;

namespace ClinicHub.Core.Models;

public class ApplicationUser : IdentityUser
{
	public string FullName { get; set; } = null!;
	public bool IsDeleted { get; set; }
	public string? CreatedById { get; set; }
	public DateTime CreatedOn { get; set; } = DateTime.Now;
	public string? LastUpdatedById { get; set; }
	public DateTime? LastUpdatedOn { get; set; }
}
