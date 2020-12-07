// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	public class ClearApiKeyCacheResponse : NodesResponseBase
	{
		/// <summary>
		/// The cluster name.
		/// </summary>
		[DataMember(Name = "cluster_name")]
		public string ClusterName { get; internal set; }

		/// <summary>
		/// A dictionary of <see cref="CompactNodeInfo"/> container details of the nodes that cleared the cache.
		/// </summary>
		[DataMember(Name = "nodes")]
		[JsonFormatter(typeof(VerbatimInterfaceReadOnlyDictionaryKeysFormatter<string, CompactNodeInfo>))]
		public IReadOnlyDictionary<string, CompactNodeInfo> Nodes { get; internal set; } = EmptyReadOnly<string, CompactNodeInfo>.Dictionary;
	}
}
