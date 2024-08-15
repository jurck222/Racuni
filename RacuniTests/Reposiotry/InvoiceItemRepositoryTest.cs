using Microsoft.EntityFrameworkCore;
using Racuni.Data;
using Racuni.Interfaces;
using Racuni.Repository;

namespace RacuniTests.Reposiotry
{
    public class InvoiceItemRepositoryTest : IDisposable
    {
        private readonly InvoiceDbContext _context;
        private readonly IInvoiceItemRepository _repository;
        public InvoiceItemRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<InvoiceDbContext>().EnableSensitiveDataLogging()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new InvoiceDbContext(options);
            _repository = new InvoiceItemRepository(_context);

            SeedDatabase();
        }

        private void SeedDatabase()
        {
            _context.InvoiceHeaders.AddRange(TestData.invoices);
            _context.SaveChanges();
        }

        [Fact]
        public void GetInvoiceItemById_Returns_Item_WhenItemExists()
        {
            var item = _repository.GetInvoiceItemById(1);

            Assert.NotNull(item);
            Assert.Equal("TV - JAVNA RABA - PAVŠAL", item.ItemName);
        }

        [Fact]
        public void GetInvoiceItemById_Returns_Null_WhenItemDoesntExists()
        {
            var item = _repository.GetInvoiceItemById(9999);

            Assert.Null(item);
        }

        [Fact]
        public void DeleteInvoiceItem_RemovesItem_WhenItemExists()
        {
            var invoiceItem = TestData.invoices[0].InvoiceItems.First();
            var result = _repository.DeleteInvoiceItem(invoiceItem);

            Assert.True(result);
            Assert.Null(_context.InvoiceItems.Find(invoiceItem.Id));
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
