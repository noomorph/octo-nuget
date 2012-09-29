using System;
using System.Collections.Generic;
using System.Linq;

namespace Code.ReleaseServices.Core.Models
{
    public class Release : IComparable, IComparable<Release>, IEquatable<Release>
    {
        public Release()
        {
            this.Packages = new List<Package>();
        }

        public Release(IGrouping<Release, Package> grouping)
        {
            if (grouping == null)
                throw new ArgumentNullException("grouping");
            this.Id = grouping.Key.Id;
            this.Version = grouping.Key.Version;
            this.Packages = new List<Package>(grouping.Select(package => package));
        }

        public string Id { get; set; }
        public string Version { get; set; }
        public List<Package> Packages { get; set; }

        #region Implementation of IEquatable<Release>

        public override bool Equals(object obj)
        {
            return obj != null && Equals(obj as Release);
        }

        public override int GetHashCode()
        {
            return (this.Id + this.Version).GetHashCode();
        }

        public bool Equals(Release other)
        {
            if (other == null)
                return false;
            return string.CompareOrdinal(this.Id, other.Id) == 0 &&
                   string.CompareOrdinal(this.Version, other.Version) == 0;
        }

        #endregion

        #region Implementation of IComparable<in Release>

        public int CompareTo(Release other)
        {
            var compareId = string.CompareOrdinal(this.Id, other.Id);
            if (compareId != 0)
                return compareId;
            var version1 = new Version(this.Version);
            var version2 = new Version(other.Version);
            return version1.CompareTo(version2);
        }

        #endregion

        #region Implementation of IComparable

        public int CompareTo(object obj)
        {
            if (obj == null)
                throw new ArgumentNullException("obj");
            return CompareTo((Release) obj);
        }

        #endregion
    }
}