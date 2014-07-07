using System;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public partial class DeleteByQueryDescriptor<T> : QueryPathDescriptorBase<DeleteByQueryDescriptor<T>, T, DeleteByQueryRequestParameters>
		where T : class
	{
		[JsonProperty("query")]
		internal IQueryContainer _Query { get; set; }

		public DeleteByQueryDescriptor<T> MatchAll()
		{
			this._Query = new QueryDescriptor<T>().MatchAll();
			return this;
		}

		public DeleteByQueryDescriptor<T> Query(Func<QueryDescriptor<T>, QueryContainer> querySelector)
		{
			this._Query = querySelector(new QueryDescriptor<T>());
			return this;
		}

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<DeleteByQueryRequestParameters> pathInfo)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.DELETE;
		}
	}
}
