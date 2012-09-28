using System;
using System.Net;
using Code.ReleaseServices.Core.Configuration;
using Code.ReleaseServices.Core.Models;
using Code.ReleaseServices.Core.Services;
using Newtonsoft.Json;

namespace Code.ReleaseServices.Windows
{
    public class JiraService : IJiraService
    {
        protected WebClient JiraClient { get; set; }

        protected void EnsureWebClient()
        {
            if (JiraClient == null)
                JiraClient = new WebClient();
            JiraClient.Headers.Add("Authorization", "Basic " + JiraConfiguration.JiraToken);
        }

        public JiraService(IJiraConfiguration jiraConfiguration)
        {
            if (jiraConfiguration == null)
                throw new ArgumentNullException("jiraConfiguration");
            JiraConfiguration = jiraConfiguration;
        }

        public JiraIssue GetIssue(string issueKey)
        {
            if (string.IsNullOrEmpty(issueKey))
                throw new ArgumentException("issueKey is null or empty");

            EnsureWebClient();

            var uri = new Uri(new Uri(JiraConfiguration.JiraHost), string.Format("/rest/api/latest/issue/{0}", issueKey));
            var response = JiraClient.DownloadString(uri);
            var jiraIssue = JsonConvert.DeserializeObject<JiraIssue>(response);
            return jiraIssue;
        }

        public IJiraConfiguration JiraConfiguration { get; private set; }

        public void Dispose()
        {
            if (JiraClient != null)
            {
                JiraClient.Dispose();
                JiraClient = null;
            }
        }
    }
}