using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using OctoNuget.Core.Services;

namespace OctoNuget.Core.Models
{
    [DataContract]
    public class Package
    {
        private static readonly Regex PackageRegex = new Regex(@"(.+)\.(\d+\.\d+\.\d+\.\d+)\.nupkg",
                                                               RegexOptions.IgnoreCase);

        private readonly IFeedLocator _feedLocator;

        public Package(IFeedLocator feedLocator)
        {
            _feedLocator = feedLocator;
        }

        public Package(string path, IFeedLocator feedLocator) : this(feedLocator)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException(path);
            string filename = System.IO.Path.GetFileName(path);
            Debug.Assert(filename != null, "filename != null");

            if (!PackageRegex.IsMatch(filename))
                throw new ArgumentException(string.Format("Filename does not match regex: {0}", filename));
            Match match = PackageRegex.Match(filename);

            Id = match.Groups[1].Value;
            Version = match.Groups[2].Value;
            Path = path;
            Created = File.GetCreationTimeUtc(path);
            Modified = File.GetLastWriteTimeUtc(path);
        }

        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public string Version { get; set; }

        [DataMember]
        public string Path { get; set; }

        [DataMember]
        public DateTime Created { get; set; }

        [DataMember]
        public DateTime Modified { get; set; }

        [DataMember]
        public bool IsPublished
        {
            get { return _feedLocator.IsPublished(Path); }
            protected set { throw new InvalidOperationException(); }
        }

        [DataMember]
        public FeedConfiguration Feed
        {
            get { return _feedLocator.GetFeedFor(Path); }
            protected set { throw new InvalidOperationException(); }
        }
    }
}