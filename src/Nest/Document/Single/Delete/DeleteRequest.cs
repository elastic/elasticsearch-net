using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IDeleteRequest : IRequest<DeleteRequestParameters> { }

	public interface IDeleteRequest<T> : IDeleteRequest 
        where T : class
    {
        T Document { get; set; }
    }

    public partial class DeleteRequest : RequestBase<DeleteRequestParameters>, IDeleteRequest
	{
		public DeleteRequest(IndexName index, TypeName type, string id) 
            : base(p => p.Required(Indices.Single(index)).Required(Types.Single(type)).RequiredId(id))
        { }
	}

	public partial class DeleteRequest<T> : RequestBase<DeleteRequestParameters>, IDeleteRequest<T>
		where T : class
	{
		public DeleteRequest(string id) 
            : base(p => p.Required(Indices.Single<T>()).Required(Types.Single<T>()).RequiredId(id)) { }

		public DeleteRequest(long id) 
            : base(p => p.Required(Indices.Single<T>()).Required(Types.Single<T>()).RequiredId(id.ToString())) { }

		public DeleteRequest(T document)
            : base(p => p.Required(Indices.Single<T>()).Required(Types.Single<T>()))
        {
            this.Document = document;
        }

        public T Document { get; set; }

        protected override void UpdateRequestPath(IConnectionSettingsValues settings, RequestPath path)
        {
            if (this.Document != null)
                path.Id = settings.Inferrer.Id(this.Document);
        }
    }

    [DescriptorFor("Delete")]
    public partial class DeleteDescriptor<T> : RequestDescriptorBase<DeleteDescriptor<T>, DeleteRequestParameters>, IDeleteRequest<T>
        where T : class
    {
        public DeleteDescriptor(string id) 
            : base(p => p.Required(Indices.Single<T>()).Required(Types.Single<T>()).RequiredId(id)) { }

		public DeleteDescriptor(long id) 
            : base(p => p.Required(Indices.Single<T>()).Required(Types.Single<T>()).RequiredId(id.ToString())) { }

		public DeleteDescriptor(T document)
            : base(p => p.Required(Indices.Single<T>()).Required(Types.Single<T>()))
        {
            this.Document = document;
        }

        public T Document { get; set; }

        protected override void UpdateRequestPath(IConnectionSettingsValues settings, RequestPath path)
        {
            if (this.Document != null)
                path.Id = settings.Inferrer.Id(this.Document);
        }
    }
}
