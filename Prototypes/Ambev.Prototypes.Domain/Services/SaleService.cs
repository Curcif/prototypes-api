using Ambev.Prototypes.Domain.Entities;
using Ambev.Prototypes.Domain.Interfaces;

namespace Ambev.Prototypes.Domain.Services
{
    public class SaleService
    {
        private readonly ISaleRepository _saleRepository;

        public SaleService(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task<IEnumerable<Sale>> GetAllSalesAsync()
        {
            return await _saleRepository.GetAllAsync();
        }

        public async Task<Sale> GetSaleByIdAsync(Guid id)
        {
            return await _saleRepository.GetByIdAsync(id);
        }

        public async Task AddSaleAsync(Sale sale)
        {
            await _saleRepository.AddAsync(sale);
        }

        public async Task UpdateSaleAsync(Sale sale)
        {
            await _saleRepository.UpdateAsync(sale);
        }

        public async Task DeleteSaleAsync(Guid id)
        {
            await _saleRepository.DeleteAsync(id);
        }

        public Sale ValidateAndCalculateSale(Sale sale)
        {
            foreach (var item in sale.Items)
            {
                if (item.Quantity > 20)
                {
                    return null; // Exceeds maximum limit
                }
                else if (item.Quantity >= 10)
                {
                    item.Discount = 0.2m; // 20% discount
                }
                else if (item.Quantity >= 4)
                {
                    item.Discount = 0.1m; // 10% discount
                }
                else
                {
                    item.Discount = 0m; // No discount
                }

                item.TotalPrice = (item.UnitPrice * item.Quantity) * (1 - item.Discount);
            }

            sale.TotalAmount = sale.Items.Sum(i => i.TotalPrice);
            return sale;
        }
    }
}
