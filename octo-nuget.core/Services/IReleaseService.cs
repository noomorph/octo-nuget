using System.Collections.Generic;
using OctoNuget.Core.Models;

namespace OctoNuget.Core.Services
{
    public interface IReleaseService
    {
        /// <summary>
        /// Lists all releases in all feeds
        /// </summary>
        IEnumerable<Release> List();

        /// <summary>
        /// Delete release from all public feeds. If purge - delete from everywhere.
        /// </summary>
        /// <param name="id">project id</param>
        /// <param name="version">release version</param>
        /// <param name="purge">if true then delete everything: private and public; false - delete public only</param>
        void Reject(string id, string version, bool purge = false);

        /// <summary>
        /// Delete release from public feed (by feed key)
        /// </summary>
        /// <param name="id">project id</param>
        /// <param name="version">release version</param>
        /// <param name="feedId">feed id</param>
        void Rollback(string id, string version, string feedId);

        /// <summary>
        /// Move release package from private to public folder in specified feed
        /// </summary>
        /// <param name="id">project id</param>
        /// <param name="version">release version</param>
        /// <param name="feedId">feed id</param>
        void Publish(string id, string version, string feedId);
    }
}