using System;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IValidateQueryRequest 
	{
		[JsonProperty("query")]
		QueryContainer Query { get; set; }
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IValidateQueryRequest<T> : IValidateQueryRequest
		where T : class
	{ }

	public partial class ValidateQueryRequest 
	{
		public QueryContainer Query { get; set; }
	}

	public partial class ValidateQueryRequest<T> 
		where T : class
	{
		public QueryContainer Query { get; set; }
	}

	[DescriptorFor("IndicesValidateQuery")]
	public partial class ValidateQueryDescriptor<T> where T : class
	{
		QueryContainer IValidateQueryRequest.Query { get; set; }

		public ValidateQueryDescriptor<T> Query(Func<QueryContainerDescriptor<T>, QueryContainer> querySelector) => Assign(a => a.Query = querySelector?.Invoke(new QueryContainerDescriptor<T>()));
	}
}
