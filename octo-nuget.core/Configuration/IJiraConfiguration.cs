namespace OctoNuget.Core.Configuration
{
    public interface IJiraConfiguration
    {
        string JiraHost { get; }
        string JiraToken { get; }
    }
}