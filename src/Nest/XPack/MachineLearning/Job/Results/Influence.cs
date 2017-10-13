using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class Influence
	{
		/// <summary>
		/// The field name of the influencer.
		/// </summary>
		[JsonProperty("influencer_field_name")]
		public string InfluencerFieldName { get; internal set; }

		/// <summary>
		/// The entities that influenced, contributed to, or was to blame for the influence.
		/// </summary>
		[JsonProperty("influencer_field_values")]
		public IReadOnlyCollection<string> InfluencerFieldValues { get; internal set; } = EmptyReadOnly<string>.Collection;
	}
}
