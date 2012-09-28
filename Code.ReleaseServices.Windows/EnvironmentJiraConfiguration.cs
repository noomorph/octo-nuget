using System;
using Code.ReleaseServices.Core.Configuration;

namespace Code.ReleaseServices.Windows
{
    public class EnvironmentJiraConfiguration : IJiraConfiguration
    {
        public EnvironmentJiraConfiguration(
            EnvironmentVariableTarget environmentVariableTarget = EnvironmentVariableTarget.Machine)
        {
            EnvironmentVariableTarget = environmentVariableTarget;
        }

        public EnvironmentVariableTarget EnvironmentVariableTarget { get; set; }

        private string AssertAndGetVariable(string variableName)
        {
            string value = Environment.GetEnvironmentVariable(variableName, EnvironmentVariableTarget);
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(variableName);
            return value;
        }

        public string JiraHost
        {
            get { return AssertAndGetVariable("crs_jira_host"); }
        }

        public string JiraToken
        {
            get { return AssertAndGetVariable("crs_jira_token"); }
        }
    }
}