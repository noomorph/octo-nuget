using System;
using System.Web;
using Code.ReleaseServices.Core.Services;

namespace Code.Services
{
    public class Jira : IHttpHandler
    {
        public IJiraService JiraService { get; set; }

        public IReleaseService ReleaseService { get; set; }

        public void ProcessRequest(HttpContext context)
        {
            var action = (context.Request["action"] ?? "").ToLower();
            if (action == "")
                return;

            var issueKey = context.Request["issue"];
            var issue = JiraService.GetIssue(issueKey);
            var project = issue.Fields.Project.Key;
            var version = issue.Fields.Version;

            switch (action)
            {
                case "reject":
                    ReleaseService.Reject(project, version, false);
                    break;
                case "purge":
                    ReleaseService.Reject(project, version, true);
                    break;
                case "publish":
                    var feedKey = context.Request["to"];
                    if (string.IsNullOrWhiteSpace(feedKey))
                        throw new ArgumentNullException("to");
                    ReleaseService.Publish(project, version, feedKey);
                    break;
            }
        }

        public bool IsReusable
        {
            get { return false; }
        }
    }
}