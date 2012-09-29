using Newtonsoft.Json;

namespace Code.ReleaseServices.Core.Models
{
    public class JiraIssueFields
    {
        [JsonProperty("customfield_13160")]
        public string Version { get; set; }

        public JiraItem Project { get; set; }
    }
}