using Ambev.Prototypes.Domain.Entities;

namespace Test.Ambev.Prototypes.Api.Unit
{
    public class SaleTests
    {
        [Fact]
        public void AddItem_ShouldApplyCorrectDiscount()
        {
            var sale = new Sale(1, Guid.NewGuid(), Guid.NewGuid());
            sale.AddItem(Guid.NewGuid(), 5, 100);

            Assert.Single(sale.Items);
            Assert.Equal(500, sale.Items[0].TotalPrice);
        }

        [Fact]
        public void AddItem_ShouldNotAllowMoreThan20Items()
        {
            var sale = new Sale(1, Guid.NewGuid(), Guid.NewGuid());

            Assert.Throws<InvalidOperationException>(() => sale.AddItem(Guid.NewGuid(), 21, 50));
        }

        [Fact]
        public void CalculateTotal_ShouldSumAllItemsCorrectly()
        {
            var sale = new Sale(1, Guid.NewGuid(), Guid.NewGuid());
            sale.AddItem(Guid.NewGuid(), 3, 100);
            sale.AddItem(Guid.NewGuid(), 10, 200);

            Assert.Equal(1900, sale.TotalAmount);
        }

        [Fact]
        public void CancelSale_ShouldMarkSaleAsCancelled()
        {
            var sale = new Sale(1, Guid.NewGuid(), Guid.NewGuid());
            sale.Cancel();

            Assert.True(sale.IsCancelled);
        }
    }
}
