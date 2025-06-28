using ASM_01_SE1708_SE182432_Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASM_01_SE1708_SE182432_Repositories.Def
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
    }
}
