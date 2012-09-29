using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Code.ReleaseServices.Core.Helpers;
using Code.ReleaseServices.Core.Models;
using Code.ReleaseServices.Core.Services;

namespace Code.ReleaseServices.Windows
{
    public class ReleaseService : IReleaseService
    {
        private readonly IFeedLocator _feedLocator;

        public ReleaseService(IFeedLocator feedLocator)
        {
            if (feedLocator == null)
                throw new ArgumentNullException("feedLocator");
            _feedLocator = feedLocator;
        }

        #region Implementation of IReleaseService

        public IEnumerable<Release> List()
        {
            List<Feed> feeds = _feedLocator.Feeds.ToList();
            IEnumerable<Package> packages =
                feeds.SelectMany(feed => feed.Packages).OrderByDescending(package => package.IsPublished).Distinct(
                    new PackageComparer());
            List<Release> releases =
                packages.GroupBy(pkg => new Release {Id = pkg.Id, Version = pkg.Version}, new ReleaseComparer()).Select(
                    group => new Release(group)).ToList();
            return releases;
        }

        public void Reject(string id, string version, bool purge = false)
        {
            List<Feed> feeds = _feedLocator.Feeds.ToList();

            IEnumerable<Package> packages =
                feeds.SelectMany(feed => feed.Packages).Where(
                    package =>
                    string.Compare(package.Id, id, StringComparison.OrdinalIgnoreCase) == 0 &&
                    string.CompareOrdinal(package.Version, version) == 0 &&
                    (purge || package.IsPublished)).ToList();

            foreach (var package in packages)
                File.Delete(package.Path);
        }

        public void Rollback(string id, string version, string feedId)
        {
            var feed = _feedLocator.GetFeedWithId(feedId);
            var package =
                feed.Packages.FirstOrDefault(
                    pkg =>
                    string.Compare(pkg.Id, id, StringComparison.OrdinalIgnoreCase) == 0 &&
                    string.CompareOrdinal(pkg.Version, version) == 0 &&
                    pkg.IsPublished);
            if (package != null)
                File.Delete(package.Path);
        }

        public void Publish(string id, string version, string feedId)
        {
            var feed = _feedLocator.GetFeedWithId(feedId);
            var package =
                feed.Packages.FirstOrDefault(
                    pkg =>
                    string.Compare(pkg.Id, id, StringComparison.OrdinalIgnoreCase) == 0 &&
                    string.CompareOrdinal(pkg.Version, version) == 0 &&
                    !pkg.IsPublished);
            if (package == null)
                throw new InvalidOperationException(string.Format("Cannot find package {0}.{1}.nupkg in private feeds",
                                                                  id, version));
            var feedConfiguration = package.Feed;
            File.Copy(package.Path, package.Path.Replace(feedConfiguration.PrivatePath, feedConfiguration.PublicPath),
                      true);
        }

        #endregion
    }
}