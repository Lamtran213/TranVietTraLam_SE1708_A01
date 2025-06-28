using TranVietTraLam_Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranVietTraLamMVC_Services.DTO.Request;
using TranVietTraLamMVC_Services.DTO.Response;

namespace TranVietTraLamMVC_Services.Def
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
        public Task<SystemAccount> ChangePasswordAsync(short id, string oldPassword, string newPassword);
    }
}
