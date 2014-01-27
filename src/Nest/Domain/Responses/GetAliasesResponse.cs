using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IGetAliasesResponse : IResponse
	{
		IDictionary<string, IndexAliases> Indices { get; }
	}

	public class GetAliasesResponse : BaseResponse, IGetAliasesResponse
	{
		public GetAliasesResponse()
		{
			this.IsValid = true;
		}

		public IDictionary<string, IndexAliases> Indices { get; internal set; }
	}
}