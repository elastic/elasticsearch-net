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
		[JsonProperty("missing")]
		string Missing { get; set; }

		[JsonProperty("order")]
		SortOrder? Order { get; set; }

		[JsonProperty("mode")]
		SortMode? Mode { get; set; }
	}

	public interface IFieldSort : ISort
	{
		PropertyPathMarker Field { get; set; }

		[JsonProperty("nested_filter")]
		FilterContainer NestedFilter { get; set; }

		[JsonProperty("nested_path")]
		PropertyPathMarker NestedPath { get; set; }

		[JsonProperty("ignore_unmapped")]
		bool? IgnoreUnmappedFields { get; set; }
	}

	public class Sort : IFieldSort
	{
		public PropertyPathMarker Field { get; set; }
		public string Missing { get; set; }
		public SortOrder? Order { get; set; }
		public SortMode? Mode { get; set; }
		public FilterContainer NestedFilter { get; set; }
		public PropertyPathMarker NestedPath { get; set; }
		public bool? IgnoreUnmappedFields { get; set; }
	}

	public class SortFieldDescriptor<T> : IFieldSort where T : class
	{
		private IFieldSort Self { get { return this; } }

		PropertyPathMarker IFieldSort.Field { get; set; }

		string ISort.Missing { get; set; }

		SortOrder? ISort.Order { get; set; }

        SortMode? ISort.Mode { get; set; }

		FilterContainer IFieldSort.NestedFilter { get; set; }

		PropertyPathMarker IFieldSort.NestedPath { get; set; }

		bool? IFieldSort.IgnoreUnmappedFields { get; set; }

		public virtual SortFieldDescriptor<T> OnField(string field)
		{
			Self.Field = field;
			return this;
		}

		public virtual SortFieldDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
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

		public virtual SortFieldDescriptor<T> IgnoreUnmappedFields(bool ignore = true)
		{
			Self.IgnoreUnmappedFields = ignore;
			return this;
		}

		public virtual SortFieldDescriptor<T> Ascending()
		{
			Self.Order = SortOrder.Ascending;
			return this;
		}

		public virtual SortFieldDescriptor<T> Descending()
		{
			Self.Order = SortOrder.Descending;
			return this;
		}

		public virtual SortFieldDescriptor<T> Order(SortOrder order)
		{
			Self.Order = order;
			return this;
		}

		public virtual SortFieldDescriptor<T> Mode(SortMode mode)
		{
			Self.Mode = mode;
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

		public virtual SortFieldDescriptor<T> NestedFilter(Func<FilterDescriptor<T>, FilterContainer> filterSelector)
		{
			filterSelector.ThrowIfNull("filterSelector");

			var filter = new FilterDescriptor<T>();
			Self.NestedFilter = filterSelector(filter);
			return this;
		}

		public virtual SortFieldDescriptor<T> NestedPath(string path)
		{
			Self.NestedPath = path;
			return this;
		}

		public SortFieldDescriptor<T> NestedPath(Expression<Func<T, object>> objectPath)
		{
			Self.NestedPath = objectPath;
			return this;
		}
	}
}
