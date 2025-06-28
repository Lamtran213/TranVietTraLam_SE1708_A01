using ASM_01_SE1708_SE182432_Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASM_01_SE1708_SE182432_Services.DTO.Request;
using ASM_01_SE1708_SE182432_Services.DTO.Response;

namespace ASM_01_SE1708_SE182432_Services.Def
{
    public interface ISystemAccountService
    {
        public Task<SystemAccount> LoginAsync(string username, string password);
        public Task<IEnumerable<GetAllSystemAccount>> GetAllSystemAccountsAsync();
        public Task<IEnumerable<GetAllSystemAccount>> GetSystemAccountByIdAsync(short id);
        public Task<SystemAccount> GetProfileByIdAsync(short id);
        public Task<SystemAccount> AddSystemAccountAsync(AddSystemAccountResponse systemAccount);
        public Task<SystemAccount> UpdateSystemAccountAsync(UpdateSystemAccountResponse systemAccount);
        public Task DeleteSystemAccountAsync(short id);
        public Task<SystemAccount> UpdateProfileAsync(SystemAccount systemAccount);
    }
}
