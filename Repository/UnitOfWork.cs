using ContactManagementV2.Data;
using ContactManagementV2.Models;
using ContactManagementV2.Repository.interfaces;

namespace ContactManagementV2.Repository;

public class UnitOfWork(
AppDbContext context,
IRepository<Contact> contact,
IRepository<Category> category) : IUnitOfWork
{
    private readonly AppDbContext _context = context;
    private IRepository<Contact> _contact = contact;
    private IRepository<Category> _category = category;

    public IRepository<Contact> Contact => _contact;

    public IRepository<Category> Category => _category;

    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }
}