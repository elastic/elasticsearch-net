using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// The response from updating a machine learning filter.
	/// </summary>
	public partial interface IUpdateFilterResponse : IResponse
	{
		[JsonProperty("filter_id")]
		string FilterId { get; }

		[JsonProperty("description")]
		string Description { get; }

		[JsonProperty("items")]
		IReadOnlyCollection<string> Items { get; }
	}

	/// <inheritdoc cref="IUpdateFilterResponse" />
	public class UpdateFilterResponse : ResponseBase, IUpdateFilterResponse
	{
		public string FilterId { get; internal set; }

		public string Description { get; internal set; }

		public IReadOnlyCollection<string> Items { get; internal set; } = EmptyReadOnly<string>.Collection;
	}
}
