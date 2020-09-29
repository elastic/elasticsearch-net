// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;
using Elasticsearch.Net;
using Nest.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(IndicesOptions))]
	public interface IIndicesOptions
	{
		[DataMember(Name = "allow_no_indices")]
		bool? AllowNoIndices { get; set; }

		[DataMember(Name = "expand_wildcards")]
		ExpandWildcards? ExpandWildcards { get; set; }

		[DataMember(Name = "ignore_unavailable")]
		bool? IgnoreUnavailable { get; set; }
	}

	[DataContract]
	public class IndicesOptions : IIndicesOptions
	{
		public bool? AllowNoIndices { get; set; }
		public ExpandWildcards? ExpandWildcards { get; set; }
		public bool? IgnoreUnavailable { get; set; }
	}

	public class IndicesOptionsDescriptor : DescriptorBase<IndicesOptionsDescriptor, IIndicesOptions>, IIndicesOptions
	{
		bool? IIndicesOptions.AllowNoIndices { get; set; }
		ExpandWildcards? IIndicesOptions.ExpandWildcards { get; set; }
		bool? IIndicesOptions.IgnoreUnavailable { get; set; }

		public IndicesOptionsDescriptor ExpandWildcards(ExpandWildcards? expandWildcards) =>
			Assign(expandWildcards, (a, v) => a.ExpandWildcards = v);

		public IndicesOptionsDescriptor IgnoreUnavailable(bool? ignoreUnavailable = true) =>
			Assign(ignoreUnavailable, (a, v) => a.IgnoreUnavailable = v);

		public IndicesOptionsDescriptor AllowNoIndices(bool? allowNoIndices = true) =>
			Assign(allowNoIndices, (a, v) => a.AllowNoIndices = v);
	}
}
