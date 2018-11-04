using System;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// Allows to add one or more sort on specific fields.
	/// </summary>
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[ContractJsonConverter(typeof(SortJsonConverter))]
	public interface ISort
	{
		/// <summary>
		/// Specifies how docs which are missing the field should be treated
		/// </summary>
		[JsonIgnore]
		[Obsolete("Use MissingValue")]
		string Missing { get; set; }

		/// <summary>
		/// Specifies how docs which are missing the field should be treated
		/// </summary>
		[JsonProperty("missing")]
		object MissingValue { get; set; }

		/// <summary>
		/// Elasticsearch supports sorting by array or multi-valued fields. <see cref="Mode" />
		/// controls what array value is picked for sorting the document it belongs to.
		/// </summary>
		[JsonProperty("mode")]
		SortMode? Mode { get; set; }

		/// <summary>
		/// A filter that the inner objects inside the nested path should match with in order for its field values
		/// to be taken into account by sorting.
		/// Common case is to repeat the query / filter inside the nested filter or query.
		/// </summary>
		[JsonProperty("nested_filter")]
		QueryContainer NestedFilter { get; set; }

		/// <summary>
		/// Defines on which nested object to sort. The actual sort field must be a direct field inside
		/// this nested object. When sorting by nested field, this field is mandatory.
		/// </summary>
		[JsonProperty("nested_path")]
		Field NestedPath { get; set; }

		/// <summary>
		/// The sort order
		/// </summary>
		[JsonProperty("order")]
		SortOrder? Order { get; set; }

		/// <summary>
		/// The field to sort on
		/// </summary>
		Field SortKey { get; }
	}

	/// <inheritdoc />
	public abstract class SortBase : ISort
	{
		/// <inheritdoc />
		[Obsolete("Use MissingValue")]
		public string Missing
		{
			get => MissingValue as string;
			set => MissingValue = value;
		}

		/// <inheritdoc />
		public object MissingValue { get; set; }

		/// <inheritdoc />
		public SortMode? Mode { get; set; }

		/// <inheritdoc />
		public QueryContainer NestedFilter { get; set; }

		/// <inheritdoc />
		public Field NestedPath { get; set; }

		/// <inheritdoc />
		public SortOrder? Order { get; set; }

		/// <summary>
		/// The field to sort on
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
		protected abstract Field SortKey { get; }

		[Obsolete("Use MissingValue")]
		string ISort.Missing
		{
			get => Self.MissingValue as string;
			set => Self.MissingValue = value;
		}

		object ISort.MissingValue { get; set; }

		SortMode? ISort.Mode { get; set; }

		QueryContainer ISort.NestedFilter { get; set; }

		Field ISort.NestedPath { get; set; }

		SortOrder? ISort.Order { get; set; }
		Field ISort.SortKey => SortKey;

		public virtual TDescriptor Ascending() => Assign(a => a.Order = SortOrder.Ascending);

		public virtual TDescriptor Descending() => Assign(a => a.Order = SortOrder.Descending);

		public virtual TDescriptor Order(SortOrder order) => Assign(a => a.Order = order);

		public virtual TDescriptor Mode(SortMode? mode) => Assign(a => a.Mode = mode);

		public virtual TDescriptor NestedFilter(Func<QueryContainerDescriptor<T>, QueryContainer> filterSelector) =>
			Assign(a => a.NestedFilter = filterSelector?.Invoke(new QueryContainerDescriptor<T>()));

		public virtual TDescriptor NestedPath(Field path) => Assign(a => a.NestedPath = path);

		public virtual TDescriptor NestedPath(Expression<Func<T, object>> objectPath) => Assign(a => a.NestedPath = objectPath);

		public virtual TDescriptor MissingLast() => Assign(a => a.MissingValue = "_last");

		public virtual TDescriptor MissingFirst() => Assign(a => a.MissingValue = "_first");

		public virtual TDescriptor MissingValue(string value) => Assign(a => a.MissingValue = value);

		public virtual TDescriptor Missing(object value) => Assign(a => a.MissingValue = value);
	}
}
