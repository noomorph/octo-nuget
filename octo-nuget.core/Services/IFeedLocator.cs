using System.Collections.Generic;
using OctoNuget.Core.Models;

namespace OctoNuget.Core.Services
{
    public interface IFeedLocator
    {
        IEnumerable<Feed> Feeds { get; }
        bool IsPublished(string path);
        FeedConfiguration GetFeedFor(string path);
        Feed GetFeedWithId(string feedId);
    }
}