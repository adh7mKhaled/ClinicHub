using System.Reflection;

namespace ClinicHub.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
		: base(options)
	{
	}

	public DbSet<Doctor> Doctors { get; set; }
	public DbSet<Nurse> Nurses { get; set; }
	public DbSet<Patient> Patients { get; set; }
	public DbSet<Specialty> Specialties { get; set; }

	protected override void OnModelCreating(ModelBuilder builder)
	{
		builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

		builder.Entity<Doctor>()
			.HasMany(x => x.Nurses)
			.WithMany(x => x.Doctors)
			.UsingEntity<DoctorNurse>();

		var cascadeFks = builder.Model.GetEntityTypes()
			.SelectMany(t => t.GetForeignKeys())
			.Where(fk => fk.DeleteBehavior == DeleteBehavior.Cascade && !fk.IsOwnership);

		foreach (var fk in cascadeFks)
			fk.DeleteBehavior = DeleteBehavior.Restrict;

		base.OnModelCreating(builder);
	}
}