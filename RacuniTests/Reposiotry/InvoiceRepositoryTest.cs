using Microsoft.EntityFrameworkCore;
using Racuni.Data;
using Racuni.Interfaces;
using Racuni.Repository;

namespace RacuniTests.Reposiotry
{
    public class InvoiceRepositoryTest : IDisposable
    {
        private readonly InvoiceDbContext _context;
        private readonly IInvoiceRepository _repository;

        public InvoiceRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<InvoiceDbContext>().EnableSensitiveDataLogging()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new InvoiceDbContext(options);
            _repository = new InvoiceRepository(_context);

            SeedDatabase();
        }

        private void SeedDatabase()
        {
            _context.InvoiceHeaders.AddRange(TestData.invoices);
            _context.SaveChanges();
        }

        [Fact]
        public void GetInvoices_Returns_Invoices()
        {
            var result = _repository.GetInvoices();

            Assert.Equal(TestData.invoices, result);
        }

        [Fact]
        public void InvoiceItemExists_Returns_False_WhenItemDoesNotExist()
        {
            var result = _repository.InvoiceExists(1);

            Assert.False(result);
        }

        [Fact]
        public void InvoiceItemExists_Returns_True()
        {
            var result = _repository.InvoiceExists(245678);

            Assert.True(result);
        }

        [Fact]
        public void GetInvoiceByInvoiceNumber_ReturnsInvoiceWithItems_WhenInvoiceExists()
        {
            var invoice = _repository.GetInvoiceByInvoiceNumber(245678);

            Assert.Equal(TestData.invoice.Address, invoice.Address);
        }

        [Fact]
        public void GetInvoiceByInvoiceNumber_ReturnsNull_WhenInvoiceDoesNotExist()
        {
            var invoice = _repository.GetInvoiceByInvoiceNumber(1);

            Assert.Null(invoice);
        }

        [Fact]
        public void DeleteInvoice_RemovesInvoice_WhenInvoiceExists()
        {
            var result = _repository.DeleteInvoice(TestData.invoices[0]);

            var deleted = _repository.InvoiceExists(245678);

            Assert.True(result);
            Assert.False(deleted);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
