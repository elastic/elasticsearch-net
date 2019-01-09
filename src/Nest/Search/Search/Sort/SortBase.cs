using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[JsonFormatter(typeof(SortFormatter))]
	public interface ISort
	{
		/// <summary>
		/// Specifies how documents which are missing the sort field should
		/// be treated.
		/// </summary>
		[DataMember(Name ="missing")]
		object Missing { get; set; }

		/// <summary>
		/// Controls what collection value is picked for sorting a document
		/// when the field is a collection
		/// </summary>
		[DataMember(Name ="mode")]
		SortMode? Mode { get; set; }

		/// <summary>
		/// Specifies the path and filter to apply when sorting on a nested field
		/// </summary>
		/// <remarks>
		/// Valid in Elasticsearch 6.1.0+
		/// </remarks>
		[DataMember(Name ="nested")]
		INestedSort Nested { get; set; }

		/// <summary>
		/// Specifies the filter to apply when sorting on a nested field
		/// </summary>
		[DataMember(Name ="nested_filter")]
		[Obsolete("Deprecated in 6.1.0. Use Nested. Will be removed in 7.x")]
		QueryContainer NestedFilter { get; set; }

		/// <summary>
		/// Specifies the path to apply when sorting on a nested field
		/// </summary>
		[DataMember(Name ="nested_path")]
		[Obsolete("Deprecated in 6.1.0. Use Nested. Will be removed in 7.x")]
		Field NestedPath { get; set; }

		/// <summary>
		/// Controls the order of sorting
		/// </summary>
		[DataMember(Name ="order")]
		SortOrder? Order { get; set; }

		/// <summary>
		/// The field on which to sort
		/// </summary>
		[IgnoreDataMember]
		Field SortKey { get; }
	}

	public abstract class SortBase : ISort
	{
		/// <inheritdoc />
		public object Missing { get; set; }

		/// <inheritdoc />
		public SortMode? Mode { get; set; }

		/// <inheritdoc />
		public INestedSort Nested { get; set; }

		/// <inheritdoc />
		[Obsolete("Deprecated in 6.1.0. Use Nested. Will be removed in 7.x")]
		public QueryContainer NestedFilter { get; set; }

		/// <inheritdoc />
		[Obsolete("Deprecated in 6.1.0. Use Nested. Will be removed in 7.x")]
		public Field NestedPath { get; set; }

		/// <inheritdoc />
		public SortOrder? Order { get; set; }

		/// <summary>
		/// The field on which to sort
		/// </summary>
		protected abstract Field SortKey { get; }

		/// <inheritdoc />
		Field ISort.SortKey => SortKey;
	}

	public abstract class SortDescriptorBase<TDescriptor, TInterface, T> : DescriptorBase<TDescriptor, TInterface>, ISort
		where T : class
		where TDescriptor : SortDescriptorBase<TDescriptor, TInterface, T>, TInterface, ISort
		where TInterface : class, ISort
	{
		/// <summary>
		/// The field on which to sort
		/// </summary>
		protected abstract Field SortKey { get; }

		object ISort.Missing { get; set; }
		SortMode? ISort.Mode { get; set; }
		INestedSort ISort.Nested { get; set; }
		QueryContainer ISort.NestedFilter { get; set; }
		Field ISort.NestedPath { get; set; }
		SortOrder? ISort.Order { get; set; }
		Field ISort.SortKey => SortKey;

		/// <summary>
		/// Sorts by ascending sort order
		/// </summary>
		public virtual TDescriptor Ascending() => Assign(a => a.Order = SortOrder.Ascending);

		/// <summary>
		/// Sorts by descending sort order
		/// </summary>
		public virtual TDescriptor Descending() => Assign(a => a.Order = SortOrder.Descending);

		/// <inheritdoc cref="ISort.Order" />
		public virtual TDescriptor Order(SortOrder? order) => Assign(a => a.Order = order);

		/// <inheritdoc cref="ISort.Mode" />
		public virtual TDescriptor Mode(SortMode? mode) => Assign(a => a.Mode = mode);

		/// <inheritdoc cref="ISort.NestedFilter" />
		[Obsolete("Deprecated in 6.1.0. Use Nested. Will be removed in 7.x")]
		public virtual TDescriptor NestedFilter(Func<QueryContainerDescriptor<T>, QueryContainer> filterSelector) =>
			Assign(a => a.NestedFilter = filterSelector?.Invoke(new QueryContainerDescriptor<T>()));

		/// <inheritdoc cref="ISort.NestedPath" />
		[Obsolete("Deprecated in 6.1.0. Use Nested. Will be removed in 7.x")]
		public virtual TDescriptor NestedPath(Field path) => Assign(a => a.NestedPath = path);

		/// <inheritdoc cref="ISort.NestedPath" />
		[Obsolete("Deprecated in 6.1.0. Use Nested. Will be removed in 7.x")]
		public virtual TDescriptor NestedPath(Expression<Func<T, object>> objectPath) => Assign(a => a.NestedPath = objectPath);

		/// <summary>
		/// Specifies that documents which are missing the sort field should be ordered last
		/// </summary>
		public virtual TDescriptor MissingLast() => Assign(a => a.Missing = "_last");

		/// <summary>
		/// Specifies that documents which are missing the sort field should be ordered first
		/// </summary>
		public virtual TDescriptor MissingFirst() => Assign(a => a.Missing = "_first");

		/// <inheritdoc cref="ISort.Missing" />
		public virtual TDescriptor Missing(object value) => Assign(a => a.Missing = value);

		/// <inheritdoc cref="ISort.Nested" />
		public virtual TDescriptor Nested(Func<NestedSortDescriptor<T>, INestedSort> selector) =>
			Assign(a => a.Nested = selector?.Invoke(new NestedSortDescriptor<T>()));
	}
}
