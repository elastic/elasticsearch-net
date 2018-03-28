using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	/// <summary>
	/// The deprecation warning level
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum DeprecationWarningLevel
	{
		/// <summary>
		/// Everything is good
		/// </summary>
		[EnumMember(Value = "none")]
		None,
		/// <summary>
		/// An advisory note that something has changed. No action needed.
		/// </summary>
		[EnumMember(Value = "info")]
		Information,
		/// <summary>
		/// You can upgrade directly, but you are using deprecated functionality
		/// which will not be available in the next major version.
		/// </summary>
		[EnumMember(Value = "warning")]
		Warning,
		/// <summary>
		/// You cannot upgrade without fixing this problem.
		/// </summary>
		[EnumMember(Value = "critical")]
		Critical
	}

	/// <summary>
	/// Information about a deprecation
	/// </summary>
	public class DeprecationInfo
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
		IReadOnlyCollection<DeprecationInfo> ClusterSettings { get; }

		[JsonProperty("node_settings")]
		IReadOnlyCollection<DeprecationInfo> NodeSettings { get; }

		[JsonProperty("index_settings")]
		IReadOnlyDictionary<string, IReadOnlyCollection<DeprecationInfo>> IndexSettings { get; }
	}

	public class DeprecationInfoResponse : ResponseBase, IDeprecationInfoResponse
	{
		public IReadOnlyCollection<DeprecationInfo> ClusterSettings { get; internal set; } =
			EmptyReadOnly<DeprecationInfo>.Collection;
		public IReadOnlyCollection<DeprecationInfo> NodeSettings { get; internal set; } =
			EmptyReadOnly<DeprecationInfo>.Collection;
		public IReadOnlyDictionary<string, IReadOnlyCollection<DeprecationInfo>> IndexSettings { get; internal set; } =
			EmptyReadOnly<string, IReadOnlyCollection<DeprecationInfo>>.Dictionary;
	}
}
