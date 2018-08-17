namespace Dashboard.Common.Security
{
    public interface IDisplayableUser
    {
        string DisplayName { get; }
        string NetworkName { get; }
    }
}
