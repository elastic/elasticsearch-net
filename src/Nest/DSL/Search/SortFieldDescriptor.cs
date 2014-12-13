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

		[JsonProperty("nested_filter")]
		FilterContainer NestedFilter { get; set; }

		[JsonProperty("nested_path")]
		PropertyPathMarker NestedPath { get; set; }
	}

	public abstract class SortBase : ISort
	{
		public string Missing { get; set; }
		public SortOrder? Order { get; set; }
		public SortMode? Mode { get; set; }
		public FilterContainer NestedFilter { get; set; }
		public PropertyPathMarker NestedPath { get; set; }
	}

	public abstract class SortDescriptorBase<T, TDescriptor> : ISort where T : class where TDescriptor : SortDescriptorBase<T, TDescriptor>
	{
		private ISort Self { get { return this; } }

		string ISort.Missing { get; set; }

		SortOrder? ISort.Order { get; set; }

		SortMode? ISort.Mode { get; set; }

		FilterContainer ISort.NestedFilter { get; set; }

		PropertyPathMarker ISort.NestedPath { get; set; }

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

		public virtual TDescriptor NestedFilter(Func<FilterDescriptor<T>, FilterContainer> filterSelector)
		{
			filterSelector.ThrowIfNull("filterSelector");

			var filter = new FilterDescriptor<T>();
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
		PropertyPathMarker Field { get; set; }

		[JsonProperty("ignore_unmapped")]
		bool? IgnoreUnmappedFields { get; set; }

        [JsonProperty("unmapped_type")]
        FieldType? UnmappedType { get; set; }
	}

	public class Sort : SortBase, IFieldSort
	{
		public PropertyPathMarker Field { get; set; }
		public bool? IgnoreUnmappedFields { get; set; }
	    public FieldType? UnmappedType { get; set; }
	}

	public class SortFieldDescriptor<T> : SortDescriptorBase<T, SortFieldDescriptor<T>>, IFieldSort where T : class
	{
		private IFieldSort Self { get { return this; } }

		PropertyPathMarker IFieldSort.Field { get; set; }

		bool? IFieldSort.IgnoreUnmappedFields { get; set; }
        FieldType? IFieldSort.UnmappedType { get; set; }

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
        public virtual SortFieldDescriptor<T> UnmappedType(FieldType type)
        {
            Self.UnmappedType = type;
            return this;
        }

        [Obsolete("Deprecated in 1.4.0 use UnmappedType")]
		public virtual SortFieldDescriptor<T> IgnoreUnmappedFields(bool ignore = true)
		{
			Self.IgnoreUnmappedFields = ignore;
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
