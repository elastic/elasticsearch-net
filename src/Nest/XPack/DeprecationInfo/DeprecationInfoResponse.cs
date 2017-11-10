using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum DeprecationWarningLevel
	{
		[EnumMember(Value = "none")]
		None,
		[EnumMember(Value = "info")]
		Information,
		[EnumMember(Value = "warning")]
		Warning,
		[EnumMember(Value = "critical")]
		Critical
	}

	public class DeprecationInfoItem
	{
		[JsonProperty("level")]
		public DeprecationWarningLevel Level { get; internal set;  }

		[JsonProperty("message")]
		public string Message { get; internal set; }

		[JsonProperty("url")]
		public string Url { get; internal set; }

		[JsonProperty("details")]
		public string Details { get; internal set; }
	}

	public interface IDeprecationInfoResponse : IResponse
	{
		[JsonProperty("cluster_settings")]
		IReadOnlyCollection<DeprecationInfoItem> ClusterSettings { get; }

		[JsonProperty("node_settings")]
		IReadOnlyCollection<DeprecationInfoItem> NodeSettings { get; }

		[JsonProperty("index_settings")]
		IReadOnlyDictionary<string, IReadOnlyCollection<DeprecationInfoItem>> IndexSettings { get; }
	}

	public class DeprecationInfoResponse : ResponseBase, IDeprecationInfoResponse
	{
		public IReadOnlyCollection<DeprecationInfoItem> ClusterSettings { get; internal set; } = EmptyReadOnly<DeprecationInfoItem>.Collection;
		public IReadOnlyCollection<DeprecationInfoItem> NodeSettings { get; internal set; } = EmptyReadOnly<DeprecationInfoItem>.Collection;
		public IReadOnlyDictionary<string, IReadOnlyCollection<DeprecationInfoItem>> IndexSettings { get; internal set; } = EmptyReadOnly<string, IReadOnlyCollection<DeprecationInfoItem>>.Dictionary;
	}
}
