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
		public GetRequest(IndexName index, TypeName type, string id)
            : base(p => p.Required(Indices.Single(index)).Required(Types.Single(type)).RequiredId(id))
        { }
	}

	public partial class GetRequest<T> 
        : RequestBase<GetRequestParameters>, IGetRequest<T> where T : class
	{
        T Document { get; set; }

        public GetRequest(string id) 
            : base(p => p.Required(Indices.Single<T>()).Required(Types.Single<T>()).RequiredId(id))
        { }

		public GetRequest(long id)
            : base(p => p.Required(Indices.Single<T>()).Required(Types.Single<T>()).RequiredId(id.ToString()))
        { }

		public GetRequest(T document)
        {
            Document = document;
        }

        protected override void UpdateRequestPath(IConnectionSettingsValues settings, RequestPath<GetRequestParameters> path)
        {
            path.IdFrom(settings, this.Document);
        }
    }
	
	public partial class GetDescriptor<T> 
        : RequestDescriptorBase<GetDescriptor<T>, GetRequestParameters>, IGetRequest
		where T : class
	{

        T Document { get; set; }

        public GetDescriptor(string id) 
            : base(p => p.Required(Indices.Single<T>()).Required(Types.Single<T>()).RequiredId(id))
        { }

		public GetDescriptor(long id)
            : base(p => p.Required(Indices.Single<T>()).Required(Types.Single<T>()).RequiredId(id.ToString()))
        { }

		public GetDescriptor(T document)
        {
            Document = document;
        }

        protected override void UpdateRequestPath(IConnectionSettingsValues settings, RequestPath<GetRequestParameters> path)
        {
            path.IdFrom(settings, this.Document);
        }

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
