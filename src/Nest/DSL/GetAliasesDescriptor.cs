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
	public partial class GetAliasesDescriptor : 
		IndicesOptionalPathDescriptor<GetAliasesDescriptor, GetAliasesRequestParameters>
		, IPathInfo<GetAliasesRequestParameters>
	{
		internal string _Alias { get; set; }

		public GetAliasesDescriptor Alias(string alias)
		{
			this._Alias = alias;
			return this;
		}

		ElasticsearchPathInfo<GetAliasesRequestParameters> IPathInfo<GetAliasesRequestParameters>.ToPathInfo(IConnectionSettingsValues settings)
		{
			var pathInfo = base.ToPathInfo<GetAliasesRequestParameters>(settings, this._QueryString);
			pathInfo.HttpMethod = PathInfoHttpMethod.GET;
			pathInfo.Name = _Alias ?? "*";
			return pathInfo;
		}
	}
}
