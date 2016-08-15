using System;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IGetAliasRequest 
	{
		[Obsolete("Deprecated. Use Name instead")]
		[JsonIgnore]
		string Alias { get; set; }
	}

	public partial class GetAliasRequest 
	{
		[Obsolete("Deprecated. Use the GetAliasRequest(Names name) constructor instead")]
		public string Alias
		{
			get { return this.RequestState.RouteValues.Get<Names>("name").GetString(null); }
			set { this.RequestState.RouteValues.Optional("name", Names.Parse(value)); }
		}
	}

	[DescriptorFor("IndicesGetAlias")]
	public partial class GetAliasDescriptor 
	{
		[Obsolete("Deprecated. Use Name instead")]
		string IGetAliasRequest.Alias
		{
			get { return this.RequestState.RouteValues.Get<Names>("name").GetString(null); }
			set { this.RequestState.RouteValues.Optional("name", Names.Parse(value)); }
		}

		[Obsolete("Deprecated. Use " + nameof(Name) + " instead")]
		public GetAliasDescriptor Alias(string alias)=> Assign(a => a.Alias = alias.IsNullOrEmpty() ? "*" : alias);
	}
}
