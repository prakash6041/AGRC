using GRC.Domain.Entities;

namespace GRC.Application.Interfaces;

public interface IOrganizationService
{
    Task<Organization?> GetOrganizationByIdAsync(int id);
    Task<IEnumerable<Organization>> GetAllOrganizationsAsync();
    Task<int> CreateOrganizationAsync(string name, string description);
}