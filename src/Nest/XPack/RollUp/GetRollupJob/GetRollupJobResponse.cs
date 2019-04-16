using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	public interface IGetRollupJobResponse : IResponse
	{
		[DataMember(Name ="jobs")]
		IReadOnlyCollection<RollupJobInformation> Jobs { get; }
	}

	public class GetRollupJobResponse : ResponseBase, IGetRollupJobResponse
	{
		public IReadOnlyCollection<RollupJobInformation> Jobs { get; internal set; } = EmptyReadOnly<RollupJobInformation>.Collection;
	}
}
