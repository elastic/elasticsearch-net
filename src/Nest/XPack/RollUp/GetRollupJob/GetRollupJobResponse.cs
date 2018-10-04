using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IGetRollupJobResponse : IResponse
	{
		[JsonProperty("jobs")]
		IReadOnlyCollection<RollupJobInformation> Jobs { get; }
	}

	public class GetRollupJobResponse : ResponseBase, IGetRollupJobResponse
	{
		public IReadOnlyCollection<RollupJobInformation> Jobs { get; internal set; } = EmptyReadOnly<RollupJobInformation>.Collection;
	}
}

