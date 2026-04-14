using EquipmentRental.Domain.entities;

namespace EquipmentRental.Application.Interfaces;


public interface ICustomerRepository
{
    Task<IEnumerable<Customer>> GetAllCustomersAsync();
    Task<Customer> GetByIdAsync(int customerId);
    Task AddNewCustomerAsync(Customer customer);
    Task UpdateCustomerAsync(Customer customer);
    
}