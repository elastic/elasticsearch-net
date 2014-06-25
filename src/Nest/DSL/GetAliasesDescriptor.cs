using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Nest.Resolvers;
using Nest.Domain;

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
