using Racuni.Models;

namespace Racuni.Interfaces
{
    public interface IInvoiceItemRepository
    {
        InvoiceItem GetInvoiceItemById(int itemId);
        bool InvoiceItemExists(int itemId);
        bool DeleteInvoiceItem(InvoiceItem item);
    }
}
