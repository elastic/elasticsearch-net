using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Linq.Expressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{

	[JsonConverter(typeof(StringEnumConverter))]
	public enum SortOrder
	{
		[EnumMember(Value = "asc")]
		Ascending,
		[EnumMember(Value = "desc")]
		Descending
	}

	[JsonConverter(typeof(StringEnumConverter))]
	public enum SortMode
	{
		[EnumMember(Value = "min")]
		Min,
		[EnumMember(Value = "max")]
		Max,
		[EnumMember(Value = "sum")]
		Sum,
		[EnumMember(Value = "avg")]
		Average
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface ISort
	{
		Field SortKey { get; }

		[JsonProperty("missing")]
		string Missing { get; set; }

		[JsonProperty("order")]
		SortOrder? Order { get; set; }

		[JsonProperty("mode")]
		SortMode? Mode { get; set; }

		[JsonProperty("nested_filter")]
		QueryContainer NestedFilter { get; set; }

		[JsonProperty("nested_path")]
		Field NestedPath { get; set; }
	}

	public abstract class SortBase : ISort
	{
		public string Missing { get; set; }
		public SortOrder? Order { get; set; }
		public SortMode? Mode { get; set; }
		public QueryContainer NestedFilter { get; set; }
		public Field NestedPath { get; set; }
		public bool? IgnoreUnmappedFields { get; set; }
		public abstract Field SortKey { get; }
	}

	//TODO rename to SortDescriptorFieldBase in 2.0
	public abstract class SortDescriptorBase<T, TDescriptor> : ISort where T : class where TDescriptor : SortDescriptorBase<T, TDescriptor>
	{
		private ISort Self => this;

		string ISort.Missing { get; set; }

		SortOrder? ISort.Order { get; set; }

		SortMode? ISort.Mode { get; set; }

		QueryContainer ISort.NestedFilter { get; set; }

		Field ISort.NestedPath { get; set; }

		Field ISort.SortKey { get { throw new NotImplementedException(); } }

		public virtual TDescriptor Ascending()
		{
			Self.Order = SortOrder.Ascending;
			return (TDescriptor)this;
		}

		public virtual TDescriptor Descending()
		{
			Self.Order = SortOrder.Descending;
			return (TDescriptor)this;
		}

		public virtual TDescriptor Order(SortOrder order)
		{
			Self.Order = order;
			return (TDescriptor)this;
		}

		public virtual TDescriptor Mode(SortMode mode)
		{
			Self.Mode = mode;
			return (TDescriptor)this;
		}

		public virtual TDescriptor NestedFilter(Func<QueryContainerDescriptor<T>, QueryContainer> filterSelector)
		{
			filterSelector.ThrowIfNull("filterSelector");

			var filter = new QueryContainerDescriptor<T>();
			Self.NestedFilter = filterSelector(filter);
			return (TDescriptor)this;
		}

		public virtual TDescriptor NestedPath(string path)
		{
			Self.NestedPath = path;
			return (TDescriptor)this;
		}

		public TDescriptor NestedPath(Expression<Func<T, object>> objectPath)
		{
			Self.NestedPath = objectPath;
			return (TDescriptor)this;
		}
	}

	public interface IFieldSort : ISort
	{
		Field Field { get; set; }

		[JsonProperty("ignore_unmapped")]
		bool? IgnoreUnmappedFields { get; set; }

		[JsonProperty("unmapped_type")]
		FieldType? UnmappedType { get; set; }
	}

	public class Sort : SortBase, IFieldSort
	{
		public Field Field { get; set; }

		public override Field SortKey
		{
			get { return Field; }
		}
		public FieldType? UnmappedType { get; set; }
	}

	public class SortDescriptor<T> where T : class
	{
		internal IList<KeyValuePair<Field, ISort>> InternalSortState { get; set; }

		public SortDescriptor()
		{
			this.InternalSortState = new List<KeyValuePair<Field, ISort>>();
		}

		/// <summary>
		/// <para>Allows to add one or more sort on specific fields. Each sort can be reversed as well.
		/// The sort is defined on a per field level, with special field name for _score to sort by score.
		/// </para>
		/// <para>
		/// Sort ascending.
		/// </para>
		/// </summary>
		public SortDescriptor<T> SortAscending(Expression<Func<T, object>> objectPath)
		{
			this.InternalSortState.Add(new KeyValuePair<Field, ISort>(objectPath, new Sort() { Order = SortOrder.Ascending}));
			return this;
		}

		/// <summary>
		/// <para>Allows to add one or more sort on specific fields. Each sort can be reversed as well.
		/// The sort is defined on a per field level, with special field name for _score to sort by score.
		/// </para>
		/// <para>
		/// Sort descending.
		/// </para>
		/// </summary>
		public SortDescriptor<T> SortDescending(Expression<Func<T, object>> objectPath)
		{
			this.InternalSortState.Add(new KeyValuePair<Field, ISort>(objectPath, new Sort() { Order = SortOrder.Descending }));
			return this;
		}

		/// <summary>
		/// <para>Allows to add one or more sort on specific fields. Each sort can be reversed as well.
		/// The sort is defined on a per field level, with special field name for _score to sort by score.
		/// </para>
		/// <para>
		/// Sort ascending.
		/// </para>
		/// </summary>
		public SortDescriptor<T> SortAscending(string field)
		{
			this.InternalSortState.Add(new KeyValuePair<Field, ISort>(field, new Sort() { Order = SortOrder.Ascending }));
			return this;
		}

		/// <summary>
		/// <para>Allows to add one or more sort on specific fields. Each sort can be reversed as well.
		/// The sort is defined on a per field level, with special field name for _score to sort by score.
		/// </para>
		/// <para>
		/// Sort descending.
		/// </para>
		/// </summary>
		public SortDescriptor<T> SortDescending(string field)
		{
			this.InternalSortState.Add(new KeyValuePair<Field, ISort>(field, new Sort() { Order = SortOrder.Descending}));
			return this;
		}

		/// <summary>
		/// <para>Sort() allows you to fully describe your sort unlike the SortAscending and SortDescending aliases.
		/// </para>
		/// </summary>
		public SortDescriptor<T> Sort(Func<SortFieldDescriptor<T>, IFieldSort> sortSelector)
		{
			if (sortSelector == null) return this;

			var descriptor = sortSelector(new SortFieldDescriptor<T>());
			if (descriptor == null || descriptor.Field.IsConditionless())
				return this;

			this.InternalSortState.Add(new KeyValuePair<Field, ISort>(descriptor.Field, descriptor));
			return this;
		}

		/// <summary>
		/// <para>SortGeoDistance() allows you to sort by a distance from a geo point.
		/// </para>
		/// </summary>
		public SortDescriptor<T> SortGeoDistance(Func<SortGeoDistanceDescriptor<T>, IGeoDistanceSort> sortSelector)
		{
			if (sortSelector == null) return this;

			var descriptor = sortSelector(new SortGeoDistanceDescriptor<T>());
			if (descriptor == null || descriptor.Field.IsConditionless())
				return this;
			this.InternalSortState.Add(new KeyValuePair<Field, ISort>("_geo_distance", descriptor));
			return this;
		}

		/// <summary>
		/// <para>SortScript() allows you to sort by a distance from a geo point.
		/// </para>
		/// </summary>
		public SortDescriptor<T> SortScript(Func<SortScriptDescriptor<T>, IScriptSort> sortSelector)
		{
			if (sortSelector == null) return this;

			var descriptor = sortSelector(new SortScriptDescriptor<T>());
			if (descriptor == null || (descriptor.Script.IsNullOrEmpty() && descriptor.File.IsNullOrEmpty()))
				return this;
			this.InternalSortState.Add(new KeyValuePair<Field, ISort>("_script", descriptor));
			return this;
		}

	}


	public class SortFieldDescriptor<T> : SortDescriptorBase<T, SortFieldDescriptor<T>>, IFieldSort where T : class
	{
		private IFieldSort Self => this;

		Field IFieldSort.Field { get; set; }

		bool? IFieldSort.IgnoreUnmappedFields { get; set; }
		FieldType? IFieldSort.UnmappedType { get; set; }

		Field ISort.SortKey { get { return Self.Field; } }

		public virtual SortFieldDescriptor<T> Field(string field)
		{
			Self.Field = field;
			return this;
		}

		public virtual SortFieldDescriptor<T> Field(Expression<Func<T, object>> objectPath)
		{
			Self.Field = objectPath;
			return this;
		}

		public virtual SortFieldDescriptor<T> MissingLast()
		{
			Self.Missing = "_last";
			return this;
		}

		public virtual SortFieldDescriptor<T> MissingFirst()
		{
			Self.Missing = "_first";
			return this;
		}

		public virtual SortFieldDescriptor<T> MissingValue(string value)
		{
			Self.Missing = value;
			return this;
		}
		public virtual SortFieldDescriptor<T> UnmappedType(FieldType type)
		{
			Self.UnmappedType = type;
			return this;
		}

		public virtual SortFieldDescriptor<T> NestedMin()
		{
			Self.Mode = SortMode.Min;
			return this;
		}

		public virtual SortFieldDescriptor<T> NestedMax()
		{
			Self.Mode = SortMode.Max;
			return this;
		}

		public virtual SortFieldDescriptor<T> NestedSum()
		{
			Self.Mode = SortMode.Sum;
			return this;
		}

		public virtual SortFieldDescriptor<T> NestedAvg()
		{
			Self.Mode = SortMode.Average;
			return this;
		}
	}
}
