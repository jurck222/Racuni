using Microsoft.EntityFrameworkCore;
using Racuni.Data;
using Racuni.Interfaces;
using Racuni.Models;

namespace Racuni.Repository
{
    public class InvoiceItemRepository : IInvoiceItemRepository
    {
        private readonly InvoiceDbContext _context;

        public InvoiceItemRepository(InvoiceDbContext context)
        {
            _context = context;
        }

        bool IInvoiceItemRepository.InvoiceItemExists(int itemId)
        {
            return _context.InvoiceItems.Any(item => item.Id == itemId);
        }

        InvoiceItem IInvoiceItemRepository.GetInvoiceItemById(int itemId)
        {
            return _context.InvoiceItems.Where(item => item.Id == itemId).FirstOrDefault();
        }

        bool IInvoiceItemRepository.DeleteInvoiceItem(InvoiceItem item)
        {
            _context.InvoiceItems.Remove(item);
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        bool IInvoiceItemRepository.AddInvoiceItem(int invoiceHeaderId, InvoiceItem item)
        {
            var invoiceHeader = _context.InvoiceHeaders.Include(invoice => invoice.InvoiceItems).FirstOrDefault(invoice => invoice.Id == invoiceHeaderId);


            if (invoiceHeader == null)
            {
                return false;
            }

            invoiceHeader.InvoiceItems.Add(item);

            var saved = _context.SaveChanges();
            return saved > 0;
        }
    }
}
