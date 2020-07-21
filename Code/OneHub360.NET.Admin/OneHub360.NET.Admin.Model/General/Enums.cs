namespace OneHub360.NET.Admin.Model
{
    public enum GroupStatus
    {
        Active = 1,
        Inactive
    }

    public enum InvitationStatus
    {
        Sent = 1,
        Accepted,
        Rejected,
        Closed
    }

    public enum SignatureStatus
    {
        Active = 1,
        Inactive,
        Expired,
        Revoked
    }

    public enum UserStatus
    {
        Registered = 1,
        Updated,
        Approved,
        Inactive
    }
}