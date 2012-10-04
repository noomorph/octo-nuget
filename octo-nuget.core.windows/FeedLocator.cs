using System;
using System.Collections.Generic;
using System.Linq;
using OctoNuget.Core.Configuration;
using OctoNuget.Core.Models;
using OctoNuget.Core.Services;

namespace OctoNuget.Core.Windows
{
    public class FeedLocator : IFeedLocator
    {
        private readonly IFeedsConfiguration _feedsConfiguration;

        public FeedLocator(IFeedsConfiguration feedsConfiguration)
        {
            _feedsConfiguration = feedsConfiguration;
        }

        public IEnumerable<Feed> Feeds
        {
            get
            {
                int i = 0;
                return _feedsConfiguration.Feeds.Select(x => new Feed(x, this) {Index = i++});
            }
        }

        public bool IsPublished(string path)
        {
            return _feedsConfiguration.Feeds.Select(config => config.PublicPath).Any(path.StartsWith);
        }

        public FeedConfiguration GetFeedFor(string path)
        {
            var privateFeed = _feedsConfiguration.Feeds.FirstOrDefault(config => path.StartsWith(config.PrivatePath));
            if (privateFeed != null)
                return privateFeed;
            var publicFeed = _feedsConfiguration.Feeds.FirstOrDefault(config => path.StartsWith(config.PublicPath));
            if (publicFeed != null)
                return publicFeed;
            throw new InvalidOperationException(string.Format("Cannot find feed for path: {0}", path));
        }

        public Feed GetFeedWithId(string feedId)
        {
            var feedConfiguration = _feedsConfiguration.Feeds.FirstOrDefault(config => config.Id == feedId);
            if (feedConfiguration == null)
                throw new ApplicationException(string.Format("Feed with id={0} not found", feedId));
            return new Feed(feedConfiguration, this);
        }
    }
}