using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IGetRequest : IRequest<GetRequestParameters> { } 
	public interface IGetRequest<T> : IGetRequest where T : class { }

	public partial class GetRequest : RequestBase<GetRequestParameters>, IGetRequest 
	{
		public GetRequest(IndexName index, TypeName type, Id id)
            : base(p => p.Required(Indices.Single(index)).Required(Types.Single(type)).Required(Ids.Single(id))) { }
	}

	public partial class GetRequest<T> 
        : RequestBase<GetRequestParameters>, IGetRequest<T> where T : class
	{
        public GetRequest(Id id) 
            : base(p => p.Required(Indices.Single<T>()).Required(Types.Single<T>()).Required(Ids.Single(id))) { }

		public GetRequest(T document) : base(r => r.Required(Ids.Single(document))) { }
    }
	
	public partial class GetDescriptor<T> 
        : RequestDescriptorBase<GetDescriptor<T>, GetRequestParameters>, IGetRequest
		where T : class
	{

        public GetDescriptor(Id id) 
            : base(p => p.Required(Indices.Single<T>()).Required(Types.Single<T>()).Required(Ids.Single(id))) { }

	    public GetDescriptor(T document) : base(r => r.Required(Ids.Single(document))) { }

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
