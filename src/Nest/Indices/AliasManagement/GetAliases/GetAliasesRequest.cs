using Newtonsoft.Json;

namespace Nest
{
	public partial interface IGetAliasesRequest 
	{
		[JsonIgnore]
		string Alias { get; set; }
	}

	//TODO alias is {name} in route parameters

	public partial class GetAliasesRequest 
	{
		public string Alias { get; set; } = "*";
	}

	[DescriptorFor("IndicesGetAliases")]
	public partial class GetAliasesDescriptor 
	{
		string IGetAliasesRequest.Alias { get; set; } = "*";

		public GetAliasesDescriptor Alias(string alias) => Assign(a => a.Alias = alias.IsNullOrEmpty() ? "*" : alias);
	}
}
