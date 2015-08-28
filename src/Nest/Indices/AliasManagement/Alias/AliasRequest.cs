using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IBulkAliasRequest : IRequest<AliasRequestParameters>
	{
		[JsonProperty("actions")]
		IList<IAliasAction> Actions { get; set; }
	}

	internal static class BulkAliasPathInfo
	{
		public static void Update(ElasticsearchPathInfo<AliasRequestParameters> pathInfo, IBulkAliasRequest request)
		{
			pathInfo.HttpMethod = HttpMethod.POST;
		}
	}
	
	public partial class BulkAliasRequest : BasePathRequest<AliasRequestParameters>, IBulkAliasRequest
	{
		public IList<IAliasAction> Actions { get; set; }
		
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<AliasRequestParameters> pathInfo)
		{
			BulkAliasPathInfo.Update(pathInfo, this);
		}
	}


	[DescriptorFor("IndicesUpdateAliases")]
	public partial class BulkAliasDescriptor : BasePathDescriptor<BulkAliasDescriptor, AliasRequestParameters>, IBulkAliasRequest
	{
		private IBulkAliasRequest Self => this;

		public BulkAliasDescriptor()
		{
			Self.Actions = new List<IAliasAction>();
		}

		IList<IAliasAction> IBulkAliasRequest.Actions { get; set; }

		public BulkAliasDescriptor Add(Func<AliasAddDescriptor, AliasAddDescriptor> addSelector)
		{
			addSelector.ThrowIfNull("addSelector");
			var descriptor = addSelector(new AliasAddDescriptor());
			descriptor.ThrowIfNull("addAliasDescriptor");
			Self.Actions.Add(descriptor);
			return this;
		}
		public BulkAliasDescriptor Remove(Func<AliasRemoveDescriptor, AliasRemoveDescriptor> removeSelector)
		{
			removeSelector.ThrowIfNull("removeSelector");
			var descriptor = removeSelector(new AliasRemoveDescriptor());
			descriptor.ThrowIfNull("removeAliasDescriptor");
			Self.Actions.Add(descriptor);
			return this;
		}

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<AliasRequestParameters> pathInfo)
		{
			BulkAliasPathInfo.Update(pathInfo, this);
		}

	}
}
