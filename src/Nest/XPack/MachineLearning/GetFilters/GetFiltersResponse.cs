using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	/// <summary>
	/// A machine learning filter
	/// </summary>
	public class Filter
	{
		/// <summary>
		/// A description of the filter
		/// </summary>
		[DataMember(Name = "description")]
		public string Description { get; set; }

		/// <summary>
		/// The filter ID
		/// </summary>
		[DataMember(Name = "filter_id")]
		public string FilterId { get; set; }

		/// <summary>
		/// The items of the filter
		/// </summary>
		[DataMember(Name = "items")]
		public IReadOnlyCollection<string> Items { get; set; } = EmptyReadOnly<string>.Collection;
	}

	/// <summary>
	/// Retrieves configuration information for machine learning filters.
	/// </summary>
	public class GetFiltersResponse : ResponseBase
	{
		/// <summary>
		/// The count of filters.
		/// </summary>
		[DataMember(Name = "count")]
		public long Count { get; internal set; }

		/// <summary>
		/// An array of filters resources
		/// </summary>
		[DataMember(Name = "filters")]
		public IReadOnlyCollection<Filter> Filters { get; internal set; } = EmptyReadOnly<Filter>.Collection;
	}
}
