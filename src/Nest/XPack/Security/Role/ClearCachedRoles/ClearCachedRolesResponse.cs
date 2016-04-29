using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Nest
{
	public interface IClearCachedRolesResponse : IResponse
	{
		[JsonProperty("cluster_name")]
		string ClusterName { get; }

		[JsonProperty("nodes")]
		IDictionary<string, ShieldNode>  Nodes { get; }
	}

	public class ClearCachedRolesResponse : ResponseBase, IClearCachedRolesResponse
	{
		public string ClusterName { get; internal set; }
		public IDictionary<string, ShieldNode>  Nodes { get; internal set; }
	}
}
