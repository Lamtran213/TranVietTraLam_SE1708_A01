using ASM_01_SE1708_SE182432_Repositories.Models;
using ASM_01_SE1708_SE182432_Services.Def;
using ASM_01_SE1708_SE182432_Services.DTO.Request;
using ASM_01_SE1708_SE182432_Services.DTO.Response;
using ASM01_SE1708_SE182432_MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASM01_SE1708_SE182432_MVC.Controllers
{
    public class SystemAccountController : Controller
    {
        private readonly ISystemAccountService _systemAccountService;
        private readonly INewsArticleService _newsArticleService;
        private readonly ITagsService _tagsService;

        public SystemAccountController(ISystemAccountService systemAccountService, INewsArticleService newsArticleService, ITagsService tagsService)
        {
            _systemAccountService = systemAccountService;
            _newsArticleService = newsArticleService;
            _tagsService = tagsService;
        }
        
        [HttpGet("/login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("/login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ViewBag.Error = "Please enter both email and password.";
                return View("Login");
            }
            var account = await _systemAccountService.LoginAsync(email, password);
            if (account == null)
            {
                ViewBag.Error = "Email or password is incorrect. Please try again.";
                return View("Login");
            }
            switch (account.AccountRole)
            {
                case 0:
                    HttpContext.Session.SetObject("AdminAccount", account);
                    return View("Admin", account);
                case 1:
                    HttpContext.Session.SetObject("StaffAccount", account);
                    return View("Staff", account);
                case 2:
                    HttpContext.Session.SetObject("LecturerAccount", account);
                    return View("Lecturer", account);
                default:
                    ViewBag.Error = "Unknown account role.";
                    return View("Login");
            }
        }
        [HttpGet("/logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("AdminAccount");
            HttpContext.Session.Remove("StaffAccount");
            HttpContext.Session.Remove("LecturerAccount");
            return RedirectToAction("Login");
        }
        [HttpGet]
        public IActionResult Lecturer()
        {
            var account = HttpContext.Session.GetObject<SystemAccount>("LecturerAccount");
            if (account == null)
            {
                return RedirectToAction("Login");
            }
            return View(account);
        }
        [HttpGet]
        public IActionResult Staff()
        {
            var account = HttpContext.Session.GetObject<SystemAccount>("StaffAccount");
            if (account == null)
            {
                return RedirectToAction("Login");
            }
            return View(account);
        }
        [HttpGet]
        public IActionResult Admin()
        {
            var account = HttpContext.Session.GetObject<SystemAccount>("AdminAccount");
            if (account == null)
            {
                return RedirectToAction("Login");
            }
            return View(account);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSystemAccounts()
        {
            var accounts = await _systemAccountService.GetAllSystemAccountsAsync();
            var model = accounts.Select(a => new GetAllSystemAccount()
            {
                AccountId = a.AccountId,
                AccountName = a.AccountName,
                AccountEmail = a.AccountEmail,
                AccountRole = a.AccountRole
            }).ToList();
            return View("ManageUser", model);
        }
        [HttpGet]
        public async Task<IActionResult> GetSystemAccountById(short id)
        {
            var account = await _systemAccountService.GetSystemAccountByIdAsync(id);
            return View("ManageUser", account);
        }
        [HttpPost]
        public async Task<IActionResult> AddSystemAccount(AddSystemAccountResponse request)
        {
            await _systemAccountService.AddSystemAccountAsync(request);
            var getAllAccounts = await _systemAccountService.GetAllSystemAccountsAsync();
            ViewBag.AddSystemAccountModel = getAllAccounts;
            return View("ManageUser", getAllAccounts);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateSystemAccount(UpdateSystemAccountResponse request)
        {
            await _systemAccountService.UpdateSystemAccountAsync(request);
            var getAllAccounts = await _systemAccountService.GetAllSystemAccountsAsync();
            ViewBag.AddSystemAccountModel = getAllAccounts;
            return View("ManageUser", getAllAccounts);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteSystemAccount(short id)
        {
            await _systemAccountService.DeleteSystemAccountAsync(id);
            var getAllAccounts = await _systemAccountService.GetAllSystemAccountsAsync();
            ViewBag.AddSystemAccountModel = getAllAccounts;
            return View("ManageUser", getAllAccounts);
        }

        [HttpGet]
        public IActionResult Profile()
        {
            var currentStaff = HttpContext.Session.GetObject<SystemAccount>("StaffAccount");

            if (currentStaff == null)
            {
                return RedirectToAction("Login");
            }

            return View(currentStaff);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfile(SystemAccount systemAccount)
        {
            var currentStaff = HttpContext.Session.GetObject<SystemAccount>("StaffAccount");

            if (currentStaff == null)
            {
                return RedirectToAction("Login");
            }

            systemAccount.AccountId = currentStaff.AccountId;

            var profile = await _systemAccountService.UpdateProfileAsync(systemAccount);

            HttpContext.Session.SetObject("StaffAccount", profile);

            return View("Profile", profile);
        }
    }
}
