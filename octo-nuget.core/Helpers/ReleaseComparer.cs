using System.Collections.Generic;
using OctoNuget.Core.Models;

namespace OctoNuget.Core.Helpers
{
    public class ReleaseComparer : IEqualityComparer<Release>
    {
        #region Implementation of IEqualityComparer<in Release>

        public bool Equals(Release x, Release y)
        {
            if ((x == null) || (y == null))
                return x == y;
            return x.Equals(y);
        }

        public int GetHashCode(Release obj)
        {
            return obj != null ? (obj.Id + obj.Version).GetHashCode() : 0;
        }

        #endregion
    }
}