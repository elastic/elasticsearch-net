using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[DescriptorFor("IndicesUpdateAliases")]
	public partial class AliasDescriptor : BasePathDescriptor<AliasDescriptor, AliasRequestParameters>
	{
		public AliasDescriptor()
		{
			this._Actions = new List<IAliasAction>();
		}

		[JsonProperty("actions")]
		internal IList<IAliasAction> _Actions { get; set; }

		public AliasDescriptor Add(Func<AliasAddDescriptor, AliasAddDescriptor> addSelector)
		{
			addSelector.ThrowIfNull("addSelector");
			var descriptor = addSelector(new AliasAddDescriptor());
			descriptor.ThrowIfNull("addAliasDescriptor");
			this._Actions.Add(descriptor);
			return this;
		}
		public AliasDescriptor Remove(Func<AliasRemoveDescriptor, AliasRemoveDescriptor> removeSelector)
		{
			removeSelector.ThrowIfNull("removeSelector");
			var descriptor = removeSelector(new AliasRemoveDescriptor());
			descriptor.ThrowIfNull("removeAliasDescriptor");
			this._Actions.Add(descriptor);
			return this;
		}


		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<AliasRequestParameters> pathInfo)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.POST;
		}

	}
}
