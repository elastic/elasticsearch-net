using System.Collections.Generic;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IDeleteIndexRequest : IIndicesOptionalExplicitAllPath<DeleteIndexRequestParameters> { }

	internal static class DeleteIndexPathInfo
	{
		public static void Update(ElasticsearchPathInfo<DeleteIndexRequestParameters> pathInfo, IDeleteIndexRequest request)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.DELETE;
		}
	}
	
	public partial class DeleteIndexRequest : IndicesOptionalExplicitAllPathBase<DeleteIndexRequestParameters>, IDeleteIndexRequest
	{

		public DeleteIndexRequest(IEnumerable<IndexNameMarker> indices)
		{
			this.Indices = indices;
			this.AllIndices = false;
		}
		public DeleteIndexRequest(IndexNameMarker index)
		{
			this.Indices = new [] { index };
		}

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<DeleteIndexRequestParameters> pathInfo)
		{
			DeleteIndexPathInfo.Update(pathInfo, this);
		}
	}
	[DescriptorFor("IndicesDelete")]
	public partial class DeleteIndexDescriptor : IndicesOptionalExplicitAllPathDescriptor<DeleteIndexDescriptor, DeleteIndexRequestParameters>, IDeleteIndexRequest
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<DeleteIndexRequestParameters> pathInfo)
		{
			DeleteIndexPathInfo.Update(pathInfo, this);
		}

	}
}
