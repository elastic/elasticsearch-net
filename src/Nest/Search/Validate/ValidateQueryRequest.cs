using System;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[MapsApi("indices.validate_query.json")]
	public partial interface IValidateQueryRequest
	{
		[DataMember(Name = "query")]
		QueryContainer Query { get; set; }
	}

	[InterfaceDataContract]
	// ReSharper disable once UnusedTypeParameter
	public partial interface IValidateQueryRequest<TDocument> where TDocument : class { }

	public partial class ValidateQueryRequest
	{
		public QueryContainer Query { get; set; }
	}

	// ReSharper disable once UnusedTypeParameter
	public partial class ValidateQueryRequest<TDocument> where TDocument : class
	{
	}

	public partial class ValidateQueryDescriptor<TDocument> where TDocument : class
	{
		QueryContainer IValidateQueryRequest.Query { get; set; }

		public ValidateQueryDescriptor<TDocument> Query(Func<QueryContainerDescriptor<TDocument>, QueryContainer> querySelector)
			=> Assign(querySelector, (a, v) => a.Query = v?.Invoke(new QueryContainerDescriptor<TDocument>()));
	}
}
