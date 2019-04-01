using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// The response from creating a machine learning filter.
	/// </summary>
	public partial interface IPutFilterResponse : IResponse
	{
		[JsonProperty("filter_id")]
		string FilterId { get; }

		[JsonProperty("description")]
		string Description { get; }

		[JsonProperty("items")]
		IReadOnlyCollection<string> Items { get; }
	}

	/// <inheritdoc cref="IPutFilterResponse" />
	public class PutFilterResponse : ResponseBase, IPutFilterResponse
	{
		public string FilterId { get; internal set; }

		public string Description { get; internal set; }

		public IReadOnlyCollection<string> Items { get; internal set; } = EmptyReadOnly<string>.Collection;
	}
}
