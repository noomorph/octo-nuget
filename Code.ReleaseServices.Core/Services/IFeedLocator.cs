using System.Collections.Generic;
using Code.ReleaseServices.Core.Models;

namespace Code.ReleaseServices.Core.Services
{
    public interface IFeedLocator
    {
        IEnumerable<Feed> Feeds { get; }
        bool IsPublished(string path);
        FeedConfiguration GetFeedFor(string path);
        Feed GetFeedWithId(string feedId);
    }
}