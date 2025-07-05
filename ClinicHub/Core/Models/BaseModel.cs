namespace ClinicHub.Core.Models;

public class BaseModel
{
	public bool IsDeleted { get; set; }
	public DateTime CreatedOn { get; set; } = DateTime.Now;
	public DateTime? LastUpdatedOn { get; set; }

	public string? CreatedById { get; set; }
	public ApplicationUser? CreatedBy { get; set; }

	public string? LastUpdatedById { get; set; }
	public ApplicationUser? LastUpdatedBy { get; set; }
}