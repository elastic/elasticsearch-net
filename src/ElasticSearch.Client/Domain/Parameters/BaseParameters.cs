using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ElasticSearch.Client
{
	public interface ISimpleUrlParameters
	{
		Replication Replication { get; set; }
		bool Refresh { get; set; }
	}
	public interface IUrlParameters 
	{
		string Version { get; set; }
		string Routing { get; set; }
		string Parent { get; set; }
		Replication Replication { get; set; }
		Consistency Consistency { get; set; }
		bool Refresh { get; set; }
	}
	public class BaseParameters : IUrlParameters
	{
		public string Version { get; set; }
		public string Routing { get; set; }
		public string Parent { get; set;}
		public Replication Replication { get; set; }
		public Consistency Consistency { get; set;}
		public bool Refresh { get; set; }
	}
}
