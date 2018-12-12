using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Nest
{
	/// <summary>
	/// The deprecation warning level
	/// </summary>

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
		[DataMember(Name ="details")]
		public string Details { get; internal set; }

		[DataMember(Name ="level")]
		public DeprecationWarningLevel Level { get; internal set; }

		[DataMember(Name ="message")]
		public string Message { get; internal set; }

		[DataMember(Name ="url")]
		public string Url { get; internal set; }
	}

	public interface IDeprecationInfoResponse : IResponse
	{
		[DataMember(Name ="cluster_settings")]
		IReadOnlyCollection<DeprecationInfo> ClusterSettings { get; }

		[DataMember(Name ="index_settings")]
		IReadOnlyDictionary<string, IReadOnlyCollection<DeprecationInfo>> IndexSettings { get; }

		[DataMember(Name ="node_settings")]
		IReadOnlyCollection<DeprecationInfo> NodeSettings { get; }
	}

	public class DeprecationInfoResponse : ResponseBase, IDeprecationInfoResponse
	{
		public IReadOnlyCollection<DeprecationInfo> ClusterSettings { get; internal set; } =
			EmptyReadOnly<DeprecationInfo>.Collection;

		public IReadOnlyDictionary<string, IReadOnlyCollection<DeprecationInfo>> IndexSettings { get; internal set; } =
			EmptyReadOnly<string, IReadOnlyCollection<DeprecationInfo>>.Dictionary;

		public IReadOnlyCollection<DeprecationInfo> NodeSettings { get; internal set; } =
			EmptyReadOnly<DeprecationInfo>.Collection;
	}
}
