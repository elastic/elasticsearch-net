using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IDeleteByQueryRequest : IQueryPath<DeleteByQueryRequestParameters>
	{
		[JsonProperty("query")]
		IQueryContainer Query { get; set; }
	}

	public interface IDeleteByQueryRequest<T> : IDeleteByQueryRequest where T : class {}

	internal static class DeleteByQueryPathInfo
	{
		public static void Update(RequestPath<DeleteByQueryRequestParameters> pathInfo, IDeleteByQueryRequest request)
		{
			pathInfo.HttpMethod = HttpMethod.DELETE;
			//query works a bit different in that if all types and all indices are specified the root 
			//needs to be /_all/_query not just /_query
			if (pathInfo.Index.IsNullOrEmpty() && pathInfo.Type.IsNullOrEmpty()
				&& request.AllIndices.GetValueOrDefault(false)
				&& request.AllTypes.GetValueOrDefault(false))
				pathInfo.Index = "_all";
		}
	}
	

	public partial class DeleteByQueryRequest : QueryPathBase<DeleteByQueryRequestParameters>, IDeleteByQueryRequest
	{
		public DeleteByQueryRequest() {}

		public DeleteByQueryRequest(IndexName index, TypeName type = null) : base(index, type) { }

		public DeleteByQueryRequest(IEnumerable<IndexName> indices, IEnumerable<TypeName> types = null) : base(indices, types) { }

		public IQueryContainer Query { get; set; }


		protected override void UpdatePathInfo(IConnectionSettingsValues settings, RequestPath<DeleteByQueryRequestParameters> pathInfo)
		{
			DeleteByQueryPathInfo.Update(pathInfo, this);
		}

	}

	public partial class DeleteByQueryRequest<T> : QueryPathBase<DeleteByQueryRequestParameters, T>, IDeleteByQueryRequest where T : class
	{
		public DeleteByQueryRequest() {}

		public DeleteByQueryRequest(IndexName index, TypeName type = null) : base(index, type) { }

		public DeleteByQueryRequest(IEnumerable<IndexName> indices, IEnumerable<TypeName> types = null) : base(indices, types) { }

		public IQueryContainer Query { get; set; }

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, RequestPath<DeleteByQueryRequestParameters> pathInfo)
		{
			DeleteByQueryPathInfo.Update(pathInfo, this);
		}

	}

	public partial class DeleteByQueryDescriptor<T> 
		: QueryPathDescriptorBase<DeleteByQueryDescriptor<T>, DeleteByQueryRequestParameters, T>, IDeleteByQueryRequest
		where T : class
	{
		private IDeleteByQueryRequest Self => this;

		IQueryContainer IDeleteByQueryRequest.Query { get; set; }

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

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, RequestPath<DeleteByQueryRequestParameters> pathInfo)
		{
			DeleteByQueryPathInfo.Update(pathInfo, this);
		}
	}
}
