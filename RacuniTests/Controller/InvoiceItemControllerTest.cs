using Microsoft.AspNetCore.Mvc;
using Moq;
using Racuni.Controllers;
using Racuni.Interfaces;

namespace RacuniTests.Controller
{
    public class InvoiceItemControllerTest
    {
        private readonly InvoiceItemController _controller;
        private readonly Mock<IInvoiceItemRepository> _mockInvoiceItemRepository;

        public InvoiceItemControllerTest()
        {
            _mockInvoiceItemRepository = new Mock<IInvoiceItemRepository>();
            _controller = new InvoiceItemController(_mockInvoiceItemRepository.Object);
        }

        [Fact]
        public void DeleteInvoiceItem_Returns_NotFound_WhenItemDoesNotExist()
        {
            int itemId = 12345;
            _mockInvoiceItemRepository.Setup(repo => repo.InvoiceItemExists(itemId)).Returns(false);

            var result = _controller.DeleteInvoiceItem(itemId);

            Assert.IsType<NotFoundResult>(result);
            _mockInvoiceItemRepository.Verify(repo => repo.InvoiceItemExists(itemId), Times.Once);
        }

        [Fact]
        public void DeleteInvoiceItem_Returns_BadRequest_WhenModelStateIsInvalid()
        {
            int itemId = 12345;
            _mockInvoiceItemRepository.Setup(repo => repo.InvoiceItemExists(itemId)).Returns(true);
            _mockInvoiceItemRepository.Setup(repo => repo.GetInvoiceItemById(itemId)).Returns(TestData.invoiceItem);

            _controller.ModelState.AddModelError("Error", "ModelState is invalid");

            var result = _controller.DeleteInvoiceItem(itemId);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<SerializableError>(badRequestResult.Value);
            _mockInvoiceItemRepository.Verify(repo => repo.InvoiceItemExists(itemId), Times.Once);
            _mockInvoiceItemRepository.Verify(repo => repo.GetInvoiceItemById(itemId), Times.Once);
        }

        [Fact]
        public void DeleteInvoice_Returns_StatusCode500_WhenDeletionFails()
        {
            int itemId = 12345;
            _mockInvoiceItemRepository.Setup(repo => repo.InvoiceItemExists(itemId)).Returns(true);
            _mockInvoiceItemRepository.Setup(repo => repo.GetInvoiceItemById(itemId)).Returns(TestData.invoiceItem);
            _mockInvoiceItemRepository.Setup(repo => repo.DeleteInvoiceItem(TestData.invoiceItem)).Returns(false);

            var result = _controller.DeleteInvoiceItem(itemId);

            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
            Assert.Equal("Something went wrong when deleting the invoice item", statusCodeResult.Value);
            _mockInvoiceItemRepository.Verify(repo => repo.InvoiceItemExists(itemId), Times.Once);
            _mockInvoiceItemRepository.Verify(repo => repo.GetInvoiceItemById(itemId), Times.Once);
            _mockInvoiceItemRepository.Verify(repo => repo.DeleteInvoiceItem(TestData.invoiceItem), Times.Once);
        }

        [Fact]
        public void AddInvoiceItem_Returns_BadRequest_WhenItemIsNull()
        {
            int invoiceId = 1;

            var result = _controller.AddInvoiceItem(invoiceId, null);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Invoice item is null.", badRequestResult.Value);
        }

        [Fact]
        public void AddInvoiceItem_Returns_NotFound_WhenInvoiceHeaderNotFound()
        {
            int invoiceId = 1;
            _mockInvoiceItemRepository.Setup(repo => repo.AddInvoiceItem(invoiceId, TestData.invoiceItem)).Returns(false);

            var result = _controller.AddInvoiceItem(invoiceId, TestData.invoiceItem);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Invoice header not found.", notFoundResult.Value);
            _mockInvoiceItemRepository.Verify(repo => repo.AddInvoiceItem(invoiceId, TestData.invoiceItem), Times.Once);
        }

        [Fact]
        public void AddInvoiceItem_Returns_Ok_WhenAdditionIsSuccessful()
        {
            int invoiceId = 1;
            _mockInvoiceItemRepository.Setup(repo => repo.AddInvoiceItem(invoiceId, TestData.invoiceItem)).Returns(true);

            var result = _controller.AddInvoiceItem(invoiceId, TestData.invoiceItem);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Invoice item added successfully.", okResult.Value);
            _mockInvoiceItemRepository.Verify(repo => repo.AddInvoiceItem(invoiceId, TestData.invoiceItem), Times.Once);
        }
    }
}
