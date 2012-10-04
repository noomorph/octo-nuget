using Newtonsoft.Json;

namespace OctoNuget.Core.Models
{
    public class JiraIssueFields
    {
        [JsonProperty("customfield_13160")]
        public string Version { get; set; }

        public JiraItem Project { get; set; }

        public JiraItem Parent { get; set; }

        public string Summary { get; set; }

        [JsonProperty("issuetype")]
        public JiraItem IssueType { get; set; }

        public string Description { get; set; }
    }
}