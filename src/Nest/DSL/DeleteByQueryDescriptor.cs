using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

	internal static class DeleteByQueryPathInfo
	{
		public static void Update(ElasticsearchPathInfo<DeleteByQueryRequestParameters> pathInfo, IDeleteByQueryRequest request)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.DELETE;
		}
	}
	

	public partial class DeleteByQueryRequest : QueryPathBase<DeleteByQueryRequestParameters>, IDeleteByQueryRequest
	{
		public IQueryContainer Query { get; set; }

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<DeleteByQueryRequestParameters> pathInfo)
		{
			DeleteByQueryPathInfo.Update(pathInfo, this);
		}

	}

	public partial class DeleteByQueryRequest<T> : QueryPathBase<DeleteByQueryRequestParameters, T>, IDeleteByQueryRequest where T : class
	{
		public IQueryContainer Query { get; set; }

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<DeleteByQueryRequestParameters> pathInfo)
		{
			DeleteByQueryPathInfo.Update(pathInfo, this);
		}

	}

	public partial class DeleteByQueryDescriptor<T> 
		: QueryPathDescriptorBase<DeleteByQueryDescriptor<T>, DeleteByQueryRequestParameters, T>, IDeleteByQueryRequest
		where T : class
	{
		private IDeleteByQueryRequest Self { get { return this; } }

		IQueryContainer IDeleteByQueryRequest.Query { get; set; }

		public DeleteByQueryDescriptor<T> MatchAll()
		{
			Self.Query = new QueryDescriptor<T>().MatchAll();
			return this;
		}

		public DeleteByQueryDescriptor<T> Query(Func<QueryDescriptor<T>, QueryContainer> querySelector)
		{
			Self.Query = querySelector(new QueryDescriptor<T>());
			return this;
		}

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<DeleteByQueryRequestParameters> pathInfo)
		{
			DeleteByQueryPathInfo.Update(pathInfo, this);
		}
	}
}
