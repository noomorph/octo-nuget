using System;
using System.Collections.Generic;
using System.Web;
using Newtonsoft.Json;
using OctoNuget.Core.Models;
using OctoNuget.Core.Services;

namespace OctoNuget.WebForms
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

            switch (action)
            {
                case "reject":
                    this.Reject(context);
                    break;
                case "purge":
                    this.Purge(context);
                    break;
                case "create-package":
                    this.CreatePackage(context);
                    break;
                case "publish":
                    this.Publish(context);
                    break;
            }
        }

        private JiraIssue GetReleasePackage(HttpContext context)
        {
            var issueKey = context.Request["issue"];
            var issue = JiraService.GetIssue(issueKey);
            return issue;
        }

        private JiraIssue GetDelivery(HttpContext context)
        {
            var issueKey = context.Request["delivery"];
            var issue = JiraService.GetIssue(issueKey);
            return issue;
        }

        private void Reject(HttpContext context)
        {
            var issue = GetReleasePackage(context);
            ReleaseService.Reject(issue.Fields.Project.Key, issue.Fields.Version, false);            
        }

        private void Purge(HttpContext context)
        {
            var issue = GetReleasePackage(context);
            ReleaseService.Reject(issue.Fields.Project.Key, issue.Fields.Version, true);
        }

        private void Publish(HttpContext context)
        {
            var issue = GetReleasePackage(context);
            var feedKey = context.Request["to"];
            if (string.IsNullOrWhiteSpace(feedKey))
                throw new ArgumentNullException("to");
            ReleaseService.Publish(issue.Fields.Project.Key, issue.Fields.Version, feedKey);
        }

        private void CreatePackage(HttpContext context)
        {
            var issue = GetDelivery(context);
            var version = context.Request["version"];
            if (string.IsNullOrWhiteSpace(version))
                throw new ArgumentNullException("version");
            var summary = string.Format("{0} release {1}", issue.Fields.Project.Key, version);
            var package = JiraService.CreateReleasePackage(issue.Key, version, summary);
            context.Response.ContentType = "application/json";
            context.Response.Write(JsonConvert.SerializeObject(package));
        }

        public bool IsReusable
        {
            get { return false; }
        }
    }
}