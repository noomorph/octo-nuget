using System.Collections.Generic;
using OctoNuget.Core.Models;

namespace OctoNuget.Core.Configuration
{
    public interface IFeedsConfiguration
    {
        List<FeedConfiguration> Feeds { get; }
    }
}