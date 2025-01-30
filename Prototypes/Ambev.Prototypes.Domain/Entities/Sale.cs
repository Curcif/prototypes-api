using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ambev.Prototypes.Domain.Entities
{
    public class Sale
    {
        public Guid Id { get; private set; }
        public int SaleNumber { get; private set; }
        public DateTime SaleDate { get; private set; }
        public Guid CustomerId { get; private set; } // External Identity
        public decimal TotalAmount { get; set; }
        public Guid BranchId { get; private set; } // External Identity
        public List<SaleItem> Items { get; private set; }
        public bool IsCancelled { get; private set; }

        public Sale(int saleNumber, Guid customerId, Guid branchId)
        {
            Id = Guid.NewGuid();
            SaleNumber = saleNumber;
            SaleDate = DateTime.UtcNow;
            CustomerId = customerId;
            BranchId = branchId;
            Items = new List<SaleItem>();
            IsCancelled = false;
        }

        public bool ValidateAndCalculate()
        {
            if (Items.Any(item => item.Quantity > 20))
                return false;

            foreach (var item in Items)
            {
                if (item.Quantity >= 10 && item.Quantity <= 20)
                    item.ApplyDiscount(0.2m);
                else if (item.Quantity >= 4)
                    item.ApplyDiscount(0.1m);
                else
                    item.ApplyDiscount(0);
            }

            CalculateTotal();
            return true;
        }

        public void AddItem(Guid productId, int quantity, decimal unitPrice)
        {
            if (quantity > 20)
                throw new InvalidOperationException("Cannot purchase more than 20 identical items.");

            var saleItem = new SaleItem(productId, quantity, unitPrice);
            Items.Add(saleItem);
            ValidateAndCalculate();
            LogEvent("ItemAdded", saleItem);
        }

        private void CalculateTotal()
        {
            TotalAmount = Items.Sum(i => i.TotalPrice);
        }

        public void Cancel()
        {
            IsCancelled = true;
            LogEvent("SaleCancelled", this);
        }

        public void ModifyItem(Guid productId, int newQuantity, decimal newUnitPrice)
        {
            var item = Items.FirstOrDefault(i => i.ProductId == productId);
            if (item == null)
                throw new InvalidOperationException("Item not found in the sale.");

            Items.Remove(item);
            AddItem(productId, newQuantity, newUnitPrice);
            LogEvent("ItemModified", item);
        }

        public void CancelItem(Guid productId)
        {
            var item = Items.FirstOrDefault(i => i.ProductId == productId);
            if (item == null)
                throw new InvalidOperationException("Item not found in the sale.");

            Items.Remove(item);
            CalculateTotal();
            LogEvent("ItemCancelled", item);
        }

        private void LogEvent(string eventName, object data)
        {
            Console.WriteLine($"Event: {eventName} - Data: {JsonSerializer.Serialize(data)}");
        }
    }
}