// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

 using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;

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
	[ReadAs(typeof(FieldCollapse))]
	public interface IFieldCollapse
	{
		/// <summary>
		/// The field used for collapsing must be a single valued keyword or number field with doc-values activated
		/// </summary>
		[DataMember(Name ="field")]
		Field Field { get; set; }

		/// <summary>
		/// It is also possible to expand each collapsed top hits with the `inner_hits` option.
		/// </summary>
		[DataMember(Name ="inner_hits")]
		IInnerHits InnerHits { get; set; }

		/// <summary>
		/// The expansion of the group is done by sending an additional query for each inner_hit request for each collapsed hit returned
		/// in the response. This can significantly slow things down if you have too many groups and/or inner_hit requests.
		/// The max_concurrent_group_searches request parameter can be used to control the maximum number of
		/// concurrent searches allowed in this phase. The default is based on the number of data nodes and the
		/// default search thread pool size.
		/// </summary>
		[DataMember(Name ="max_concurrent_group_searches")]
		int? MaxConcurrentGroupSearches { get; set; }
	}

	/// <inheritdoc cref="IFieldCollapse" />
	public class FieldCollapse : IFieldCollapse
	{
		/// <inheritdoc cref="IFieldCollapse.Field" />
		public Field Field { get; set; }

		/// <inheritdoc cref="IFieldCollapse.InnerHits" />
		public IInnerHits InnerHits { get; set; }

		/// <inheritdoc cref="IFieldCollapse.MaxConcurrentGroupSearches" />
		public int? MaxConcurrentGroupSearches { get; set; }
	}

	/// <inheritdoc cref="IFieldCollapse" />
	public class FieldCollapseDescriptor<T> : DescriptorBase<FieldCollapseDescriptor<T>, IFieldCollapse>, IFieldCollapse
		where T : class
	{
		Field IFieldCollapse.Field { get; set; }
		IInnerHits IFieldCollapse.InnerHits { get; set; }
		int? IFieldCollapse.MaxConcurrentGroupSearches { get; set; }

		/// <inheritdoc cref="IFieldCollapse.MaxConcurrentGroupSearches" />
		public FieldCollapseDescriptor<T> MaxConcurrentGroupSearches(int? maxConcurrentGroupSearches) =>
			Assign(maxConcurrentGroupSearches, (a, v) => a.MaxConcurrentGroupSearches = v);

		/// <inheritdoc cref="IFieldCollapse.Field" />
		public FieldCollapseDescriptor<T> Field(Field field) => Assign(field, (a, v) => a.Field = v);

		/// <inheritdoc cref="IFieldCollapse.Field" />
		public FieldCollapseDescriptor<T> Field<TValue>(Expression<Func<T, TValue>> objectPath) => Assign(objectPath, (a, v) => a.Field = v);

		/// <inheritdoc cref="IFieldCollapse.InnerHits" />
		public FieldCollapseDescriptor<T> InnerHits(Func<InnerHitsDescriptor<T>, IInnerHits> selector = null) =>
			Assign(selector.InvokeOrDefault(new InnerHitsDescriptor<T>()), (a, v) => a.InnerHits = v);
	}
}
