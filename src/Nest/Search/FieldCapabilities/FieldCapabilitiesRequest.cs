// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Runtime.Serialization;
using Elastic.Transport;

namespace Nest
{
	[MapsApi("field_caps.json")]
	public partial interface IFieldCapabilitiesRequest
	{
		[DataMember(Name = "index_filter")]
		QueryContainer IndexFilter { get; set; }
	}

	public partial class FieldCapabilitiesRequest
	{
		protected override HttpMethod HttpMethod => IndexFilter != null? HttpMethod.POST : HttpMethod.GET;

		public QueryContainer IndexFilter { get; set; }
	}

	public partial class FieldCapabilitiesDescriptor
	{
		QueryContainer IFieldCapabilitiesRequest.IndexFilter { get; set; }

		protected override HttpMethod HttpMethod => Self.IndexFilter != null? HttpMethod.POST : HttpMethod.GET;

		public FieldCapabilitiesDescriptor IndexFilter<T>(Func<QueryContainerDescriptor<T>, QueryContainer> query) where T : class =>
			Assign(query, (a, v) => a.IndexFilter = v?.Invoke(new QueryContainerDescriptor<T>()));
	}
}
