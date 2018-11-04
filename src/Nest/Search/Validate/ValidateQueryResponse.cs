using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IValidateQueryResponse : IResponse
	{
		IReadOnlyCollection<ValidationExplanation> Explanations { get; }
		ShardsMetaData Shards { get; }
		bool Valid { get; }
	}

	[JsonObject]
	public class ValidateQueryResponse : ResponseBase, IValidateQueryResponse
	{
		/// <summary>
		/// Gets the explanations if Explain() was set.
		/// </summary>
		[JsonProperty(PropertyName = "explanations")]
		public IReadOnlyCollection<ValidationExplanation> Explanations { get; internal set; } = EmptyReadOnly<ValidationExplanation>.Collection;

		[JsonProperty(PropertyName = "_shards")]
		public ShardsMetaData Shards { get; internal set; }

		[JsonProperty(PropertyName = "valid")]
		public bool Valid { get; internal set; }
	}
}
