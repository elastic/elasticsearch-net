using Newtonsoft.Json;

namespace Nest
{
	public partial interface IGetAliasRequest 
	{
		[JsonIgnore]
		string Alias { get; set; }
	}
	//TODO alias is {name} in route parameters

	public partial class GetAliasRequest 
	{
		public string Alias { get; set; }
	}

	[DescriptorFor("IndicesGetAlias")]
	public partial class GetAliasDescriptor 
	{
		string IGetAliasRequest.Alias { get; set; }

		public GetAliasDescriptor Alias(string alias)=> Assign(a => a.Alias = alias.IsNullOrEmpty() ? "*" : alias);

	}
}
