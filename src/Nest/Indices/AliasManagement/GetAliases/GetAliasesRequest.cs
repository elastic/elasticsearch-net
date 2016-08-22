using System;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IGetAliasesRequest
	{
		[Obsolete("Deprecated. Use Name instead")]
		[JsonIgnore]
		string Alias { get; set; }
	}

	public partial class GetAliasesRequest
	{
		[Obsolete("Deprecated. Use GetAliasesRequest(Names name) constructor instead")]
		public string Alias
		{
			get { return this.RequestState.RouteValues.Get<Names>("name").GetString(null); }
			set { this.RequestState.RouteValues.Optional("name", Names.Parse(value)); }
		}
	}

	[DescriptorFor("IndicesGetAliases")]
	public partial class GetAliasesDescriptor
	{
		[Obsolete("Deprecated. Use Name instead")]
		string IGetAliasesRequest.Alias
		{
			get { return this.RequestState.RouteValues.Get<Names>("name").GetString(null); }
			set { this.RequestState.RouteValues.Optional("name", Names.Parse(value)); }
		}

		[Obsolete("Deprecated. Use " + nameof(Name) + " instead")]
		public GetAliasesDescriptor Alias(string alias) => Assign(a => a.Alias = alias.IsNullOrEmpty() ? "*" : alias);
	}
}
