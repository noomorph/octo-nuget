using System.Collections.Generic;
using Code.ReleaseServices.Core.Models;

namespace Code.ReleaseServices.Core.Configuration
{
    public interface IFeedsConfiguration
    {
        List<FeedConfiguration> Feeds { get; }
    }
}