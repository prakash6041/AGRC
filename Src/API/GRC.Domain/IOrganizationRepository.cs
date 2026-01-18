using GRC.Domain.Entities;

namespace GRC.Domain.Interfaces;

public interface IOrganizationRepository
{
    Task<Organization?> GetByIdAsync(int id);
    Task<IEnumerable<Organization>> GetAllAsync();
    Task<int> AddAsync(Organization organization);
    Task UpdateAsync(Organization organization);
    Task DeleteAsync(int id);
}