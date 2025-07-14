namespace ClinicHub.Core.ViewModels;

public class SpecialtyViewModel
{
	public int Id { get; set; }
	public string Name { get; set; } = null!;
	public string? Description { get; set; }
	public DateTime CreatedOn { get; set; }
	public DateTime? LastUpdatedOn { get; set; }
	public bool IsDeleted { get; set; }
}