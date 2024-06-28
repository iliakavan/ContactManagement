using ContactManagementV2.Models;

namespace ContactManagementV2.Repository.interfaces;

public interface IUnitOfWork
{
    IRepository<Contact> Contact { get; }
    IRepository<Category> Category { get; }
    Task Save();
}