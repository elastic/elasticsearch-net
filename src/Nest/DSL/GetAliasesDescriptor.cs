using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Nest.Resolvers;
using Nest.Domain;

namespace Nest
{
	[DescriptorFor("IndicesGetAlias")]
	public partial class GetAliasesDescriptor : 
		IndicesOptionalPathDescriptor<GetAliasesDescriptor, GetAliasesQueryString>
		, IPathInfo<GetAliasesQueryString>
	{
		internal string _Alias { get; set; }

		public GetAliasesDescriptor Alias(string alias)
		{
			this._Alias = alias;
			return this;
		}

		ElasticSearchPathInfo<GetAliasesQueryString> IPathInfo<GetAliasesQueryString>.ToPathInfo(IConnectionSettings settings)
		{
			var pathInfo = base.ToPathInfo<GetAliasesQueryString>(settings, this._QueryString);
			pathInfo.HttpMethod = PathInfoHttpMethod.GET;
			pathInfo.Name = _Alias ?? "*";
			return pathInfo;
		}
	}
}
