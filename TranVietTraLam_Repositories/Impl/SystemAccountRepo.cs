﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using TranVietTraLam_Repositories.Def;
using TranVietTraLam_Repositories.Models;

namespace TranVietTraLam_Repositories.Impl
{
    public class SystemAccountRepo : ISystemAccountRepo
    {
        private readonly FunewsManagementContext _context;
        private readonly IConfiguration _configuration;

        public SystemAccountRepo(FunewsManagementContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<SystemAccount> LoginAsync(string username, string password)
        {
            var adminEmail = _configuration["AdminAccount:Email"];
            var adminPassword = _configuration["AdminAccount:Password"];
            if (username == adminEmail && password == adminPassword)
            {
                return new SystemAccount
                {
                    AccountName = "Admin",
                    AccountEmail = adminEmail,
                    AccountRole = 0 
                };
            }

            if(username == _context.SystemAccounts.FirstOrDefault(a => a.AccountEmail == username)?.AccountEmail &&
               password == _context.SystemAccounts.FirstOrDefault(a => a.AccountEmail == username)?.AccountPassword)
            {
                return await _context.SystemAccounts
                    .FirstOrDefaultAsync(a => a.AccountEmail == username && a.AccountPassword == password);
            }
            else
            {
                return null; 
            }

        }

        public async Task<SystemAccount> GetProfileByIdAsync(short id)
        {
            var account = await _context.SystemAccounts
                .FirstOrDefaultAsync(a => a.AccountId == id);
            if (account != null) return account;
            throw new KeyNotFoundException("System account not found.");
        }

        public async Task<IEnumerable<SystemAccount>> GetAllSystemAccountsAsync()
        {
            return await _context.SystemAccounts
                .ToListAsync();
        }

        public async Task<SystemAccount> GetSystemAccountByIdAsync(short id)
        {
            return await _context.SystemAccounts.Where(a=> a.AccountId == id).FirstOrDefaultAsync()
                ?? throw new KeyNotFoundException("System account not found.");
        }

        public async Task<SystemAccount> AddSystemAccountAsync(SystemAccount systemAccount)
        {
            var maxId = await _context.SystemAccounts.MaxAsync(a => (short?)a.AccountId) ?? 0;
            systemAccount.AccountId = (short)(maxId + 1);
            _context.SystemAccounts.Add(systemAccount);
            await _context.SaveChangesAsync();
            return await Task.FromResult(systemAccount);
        }

        public async Task<SystemAccount> UpdateSystemAccountAsync(SystemAccount systemAccount)
        {
            var existingAccount = await _context.SystemAccounts.FirstOrDefaultAsync(a => a.AccountId == systemAccount.AccountId);
            if (existingAccount == null)
            {
                throw new KeyNotFoundException("System account not found.");
            }
            existingAccount.AccountName = systemAccount.AccountName;
            existingAccount.AccountEmail = systemAccount.AccountEmail;
            existingAccount.AccountRole = systemAccount.AccountRole;
            await _context.SaveChangesAsync();
            return existingAccount;
        }

        public async Task DeleteSystemAccountAsync(short id)
        {
            var existingAccount = await GetSystemAccountByIdAsync(id);
            if (existingAccount == null)
            {
                throw new KeyNotFoundException("System account not found.");
            }
            _context.SystemAccounts.Remove(existingAccount);
            await _context.SaveChangesAsync();
        }

        public async Task<SystemAccount> UpdateProfileAsync(SystemAccount systemAccount)
        {
            var existingAccount = await GetSystemAccountByIdAsync(systemAccount.AccountId);
            if (existingAccount == null)
            {
                throw new KeyNotFoundException("System account not found.");
            }
            existingAccount.AccountName = systemAccount.AccountName;
            existingAccount.AccountEmail = systemAccount.AccountEmail;
            await _context.SaveChangesAsync();
            return existingAccount;
        }

        public async Task<SystemAccount> ChangePasswordAsync(short id, string oldPassword, string newPassword)
        {
            var existingAccount = _context.SystemAccounts.FirstOrDefault(a => a.AccountId == id);
            if (existingAccount == null)
            {
                throw new KeyNotFoundException("System account not found.");
            }
            if (existingAccount.AccountPassword != oldPassword)
            {
                throw new UnauthorizedAccessException("Old password is incorrect.");
            }
            existingAccount.AccountPassword = newPassword;
            _context.SystemAccounts.Update(existingAccount);
            await _context.SaveChangesAsync();
            return existingAccount;
        }
    }
}
