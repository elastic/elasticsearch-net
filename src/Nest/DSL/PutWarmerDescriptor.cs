using System;
using Elasticsearch.Net;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[DescriptorFor("IndicesPutWarmer")]
	[JsonConverter(typeof(CustomJsonConverter))]
	public partial class PutWarmerDescriptor : IndicesOptionalTypesNamePathDecriptor<PutWarmerDescriptor, PutWarmerRequestParameters>
		, ICustomJson
	{
		private ISearchRequest _searchDescriptor { get; set; }

		public PutWarmerDescriptor Search<T>(Func<SearchDescriptor<T>, SearchDescriptor<T>> selector)
			where T : class
		{
			this._searchDescriptor = selector(new SearchDescriptor<T>());
			return this;
		}

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<PutWarmerRequestParameters> pathInfo)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.PUT;
		}
		object ICustomJson.GetCustomJson()
		{
			return this._searchDescriptor;
		}
	}
}
