using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IGetRequest : IDocumentOptionalPath<GetRequestParameters> { } 
	public interface IGetRequest<T> : IGetRequest where T : class { }

	internal static class GetPathInfo
	{
		public static void Update(ElasticsearchPathInfo<GetRequestParameters> pathInfo, IGetRequest request)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.GET;
		}
	}
	
	public partial class GetRequest : DocumentPathBase<GetRequestParameters>, IGetRequest 
	{
		public GetRequest(IndexNameMarker indexName, TypeNameMarker typeName, string id) : base(indexName, typeName, id) { }

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<GetRequestParameters> pathInfo)
		{
			GetPathInfo.Update(pathInfo, this);
		}
	}

	public partial class GetRequest<T> : DocumentPathBase<GetRequestParameters, T>, IGetRequest where T : class
	{
		public GetRequest(string id) : base(id) { }

		public GetRequest(long id) : base(id) { }

		public GetRequest(T document) : base(document) { }

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<GetRequestParameters> pathInfo)
		{
			GetPathInfo.Update(pathInfo, this);
		}
	}
	
	public partial class GetDescriptor<T> : DocumentPathDescriptor<GetDescriptor<T>, GetRequestParameters, T>, IGetRequest
		where T : class
	{

		public GetDescriptor<T> ExecuteOnPrimary()
		{
			return this.Preference("_primary");
		}

		public GetDescriptor<T> ExecuteOnLocalShard()
		{
			return this.Preference("_local");
		}

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<GetRequestParameters> pathInfo)
		{
			GetPathInfo.Update(pathInfo, this);
		}
	}
}
