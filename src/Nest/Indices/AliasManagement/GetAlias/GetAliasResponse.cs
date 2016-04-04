using System.Collections.Generic;

namespace Nest
{
	public interface IGetAliasResponse : IResponse
	{
		IDictionary<string, IList<AliasDefinition>> Indices { get; }
	}

	public class GetAliasResponse : ResponseBase, IGetAliasResponse
	{
		public IDictionary<string, IList<AliasDefinition>> Indices { get; internal set; } = new Dictionary<string, IList<AliasDefinition>>();
	}
}
