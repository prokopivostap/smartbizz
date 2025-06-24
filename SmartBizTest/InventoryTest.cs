using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using SmartBiz.Web.Controllers;
using SmartBiz.Application.DTO;
using SmartBiz.Application.Interfaces;
using System.Collections.Generic;

namespace SmartBiz.Tests
{
    public class InventoryControllerTests
    {
        private readonly Mock<IInventoryService> _mockService;
        private readonly InventoryController _controller;

        public InventoryControllerTests()
        {
            _mockService = new Mock<IInventoryService>();
            _controller = new InventoryController(_mockService.Object);
        }

        [Fact]
        public void Index_ReturnsViewWithItems()
        {
            var expectedItems = new List<InventoryDto> {
                new InventoryDto { Id = 1, ProductName = "Item1", Quantity = 10 }
            };

            _mockService.Setup(s => s.GetAllItems(null)).Returns(expectedItems);

            var result = _controller.Index(null) as ViewResult;

            Assert.NotNull(result);
            var model = Assert.IsAssignableFrom<IEnumerable<InventoryDto>>(result.Model);
            Assert.Single(model);
        }

        [Fact]
        public void Create_Get_ReturnsViewWithEmptyDto()
        {
            var result = _controller.Create() as ViewResult;

            Assert.NotNull(result);
            Assert.IsType<InventoryDto>(result.Model);
        }

        [Fact]
        public void Create_Post_ValidDto_RedirectsToIndex()
        {
            var dto = new InventoryDto { Id = 1, ProductName = "New Item", Quantity = 5 };

            var result = _controller.Create(dto) as RedirectToActionResult;

            _mockService.Verify(s => s.AddItem(dto), Times.Once);
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
        }

        [Fact]
        public void Create_Post_InvalidModel_ReturnsViewWithModel()
        {
            _controller.ModelState.AddModelError("Name", "Required");
            var dto = new InventoryDto();

            var result = _controller.Create(dto) as ViewResult;

            Assert.NotNull(result);
            Assert.Equal(dto, result.Model);
        }

        [Fact]
        public void Edit_Get_ValidId_ReturnsViewWithDto()
        {
            var dto = new InventoryDto { Id = 1, ProductName = "Test" };
            _mockService.Setup(s => s.GetItemById(1)).Returns(dto);

            var result = _controller.Edit(1) as ViewResult;

            Assert.NotNull(result);
            Assert.Equal(dto, result.Model);
        }

        [Fact]
        public void Edit_Get_InvalidId_ReturnsNotFound()
        {
            _mockService.Setup(s => s.GetItemById(999)).Returns((InventoryDto)null);

            var result = _controller.Edit(999);

            Assert.IsType<NotFoundResult>(result);
        }

    

        [Fact]
        public void UpdateQuantity_CallsServiceAndRedirects()
        {
            var result = _controller.UpdateQuantity(1, 20) as RedirectToActionResult;

            _mockService.Verify(s => s.UpdateQuantity(1, 20), Times.Once);
            Assert.Equal("Index", result.ActionName);
        }

        [Fact]
        public void AutomateReplenishment_CallsServiceAndRedirects()
        {
            var result = _controller.AutomateReplenishment() as RedirectToActionResult;

            _mockService.Verify(s => s.AutomateReplenishment(), Times.Once);
            Assert.Equal("Index", result.ActionName);
        }

        [Fact]
        public void Delete_CallsServiceAndRedirects()
        {
            var result = _controller.Delete(5) as RedirectToActionResult;

            _mockService.Verify(s => s.DeleteItem(5), Times.Once);
            Assert.Equal("Index", result.ActionName);
        }
    }
}
