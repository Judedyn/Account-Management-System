using CustomerApi.Data;
using CustomerApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerApi.Services
{
    public class AccountService
    {
        private readonly AppDbContext _context;

        public AccountService(AppDbContext context)
        {
            _context = context;
        }

        // Register a new account
        public async Task<Account?> RegisterAsync(Account account)
        {
            bool emailExists = await _context.Accounts
                .AnyAsync(a => a.Email == account.Email);

            if (emailExists)
            {
                return null;
            }

            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();
            return account;
        }

        // Get all accounts
        public async Task<List<Account>> GetAllAsync()
        {
            return await _context.Accounts.ToListAsync();
        }

        // Get account by ID
        public async Task<Account?> GetByIdAsync(int id)
        {
            return await _context.Accounts.FindAsync(id);
        }

        // Get account by Email
        public async Task<Account?> GetByEmailAsync(string email)
        {
            return await _context.Accounts
                .FirstOrDefaultAsync(a => a.Email == email);
        }

        // Update account
        public async Task<Account?> UpdateAsync(int id, Account updatedAccount)
        {
            var existingAccount = await _context.Accounts.FindAsync(id);

            if (existingAccount == null)
            {
                return null;
            }

            bool emailUsedByAnother = await _context.Accounts
                .AnyAsync(a => a.Email == updatedAccount.Email && a.Id != id);

            if (emailUsedByAnother)
            {
                throw new InvalidOperationException("Email is already in use by another account.");
            }

            existingAccount.FirstName = updatedAccount.FirstName;
            existingAccount.LastName = updatedAccount.LastName;
            existingAccount.Email = updatedAccount.Email;

            await _context.SaveChangesAsync();
            return existingAccount;
        }
    }
}