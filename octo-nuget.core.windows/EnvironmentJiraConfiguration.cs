using System;
using OctoNuget.Core.Configuration;

namespace OctoNuget.Core.Windows
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

        public long ReleasePackageTypeId
        {
            get { return long.Parse(AssertAndGetVariable("crs_jira_rpkg_id")); }
        }
    }
}