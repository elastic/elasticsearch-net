// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;


namespace Nest
{

	public class DeprecationInfoResponse : ResponseBase
	{
		[DataMember(Name ="cluster_settings")]
		public IReadOnlyCollection<DeprecationInfo> ClusterSettings { get; internal set; } =
			EmptyReadOnly<DeprecationInfo>.Collection;

		[DataMember(Name ="index_settings")]
		public IReadOnlyDictionary<string, IReadOnlyCollection<DeprecationInfo>> IndexSettings { get; internal set; } =
			EmptyReadOnly<string, IReadOnlyCollection<DeprecationInfo>>.Dictionary;

		[DataMember(Name ="node_settings")]
		public IReadOnlyCollection<DeprecationInfo> NodeSettings { get; internal set; } =
			EmptyReadOnly<DeprecationInfo>.Collection;
	}

	/// <summary> The deprecation warning level</summary>
	[StringEnum]
	public enum DeprecationWarningLevel
	{
		/// <summary> Everything is good </summary>
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

		/// <summary> You cannot upgrade without fixing this problem.</summary>
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

}
