using System;
using System.Collections.Generic;
using System.Linq;
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
			pathInfo.HttpMethod = HttpMethod.DELETE;
		}
	}
	
	public partial class DeleteIndexRequest : IndicesOptionalExplicitAllPathBase<DeleteIndexRequestParameters>, IDeleteIndexRequest
	{
		public DeleteIndexRequest(Indices indices) : base(indices) { }

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<DeleteIndexRequestParameters> pathInfo) =>
			DeleteIndexPathInfo.Update(pathInfo, this);
	}
	[DescriptorFor("IndicesDelete")]
	public partial class DeleteIndexDescriptor : IndicesOptionalExplicitAllPathDescriptor<DeleteIndexDescriptor, DeleteIndexRequestParameters>, IDeleteIndexRequest
	{
		public DeleteIndexDescriptor(Indices indices) : base(indices) { }

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<DeleteIndexRequestParameters> pathInfo) =>
			DeleteIndexPathInfo.Update(pathInfo, this);

	}
}
