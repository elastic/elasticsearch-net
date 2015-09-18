using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IValidateQueryRequest : IRequest<ValidateQueryRequestParameters>
	{
		[JsonProperty("query")]
		IQueryContainer Query { get; set; }
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IValidateQueryRequest<T> : IValidateQueryRequest
		where T : class
	{ }

	public partial class ValidateQueryRequest : RequestBase<ValidateQueryRequestParameters>, IValidateQueryRequest
	{
        public ValidateQueryRequest() { }
        public ValidateQueryRequest(Indices indices) : base(r => r.Optional(indices)) { }
        public ValidateQueryRequest(Indices indices, Types types) : base(r => r.Optional(indices).Optional(types)) { }

		public IQueryContainer Query { get; set; }
	}

	public partial class ValidateQueryRequest<T> : ValidateQueryRequest, IValidateQueryRequest<T>
		where T : class
	{
        public ValidateQueryRequest() : base() { }
        public ValidateQueryRequest(Indices indices) : base(indices) { }
        public ValidateQueryRequest(Indices indices, Types types) : base(indices, types) { }
	}

	[DescriptorFor("IndicesValidateQuery")]
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public partial class ValidateQueryDescriptor<T> : RequestDescriptorBase<ValidateQueryDescriptor<T>,  ValidateQueryRequestParameters>, IValidateQueryRequest<T>
		where T : class
	{
		private IValidateQueryRequest Self => this;

		IQueryContainer IValidateQueryRequest.Query { get; set; }

		public ValidateQueryDescriptor<T> Query(Func<QueryContainerDescriptor<T>, QueryContainer> querySelector)
		{
			Self.Query = querySelector(new QueryContainerDescriptor<T>());
			return this;
		}
	}
}
