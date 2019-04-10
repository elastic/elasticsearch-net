using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IIlmRemovePolicyResponse : IResponse
	{
		[JsonProperty("has_failures")]
		bool HasFailures { get; }

		[JsonProperty("failed_indexes")]
		IReadOnlyCollection<string> FailedIndexes { get; }
	}

	public class IlmRemovePolicyResponse : ResponseBase, IIlmRemovePolicyResponse
	{
		public bool HasFailures { get; internal set; }

		public IReadOnlyCollection<string> FailedIndexes { get; internal set; } = EmptyReadOnly<string>.Collection;
	}
}
