using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IGetAliasRequest : IRequest<GetAliasRequestParameters>
	{
		[JsonIgnore]
		string Alias { get; set; }
	}
	//TODO alias is {name} in route parameters

	public partial class GetAliasRequest : RequestBase<GetAliasRequestParameters>, IGetAliasRequest
	{
		public string Alias { get; set; }
	}

	[DescriptorFor("IndicesGetAlias")]
	public partial class GetAliasDescriptor 
		: RequestDescriptorBase<GetAliasDescriptor, GetAliasRequestParameters, IGetAliasRequest>, IGetAliasRequest
	{
		string IGetAliasRequest.Alias { get; set; }

		public GetAliasDescriptor Alias(string alias)=> Assign(a => a.Alias = alias.IsNullOrEmpty() ? "*" : alias);

	}
}
