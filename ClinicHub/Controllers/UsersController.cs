using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClinicHub.Controllers;

public class UsersController : Controller
{
	private readonly UserManager<ApplicationUser> _userManager;
	private readonly RoleManager<IdentityRole> _roleManager;
	private readonly IMapper _mapper;

	public UsersController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
		IMapper mapper)
	{
		_userManager = userManager;
		_roleManager = roleManager;
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
}