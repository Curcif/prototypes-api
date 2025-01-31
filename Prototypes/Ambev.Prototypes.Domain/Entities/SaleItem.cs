namespace Ambev.Prototypes.Domain.Entities
{
    public class SaleItem
    {
        public Guid ProductId { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal Discount { get; set; }
        //public decimal TotalPrice => Quantity * UnitPrice * (1 - Discount);
        public decimal TotalPrice { get; set; }

        public SaleItem(Guid productId, int quantity, decimal unitPrice)
        {
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
            Discount = 0;
            TotalPrice = Quantity * UnitPrice * (1 - Discount);
        }

        public void ApplyDiscount(decimal discount)
        {
            Discount = discount;
            TotalPrice = Quantity * UnitPrice * (1 - Discount);
        }
    }
}
