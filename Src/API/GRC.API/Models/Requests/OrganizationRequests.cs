namespace GRC.API.Models.Requests;

public class CreateOrganizationRequest
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}