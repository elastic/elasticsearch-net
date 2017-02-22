using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest_5_2_0
{
	public interface ICatResponse<out TCatRecord> : IResponse
		where TCatRecord : ICatRecord
	{
		IReadOnlyCollection<TCatRecord> Records { get; }
	}

	[JsonObject]
	public class CatResponse<TCatRecord> : ResponseBase, ICatResponse<TCatRecord>
		where TCatRecord : ICatRecord
	{
		public IReadOnlyCollection<TCatRecord> Records { get; internal set; } = EmptyReadOnly<TCatRecord>.Collection;
	}
}
