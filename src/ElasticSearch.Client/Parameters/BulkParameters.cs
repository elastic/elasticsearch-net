using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ElasticSearch.Client
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
		public string Routing { get; set; }
		public string Parent { get; set; }

		public T Document { get; private set; }
		public BulkParameters(T document)
		{
			this.Document = document;
		}
	}
}
