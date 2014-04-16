using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Nest.Resolvers;
using Elasticsearch.Net;

namespace Nest
{
	public interface INumericRangeFilter : IFilterBase
	{
		[JsonProperty("from")]
		object _From { get; set; }

		[JsonProperty("to")]
		object _To { get; set; }

		[JsonProperty("include_lower")]
		bool? _FromInclusive { get; set; }

		[JsonProperty("include_upper")]
		bool? _ToInclusive { get; set; }

		PropertyPathMarker _Field { get; set; }
	}

	/// <summary>
	/// Filters documents with fields that have values within a certain numeric range. Similar to range filter, except that it works only with numeric values
	/// </summary>
	/// <typeparam name="T">Type of document</typeparam>
	[JsonObject(MemberSerialization=MemberSerialization.OptIn)]
	public class NumericRangeFilterDescriptor<T> : FilterBase, INumericRangeFilter where T : class
	{
		object INumericRangeFilter._From { get; set;}
		
		object INumericRangeFilter._To { get; set; }
		
		bool? INumericRangeFilter._FromInclusive { get; set; }
		
		bool? INumericRangeFilter._ToInclusive { get; set; }

		PropertyPathMarker INumericRangeFilter._Field { get; set; }

		bool IFilterBase.IsConditionless
		{
			get
			{
				return ((INumericRangeFilter)this)._Field.IsConditionless() || (((INumericRangeFilter)this)._From == null && ((INumericRangeFilter)this)._To == null);
			}
		}


		public NumericRangeFilterDescriptor<T> OnField(string field)
		{
			((INumericRangeFilter)this)._Field = field;
			return this;
		}
		public NumericRangeFilterDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
		{
			((INumericRangeFilter)this)._Field = objectPath;
			return this;
		}

		/// <summary>
		/// Forces the 'From()' to be exclusive (which is inclusive by default).
		/// </summary>
		public NumericRangeFilterDescriptor<T> FromExclusive()
		{
			((INumericRangeFilter)this)._FromInclusive = false;
			return this;
		}

		/// <summary>
		/// Forces the 'To()' to be exclusive (which is inclusive by default).
		/// </summary>
		public NumericRangeFilterDescriptor<T> ToExclusive()
		{
			((INumericRangeFilter)this)._ToInclusive = false;
			return this;
		}

		#region int
		/// <summary>
		/// The upper bound. Defaults to unbounded.
		/// </summary>
		public NumericRangeFilterDescriptor<T> To(int? to)
		{
			((INumericRangeFilter)this)._To = to;
			return this;
		}
		/// <summary>
		/// The lower bound. Defaults to start from the first.
		/// </summary>
		/// <returns></returns>
		public NumericRangeFilterDescriptor<T> From(int? from)
		{
			((INumericRangeFilter)this)._From = from;
			return this;
		}

		/// <summary>
		/// Same as setting from and include_lower to false.
		/// </summary>
		public NumericRangeFilterDescriptor<T> Greater(int? from)
		{
			((INumericRangeFilter)this)._From = from;
			((INumericRangeFilter)this)._FromInclusive = false;
			return this;
		}
		/// <summary>
		/// Same as setting from and include_lower to true.
		/// </summary>
		public NumericRangeFilterDescriptor<T> GreaterOrEquals(int? from)
		{
			((INumericRangeFilter)this)._From = from;
			((INumericRangeFilter)this)._FromInclusive = from.HasValue ? new Nullable<bool>(true) : null; 
			return this;
		}
		/// <summary>
		/// Same as setting to and include_upper to false.
		/// </summary>
		public NumericRangeFilterDescriptor<T> Lower(int? to)
		{
			((INumericRangeFilter)this)._To = to;
			((INumericRangeFilter)this)._ToInclusive = to.HasValue ? new Nullable<bool>(false) : null; 
			return this;
		}
		/// <summary>
		/// Same as setting to and include_upper to true.
		/// </summary>
		public NumericRangeFilterDescriptor<T> LowerOrEquals(int? to)
		{
			((INumericRangeFilter)this)._To = to;
			((INumericRangeFilter)this)._ToInclusive = to.HasValue ? new Nullable<bool>(true) : null;
			return this;
		}
		#endregion

		#region double
		/// <summary>
		/// The upper bound. Defaults to unbounded.
		/// </summary>
		public NumericRangeFilterDescriptor<T> To(double? to)
		{
			((INumericRangeFilter)this)._To = to;
			return this;
		}
		/// <summary>
		/// The lower bound. Defaults to start from the first.
		/// </summary>
		/// <returns></returns>
		public NumericRangeFilterDescriptor<T> From(double? from)
		{
			((INumericRangeFilter)this)._From = from;
			return this;
		}

