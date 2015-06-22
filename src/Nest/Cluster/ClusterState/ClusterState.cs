using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	//TODO Separate out classes 
	public class NodeState
	{
		[JsonProperty("name")]
		public string Name { get; internal set; }

		[JsonProperty("transport_address")]
		public string TransportAddress { get; internal set; }

		[JsonProperty("attributes")]
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		public Dictionary<string, string> Attributes { get; internal set; }
	}

	public class RoutingTableState
	{
		[JsonProperty("indices")]
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		public Dictionary<string, IndexRoutingTable> Indices { get; internal set; }
	}

	public class IndexRoutingTable
	{
		[JsonProperty("shards")]
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		public Dictionary<string, List<RoutingShard>> Shards { get; internal set; }
	}

	public class RoutingShard
	{
		[JsonProperty("state")]
		public string State { get; internal set; }

		[JsonProperty("primary")]
		public bool Primary { get; internal set; }

		[JsonProperty("node")]
		public string Node { get; internal set; }

		[JsonProperty("relocating_node")]
		public string RelocatingNode { get; internal set; }

		[JsonProperty("shard")]
		public int Shard { get; internal set; }

		[JsonProperty("index")]
		public string Index { get; internal set; }
	}

	public class RoutingNodesState
	{
		[JsonProperty("unassigned")]
		public List<RoutingShard> Unassigned { get; internal set; }

		[JsonProperty("nodes")]
		public Dictionary<string, List<RoutingShard>> Nodes { get; internal set; }
	}
	public class BlockState
	{
		[JsonProperty("read_only")]
		public bool ReadOnly { get; set; }
	}
	public class MetadataState
	{
		//[JsonProperty("templates")]
		//public ?? Templates { get; internal set; }

		[JsonProperty("indices")]
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		public Dictionary<string, MetadataIndexState> Indices { get; internal set; }
	}

	public class MetadataIndexState
	{
		[JsonProperty("state")]
		public string State { get; internal set; }

		[JsonProperty("settings")]
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		public DynamicDictionary Settings { get; internal set; }

		[JsonProperty("mappings")]
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		public Dictionary<string, ObjectMapping> Mappings { get; internal set; }

		[JsonProperty("aliases")]
		public IEnumerable<string> Aliases { get; internal set; }
	}
}
