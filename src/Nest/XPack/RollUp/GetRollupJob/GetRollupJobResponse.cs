using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	public class GetRollupJobResponse : ResponseBase
	{
		[DataMember(Name ="jobs")]
		public IReadOnlyCollection<RollupJobInformation> Jobs { get; internal set; } = EmptyReadOnly<RollupJobInformation>.Collection;
	}
}
