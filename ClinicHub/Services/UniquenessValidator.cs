using ClinicHub.Data.Repositories;

namespace ClinicHub.Services;

public class UniquenessValidator : IUniquenessValidator
{
	public JsonResult IsUnique<T>(IBaseRepository<T> repository, Expression<Func<T, bool>> predicate, int? currentId, Func<T, int> existingEntityId) where T : class
	{
		var isExistingEntity = repository.Find(predicate);

		var isAllowed = isExistingEntity is null || existingEntityId(isExistingEntity).Equals(currentId);

		return new JsonResult(isAllowed);
	}
}