		/// <summary>
		/// Same as setting from and include_lower to false.
		/// </summary>
		public NumericRangeFilterDescriptor<T> Greater(double? from)
		{
			((INumericRangeFilter)this)._From = from;
			((INumericRangeFilter)this)._FromInclusive = from.HasValue ? new Nullable<bool>(false) : null;
			return this;
		}
		/// <summary>
		/// Same as setting from and include_lower to true.
		/// </summary>
		public NumericRangeFilterDescriptor<T> GreaterOrEquals(double? from)
		{
			((INumericRangeFilter)this)._From = from;
			((INumericRangeFilter)this)._FromInclusive = from.HasValue ? new Nullable<bool>(true) : null;
			return this;
		}
		/// <summary>
		/// Same as setting to and include_upper to false.
		/// </summary>
		public NumericRangeFilterDescriptor<T> Lower(double? to)
		{
			((INumericRangeFilter)this)._To = to;
			((INumericRangeFilter)this)._ToInclusive = to.HasValue ? new Nullable<bool>(false) : null;
			return this;
		}
		/// <summary>
		/// Same as setting to and include_upper to true.
		/// </summary>
		public NumericRangeFilterDescriptor<T> LowerOrEquals(double? to)
		{
			((INumericRangeFilter)this)._To = to;
			((INumericRangeFilter)this)._ToInclusive = to.HasValue ? new Nullable<bool>(true) : null;
			return this;
		}
		#endregion

		#region string
		/// <summary>
		/// The upper bound. Defaults to unbounded.
		/// </summary>
		public NumericRangeFilterDescriptor<T> To(string to)
		{
			((INumericRangeFilter)this)._To = to;
			return this;
		}
		/// <summary>
		/// The lower bound. Defaults to start from the first.
		/// </summary>
		/// <returns></returns>
		public NumericRangeFilterDescriptor<T> From(string from)
		{
			((INumericRangeFilter)this)._From = from;
			return this;
		}

		/// <summary>
		/// Same as setting from and include_lower to false.
		/// </summary>
		public NumericRangeFilterDescriptor<T> Greater(string from)
		{
			((INumericRangeFilter)this)._From = from;
			((INumericRangeFilter)this)._FromInclusive = !from.IsNullOrEmpty() ? new Nullable<bool>(false) : null; 
			return this;
		}
		/// <summary>
		/// Same as setting from and include_lower to true.
		/// </summary>
		public NumericRangeFilterDescriptor<T> GreaterOrEquals(string from)
		{
			((INumericRangeFilter)this)._From = from;
			((INumericRangeFilter)this)._FromInclusive = !from.IsNullOrEmpty() ? new Nullable<bool>(true) : null; 
			return this;
		}
		/// <summary>
		/// Same as setting to and include_upper to false.
		/// </summary>
		public NumericRangeFilterDescriptor<T> Lower(string to)
		{
			((INumericRangeFilter)this)._To = to;
			((INumericRangeFilter)this)._ToInclusive = !to.IsNullOrEmpty() ? new Nullable<bool>(false) : null;
			return this;
		}
		/// <summary>
		/// Same as setting to and include_upper to true.
		/// </summary>
		public NumericRangeFilterDescriptor<T> LowerOrEquals(string to)
		{
			((INumericRangeFilter)this)._To = to;
			((INumericRangeFilter)this)._ToInclusive = !to.IsNullOrEmpty() ? new Nullable<bool>(true) : null; 
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
			((INumericRangeFilter)this)._To = to.Value.ToString(format);
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

			((INumericRangeFilter)this)._From = from.Value.ToString(format);
			return this;
		}

		/// <summary>
		/// Same as setting from and include_lower to false.
		/// </summary>
        public NumericRangeFilterDescriptor<T> Greater(DateTime? from, string format = "yyyy-MM-dd'T'HH:mm:ss")
		{
			if (!from.HasValue)
				return this;

			((INumericRangeFilter)this)._From = from.Value.ToString(format);
			((INumericRangeFilter)this)._FromInclusive = false;
			return this;
		}
		/// <summary>
		/// Same as setting from and include_lower to true.
		/// </summary>
        public NumericRangeFilterDescriptor<T> GreaterOrEquals(DateTime? from, string format = "yyyy-MM-dd'T'HH:mm:ss")
		{
			if (!from.HasValue)
				return this;

			((INumericRangeFilter)this)._From = from.Value.ToString(format);
			((INumericRangeFilter)this)._FromInclusive = true;
			return this;
		}
		/// <summary>
		/// Same as setting to and include_upper to false.
		/// </summary>
        public NumericRangeFilterDescriptor<T> Lower(DateTime? to, string format = "yyyy-MM-dd'T'HH:mm:ss")
		{
			if (!to.HasValue)
				return this;

			((INumericRangeFilter)this)._To = to.Value.ToString(format);
			((INumericRangeFilter)this)._ToInclusive = false;
			return this;
		}
		/// <summary>
		/// Same as setting to and include_upper to true.
		/// </summary>
        public NumericRangeFilterDescriptor<T> LowerOrEquals(DateTime? to, string format = "yyyy-MM-dd'T'HH:mm:ss")
		{
			if (!to.HasValue)
				return this;

			((INumericRangeFilter)this)._To = to.Value.ToString(format);
			((INumericRangeFilter)this)._ToInclusive = true;
			return this;
		}
		#endregion
	}
}
