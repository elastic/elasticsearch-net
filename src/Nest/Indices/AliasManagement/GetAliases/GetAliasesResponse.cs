using System.Collections.Generic;

namespace Nest
{
	public interface IGetAliasesResponse : IResponse
	{
		IDictionary<string, IList<AliasDefinition>> Indices { get; }
	}

	public class GetAliasesResponse : BaseResponse, IGetAliasesResponse
	{
		public GetAliasesResponse()
		{
			this.IsValid = true;
			this.Indices = new Dictionary<string, IList<AliasDefinition>>();
		}

		public IDictionary<string, IList<AliasDefinition>> Indices { get; internal set; }
	}
}