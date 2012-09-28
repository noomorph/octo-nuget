namespace Code.ReleaseServices.Core.Configuration
{
    public interface IJiraConfiguration
    {
        string JiraHost { get; }
        string JiraToken { get; }
    }
}