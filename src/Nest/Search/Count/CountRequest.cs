// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Runtime.Serialization;
using Elastic.Transport;

namespace Nest
{
	[MapsApi("count.json")]
	[ReadAs(typeof(CountRequest))]
	public partial interface ICountRequest
	{
		[DataMember(Name ="query")]
		QueryContainer Query { get; set; }
	}

	// ReSharper disable once UnusedTypeParameter
	public partial interface ICountRequest<TDocument> where TDocument : class { }

	public partial class CountRequest
	{
		public QueryContainer Query { get; set; }

		protected override HttpMethod HttpMethod =>
			Self.RequestParameters.ContainsQueryString("source") || Self.RequestParameters.ContainsQueryString("q") || Self.Query == null
			|| Self.Query.IsConditionless()
				? HttpMethod.GET
				: HttpMethod.POST;
	}

	// ReSharper disable once UnusedTypeParameter
	public partial class CountRequest<TDocument> where TDocument : class { }

	public partial class CountDescriptor<TDocument> where TDocument : class
	{
		protected override HttpMethod HttpMethod =>
			Self.RequestParameters.ContainsQueryString("source") || Self.RequestParameters.ContainsQueryString("q") || Self.Query == null
			|| Self.Query.IsConditionless()
				? HttpMethod.GET
				: HttpMethod.POST;

		QueryContainer ICountRequest.Query { get; set; }

		public CountDescriptor<TDocument> Query(Func<QueryContainerDescriptor<TDocument>, QueryContainer> querySelector) =>
			Assign(querySelector, (a, v) => a.Query = v?.Invoke(new QueryContainerDescriptor<TDocument>()));
	}
}
