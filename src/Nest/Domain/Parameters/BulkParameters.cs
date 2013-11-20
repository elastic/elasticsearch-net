using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public class SimpleBulkParameters : ISimpleUrlParameters
	{
		public Replication Replication { get; set; }
		public bool Refresh { get; set; }
	}
	public class BulkParameters<T> where T : class
	{
		public VersionType VersionType { get; set; }
		public string Version { get; set; }
		public string Id { get; set; }
		public string Routing { get; set; }
		public string Parent { get; set; }

		public T Document { get; private set; }
		public string Ttl { get; set; }
		public long Timestamp { get; set; }
		public string Percolate { get; set; }

		public BulkParameters(T document)
		{
			this.Document = document;
		}
	}
}
