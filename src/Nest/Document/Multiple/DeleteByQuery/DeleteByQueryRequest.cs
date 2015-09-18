using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IDeleteByQueryRequest : IRequest<DeleteByQueryRequestParameters>
	{
		[JsonProperty("query")]
		IQueryContainer Query { get; set; }
	}

	public interface IDeleteByQueryRequest<T> : IDeleteByQueryRequest where T : class {}

	internal static class DeleteByQueryPath
	{
		public static void Update(IRequestPath path, IDeleteByQueryRequest request)
		{
			path.HttpMethod = HttpMethod.DELETE;
		}
	}
	

	public partial class DeleteByQueryRequest : RequestBase<DeleteByQueryRequestParameters>, IDeleteByQueryRequest
	{
		public DeleteByQueryRequest() {}

		public DeleteByQueryRequest(Indices indices, Types types) : base(p => p.Required(indices).Required(types)) { }

		public DeleteByQueryRequest(Indices indices) : base(p => p.Required(indices)) { }

        public DeleteByQueryRequest(Types types) : base(p => p.Required(types)) { }

		public IQueryContainer Query { get; set; }

		protected override void UpdateRequestPath(IConnectionSettingsValues settings, RequestPath path)
		{
			DeleteByQueryPath.Update(path, this);
		}
	}

	public partial class DeleteByQueryRequest<T> : RequestBase<DeleteByQueryRequestParameters>, IDeleteByQueryRequest where T : class
	{
        public DeleteByQueryRequest()
            : base(p => p.Required(Indices.Single<T>()).Required(Types.Single<T>()))
        { }

		public DeleteByQueryRequest(Indices indices, Types types)
            : base(p => p.Required(indices).Required(types))
        { }

		public DeleteByQueryRequest(Indices indices)
            : base(p => p.Required(indices).Required(Types.Single<T>()))
        { }

        public DeleteByQueryRequest(Types types)
            : base(p => p.Required(types).Required(Indices.Single<T>()))
        { }

		public IQueryContainer Query { get; set; }

		protected override void UpdateRequestPath(IConnectionSettingsValues settings, RequestPath path)
		{
			DeleteByQueryPath.Update(path, this);
		}

	}

	public partial class DeleteByQueryDescriptor<T> 
		: RequestDescriptorBase<DeleteByQueryDescriptor<T>, DeleteByQueryRequestParameters>, IDeleteByQueryRequest
		where T : class
	{
		private IDeleteByQueryRequest Self => this;

		IQueryContainer IDeleteByQueryRequest.Query { get; set; }

        public DeleteByQueryDescriptor()
            : base(p => p.Required(Indices.Single<T>()).Required(Types.Single<T>()))
        { }

		public DeleteByQueryDescriptor(Indices indices, Types types)
            : base(p => p.Required(indices).Required(types))
        { }

		public DeleteByQueryDescriptor(Indices indices)
            : base(p => p.Required(indices).Required(Types.Single<T>()))
        { }

        public DeleteByQueryDescriptor(Types types)
            : base(p => p.Required(types).Required(Indices.Single<T>()))
        { }


		public DeleteByQueryDescriptor<T> MatchAll()
		{
			Self.Query = new QueryContainerDescriptor<T>().MatchAll();
			return this;
		}

		public DeleteByQueryDescriptor<T> Query(Func<QueryContainerDescriptor<T>, QueryContainer> querySelector)
		{
			Self.Query = querySelector(new QueryContainerDescriptor<T>());
			return this;
		}

		protected override void UpdateRequestPath(IConnectionSettingsValues settings, RequestPath path)
		{
			DeleteByQueryPath.Update(path, this);
		}
	}
}
