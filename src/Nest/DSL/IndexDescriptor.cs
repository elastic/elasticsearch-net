using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IIndexRequest<TDocument> : IDocumentOptionalPath<IndexRequestParameters, TDocument> 
		where TDocument : class
	{
		TDocument Document { get; set;  }
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
	
	public partial class IndexRequest<TDocument> : DocumentPathBase<IndexRequestParameters, TDocument>, IIndexRequest<TDocument>
		where TDocument : class
	{
		public TDocument Document { get; set; }

		public IndexRequest(TDocument document) : base(document)
		{
			this.Document = document;
		}

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<IndexRequestParameters> pathInfo)
		{
			IndexPathInfo.Update(pathInfo, this);
		}

	}
	
	public partial class IndexDescriptor<T> : DocumentOptionalPathDescriptor<IndexDescriptor<T>, IndexRequestParameters, T>, IIndexRequest<T>
		where T : class
	{
		T IIndexRequest<T>.Document { get; set; }

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<IndexRequestParameters> pathInfo)
		{
			IndexPathInfo.Update(pathInfo, this);
		}

	}
}
