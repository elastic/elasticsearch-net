// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Runtime.Serialization;
using Elastic.Transport;

namespace Nest
{
	[MapsApi("explain.json")]
	public partial interface IExplainRequest
	{
		[DataMember(Name ="query")]
		QueryContainer Query { get; set; }
	}

	// ReSharper disable once UnusedTypeParameter
	public partial interface IExplainRequest<TDocument> where TDocument : class { }

	public partial class ExplainRequest
	{
		public QueryContainer Query { get; set; }
		public Fields StoredFields { get; set; }
	}

	// ReSharper disable once UnusedTypeParameter
	public partial class ExplainRequest<TDocument> where TDocument : class
	{
		protected override HttpMethod HttpMethod =>
			RequestState.RequestParameters?.ContainsQueryString("source") == true || RequestState.RequestParameters?.ContainsQueryString("q") == true
				? HttpMethod.GET
				: HttpMethod.POST;

	}

	public partial class ExplainDescriptor<TDocument> where TDocument : class
	{
		protected override HttpMethod HttpMethod =>
			RequestState.RequestParameters?.ContainsQueryString("source") == true || RequestState.RequestParameters?.ContainsQueryString("q") == true
				? HttpMethod.GET
				: HttpMethod.POST;

		Fields IExplainRequest.StoredFields { get; set; }
		QueryContainer IExplainRequest.Query { get; set; }

		public ExplainDescriptor<TDocument> Query(Func<QueryContainerDescriptor<TDocument>, QueryContainer> querySelector) =>
			Assign(querySelector, (a, v) => a.Query = v?.Invoke(new QueryContainerDescriptor<TDocument>()));

		//TODO write a code standards tests for Field/Fields descriptors (if not already exists)
		/// <summary>
		/// Allows to selectively load specific fields for each document
		/// represented by a search hit. Defaults to load the internal _source field.
		/// </summary>
		public ExplainDescriptor<TDocument> StoredFields(Func<FieldsDescriptor<TDocument>, IPromise<Fields>> fields) =>
			Assign(fields, (a, v) => a.StoredFields = v?.Invoke(new FieldsDescriptor<TDocument>())?.Value);

		public ExplainDescriptor<TDocument> StoredFields(Fields fields) => Assign(fields, (a, v) => a.StoredFields = v);
	}
}
