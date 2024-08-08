using Racuni.Data;
using Racuni.Interfaces;
using Racuni.Models;

namespace Racuni.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly InvoiceDbContext _context;

        public AccountRepository(InvoiceDbContext context)
        {
            _context = context;
        }

        public ICollection<AccountHeader> GetAccounts()
        {
            return _context.AccountHeaders.OrderBy(account => account.Id).ToList();
        }

        bool IAccountRepository.AccountExists(int invoiceNumber)
        {
            return _context.AccountHeaders.Any(account => account.InvoiceNumber == invoiceNumber);
        }

        AccountHeader IAccountRepository.GetAccountByInvoiceNumber(int invoiceNumber)
        {
            return _context.AccountHeaders.Where(account => account.InvoiceNumber == invoiceNumber).FirstOrDefault();
        }
    }
}
