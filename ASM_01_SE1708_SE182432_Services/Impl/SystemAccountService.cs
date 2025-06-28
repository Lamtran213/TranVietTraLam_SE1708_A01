using ASM_01_SE1708_SE182432_Repositories.Def;
using ASM_01_SE1708_SE182432_Repositories.Models;
using ASM_01_SE1708_SE182432_Services.Def;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASM_01_SE1708_SE182432_Services.DTO.Request;
using ASM_01_SE1708_SE182432_Services.DTO.Response;

namespace ASM_01_SE1708_SE182432_Services.Impl
{
    public class SystemAccountService : ISystemAccountService
    {
        private readonly ISystemAccountRepo _systemAccountRepo;
        public SystemAccountService(ISystemAccountRepo systemAccountRepo)
        {
            _systemAccountRepo = systemAccountRepo;
        }
        public Task<SystemAccount> LoginAsync(string username, string password) => _systemAccountRepo.LoginAsync(username, password);

        public async Task<IEnumerable<GetAllSystemAccount>> GetAllSystemAccountsAsync()
        {
            var result = await _systemAccountRepo.GetAllSystemAccountsAsync();
            return result.Select(account => new GetAllSystemAccount
            {
                AccountId = account.AccountId,
                AccountName = account.AccountName,
                AccountEmail = account.AccountEmail,
                AccountRole = account.AccountRole,
            }).ToList();
        }

        public async Task<IEnumerable<GetAllSystemAccount>> GetSystemAccountByIdAsync(short id)
        {
            var result = await _systemAccountRepo.GetSystemAccountByIdAsync(id);
            if (result == null)
            {
                throw new KeyNotFoundException("System account not found.");
            }
            return new List<GetAllSystemAccount>
            {
                new GetAllSystemAccount
                {
                    AccountId = result.AccountId,
                    AccountName = result.AccountName,
                    AccountEmail = result.AccountEmail,
                    AccountRole = result.AccountRole
                }
            };
        }

        public async Task<SystemAccount> GetProfileByIdAsync(short id) => await _systemAccountRepo.GetProfileByIdAsync(id);

        public async Task<SystemAccount> AddSystemAccountAsync(AddSystemAccountResponse systemAccount)
        {
            var newAccount = new SystemAccount
            {
                AccountName = systemAccount.AccountName,
                AccountEmail = systemAccount.AccountEmail,
                AccountRole = systemAccount.AccountRole,
                AccountPassword = "@1"
            };
            return await _systemAccountRepo.AddSystemAccountAsync(newAccount);
        }

        public async Task<SystemAccount> UpdateSystemAccountAsync(UpdateSystemAccountResponse systemAccount)
        {
            var updatedAccount = new SystemAccount
            {
                AccountId = systemAccount.AccountId,
                AccountName = systemAccount.AccountName,
                AccountEmail = systemAccount.AccountEmail,
                AccountRole = systemAccount.AccountRole
            };
            return await _systemAccountRepo.UpdateSystemAccountAsync(updatedAccount);
        }

        public async Task DeleteSystemAccountAsync(short id)
        {
            var existingAccount = await _systemAccountRepo.GetSystemAccountByIdAsync(id);
            if (existingAccount == null)
            {
                throw new KeyNotFoundException("System account not found.");
            }
            await _systemAccountRepo.DeleteSystemAccountAsync(id);
        }

        public Task<SystemAccount> UpdateProfileAsync(SystemAccount systemAccount) => _systemAccountRepo.UpdateProfileAsync(systemAccount);
    }
}
