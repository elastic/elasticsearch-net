using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface ICatResponse<out TCatRecord> : IResponse
		where TCatRecord : ICatRecord
	{
		IEnumerable<TCatRecord> Records { get; }
	}

	[JsonObject]
	public class CatResponse<TCatRecord> : ResponseBase, ICatResponse<TCatRecord>
		where TCatRecord : ICatRecord
	{
		public IEnumerable<TCatRecord> Records { get; internal set; }
	}
}
