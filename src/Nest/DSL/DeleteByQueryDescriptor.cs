using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IDeleteByQueryRequest<T> : IRequest<DeleteByQueryRequestParameters>
		where T : class
	{
		[JsonProperty("query")]
		IQueryContainer Query { get; set; }
		
	}

	internal static class DeleteByQueryPathInfo
	{
		public static void Update<T>(ElasticsearchPathInfo<DeleteByQueryRequestParameters> pathInfo, IDeleteByQueryRequest<T> request)
			where T : class
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.DELETE;
		}
	}
	
	public partial class DeleteByQueryRequest<T> : QueryPathBase<DeleteByQueryRequestParameters, T>, IDeleteByQueryRequest<T>
		where T : class
	{
		public IQueryContainer Query { get; set; }

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<DeleteByQueryRequestParameters> pathInfo)
		{
			DeleteByQueryPathInfo.Update(pathInfo, this);
		}

	}

	public partial class DeleteByQueryDescriptor<T> 
		: QueryPathDescriptorBase<DeleteByQueryDescriptor<T>, T, DeleteByQueryRequestParameters>, IDeleteByQueryRequest<T>
		where T : class
	{
		private IDeleteByQueryRequest<T> Self { get { return this; } }

		IQueryContainer IDeleteByQueryRequest<T>.Query { get; set; }

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
