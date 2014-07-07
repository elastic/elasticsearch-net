using Elasticsearch.Net;

namespace Nest
{
	[DescriptorFor("IndicesGetAlias")]
	public partial class GetAliasesDescriptor : IndicesOptionalPathDescriptor<GetAliasesDescriptor, GetAliasesRequestParameters>
	{
		internal string _Alias { get; set; }

		public GetAliasesDescriptor Alias(string alias)
		{
			this._Alias = alias;
			return this;
		}

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<GetAliasesRequestParameters> pathInfo)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.GET;
			pathInfo.Name = _Alias ?? "*";
		}
	}
}
