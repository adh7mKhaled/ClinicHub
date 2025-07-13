using ClinicHub.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClinicHub.Controllers;

public class UsersController : Controller
{
	private readonly UserManager<ApplicationUser> _userManager;
	private readonly RoleManager<IdentityRole> _roleManager;
	private readonly IValidator<UserFormViewModel> _userFormValidator;
	private readonly IValidator<ResetPasswordFormViewModel> _resetPassswordValidator;
	private readonly IMapper _mapper;

	public UsersController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
		IValidator<UserFormViewModel> validator, IValidator<ResetPasswordFormViewModel> resetPassswordValidator,
		IMapper mapper)
	{
		_userManager = userManager;
		_roleManager = roleManager;
		_userFormValidator = validator;
		_resetPassswordValidator = resetPassswordValidator;
		_mapper = mapper;
	}

	public async Task<IActionResult> Index()
	{
		var users = await _userManager.Users.ToListAsync();

		return View(_mapper.Map<IEnumerable<UserViewModel>>(users));
	}

	[AjaxOnly]
	public async Task<IActionResult> Create()
	{
		UserFormViewModel viewModel = new()
		{
			Roles = await _roleManager.Roles
			.Select(r => new SelectListItem
			{
				Text = r.Name,
				Value = r.Name
			}).ToListAsync()
		};

		return PartialView("_Form", viewModel);
	}

	[HttpPost]
	public async Task<IActionResult> Create(UserFormViewModel model)
	{
		var validationResult = _userFormValidator.Validate(model);

		if (!validationResult.IsValid)
			return BadRequest();

		ApplicationUser user = new()
		{
			FullName = model.FullName,
			UserName = model.UserName,
			Email = model.Email,
			CreatedById = User.GetUserId()
		};

		var result = await _userManager.CreateAsync(user, model.Password!);

		if (result.Succeeded)
		{
			await _userManager.AddToRolesAsync(user, model.SelectedRoles);

			return PartialView("_UserRow", _mapper.Map<UserViewModel>(user));
		}
		return BadRequest(string.Join(',', result.Errors.Select(e => e.Description)));
	}

	[AjaxOnly]
	public async Task<IActionResult> Edit(string id)
	{
		var user = await _userManager.FindByIdAsync(id);

		if (user is null)
			return NotFound();

		var viewModel = _mapper.Map<UserFormViewModel>(user);

		viewModel.SelectedRoles = await _userManager.GetRolesAsync(user);

		viewModel.Roles = await _roleManager.Roles
			.Select(r => new SelectListItem
			{
				Text = r.Name,
				Value = r.Name
			}).ToListAsync();

		return PartialView("_Form", viewModel);
	}

	[HttpPost]
	public async Task<IActionResult> Edit(UserFormViewModel model)
	{
		var validationResult = _userFormValidator.Validate(model);

		if (!validationResult.IsValid)
			return BadRequest();

		var user = await _userManager.FindByIdAsync(model.Id!);

		if (user is null)
			return NotFound();

		user = _mapper.Map(model, user);
		user.LastUpdatedById = User.GetUserId();
		user.LastUpdatedOn = DateTime.Now;

		var result = await _userManager.UpdateAsync(user);

		if (result.Succeeded)
		{
			var currentRoles = await _userManager.GetRolesAsync(user);

			var rolesUpdated = currentRoles.SequenceEqual(model.SelectedRoles);

			if (!rolesUpdated)
			{
				await _userManager.RemoveFromRolesAsync(user, currentRoles);
				await _userManager.AddToRolesAsync(user, model.SelectedRoles);
			}

			await _userManager.UpdateSecurityStampAsync(user);

			return PartialView("_UserRow", _mapper.Map<UserViewModel>(user));
		}
		return BadRequest(string.Join(',', result.Errors.Select(e => e.Description)));
	}

	public async Task<IActionResult> ToggleStatus(string id)
	{
		var user = await _userManager.FindByIdAsync(id);

		if (user is null) return NotFound();

		user.IsDeleted = !user.IsDeleted;
		user.LastUpdatedOn = DateTime.Now;
		user.LastUpdatedById = User.GetUserId();

		await _userManager.UpdateAsync(user);

		if (user.IsDeleted)
			await _userManager.UpdateSecurityStampAsync(user);

		return Ok(user.LastUpdatedOn.ToString());
	}

	[AjaxOnly]
	public async Task<IActionResult> ResetPassword(string id)
	{
		var user = await _userManager.FindByIdAsync(id);

		if (user is null)
			return NotFound();

		return PartialView("_ResetPasswordForm");
	}

	[HttpPost]
	public async Task<IActionResult> ResetPassword(ResetPasswordFormViewModel model)
	{
		var validationResult = _resetPassswordValidator.Validate(model);

		if (!validationResult.IsValid)
			return BadRequest();

		var user = await _userManager.FindByIdAsync(model.Id);

		if (user is null)
			return NotFound();

		var currentPassword = user.PasswordHash;

		await _userManager.RemovePasswordAsync(user);
		var result = await _userManager.AddPasswordAsync(user, model.Password);

		if (!result.Succeeded)
		{
			user.LastUpdatedById = User.GetUserId();
			user.LastUpdatedOn = DateTime.Now;

			await _userManager.UpdateAsync(user);

			return PartialView("_UserRow", _mapper.Map<UserViewModel>(user));
		}
		user.PasswordHash = currentPassword;
		await _userManager.UpdateAsync(user);

		return BadRequest(string.Join(',', result.Errors.Select(e => e.Description)));
	}

	[HttpPost]
	public async Task<IActionResult> Unlock(string id)
	{
		var user = await _userManager.FindByIdAsync(id);

		if (user is null)
			return NotFound();

		var isLocked = await _userManager.IsLockedOutAsync(user);

		if (isLocked)
			await _userManager.SetLockoutEndDateAsync(user, null!);
		else
			return BadRequest();

		return Ok();
	}

	public async Task<IActionResult> AllowUniqueUsername(UserFormViewModel model)
	{
		var user = await _userManager.FindByNameAsync(model.UserName!);

		var isAllowed = user is null || user.Id.Equals(model.Id);

		return Json(isAllowed);
	}

	public async Task<IActionResult> AllowUniqueEmail(UserFormViewModel model)
	{
		var user = await _userManager.FindByEmailAsync(model.Email);

		var isAllowed = user is null || user.Id.Equals(model.Id);

		return Json(isAllowed);
	}
}