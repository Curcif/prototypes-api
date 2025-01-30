using Ambev.Prototypes.Domain.Entities;
using Ambev.Prototypes.Domain.Interfaces;

namespace Ambev.Prototypes.Infra.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly List<Sale> _sales = new(); // Simulated in-memory database

        public Task<Sale?> GetByIdAsync(Guid id)
        {
            return Task.FromResult(_sales.FirstOrDefault(s => s.Id == id));
        }

        public Task<IEnumerable<Sale>> GetAllAsync()
        {
            return Task.FromResult(_sales.AsEnumerable());
        }

        public Task AddAsync(Sale sale)
        {
            _sales.Add(sale);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Sale sale)
        {
            var existingSale = _sales.FirstOrDefault(s => s.Id == sale.Id);
            if (existingSale != null)
            {
                _sales.Remove(existingSale);
                _sales.Add(sale);
            }
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Guid id)
        {
            var sale = _sales.FirstOrDefault(s => s.Id == id);
            if (sale != null)
            {
                _sales.Remove(sale);
            }
            return Task.CompletedTask;
        }
    }
}
