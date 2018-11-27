using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	public interface ICatResponse<out TCatRecord> : IResponse
		where TCatRecord : ICatRecord
	{
		IReadOnlyCollection<TCatRecord> Records { get; }
	}

	[DataContract]
	public class CatResponse<TCatRecord> : ResponseBase, ICatResponse<TCatRecord>
		where TCatRecord : ICatRecord
	{
		public IReadOnlyCollection<TCatRecord> Records { get; internal set; } = EmptyReadOnly<TCatRecord>.Collection;
	}
}
