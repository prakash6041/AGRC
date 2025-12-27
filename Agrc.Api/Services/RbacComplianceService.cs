
using Agrc.Api.Entities;
using System.Text.Json;

namespace Agrc.Api.Services
{
    public class RbacComplianceService
    {
        private readonly GraphClient _graph;

        public RbacComplianceService(GraphClient graph)
        {
            _graph = graph;
        }

        public async Task<RbacComplianceResult> EvaluateAsync(string appId)
        {
            // 1. Fetch roles
            var rolesJson = await _graph.GetAsync(
                $"servicePrincipals/{appId}/appRoles");

            var roles = JsonDocument.Parse(rolesJson)
                .RootElement.GetProperty("value")
                .EnumerateArray()
                .Select(r => new AppRole(
                    r.GetProperty("id").GetString()!,
                    r.GetProperty("displayName").GetString()!,
                    r.GetProperty("value").GetString()!
                )).ToList();

            // 2. Fetch assignments
            var assignJson = await _graph.GetAsync(
                $"servicePrincipals/{appId}/appRoleAssignedTo");

            var assignments = JsonDocument.Parse(assignJson)
                .RootElement.GetProperty("value")
                .EnumerateArray()
                .Select(a => new RoleAssignment(
                    a.GetProperty("principalId").GetString()!,
                    a.GetProperty("principalDisplayName").GetString()!,
                    a.GetProperty("principalType").GetString()!,
                    a.GetProperty("appRoleId").GetString()!,
                    a.GetProperty("createdDateTime").GetDateTime()
                )).ToList();

            // 3. Map assignments to role names
            var roleMap = roles.ToDictionary(r => r.Id, r => r.Value);

            var resolved = assignments.Select(a => new
            {
                a.PrincipalDisplayName,
                Role = roleMap.GetValueOrDefault(a.AppRoleId, "UNKNOWN"),
                a.CreatedDateTime
            }).ToList();

            // 4. SOC 2 RBAC RULES
            var adminUsers = resolved.Where(r => r.Role == "Admin").ToList();

            bool pass =
                roles.Count >= 2 &&                 // RBAC exists
                adminUsers.Count <= 2;               // Least privilege

            return new RbacComplianceResult
            {
                Pass = pass,
                Roles = roles.Select(r => r.Value).ToList(),
                AdminCount = adminUsers.Count,
                EvidenceGeneratedAt = DateTime.UtcNow,
                Assignments = resolved
            };
        }
    }

}
