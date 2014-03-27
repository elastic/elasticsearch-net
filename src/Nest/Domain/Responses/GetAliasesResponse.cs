using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IGetAliasesResponse : IResponse
	{
		IDictionary<string, IList<string>> Indices { get; }
	}

	public class GetAliasesResponse : BaseResponse, IGetAliasesResponse
	{
		public GetAliasesResponse()
		{
			this.IsValid = true;
			this.Indices = new Dictionary<string, IList<string>>();
		}

		public IDictionary<string, IList<string>> Indices { get; internal set; }
	}
}