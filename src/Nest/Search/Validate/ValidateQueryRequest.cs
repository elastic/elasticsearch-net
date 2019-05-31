using System;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[MapsApi("indices.validate_query.json")]
	public partial interface IValidateQueryRequest
	{
		[DataMember(Name = "query")]
		QueryContainer Query { get; set; }
	}

	[InterfaceDataContract]
	public partial interface IValidateQueryRequest<TDocument> where TDocument : class { }

	public partial class ValidateQueryRequest
	{
		public QueryContainer Query { get; set; }
	}

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
