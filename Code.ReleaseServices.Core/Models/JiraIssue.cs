namespace Code.ReleaseServices.Core.Models
{
    public class JiraIssue : JiraItem
    {
        public string Summary { get; set; }
        public JiraIssueFields Fields { get; set; }
    }
}