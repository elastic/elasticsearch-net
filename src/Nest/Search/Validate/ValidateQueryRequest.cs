using System;
using Newtonsoft.Json;

namespace Nest
{
	[MapsApi("indices.validate_query.json")]
	public partial interface IValidateQueryRequest
	{
		[JsonProperty("query")]
		QueryContainer Query { get; set; }
	}

	public partial interface IValidateQueryRequest<T> where T : class { }

	public partial class ValidateQueryRequest
	{
		public QueryContainer Query { get; set; }
	}

	public partial class ValidateQueryRequest<T> where T : class
	{
	}

	public partial class ValidateQueryDescriptor<T> where T : class
	{
		QueryContainer IValidateQueryRequest.Query { get; set; }

		public ValidateQueryDescriptor<T> Query(Func<QueryContainerDescriptor<T>, QueryContainer> querySelector) =>
			Assign(a => a.Query = querySelector?.Invoke(new QueryContainerDescriptor<T>()));
	}
}
