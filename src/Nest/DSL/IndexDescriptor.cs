using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IIndexRequest : IRequest<IndexRequestParameters> { }
	public interface IIndexRequest<T> : IIndexRequest where T : class { }

	internal static class IndexPathInfo
	{
		public static void Update(ElasticsearchPathInfo<IndexRequestParameters> pathInfo, IIndexRequest request) 
		{
			pathInfo.Index.ThrowIfNull("index");
			pathInfo.Type.ThrowIfNull("type");
			var id = pathInfo.Id;
			pathInfo.HttpMethod = id == null || id.IsNullOrEmpty() ? PathInfoHttpMethod.POST : PathInfoHttpMethod.PUT;
		}
	}
	
	public partial class IndexRequest : DocumentPathBase<IndexRequestParameters>, IIndexRequest
	{
		public IndexRequest(IndexNameMarker indexName, TypeNameMarker typeName, string id) : base(indexName, typeName, id) { }

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<IndexRequestParameters> pathInfo)
		{
			IndexPathInfo.Update(pathInfo, this);
		}
	}	
	public partial class IndexRequest<T> : DocumentPathBase<IndexRequestParameters, T>, IIndexRequest
		where T : class
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<IndexRequestParameters> pathInfo)
		{
			IndexPathInfo.Update(pathInfo, this);
		}
	}
	
	public partial class IndexDescriptor<T> : DocumentOptionalPathDescriptor<IndexDescriptor<T>, IndexRequestParameters, T>, IIndexRequest
		where T : class
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<IndexRequestParameters> pathInfo)
		{
			IndexPathInfo.Update(pathInfo, this);
		}
	}
}
