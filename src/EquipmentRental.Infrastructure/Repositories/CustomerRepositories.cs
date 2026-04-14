using EquipmentRental.Application.Interfaces;
using EquipmentRental.Domain.entities;
using EquipmentRental.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EquipmentRental.Infrastructure.Repositories;


public class CustomerRepository : ICustomerRepository
{
    private readonly AppDbContext _context;

    public CustomerRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Customer> GetByIdAsync(int customerId)
    {
        var customer = await _context.Customers.FindAsync(customerId);
        if (customer == null)
        {
            throw new KeyNotFoundException($"Customer with ID {customerId} not found.");
        }
        return customer;
    }

    public async Task AddNewCustomerAsync(Customer customer)
    {
        await _context.Customers.AddAsync(customer);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateCustomerAsync(Customer customer)
    {
        _context.Customers.Update(customer);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
    {
        return await _context.Customers.ToListAsync();
    }
}
