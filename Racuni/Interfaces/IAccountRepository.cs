using Racuni.Models;

namespace Racuni.Interfaces
{
    public interface IAccountRepository
    {
        ICollection<AccountHeader> GetAccounts();
        AccountHeader GetAccountByInvoiceNumber(int invoiceNumber);
        bool AccountExists(int invoiceNumber);
    }
}
