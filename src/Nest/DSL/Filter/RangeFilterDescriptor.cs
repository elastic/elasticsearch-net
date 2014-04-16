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
	public interface IRangeFilter : IFilterBase
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

	[JsonObject(MemberSerialization=MemberSerialization.OptIn)]
	public class RangeFilterDescriptor<T> : FilterBase, IRangeFilter where T : class
	{
		object IRangeFilter._From { get; set;}
		
		object IRangeFilter._To { get; set; }
		
		bool? IRangeFilter._FromInclusive { get; set; }
		
		bool? IRangeFilter._ToInclusive { get; set; }

		PropertyPathMarker IRangeFilter._Field { get; set; }

		bool IFilterBase.IsConditionless
		{
			get
			{
				return ((IRangeFilter)this)._Field.IsConditionless() || (((IRangeFilter)this)._From == null && ((IRangeFilter)this)._To == null);
			}

		}

		public RangeFilterDescriptor<T> OnField(string field)
		{
			((IRangeFilter)this)._Field = field;
			return this;
		}
		public RangeFilterDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
		{
			((IRangeFilter)this)._Field = objectPath;
			return this;
		}
		/// <summary>
		/// Forces the 'From()' to be exclusive (which is inclusive by default).
		/// </summary>
		public RangeFilterDescriptor<T> FromExclusive()
		{
			((IRangeFilter)this)._FromInclusive = false;
			return this;
		}
		/// <summary>
		/// Forces the 'To()' to be exclusive (which is inclusive by default).
		/// </summary>
		public RangeFilterDescriptor<T> ToExclusive()
		{
			((IRangeFilter)this)._ToInclusive = false;
			return this;
		}

		#region int
		/// <summary>
		/// The upper bound. Defaults to unbounded.
		/// </summary>
		public RangeFilterDescriptor<T> To(int? to)
		{
			((IRangeFilter)this)._To = to;
			return this;
		}
		/// <summary>
		/// The lower bound. Defaults to start from the first.
		/// </summary>
		/// <returns></returns>
		public RangeFilterDescriptor<T> From(int? from)
		{
			((IRangeFilter)this)._From = from;
			return this;
		}

		/// <summary>
		/// Same as setting from and include_lower to false.
		/// </summary>
		public RangeFilterDescriptor<T> Greater(int? from)
		{
			((IRangeFilter)this)._From = from;
			((IRangeFilter)this)._FromInclusive = false;
			return this;
		}
		/// <summary>
		/// Same as setting from and include_lower to true.
		/// </summary>
		public RangeFilterDescriptor<T> GreaterOrEquals(int? from)
		{
			((IRangeFilter)this)._From = from;
			((IRangeFilter)this)._FromInclusive = from.HasValue ? new Nullable<bool>(true) : null;
			return this;
		}
		/// <summary>
		/// Same as setting to and include_upper to false.
		/// </summary>
		public RangeFilterDescriptor<T> Lower(int? to)
		{
			((IRangeFilter)this)._To = to;
			((IRangeFilter)this)._ToInclusive = to.HasValue ? new Nullable<bool>(false) : null;
			return this;
		}
		/// <summary>
		/// Same as setting to and include_upper to true.
		/// </summary>
		public RangeFilterDescriptor<T> LowerOrEquals(int? to)
		{
			((IRangeFilter)this)._To = to;
			((IRangeFilter)this)._ToInclusive = to.HasValue ? new Nullable<bool>(true) : null;
			return this;
		}
		#endregion

		#region double
		/// <summary>
		/// The upper bound. Defaults to unbounded.
		/// </summary>
		public RangeFilterDescriptor<T> To(double? to)
		{
			((IRangeFilter)this)._To = to;
			return this;
		}
		/// <summary>
		/// The lower bound. Defaults to start from the first.
		/// </summary>
		/// <returns></returns>
		public RangeFilterDescriptor<T> From(double? from)
		{
			((IRangeFilter)this)._From = from;
			return this;
		}

		/// <summary>
		/// Same as setting from and include_lower to false.
		/// </summary>
		public RangeFilterDescriptor<T> Greater(double? from)
		{
			((IRangeFilter)this)._From = from;
			((IRangeFilter)this)._FromInclusive = from.HasValue ? new Nullable<bool>(false) : null;
			return this;
		}
		/// <summary>
		/// Same as setting from and include_lower to true.
		/// </summary>
		public RangeFilterDescriptor<T> GreaterOrEquals(double? from)
		{
			((IRangeFilter)this)._From = from;
			((IRangeFilter)this)._FromInclusive = from.HasValue ? new Nullable<bool>(true) : null;
			return this;
		}
		/// <summary>
		/// Same as setting to and include_upper to false.
		/// </summary>
		public RangeFilterDescriptor<T> Lower(double? to)
		{
			((IRangeFilter)this)._To = to;
			((IRangeFilter)this)._ToInclusive = to.HasValue ? new Nullable<bool>(false) : null;
			return this;
		}
		/// <summary>
		/// Same as setting to and include_upper to true.
		/// </summary>
		public RangeFilterDescriptor<T> LowerOrEquals(double? to)
		{
			((IRangeFilter)this)._To = to;
			((IRangeFilter)this)._ToInclusive = to.HasValue ? new Nullable<bool>(true) : null;
			return this;
		}
		#endregion

		#region string
		/// <summary>
		/// The upper bound. Defaults to unbounded.
		/// </summary>
		public RangeFilterDescriptor<T> To(string to)
		{
			((IRangeFilter)this)._To = to;
			return this;
		}
		/// <summary>
		/// The lower bound. Defaults to start from the first.
		/// </summary>
		/// <returns></returns>
		public RangeFilterDescriptor<T> From(string from)
		{
			((IRangeFilter)this)._From = from;
			return this;
		}

		/// <summary>
		/// Same as setting from and include_lower to false.
		/// </summary>
		public RangeFilterDescriptor<T> Greater(string from)
		{
			((IRangeFilter)this)._From = from;
			((IRangeFilter)this)._FromInclusive = !from.IsNullOrEmpty() ? new Nullable<bool>(false) : null;
			return this;
		}
		/// <summary>
		/// Same as setting from and include_lower to true.
		/// </summary>
		public RangeFilterDescriptor<T> GreaterOrEquals(string from)
		{
			((IRangeFilter)this)._From = from;
			((IRangeFilter)this)._FromInclusive = !from.IsNullOrEmpty() ? new Nullable<bool>(true) : null;
			return this;
		}
		/// <summary>
		/// Same as setting to and include_upper to false.
		/// </summary>
		public RangeFilterDescriptor<T> Lower(string to)
		{
			((IRangeFilter)this)._To = to;
			((IRangeFilter)this)._ToInclusive = !to.IsNullOrEmpty() ? new Nullable<bool>(false) : null;
			return this;
		}
		/// <summary>
		/// Same as setting to and include_upper to true.
		/// </summary>
		public RangeFilterDescriptor<T> LowerOrEquals(string to)
		{
			((IRangeFilter)this)._To = to;
			((IRangeFilter)this)._ToInclusive = !to.IsNullOrEmpty() ? new Nullable<bool>(true) : null;
			return this;
		}
		#endregion

		#region DateTime
		/// <summary>
		/// The upper bound. Defaults to unbounded.
		/// </summary>
		public RangeFilterDescriptor<T> To(DateTime? to, string format = "yyyy-MM-dd'T'HH:mm:ss")
		{
			if (!to.HasValue)
				return this;
			((IRangeFilter)this)._To = to.Value.ToString(format);
			return this;
		}
		/// <summary>
		/// The lower bound. Defaults to start from the first.
		/// </summary>
		/// <returns></returns>
		public RangeFilterDescriptor<T> From(DateTime? from, string format = "yyyy-MM-dd'T'HH:mm:ss")
		{
			if (!from.HasValue)
				return this;

			((IRangeFilter)this)._From = from.Value.ToString(format);
			return this;
		}

		/// <summary>
		/// Same as setting from and include_lower to false.
		/// </summary>
		public RangeFilterDescriptor<T> Greater(DateTime? from, string format = "yyyy-MM-dd'T'HH:mm:ss")
		{
			if (!from.HasValue)
				return this;

			((IRangeFilter)this)._From = from.Value.ToString(format);
			((IRangeFilter)this)._FromInclusive = false;
			return this;
		}
		/// <summary>
		/// Same as setting from and include_lower to true.
		/// </summary>
		public RangeFilterDescriptor<T> GreaterOrEquals(DateTime? from, string format = "yyyy-MM-dd'T'HH:mm:ss")
		{
			if (!from.HasValue)
				return this;

			((IRangeFilter)this)._From = from.Value.ToString(format);
			((IRangeFilter)this)._FromInclusive = true;
			return this;
		}
		/// <summary>
		/// Same as setting to and include_upper to false.
		/// </summary>
		public RangeFilterDescriptor<T> Lower(DateTime? to, string format = "yyyy-MM-dd'T'HH:mm:ss")
		{
			if (!to.HasValue)
				return this;

			((IRangeFilter)this)._To = to.Value.ToString(format);
			((IRangeFilter)this)._ToInclusive = false;
			return this;
		}
		/// <summary>
		/// Same as setting to and include_upper to true.
		/// </summary>
		public RangeFilterDescriptor<T> LowerOrEquals(DateTime? to, string format = "yyyy-MM-dd'T'HH:mm:ss")
		{
			if (!to.HasValue)
				return this;

			((IRangeFilter)this)._To = to.Value.ToString(format);
			((IRangeFilter)this)._ToInclusive = true;
			return this;
		}
		#endregion
	
	}
}
