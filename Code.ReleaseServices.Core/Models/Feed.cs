using System.Collections.Generic;
using System.IO;
using System.Linq;
using Code.ReleaseServices.Core.Services;

namespace Code.ReleaseServices.Core.Models
{
    public class Feed
    {
        private readonly IFeedLocator _feedLocator;

        public Feed(IFeedLocator feedLocator)
        {
            _feedLocator = feedLocator;
            Packages = new List<Package>();
        }

        public Feed(FeedConfiguration configuration, IFeedLocator feedLocator) : this(feedLocator)
        {
            if (string.IsNullOrEmpty(configuration.PublicPath) || !Directory.Exists(configuration.PublicPath))
                throw new DirectoryNotFoundException(string.Format("Directory not found: {0}", configuration.PublicPath));

            Id = configuration.Id;
            Packages.AddRange(
                Directory.EnumerateFiles(configuration.PrivatePath, "*.nupkg").Select(
                    path => new Package(path, _feedLocator)));
            Packages.AddRange(
                Directory.EnumerateFiles(configuration.PublicPath, "*.nupkg").Select(
                    path => new Package(path, _feedLocator)));
        }

        public string Id { get; set; }
        public int Index { get; set; }
        public List<Package> Packages { get; private set; }
    }
}