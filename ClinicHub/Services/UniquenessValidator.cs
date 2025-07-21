using ClinicHub.Data.Repositories;

namespace ClinicHub.Services;

public class UniquenessValidator : IUniquenessValidator
{
	public JsonResult IsUnique<T>(IBaseRepository<T> repository, Expression<Func<T, bool>> predicate, int? currentId, Func<T, int> existingEntityId) where T : class
	{
		var existingEntity = repository.Find(predicate);

		var isAllowed = existingEntity is null || existingEntityId(existingEntity).Equals(currentId);

		return new JsonResult(isAllowed);
	}
}