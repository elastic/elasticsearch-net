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
		where T : class { }

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

		public ValidateQueryDescriptor<T> Query(Func<QueryContainerDescriptor<T>, QueryContainer> querySelector)
			=> Assign(a => a.Query = querySelector?.Invoke(new QueryContainerDescriptor<T>()));

		[Obsolete("Removed in Elasticsearch 6.2. Will be removed in NEST 7.x. Calling this is a no-op.")]
		public ValidateQueryDescriptor<T> OperationThreading(string operationThreading) => this;
	}
}
