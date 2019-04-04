using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// Retrieves configuration information for machine learning filters.
	/// </summary>
	public interface IGetFiltersResponse : IResponse
	{
		/// <summary>
		/// The count of filters.
		/// </summary>
		[JsonProperty("count")]
		long Count { get; }

		/// <summary>
		/// An array of filters resources
		/// </summary>
		[JsonProperty("filters")]
		IReadOnlyCollection<Filter> Filters { get; }
	}

	/// <summary>
	/// A machine learning filter
	/// </summary>
	public class Filter
	{
		/// <summary>
		/// The filter ID
		/// </summary>
		[JsonProperty("filter_id")]
		public string FilterId { get; set; }

		/// <summary>
		/// The items of the filter
		/// </summary>
		[JsonProperty("items")]
		public IReadOnlyCollection<string> Items { get; set; } = EmptyReadOnly<string>.Collection;

		/// <summary>
		/// A description of the filter
		/// </summary>
		[JsonProperty("description")]
		public string Description { get; set; }
	}

	public class GetFiltersResponse : ResponseBase, IGetFiltersResponse
	{
		/// <inheritdoc cref="IGetFiltersResponse.Count" />
		public long Count { get; internal set; }

		/// <inheritdoc cref="IGetFiltersResponse.Filters" />
		public IReadOnlyCollection<Filter> Filters { get; internal set; } = EmptyReadOnly<Filter>.Collection;
	}
}
