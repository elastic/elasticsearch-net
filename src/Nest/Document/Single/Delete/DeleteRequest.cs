using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IDeleteRequest : IDocumentOptionalPath<DeleteRequestParameters> { }

	public interface IDeleteRequest<T> : IDeleteRequest where T : class {}

	internal static class DeletePathInfo
	{
		public static void Update(ElasticsearchPathInfo<DeleteRequestParameters> pathInfo, IDeleteRequest request)
		{
			pathInfo.HttpMethod = HttpMethod.DELETE;
		}
	}
	
	public partial class DeleteRequest : DocumentPathBase<DeleteRequestParameters>, IDeleteRequest
	{
		public DeleteRequest(IndexName indexName, TypeName typeName, string id) : base(indexName, typeName, id) { }

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<DeleteRequestParameters> pathInfo)
		{
			DeletePathInfo.Update(pathInfo, this);
		}
	}

	public partial class DeleteRequest<T> : DocumentPathBase<DeleteRequestParameters, T>, IDeleteRequest
		where T : class
	{
		public DeleteRequest(string id) : base(id) { }

		public DeleteRequest(long id) : base(id) { }

		public DeleteRequest(T document) : base(document) { }

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<DeleteRequestParameters> pathInfo)
		{
			DeletePathInfo.Update(pathInfo, this);
		}
	}

	[DescriptorFor("Delete")]
	public partial class DeleteDescriptor<T> : DocumentPathDescriptor<DeleteDescriptor<T>, DeleteRequestParameters, T>, IDeleteRequest
		where T : class
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<DeleteRequestParameters> pathInfo)
		{
			DeletePathInfo.Update(pathInfo, this);
		}
	}
}
