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
    }

    public partial class DeleteRequest : RequestBase<DeleteRequestParameters>, IDeleteRequest
	{
		public DeleteRequest(IndexName index, TypeName type, Id id) 
            : base(p => p.Required(Indices.Single(index)).Required(Types.Single(type)).Required(Ids.Single(id))) { }
	}

	public partial class DeleteRequest<T> : RequestBase<DeleteRequestParameters>, IDeleteRequest<T>
		where T : class
	{
		public DeleteRequest(Id id) 
            : base(p => p.Required(Indices.Single<T>()).Required(Types.Single<T>()).Required(Ids.Single(id))) { }

        public DeleteRequest(T document)
            : base(p => p.Required(Indices.Single<T>()).Required(Types.Single<T>()).Required(Ids.Single(document))) { }
    }

    [DescriptorFor("Delete")]
    public partial class DeleteDescriptor<T> : RequestDescriptorBase<DeleteDescriptor<T>, DeleteRequestParameters>, IDeleteRequest<T>
        where T : class
    {
        public DeleteDescriptor(Id id) 
            : base(p => p.Required(Indices.Single<T>()).Required(Types.Single<T>()).Required(Ids.Single(id))) { }

		public DeleteDescriptor(T document)
            : base(p => p.Required(Indices.Single<T>()).Required(Types.Single<T>()).Required(Ids.Single(document))) { }
    }
}
