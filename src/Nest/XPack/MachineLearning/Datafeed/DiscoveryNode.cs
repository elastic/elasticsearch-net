using System.Collections.Generic;
using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	[DataContract]
	public class DiscoveryNode
	{
		/// <summary>
		/// The node attributes
		/// </summary>
		[DataMember(Name = "attributes")]
		[JsonFormatter(typeof(VerbatimDictionaryInterfaceKeysFormatter<string, string>))]
		public IReadOnlyDictionary<string, string> Attributes { get; internal set; } = EmptyReadOnly<string, string>.Dictionary;

		/// <summary>
		/// The ephemeral id of the node.
		/// </summary>
		[DataMember(Name = "ephemeral_id")]
		public string EphemeralId { get; internal set; }

		/// <summary>
		/// The unique identifier of the node.
		/// </summary>
		[DataMember(Name = "id")]
		public string Id { get; internal set; }

		/// <summary>
		/// The node name.
		/// </summary>
		[DataMember(Name = "name")]
		public string Name { get; internal set; }

		/// <summary>
		/// The host and port where transport HTTP connections are accepted.
		/// </summary>
		[DataMember(Name = "transport_address")]
		public string TransportAddress { get; internal set; }
	}
}
