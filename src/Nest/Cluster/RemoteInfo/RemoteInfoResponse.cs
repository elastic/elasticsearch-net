using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IRemoteInfoResponse : IResponse
	{
		IReadOnlyDictionary<string, RemoteInfo> Remotes	{ get; }
	}

	[JsonObject]
	[JsonConverter(typeof(DictionaryResponseJsonConverter<RemoteInfoResponse, string, RemoteInfo>))]
	public class RemoteInfoResponse : DictionaryResponseBase<string, RemoteInfo>, IRemoteInfoResponse
	{
		[JsonIgnore]
		public IReadOnlyDictionary<string, RemoteInfo> Remotes => Self.BackingDictionary;
	}

	public class RemoteInfo
	{
		[JsonProperty("connected")]
		public bool Connected { get; internal set; }
		[JsonProperty("num_nodes_connected")]
		public long NumNodesConnected { get; internal set; }
		[JsonProperty("max_connections_per_cluster")]
		public int MaxConnectionsPerCluster { get; internal set; }
		[JsonProperty("initial_connect_timeout")]
		public Time InitialConnectTimeout { get; internal set; }

		[JsonProperty("seeds")]
		public IReadOnlyCollection<string> Seeds { get; internal set; }= EmptyReadOnly<string>.Collection;

		[JsonProperty("http_addresses")]
		public IReadOnlyCollection<string> HttpAddresses { get; internal set; }= EmptyReadOnly<string>.Collection;
	}
}
