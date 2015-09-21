using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IValidateQueryRequest 
	{
		[JsonProperty("query")]
		IQueryContainer Query { get; set; }
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IValidateQueryRequest<T> : IValidateQueryRequest
		where T : class
	{ }

	public partial class ValidateQueryRequest 
	{
		public IQueryContainer Query { get; set; }
	}

	public partial class ValidateQueryRequest<T> 
		where T : class
	{
		public IQueryContainer Query { get; set; }
	}

	[DescriptorFor("IndicesValidateQuery")]
	public partial class ValidateQueryDescriptor<T> where T : class
	{
		IQueryContainer IValidateQueryRequest.Query { get; set; }

		public ValidateQueryDescriptor<T> Query(Func<QueryContainerDescriptor<T>, QueryContainer> querySelector) => Assign(a => a.Query = querySelector?.Invoke(new QueryContainerDescriptor<T>()));
	}
}
