using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	public interface IRemoteInfoResponse : IResponse
	{
		IReadOnlyDictionary<string, RemoteInfo> Remotes { get; }
	}

	[DataContract]
	[JsonFormatter(typeof(DictionaryResponseFormatter<RemoteInfoResponse, string, RemoteInfo>))]
	public class RemoteInfoResponse : DictionaryResponseBase<string, RemoteInfo>, IRemoteInfoResponse
	{
		[IgnoreDataMember]
		public IReadOnlyDictionary<string, RemoteInfo> Remotes => Self.BackingDictionary;
	}

	public class RemoteInfo
	{
		[DataMember(Name = "connected")]
		public bool Connected { get; internal set; }

		[DataMember(Name = "skip_unavailable")]
		public bool SkipUnavailable { get; internal set; }

		[DataMember(Name = "initial_connect_timeout")]
		public Time InitialConnectTimeout { get; internal set; }

		[DataMember(Name = "max_connections_per_cluster")]
		public int MaxConnectionsPerCluster { get; internal set; }

		[DataMember(Name = "num_nodes_connected")]
		public long NumNodesConnected { get; internal set; }

		[DataMember(Name = "seeds")]
		public IReadOnlyCollection<string> Seeds { get; internal set; } = EmptyReadOnly<string>.Collection;
	}
}
