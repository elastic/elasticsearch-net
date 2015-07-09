using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IAliasRequest : IRequest<AliasRequestParameters>
	{
		[JsonProperty("actions")]
		IList<IAliasAction> Actions { get; set; }
	}

	internal static class AliasPathInfo
	{
		public static void Update(ElasticsearchPathInfo<AliasRequestParameters> pathInfo, IAliasRequest request)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.POST;
		}
	}
	
	public partial class AliasRequest : BasePathRequest<AliasRequestParameters>, IAliasRequest
	{
		public IList<IAliasAction> Actions { get; set; }
		
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<AliasRequestParameters> pathInfo)
		{
			AliasPathInfo.Update(pathInfo, this);
		}
	}


	[DescriptorFor("IndicesUpdateAliases")]
	public partial class AliasDescriptor : BasePathDescriptor<AliasDescriptor, AliasRequestParameters>, IAliasRequest
	{
		private IAliasRequest Self => this;

		public AliasDescriptor()
		{
			Self.Actions = new List<IAliasAction>();
		}

		IList<IAliasAction> IAliasRequest.Actions { get; set; }

		public AliasDescriptor Add(Func<AliasAddDescriptor, AliasAddDescriptor> addSelector)
		{
			addSelector.ThrowIfNull("addSelector");
			var descriptor = addSelector(new AliasAddDescriptor());
			descriptor.ThrowIfNull("addAliasDescriptor");
			Self.Actions.Add(descriptor);
			return this;
		}
		public AliasDescriptor Remove(Func<AliasRemoveDescriptor, AliasRemoveDescriptor> removeSelector)
		{
			removeSelector.ThrowIfNull("removeSelector");
			var descriptor = removeSelector(new AliasRemoveDescriptor());
			descriptor.ThrowIfNull("removeAliasDescriptor");
			Self.Actions.Add(descriptor);
			return this;
		}


		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<AliasRequestParameters> pathInfo)
		{
			AliasPathInfo.Update(pathInfo, this);
		}

	}
}
