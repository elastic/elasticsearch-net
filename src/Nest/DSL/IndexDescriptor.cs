using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IIndexRequest<T> : IRequest<IndexRequestParameters>
		where T : class
	{
		
	}

	internal static class IndexPathInfo
	{
		public static void Update<T>(ElasticsearchPathInfo<IndexRequestParameters> pathInfo, IIndexRequest<T> request)
			where T : class
		{
			pathInfo.Index.ThrowIfNull("index");
			pathInfo.Type.ThrowIfNull("type");
			var id = pathInfo.Id;
			pathInfo.HttpMethod = id == null || id.IsNullOrEmpty() ? PathInfoHttpMethod.POST : PathInfoHttpMethod.PUT;
		}
	}
	
	public partial class IndexRequest<T> : DocumentPathBase<IndexRequestParameters, T>, IIndexRequest<T>
		where T : class
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<IndexRequestParameters> pathInfo)
		{
			IndexPathInfo.Update(pathInfo, this);
		}
	}
	
	public partial class IndexDescriptor<T> : DocumentOptionalPathDescriptor<IndexDescriptor<T>, IndexRequestParameters, T>, IIndexRequest<T>
		where T : class
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<IndexRequestParameters> pathInfo)
		{
			IndexPathInfo.Update(pathInfo, this);
		}
	}
}
