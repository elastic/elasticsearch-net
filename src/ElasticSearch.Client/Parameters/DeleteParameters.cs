using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ElasticSearch.Client
{
	public class DeleteParameters
	{
		public string Version { get; set; }
		public string Routing { get; set; }
		public string Parent { get; set;}
		public Replication Replication { get; set; }
		public Consistency Consistency { get; set;}
		public bool Refresh { get; set; }
	}
}
