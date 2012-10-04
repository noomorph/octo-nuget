using System;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using OctoNuget.Core.Configuration;
using OctoNuget.Core.Models;
using OctoNuget.Core.Services;

namespace OctoNuget.Core.Windows
{
    public class JiraService : IJiraService
    {
        public JiraService(IJiraConfiguration jiraConfiguration)
        {
            if (jiraConfiguration == null)
                throw new ArgumentNullException("jiraConfiguration");
            JiraConfiguration = jiraConfiguration;
        }

        protected WebClient JiraClient { get; set; }
        public IJiraConfiguration JiraConfiguration { get; private set; }

        #region IJiraService Members

        public JiraIssue GetIssue(string issueKey)
        {
            if (string.IsNullOrEmpty(issueKey))
                throw new ArgumentException("issueKey is null or empty");

            EnsureWebClient();

            var uri = new Uri(new Uri(JiraConfiguration.JiraHost), string.Format("/rest/api/latest/issue/{0}", issueKey));
            string response = JiraClient.DownloadString(uri);
            var jiraIssue = JsonConvert.DeserializeObject<JiraIssue>(response);
            return jiraIssue;
        }

        public JiraIssue CreateReleasePackage(string deliveryIssueKey, string buildNumber, string summary)
        {
            if (string.IsNullOrEmpty(deliveryIssueKey))
                throw new ArgumentException("deliveryIssueKey is null or empty");
            if (string.IsNullOrEmpty(buildNumber))
                throw new ArgumentException("buildNumber is null or empty");
            if (string.IsNullOrEmpty(summary))
                throw new ArgumentException("summary is null or empty");

            EnsureWebClient();
            JiraIssue parent = GetIssue(deliveryIssueKey);
            var package = new JiraIssue
                              {
                                  Fields = new JiraIssueFields
                                               {
                                                   Parent = new JiraItem {Key = parent.Key},
                                                   Project = new JiraItem {Key = parent.Fields.Project.Key},
                                                   IssueType = new JiraItem {Name = "Release Package"},
                                                   Summary = summary,
                                                   Version = buildNumber
                                               }
                              };

            var uri = new Uri(new Uri(JiraConfiguration.JiraHost), "/rest/api/latest/issue/");
            WebRequest request = WebRequest.Create(uri);
            request.Headers.Add("Authorization", "Basic " + JiraConfiguration.JiraToken);
            request.Method = "POST";
            string postData = JsonConvert.SerializeObject(package, Formatting.Indented,
                                                          new JsonSerializerSettings
                                                              {
                                                                  ContractResolver =
                                                                      new CamelCasePropertyNamesContractResolver(),
                                                                  NullValueHandling = NullValueHandling.Ignore
                                                              });
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            request.ContentType = "application/json";
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            WebResponse response = request.GetResponse();
            string statusDescription = ((HttpWebResponse) response).StatusDescription;
            dataStream = response.GetResponseStream();
            var reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            reader.Close();
            dataStream.Close();
            response.Close();
            return JsonConvert.DeserializeObject<JiraIssue>(responseFromServer);
        }

        public void Dispose()
        {
            if (JiraClient != null)
            {
                JiraClient.Dispose();
                JiraClient = null;
            }
        }

        #endregion

        protected void EnsureWebClient()
        {
            if (JiraClient == null)
                JiraClient = new WebClient();
            JiraClient.Headers.Add("Authorization", "Basic " + JiraConfiguration.JiraToken);
        }
    }
}