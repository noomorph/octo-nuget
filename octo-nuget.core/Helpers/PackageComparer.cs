using System.Collections.Generic;
using OctoNuget.Core.Models;

namespace OctoNuget.Core.Helpers
{
    public class PackageComparer : IEqualityComparer<Package>
    {
        public bool Equals(Package x, Package y)
        {
            if (x == null || y == null)
                return x == y;
            return x.Id == y.Id && x.Version == y.Version && x.Feed.Id == y.Feed.Id;
        }

        public int GetHashCode(Package obj)
        {
            return obj != null ? (obj.Id + obj.Version + obj.Feed.Id).GetHashCode() : 0;
        }
    }
}