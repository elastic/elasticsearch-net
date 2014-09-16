using Elasticsearch.Net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IPutAliasRequest : IIndexOptionalNamePath<PutAliasRequestParameters>
	{
		[JsonProperty("routing")]
		string Routing { get; set; }

		[JsonProperty("filter")]
		IFilterContainer Filter { get; set; }
	}

	internal static class PutAliasPathInfo
	{
		public static void Update(ElasticsearchPathInfo<PutAliasRequestParameters> pathInfo)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.PUT;
		}
	}

	public partial class PutAliasRequest : IndexOptionalNamePathBase<PutAliasRequestParameters>, IPutAliasRequest
	{
		public PutAliasRequest(string name) : base(name) { }

		public PutAliasRequest(string index, string name) : base(index, name) { }

		public string Routing { get; set; }

		public IFilterContainer Filter { get; set; }

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<PutAliasRequestParameters> pathInfo)
		{
			PutAliasPathInfo.Update(pathInfo);
		}

	}

	[DescriptorFor("IndicesPutAlias")]
	public partial class PutAliasDescriptor 
		: IndexOptionalNamePathDescriptor<PutAliasDescriptor, PutAliasRequestParameters>, IPutAliasRequest
	{
		IPutAliasRequest Self { get { return this; } }
		string IPutAliasRequest.Routing { get; set; }
		IFilterContainer IPutAliasRequest.Filter { get; set; }
		
		public PutAliasDescriptor Routing(string routing)
		{
			Self.Routing = routing;
			return this;
		}

		public PutAliasDescriptor Filter<T>(Func<FilterDescriptor<T>, FilterContainer> filterSelector)
			where T : class
		{
			filterSelector.ThrowIfNull("filterSelector");
			Self.Filter = filterSelector(new FilterDescriptor<T>());
			return this;
		}

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<PutAliasRequestParameters> pathInfo)
		{
			PutAliasPathInfo.Update(pathInfo);
		}

	}
}
