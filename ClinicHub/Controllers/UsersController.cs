using ClinicHub.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClinicHub.Controllers;

public class UsersController : Controller
{
	private readonly UserManager<ApplicationUser> _userManager;
	private readonly RoleManager<IdentityRole> _roleManager;
	private readonly IValidator<UserFormViewModel> _validator;
	private readonly IMapper _mapper;

	public UsersController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
		IValidator<UserFormViewModel> validator, IMapper mapper)
	{
		_userManager = userManager;
		_roleManager = roleManager;
		_validator = validator;
		_mapper = mapper;
	}

	public async Task<IActionResult> Index()
	{
		var users = await _userManager.Users.ToListAsync();

		var viewModel = _mapper.Map<IEnumerable<UserViewModel>>(users);

		return View(viewModel);
	}

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
		var validationResult = _validator.Validate(model);

		if (!validationResult.IsValid)
			return BadRequest();

		ApplicationUser user = new()
		{
			FullName = model.UserName,
			UserName = model.UserName,
			Email = model.Email,
			CreatedById = User.GetUserId()
		};

		var result = await _userManager.CreateAsync(user, model.Password!);

		if (result.Succeeded)
		{
			await _userManager.AddToRolesAsync(user, model.SelectedRoles);

			return Ok();
		}
		return BadRequest(string.Join(',', result.Errors.Select(e => e.Description)));
	}
}