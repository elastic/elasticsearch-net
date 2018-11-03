using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IValidateQueryResponse : IResponse
	{
		IReadOnlyCollection<ValidationExplanation> Explanations { get; }
		ShardStatistics Shards { get; }
		bool Valid { get; }
	}

	[JsonObject]
	public class ValidateQueryResponse : ResponseBase, IValidateQueryResponse
	{
		/// <summary>
		/// Gets the explanations if Explain() was set.
		/// </summary>
		[JsonProperty("explanations")]
		public IReadOnlyCollection<ValidationExplanation> Explanations { get; internal set; } = EmptyReadOnly<ValidationExplanation>.Collection;

		[JsonProperty("_shards")]
		public ShardStatistics Shards { get; internal set; }

		[JsonProperty("valid")]
		public bool Valid { get; internal set; }
	}
}
