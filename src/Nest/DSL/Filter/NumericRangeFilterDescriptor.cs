using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Nest.Resolvers;
using Elasticsearch.Net;

namespace Nest
{
	[JsonConverter(typeof(CompositeJsonConverter<ReadAsTypeConverter<NumericRangeFilterDescriptor<object>>,CustomJsonConverter>))]
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface INumericRangeFilter : IFilterBase, ICustomJson
	{
		[JsonProperty("from")]
		object From { get; set; }

		[JsonProperty("to")]
		object To { get; set; }

		[JsonProperty("include_lower")]
		bool? IncludeLower { get; set; }

		[JsonProperty("include_upper")]
		bool? IncludeUpper { get; set; }

		PropertyPathMarker Field { get; set; }
	}

	/// <summary>
	/// Filters documents with fields that have values within a certain numeric range. Similar to range filter, except that it works only with numeric values
	/// </summary>
	/// <typeparam name="T">Type of document</typeparam>
	public class NumericRangeFilterDescriptor<T> : FilterBase, INumericRangeFilter
		where T : class
	{
		object INumericRangeFilter.From { get; set;}
		
		object INumericRangeFilter.To { get; set; }
		
		bool? INumericRangeFilter.IncludeLower { get; set; }
		
		bool? INumericRangeFilter.IncludeUpper { get; set; }

		PropertyPathMarker INumericRangeFilter.Field { get; set; }

		bool IFilterBase.IsConditionless
		{
			get
			{
				return ((INumericRangeFilter)this).Field.IsConditionless() || (((INumericRangeFilter)this).From == null && ((INumericRangeFilter)this).To == null);
			}
		}
		
		object ICustomJson.GetCustomJson()
		{
			var f = (INumericRangeFilter)this;
			var rf = new
			{
				from = f.From,
				to = f.To,
				include_lower = f.IncludeLower,
				include_upper = f.IncludeUpper
			};
			return this.FieldNameAsKeyFormat(f.Field, rf);
		}

		public NumericRangeFilterDescriptor<T> OnField(string field)
		{
			((INumericRangeFilter)this).Field = field;
			return this;
		}
		public NumericRangeFilterDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
		{
			((INumericRangeFilter)this).Field = objectPath;
			return this;
		}

		/// <summary>
		/// Forces the 'From()' to be exclusive (which is inclusive by default).
		/// </summary>
		public NumericRangeFilterDescriptor<T> FromExclusive()
		{
			((INumericRangeFilter)this).IncludeLower = false;
			return this;
		}

		/// <summary>
		/// Forces the 'To()' to be exclusive (which is inclusive by default).
		/// </summary>
		public NumericRangeFilterDescriptor<T> ToExclusive()
		{
			((INumericRangeFilter)this).IncludeUpper = false;
			return this;
		}

		#region int
		/// <summary>
		/// The upper bound. Defaults to unbounded.
		/// </summary>
		public NumericRangeFilterDescriptor<T> To(int? to)
		{
			((INumericRangeFilter)this).To = to;
			return this;
		}
		/// <summary>
		/// The lower bound. Defaults to start from the first.
		/// </summary>
		/// <returns></returns>
		public NumericRangeFilterDescriptor<T> From(int? from)
		{
			((INumericRangeFilter)this).From = from;
			return this;
		}

		/// <summary>
		/// Same as setting from and include_lower to false.
		/// </summary>
		public NumericRangeFilterDescriptor<T> Greater(int? from)
		{
			((INumericRangeFilter)this).From = from;
			((INumericRangeFilter)this).IncludeLower = false;
			return this;
		}
		/// <summary>
		/// Same as setting from and include_lower to true.
		/// </summary>
		public NumericRangeFilterDescriptor<T> GreaterOrEquals(int? from)
		{
			((INumericRangeFilter)this).From = from;
			((INumericRangeFilter)this).IncludeLower = from.HasValue ? new Nullable<bool>(true) : null; 
			return this;
		}
		/// <summary>
		/// Same as setting to and include_upper to false.
		/// </summary>
		public NumericRangeFilterDescriptor<T> Lower(int? to)
		{
			((INumericRangeFilter)this).To = to;
			((INumericRangeFilter)this).IncludeUpper = to.HasValue ? new Nullable<bool>(false) : null; 
			return this;
		}
		/// <summary>
		/// Same as setting to and include_upper to true.
		/// </summary>
		public NumericRangeFilterDescriptor<T> LowerOrEquals(int? to)
		{
			((INumericRangeFilter)this).To = to;
			((INumericRangeFilter)this).IncludeUpper = to.HasValue ? new Nullable<bool>(true) : null;
			return this;
		}
		#endregion

		#region double
		/// <summary>
		/// The upper bound. Defaults to unbounded.
		/// </summary>
		public NumericRangeFilterDescriptor<T> To(double? to)
		{
			((INumericRangeFilter)this).To = to;
			return this;
		}
		/// <summary>
		/// The lower bound. Defaults to start from the first.
		/// </summary>
		/// <returns></returns>
		public NumericRangeFilterDescriptor<T> From(double? from)
		{
			((INumericRangeFilter)this).From = from;
			return this;
		}

		/// <summary>
		/// Same as setting from and include_lower to false.
		/// </summary>
		public NumericRangeFilterDescriptor<T> Greater(double? from)
		{
			((INumericRangeFilter)this).From = from;
			((INumericRangeFilter)this).IncludeLower = from.HasValue ? new Nullable<bool>(false) : null;
			return this;
		}
		/// <summary>
		/// Same as setting from and include_lower to true.
		/// </summary>
		public NumericRangeFilterDescriptor<T> GreaterOrEquals(double? from)
		{
			((INumericRangeFilter)this).From = from;
			((INumericRangeFilter)this).IncludeLower = from.HasValue ? new Nullable<bool>(true) : null;
			return this;
		}
		/// <summary>
		/// Same as setting to and include_upper to false.
		/// </summary>
		public NumericRangeFilterDescriptor<T> Lower(double? to)
		{
			((INumericRangeFilter)this).To = to;
			((INumericRangeFilter)this).IncludeUpper = to.HasValue ? new Nullable<bool>(false) : null;
			return this;
		}
		/// <summary>
		/// Same as setting to and include_upper to true.
		/// </summary>
		public NumericRangeFilterDescriptor<T> LowerOrEquals(double? to)
		{
			((INumericRangeFilter)this).To = to;
			((INumericRangeFilter)this).IncludeUpper = to.HasValue ? new Nullable<bool>(true) : null;
			return this;
		}
		#endregion

		#region string
		/// <summary>
		/// The upper bound. Defaults to unbounded.
		/// </summary>
		public NumericRangeFilterDescriptor<T> To(string to)
		{
			((INumericRangeFilter)this).To = to;
			return this;
		}
		/// <summary>
		/// The lower bound. Defaults to start from the first.
		/// </summary>
		/// <returns></returns>
		public NumericRangeFilterDescriptor<T> From(string from)
		{
			((INumericRangeFilter)this).From = from;
			return this;
		}

		/// <summary>
		/// Same as setting from and include_lower to false.
		/// </summary>
		public NumericRangeFilterDescriptor<T> Greater(string from)
		{
			((INumericRangeFilter)this).From = from;
			((INumericRangeFilter)this).IncludeLower = !from.IsNullOrEmpty() ? new Nullable<bool>(false) : null; 
			return this;
		}
		/// <summary>
		/// Same as setting from and include_lower to true.
		/// </summary>
		public NumericRangeFilterDescriptor<T> GreaterOrEquals(string from)
		{
			((INumericRangeFilter)this).From = from;
			((INumericRangeFilter)this).IncludeLower = !from.IsNullOrEmpty() ? new Nullable<bool>(true) : null; 
			return this;
		}
		/// <summary>
		/// Same as setting to and include_upper to false.
		/// </summary>
		public NumericRangeFilterDescriptor<T> Lower(string to)
		{
			((INumericRangeFilter)this).To = to;
			((INumericRangeFilter)this).IncludeUpper = !to.IsNullOrEmpty() ? new Nullable<bool>(false) : null;
			return this;
		}
		/// <summary>
		/// Same as setting to and include_upper to true.
		/// </summary>
		public NumericRangeFilterDescriptor<T> LowerOrEquals(string to)
		{
			((INumericRangeFilter)this).To = to;
			((INumericRangeFilter)this).IncludeUpper = !to.IsNullOrEmpty() ? new Nullable<bool>(true) : null; 
			return this;
		}
		#endregion

		#region DateTime
		/// <summary>
		/// The upper bound. Defaults to unbounded.
		/// </summary>
		public NumericRangeFilterDescriptor<T> To(DateTime? to, string format = "yyyy-MM-dd'T'HH:mm:ss")
		{
			if (!to.HasValue)
				return this;
			((INumericRangeFilter)this).To = to.Value.ToString(format);
			return this;
		}
		/// <summary>
		/// The lower bound. Defaults to start from the first.
		/// </summary>
		/// <returns></returns>
        public NumericRangeFilterDescriptor<T> From(DateTime? from, string format = "yyyy-MM-dd'T'HH:mm:ss")
		{
			if (!from.HasValue)
				return this;

			((INumericRangeFilter)this).From = from.Value.ToString(format);
			return this;
		}

		/// <summary>
		/// Same as setting from and include_lower to false.
		/// </summary>
        public NumericRangeFilterDescriptor<T> Greater(DateTime? from, string format = "yyyy-MM-dd'T'HH:mm:ss")
		{
			if (!from.HasValue)
				return this;

			((INumericRangeFilter)this).From = from.Value.ToString(format);
			((INumericRangeFilter)this).IncludeLower = false;
			return this;
		}
		/// <summary>
		/// Same as setting from and include_lower to true.
		/// </summary>
        public NumericRangeFilterDescriptor<T> GreaterOrEquals(DateTime? from, string format = "yyyy-MM-dd'T'HH:mm:ss")
		{
			if (!from.HasValue)
				return this;

			((INumericRangeFilter)this).From = from.Value.ToString(format);
			((INumericRangeFilter)this).IncludeLower = true;
			return this;
		}
		/// <summary>
		/// Same as setting to and include_upper to false.
		/// </summary>
        public NumericRangeFilterDescriptor<T> Lower(DateTime? to, string format = "yyyy-MM-dd'T'HH:mm:ss")
		{
			if (!to.HasValue)
				return this;

			((INumericRangeFilter)this).To = to.Value.ToString(format);
			((INumericRangeFilter)this).IncludeUpper = false;
			return this;
		}
		/// <summary>
		/// Same as setting to and include_upper to true.
		/// </summary>
        public NumericRangeFilterDescriptor<T> LowerOrEquals(DateTime? to, string format = "yyyy-MM-dd'T'HH:mm:ss")
		{
			if (!to.HasValue)
				return this;

			((INumericRangeFilter)this).To = to.Value.ToString(format);
			((INumericRangeFilter)this).IncludeUpper = true;
			return this;
		}
		#endregion
	}
}
