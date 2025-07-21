using ClinicHub.Data.Repositories;

namespace ClinicHub.Services;

public interface IUniquenessValidator
{
	JsonResult IsUnique<T>(IBaseRepository<T> repository, Expression<Func<T, bool>> predicate, int? currentId, Func<T, int> getId)
		where T : class;
}