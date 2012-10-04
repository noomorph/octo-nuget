using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using OctoNuget.Core.Configuration;
using OctoNuget.Core.Models;

namespace OctoNuget.Core.Windows
{
    public class EnvironmentFeedsConfiguration : IFeedsConfiguration
    {
        private readonly Regex _envRegex = new Regex(@"^CRS_(.+)_(PRIVATE|PUBLIC)$", RegexOptions.IgnoreCase);
        private List<FeedConfiguration> _feeds;

        public EnvironmentFeedsConfiguration(
            EnvironmentVariableTarget environmentVariableTarget = EnvironmentVariableTarget.Machine)
        {
            EnvironmentVariableTarget = environmentVariableTarget;
        }

        public EnvironmentVariableTarget EnvironmentVariableTarget { get; set; }

        public List<FeedConfiguration> Feeds
        {
            get
            {
                if (_feeds == null)
                {
                    var variables = Environment.GetEnvironmentVariables(EnvironmentVariableTarget);
                    var feedConfigurations = new Dictionary<string, FeedConfiguration>();
                    foreach (var keyObj in variables.Keys)
                    {
                        var key = keyObj.ToString();
                        if (_envRegex.IsMatch(key))
                        {
                            var match = _envRegex.Match(key);
                            var feedKey = match.Groups[1].Value.ToLower();
                            var isPublic = match.Groups[2].Value.ToLower() == "public";

                            var configuration = (feedConfigurations.ContainsKey(feedKey))
                                                    ? feedConfigurations[feedKey]
                                                    : new FeedConfiguration {Id = feedKey};
                            if (isPublic)
                                configuration.PublicPath = variables[keyObj].ToString();
                            else
                                configuration.PrivatePath = variables[keyObj].ToString();

                            if (!feedConfigurations.ContainsValue(configuration))
                                feedConfigurations.Add(feedKey, configuration);
                        }
                    }

                    _feeds = feedConfigurations.Values.Select(v => v).ToList();
                }
                return _feeds;
            }
        }
    }
}