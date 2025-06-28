using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranVietTraLam_Repositories.Models;

namespace TranVietTraLam_Repositories.Def
{
    public interface ISystemAccountRepo
    {
        public Task<SystemAccount> LoginAsync(string username, string password);
        public Task<SystemAccount> GetProfileByIdAsync(short id);
        public Task<IEnumerable<SystemAccount>> GetAllSystemAccountsAsync();
        public Task<SystemAccount> GetSystemAccountByIdAsync(short id);
        public Task<SystemAccount> AddSystemAccountAsync(SystemAccount systemAccount);
        public Task<SystemAccount> UpdateSystemAccountAsync(SystemAccount systemAccount);
        public Task DeleteSystemAccountAsync(short id);
        public Task<SystemAccount> UpdateProfileAsync(SystemAccount systemAccount);
        public Task<SystemAccount> ChangePasswordAsync(short id, string oldPassword, string newPassword);
    }
}
