using TranVietTraLam_Repositories.Models;
using TranVietTraLamMVC_Services.Def;
using TranVietTraLamMVC_Services.DTO.Request;
using TranVietTraLamMVC_Services.DTO.Response;
using TranVietTraLamMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace TranVietTraLamMVC.Controllers
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
            if (request.AccountEmail != null)
            {
                var existingAccounts = await _systemAccountService.GetAllSystemAccountsAsync();
                if (existingAccounts.Any(a => a.AccountEmail == request.AccountEmail))
                {
                    ViewBag.Error = "Email already exists. Please use a different email.";
                    return View("ManageUser", existingAccounts);
                }
            }
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(short AccountId, string OldPassword, string NewPassword)
        {
            var currentStaff = HttpContext.Session.GetObject<SystemAccount>("StaffAccount");
            if (currentStaff == null)
            {
                return RedirectToAction("Login");
            }
            if (AccountId != currentStaff.AccountId)
            {
                return BadRequest("Invalid account ID.");
            }
            if (string.IsNullOrEmpty(OldPassword) || string.IsNullOrEmpty(NewPassword))
            {
                ViewBag.Error = "Please enter both old and new passwords.";
                return View("Profile", currentStaff);
            }
            try
            {
                var updatedAccount = await _systemAccountService.ChangePasswordAsync(AccountId, OldPassword, NewPassword);
                HttpContext.Session.SetObject("StaffAccount", updatedAccount);
                ViewBag.Success = "Password changed successfully.";
                return View("Profile", updatedAccount);
            }
            catch (KeyNotFoundException)
            {
                ViewBag.Error = "Account not found.";
                return View("Profile", currentStaff);
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"An error occurred: {ex.Message}";
                return View("Profile", currentStaff);
            }
        }
    }
}
