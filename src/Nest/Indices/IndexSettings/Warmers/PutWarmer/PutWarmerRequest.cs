using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(CustomJsonConverter))]
	public interface IPutWarmerRequest : IIndicesOptionalTypesNamePath<PutWarmerRequestParameters>, ICustomJson
	{
		ISearchRequest SearchDescriptor { get; set; }
	}

	internal static class PutWarmerPathInfo
	{
		public static void Update(ElasticsearchPathInfo<PutWarmerRequestParameters> pathInfo, IPutWarmerRequest request)
		{
			pathInfo.HttpMethod = HttpMethod.PUT;
		}
	}
	
	public partial class PutWarmerRequest : IndicesOptionalTypesNamePathBase<PutWarmerRequestParameters>, IPutWarmerRequest
	{
		public PutWarmerRequest(string name)
		{
			this.Name = name;
		}

		public ISearchRequest SearchDescriptor { get; set; }

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<PutWarmerRequestParameters> pathInfo)
		{
			PutWarmerPathInfo.Update(pathInfo, this);
		}
		
		object ICustomJson.GetCustomJson() { return this.SearchDescriptor; }

	}
	[DescriptorFor("IndicesPutWarmer")]
	public partial class PutWarmerDescriptor : IndicesOptionalTypesNamePathDescriptor<PutWarmerDescriptor, PutWarmerRequestParameters>
		, IPutWarmerRequest
	{
		private IPutWarmerRequest Self => this;

		ISearchRequest IPutWarmerRequest.SearchDescriptor { get; set; }

		public PutWarmerDescriptor Search<T>(Func<SearchDescriptor<T>, SearchDescriptor<T>> selector)
			where T : class
		{
			Self.SearchDescriptor = selector(new SearchDescriptor<T>());
			return this;
		}

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<PutWarmerRequestParameters> pathInfo)
		{
			PutWarmerPathInfo.Update(pathInfo, this);
		}

		object ICustomJson.GetCustomJson() { return Self.SearchDescriptor; }
	}
}
