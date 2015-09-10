using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IGetRequest : IDocumentOptionalPath<GetRequestParameters> { } 
	public interface IGetRequest<T> : IGetRequest where T : class { }

	public partial class GetRequest : DocumentPathBase<GetRequestParameters>, IGetRequest 
	{
		public GetRequest(IndexName indexName, TypeName typeName, string id) : base(indexName, typeName, id) { }
	}

	public partial class GetRequest<T> : DocumentPathBase<GetRequestParameters, T>, IGetRequest where T : class
	{
		public GetRequest(string id) : base(id) { }

		public GetRequest(long id) : base(id) { }

		public GetRequest(T document) : base(document) { }
	}
	
	public partial class GetDescriptor<T> : DocumentPathDescriptor<GetDescriptor<T>, GetRequestParameters, T>, IGetRequest
		where T : class
	{
		public GetDescriptor<T> ExecuteOnPrimary()
		{
			return this.Preference("_primary");
		}

		public GetDescriptor<T> ExecuteOnLocalShard()
		{
			return this.Preference("_local");
		}
	}
}
