using GRC.Application.Interfaces;
using GRC.Domain.Entities;
using GRC.Domain.Interfaces;

namespace GRC.Application.Services;

public class OrganizationService : IOrganizationService
{
    private readonly IOrganizationRepository _organizationRepository;

    public OrganizationService(IOrganizationRepository organizationRepository)
    {
        _organizationRepository = organizationRepository;
    }

    public async Task<Organization?> GetOrganizationByIdAsync(int id)
    {
        return await _organizationRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Organization>> GetAllOrganizationsAsync()
    {
        return await _organizationRepository.GetAllAsync();
    }

    public async Task<int> CreateOrganizationAsync(string name, string description)
    {
        var organization = new Organization
        {
            Name = name,
            Description = description
        };
        return await _organizationRepository.AddAsync(organization);
    }
}