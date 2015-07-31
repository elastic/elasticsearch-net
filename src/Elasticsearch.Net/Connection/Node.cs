using System;
using PurifyNet;

namespace Elasticsearch.Net.Connection
{
	public class Node
	{
		public Node(Uri uri)
		{
			//this makes sure that paths stay relative i.e if the root uri is:
			//http://my-saas-provider.com/instance
			if (!uri.OriginalString.EndsWith("/", StringComparison.Ordinal))
				uri = new Uri(uri.OriginalString + "/");
			this.Uri = uri;
			this.IsAlive = true;
		}

		public Uri Uri { get; }
		public bool NeedsPing { get; }
		public bool IsAlive { get; set; }

		public Uri CreatePath(string path) => new Uri(this.Uri, path).Purify();
	}
}
