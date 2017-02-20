using System;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// Allows to collapse search results based on field values.
	/// The collapsing is done by selecting only the top sorted document per collapse key.
	/// For instance the query below retrieves the best tweet for each user and sorts them by number of likes.
	/// <para>
	/// NOTE: The collapsing is applied to the top hits only and does not affect aggregations.
	/// </para>
	/// </summary>
	[JsonConverter(typeof(ReadAsTypeJsonConverter<FieldCollapse>))]
	public interface IFieldCollapse
	{
		/// <summary>
		/// The field used for collapsing must be a single valued keyword or number field with doc-values activated
		/// </summary>
		[JsonProperty("field")]
		Field Field { get; set; }

		/// <summary>
		/// It is also possible to expand each collapsed top hits with the `inner_hits` option.
		/// </summary>
		[JsonProperty("inner_hits")]
		IInnerHits InnerHits { get; set; }

		[JsonProperty("max_concurrent_group_searches")]
		int? MaxConcurrentGroupSearches { get; set; }
	}

	/// <inheritdoc/>
	public class FieldCollapse : IFieldCollapse
	{
		/// <inheritdoc/>
		public Field Field { get; set; }

		public IInnerHits InnerHits { get; set; }

		public int? MaxConcurrentGroupSearches { get; set; }
	}

	/// <inheritdoc/>
	public class FieldCollapseDescriptor<T> : DescriptorBase<FieldCollapseDescriptor<T>, IFieldCollapse>, IFieldCollapse
		where T : class
	{
		/// <inheritdoc/>
		Field IFieldCollapse.Field { get; set; }
		IInnerHits IFieldCollapse.InnerHits { get; set; }
		int? IFieldCollapse.MaxConcurrentGroupSearches { get; set; }

		public FieldCollapseDescriptor<T> MaxConcurrentGroupSearches(int? maxConcurrentGroupSearches) =>
			Assign(a => a.MaxConcurrentGroupSearches = maxConcurrentGroupSearches);

		public FieldCollapseDescriptor<T> Field(Field field) => Assign(a => a.Field = field);

		public FieldCollapseDescriptor<T> Field(Expression<Func<T, object>> objectPath) => Assign(a => a.Field = objectPath);

		public FieldCollapseDescriptor<T> InnerHits(Func<InnerHitsDescriptor<T>, IInnerHits> selector = null) =>
			Assign(a => a.InnerHits = selector.InvokeOrDefault(new InnerHitsDescriptor<T>()));
	}
}
