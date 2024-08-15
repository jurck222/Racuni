using Microsoft.EntityFrameworkCore;
using Racuni.Data;
using Racuni.Interfaces;
using Racuni.Models;

namespace Racuni.Repository
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly InvoiceDbContext _context;

        public InvoiceRepository(InvoiceDbContext context)
        {
            _context = context;
        }

        public ICollection<InvoicesHeader> GetInvoices()
        {
            return _context.InvoiceHeaders.Include(invoice => invoice.InvoiceItems).OrderBy(invoice => invoice.Id).ToList();
        }

        bool IInvoiceRepository.InvoiceExists(int invoiceNumber)
        {
            return _context.InvoiceHeaders.Any(invoice => invoice.InvoiceNumber == invoiceNumber);
        }

        InvoicesHeader IInvoiceRepository.GetInvoiceByInvoiceNumber(int invoiceNumber)
        {
            return _context.InvoiceHeaders.Where(invoice => invoice.InvoiceNumber == invoiceNumber).Include(invoice => invoice.InvoiceItems).FirstOrDefault();
        }

        bool IInvoiceRepository.DeleteInvoice(InvoicesHeader invoice)
        {
            _context.Remove(invoice);
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
