using System.Collections.Generic;

namespace Nest
{
	public interface IGetAliasesResponse : IResponse
	{
		IDictionary<IndexName, IList<AliasDefinition>> Indices { get; }
	}

	public class GetAliasesResponse : BaseResponse, IGetAliasesResponse
	{
		public IDictionary<IndexName, IList<AliasDefinition>> Indices { get; internal set; } = new Dictionary<IndexName, IList<AliasDefinition>>();
	}
}