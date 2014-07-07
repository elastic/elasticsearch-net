using Elasticsearch.Net;

namespace Nest
{
	public partial class GetDescriptor<T> : DocumentPathDescriptorBase<GetDescriptor<T>,T, GetRequestParameters>
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
			pathInfo.HttpMethod = PathInfoHttpMethod.GET;
		}
	}
}
