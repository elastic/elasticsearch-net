using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IRemovePolicyResponse : IResponse
	{
		[JsonProperty("has_failures")]
		bool HasFailures { get; }

		[JsonProperty("failed_indexes")]
		IReadOnlyCollection<string> FailedIndexes { get; }
	}

	public class RemovePolicyResponse : ResponseBase, IRemovePolicyResponse
	{
		public bool HasFailures { get; internal set; }

		public IReadOnlyCollection<string> FailedIndexes { get; internal set; } = EmptyReadOnly<string>.Collection;
	}
}
