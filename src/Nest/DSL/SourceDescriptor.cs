using Elasticsearch.Net;

namespace Nest
{
	[DescriptorFor("GetSource")]
	public partial class SourceDescriptor<T> : DocumentPathDescriptorBase<SourceDescriptor<T>,T, SourceRequestParameters>
		where T : class
	{

		public SourceDescriptor<T> ExecuteOnPrimary()
		{
			return this.Preference("_primary");
		}

		public SourceDescriptor<T> ExecuteOnLocalShard()
		{
			return this.Preference("_local");
		}


		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<SourceRequestParameters> pathInfo)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.GET;
		}
	}
}
