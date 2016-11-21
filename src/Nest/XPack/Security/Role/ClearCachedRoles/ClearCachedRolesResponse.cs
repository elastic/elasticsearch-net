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
		IReadOnlyDictionary<string, SecurityNode>  Nodes { get; }
	}

	public class ClearCachedRolesResponse : ResponseBase, IClearCachedRolesResponse
	{
		public string ClusterName { get; internal set; }
		public IReadOnlyDictionary<string, SecurityNode> Nodes { get; internal set; } = EmptyReadOnly<string, SecurityNode>.Dictionary;
	}
}
