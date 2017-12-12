using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	public interface IValidateQueryResponse : IResponse
	{
		bool Valid { get; }
		ShardStatistics Shards { get; }
		IReadOnlyCollection<ValidationExplanation> Explanations { get; }
	}

	[JsonObject]
	public class ValidateQueryResponse : ResponseBase, IValidateQueryResponse
	{
		[JsonProperty("valid")]
		public bool Valid { get; internal set; }

		[JsonProperty("_shards")]
		public ShardStatistics Shards { get; internal set; }

		/// <summary>
		/// Gets the explanations if Explain() was set.
		/// </summary>
		[JsonProperty("explanations")]
		public IReadOnlyCollection<ValidationExplanation> Explanations { get; internal set; } = EmptyReadOnly<ValidationExplanation>.Collection;
	}
}
