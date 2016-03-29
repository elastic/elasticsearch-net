using System.Collections.Generic;

namespace Nest
{
	public interface IGetAliasesResponse : IResponse
	{
		IDictionary<string, IList<AliasDefinition>> Indices { get; }
	}

	public class GetAliasesResponse : ResponseBase, IGetAliasesResponse
	{
		public IDictionary<string, IList<AliasDefinition>> Indices { get; internal set; } = new Dictionary<string, IList<AliasDefinition>>();
	}
}