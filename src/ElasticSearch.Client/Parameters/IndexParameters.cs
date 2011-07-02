using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ElasticSearch.Client
{
	public class IndexParameters
	{
		public VersionType VersionType { get; set; }
		public string Version { get; set; }
		public string Routing { get; set; }
		public string Parent { get; set; }
		public Replication Replication { get; set; }
		public Consistency Consistency { get; set; }
		public bool Refresh { get; set; }
		/// <summary>
		/// string because you can pass 5m, or 1h to ES
		/// </summary>
		public string Timeout { get; set; }
	}
}
