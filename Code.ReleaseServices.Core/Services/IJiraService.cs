using System;
using Code.ReleaseServices.Core.Models;

namespace Code.ReleaseServices.Core.Services
{
    public interface IJiraService : IDisposable
    {
        JiraIssue GetIssue(string issueKey);
    }
}