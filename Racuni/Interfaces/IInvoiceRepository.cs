using Racuni.Models;

namespace Racuni.Interfaces
{
    public interface IInvoiceRepository
    {
        ICollection<InvoicesHeader> GetInvoices();
        InvoicesHeader GetInvoiceByInvoiceNumber(int invoiceNumber);
        bool InvoiceExists(int invoiceNumber);

        bool DeleteInvoice(InvoicesHeader invoice);
    }
}
