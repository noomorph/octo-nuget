using System;
using OctoNuget.Core.Models;

namespace OctoNuget.Core.Services
{
    /// <summary>
    /// Service responsible for integration with Atlassian JIRA API
    /// </summary>
    public interface IJiraService : IDisposable
    {
        /// <summary>
        /// Gets ticket by its key
        /// </summary>
        /// <param name="issueKey">PRJ-NNNN (ticket key)</param>
        /// <returns>Issue info</returns>
        JiraIssue GetIssue(string issueKey);
        /// <summary>
        /// Creates release package ticket bind to specified delivery
        /// </summary>
        /// <param name="deliveryIssueKey">delivery key PRJ-NNNN to bind release package</param>
        /// <param name="buildNumber">major.minor.hotfix.build</param>
        /// <param name="summary">Summary (title) for JIRA issue</param>
        /// <returns>Created issue info</returns>
        JiraIssue CreateReleasePackage(string deliveryIssueKey, string buildNumber, string summary);
    }
}