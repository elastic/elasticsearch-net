using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IGetAliasesRequest : IRequest<GetAliasesRequestParameters>
	{
		[JsonIgnore]
		string Alias { get; set; }
	}

	//TODO alias is {name} in route parameters

	public partial class GetAliasesRequest : RequestBase<GetAliasesRequestParameters>, IGetAliasesRequest
	{
		public string Alias { get; set; } = "*";
	}

	[DescriptorFor("IndicesGetAliases")]
	public partial class GetAliasesDescriptor 
		: RequestDescriptorBase<GetAliasesDescriptor, GetAliasesRequestParameters, IGetAliasesRequest>, IGetAliasesRequest
	{
		string IGetAliasesRequest.Alias { get; set; } = "*";

		public GetAliasesDescriptor Alias(string alias) => Assign(a => a.Alias = alias.IsNullOrEmpty() ? "*" : alias);
	}
}
