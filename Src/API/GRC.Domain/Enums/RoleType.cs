namespace GRC.Domain.Enums;

/// <summary>
/// Represents the role type of a user.
/// </summary>
public enum RoleType
{
    /// <summary>
    /// Management approver role.
    /// </summary>
    ManagementApprover = 0,

    /// <summary>
    /// Super administrator role.
    /// </summary>
    SuperAdmin = 1,

    /// <summary>
    /// Administrator role.
    /// </summary>
    Admin = 2,

    /// <summary>
    /// Approver role.
    /// </summary>
    Approver = 3,

    /// <summary>
    /// Checker/Reviewer role.
    /// </summary>
    CheckerReviewer = 4,

    /// <summary>
    /// Maker role.
    /// </summary>
    Maker = 5,

    /// <summary>
    /// Process owner role.
    /// </summary>
    ProcessOwner = 6,

    /// <summary>
    /// Control owner role.
    /// </summary>
    ControlOwner = 7,

    /// <summary>
    /// Risk owner role.
    /// </summary>
    RiskOwner = 8
}
