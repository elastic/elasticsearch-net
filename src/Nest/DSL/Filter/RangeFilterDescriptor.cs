using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest.Resolvers.Converters;
using Nest.Resolvers.Converters.Filters;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Nest.Resolvers;
using Elasticsearch.Net;

namespace Nest
{
	[JsonConverter(typeof(CompositeJsonConverter<RangeFilterJsonReader,CustomJsonConverter>))]
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IRangeFilter : IFilterBase, ICustomJson
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

	public class RangeFilterDescriptor<T> : FilterBase, IRangeFilter where T : class
	{
		object IRangeFilter.From { get; set;}
		
		object IRangeFilter.To { get; set; }
		
		bool? IRangeFilter.IncludeLower { get; set; }
		
		bool? IRangeFilter.IncludeUpper { get; set; }

		PropertyPathMarker IRangeFilter.Field { get; set; }

		bool IFilterBase.IsConditionless
		{
			get
			{
				return ((IRangeFilter)this).Field.IsConditionless() || (((IRangeFilter)this).From == null && ((IRangeFilter)this).To == null);
			}

		}
		
		object ICustomJson.GetCustomJson()
		{
			var f = (IRangeFilter)this;
			var rf = new
			{
				from = f.From,
				to = f.To,
				include_lower = f.IncludeLower,
				include_upper = f.IncludeUpper
			};
			return this.FieldNameAsKeyFormat(f.Field, rf);
		}
		
		public RangeFilterDescriptor<T> OnField(string field)
		{
			((IRangeFilter)this).Field = field;
			return this;
		}
		public RangeFilterDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
		{
			((IRangeFilter)this).Field = objectPath;
			return this;
		}

		/// <summary>
		/// Forces the 'From()' to be exclusive (which is inclusive by default).
		/// </summary>
		public RangeFilterDescriptor<T> FromExclusive()
		{
			((IRangeFilter)this).IncludeLower = false;
			return this;
		}

		/// <summary>
		/// Forces the 'To()' to be exclusive (which is inclusive by default).
		/// </summary>
		public RangeFilterDescriptor<T> ToExclusive()
		{
			((IRangeFilter)this).IncludeUpper = false;
			return this;
		}

		#region long
		/// <summary>
		/// The upper bound. Defaults to unbounded.
		/// </summary>
		public RangeFilterDescriptor<T> To(long? to)
		{
			((IRangeFilter)this).To = to;
			return this;
		}
		/// <summary>
		/// The lower bound. Defaults to start from the first.
		/// </summary>
		/// <returns></returns>
		public RangeFilterDescriptor<T> From(long? from)
		{
			((IRangeFilter)this).From = from;
			return this;
		}

		/// <summary>
		/// Same as setting from and include_lower to false.
		/// </summary>
		public RangeFilterDescriptor<T> Greater(long? from)
		{
			((IRangeFilter)this).From = from;
			((IRangeFilter)this).IncludeLower = false;
			return this;
		}
		/// <summary>
		/// Same as setting from and include_lower to true.
		/// </summary>
		public RangeFilterDescriptor<T> GreaterOrEquals(long? from)
		{
			((IRangeFilter)this).From = from;
			((IRangeFilter)this).IncludeLower = from.HasValue ? new Nullable<bool>(true) : null;
			return this;
		}
		/// <summary>
		/// Same as setting to and include_upper to false.
		/// </summary>
		public RangeFilterDescriptor<T> Lower(long? to)
		{
			((IRangeFilter)this).To = to;
			((IRangeFilter)this).IncludeUpper = to.HasValue ? new Nullable<bool>(false) : null;
			return this;
		}
		/// <summary>
		/// Same as setting to and include_upper to true.
		/// </summary>
		public RangeFilterDescriptor<T> LowerOrEquals(long? to)
		{
			((IRangeFilter)this).To = to;
			((IRangeFilter)this).IncludeUpper = to.HasValue ? new Nullable<bool>(true) : null;
			return this;
		}
		#endregion

		#region double
		/// <summary>
		/// The upper bound. Defaults to unbounded.
		/// </summary>
		public RangeFilterDescriptor<T> To(double? to)
		{
			((IRangeFilter)this).To = to;
			return this;
		}
		/// <summary>
		/// The lower bound. Defaults to start from the first.
		/// </summary>
		/// <returns></returns>
		public RangeFilterDescriptor<T> From(double? from)
		{
			((IRangeFilter)this).From = from;
			return this;
		}

		/// <summary>
		/// Same as setting from and include_lower to false.
		/// </summary>
		public RangeFilterDescriptor<T> Greater(double? from)
		{
			((IRangeFilter)this).From = from;
			((IRangeFilter)this).IncludeLower = from.HasValue ? new Nullable<bool>(false) : null;
			return this;
		}
		/// <summary>
		/// Same as setting from and include_lower to true.
		/// </summary>
		public RangeFilterDescriptor<T> GreaterOrEquals(double? from)
		{
			((IRangeFilter)this).From = from;
			((IRangeFilter)this).IncludeLower = from.HasValue ? new Nullable<bool>(true) : null;
			return this;
		}
		/// <summary>
		/// Same as setting to and include_upper to false.
		/// </summary>
		public RangeFilterDescriptor<T> Lower(double? to)
		{
			((IRangeFilter)this).To = to;
			((IRangeFilter)this).IncludeUpper = to.HasValue ? new Nullable<bool>(false) : null;
			return this;
		}
		/// <summary>
		/// Same as setting to and include_upper to true.
		/// </summary>
		public RangeFilterDescriptor<T> LowerOrEquals(double? to)
		{
			((IRangeFilter)this).To = to;
			((IRangeFilter)this).IncludeUpper = to.HasValue ? new Nullable<bool>(true) : null;
			return this;
		}
		#endregion

		#region string
		/// <summary>
		/// The upper bound. Defaults to unbounded.
		/// </summary>
		public RangeFilterDescriptor<T> To(string to)
		{
			((IRangeFilter)this).To = to;
			return this;
		}
		/// <summary>
		/// The lower bound. Defaults to start from the first.
		/// </summary>
		/// <returns></returns>
		public RangeFilterDescriptor<T> From(string from)
		{
			((IRangeFilter)this).From = from;
			return this;
		}

		/// <summary>
		/// Same as setting from and include_lower to false.
		/// </summary>
		public RangeFilterDescriptor<T> Greater(string from)
		{
			((IRangeFilter)this).From = from;
			((IRangeFilter)this).IncludeLower = !from.IsNullOrEmpty() ? new Nullable<bool>(false) : null;
			return this;
		}
		/// <summary>
		/// Same as setting from and include_lower to true.
		/// </summary>
		public RangeFilterDescriptor<T> GreaterOrEquals(string from)
		{
			((IRangeFilter)this).From = from;
			((IRangeFilter)this).IncludeLower = !from.IsNullOrEmpty() ? new Nullable<bool>(true) : null;
			return this;
		}
		/// <summary>
		/// Same as setting to and include_upper to false.
		/// </summary>
		public RangeFilterDescriptor<T> Lower(string to)
		{
			((IRangeFilter)this).To = to;
			((IRangeFilter)this).IncludeUpper = !to.IsNullOrEmpty() ? new Nullable<bool>(false) : null;
			return this;
		}
		/// <summary>
		/// Same as setting to and include_upper to true.
		/// </summary>
		public RangeFilterDescriptor<T> LowerOrEquals(string to)
		{
			((IRangeFilter)this).To = to;
			((IRangeFilter)this).IncludeUpper = !to.IsNullOrEmpty() ? new Nullable<bool>(true) : null;
			return this;
		}
		#endregion

		#region DateTime
		/// <summary>
		/// The upper bound. Defaults to unbounded.
		/// </summary>
		public RangeFilterDescriptor<T> To(DateTime? to, string format = "yyyy-MM-dd'T'HH:mm:ss.fff")
		{
			if (!to.HasValue)
				return this;
			((IRangeFilter)this).To = to.Value.ToString(format);
			return this;
		}
		/// <summary>
		/// The lower bound. Defaults to start from the first.
		/// </summary>
		/// <returns></returns>
		public RangeFilterDescriptor<T> From(DateTime? from, string format = "yyyy-MM-dd'T'HH:mm:ss.fff")
		{
			if (!from.HasValue)
				return this;

			((IRangeFilter)this).From = from.Value.ToString(format);
			return this;
		}

		/// <summary>
		/// Same as setting from and include_lower to false.
		/// </summary>
		public RangeFilterDescriptor<T> Greater(DateTime? from, string format = "yyyy-MM-dd'T'HH:mm:ss.fff")
		{
			if (!from.HasValue)
				return this;

			((IRangeFilter)this).From = from.Value.ToString(format);
			((IRangeFilter)this).IncludeLower = false;
			return this;
		}
		/// <summary>
		/// Same as setting from and include_lower to true.
		/// </summary>
		public RangeFilterDescriptor<T> GreaterOrEquals(DateTime? from, string format = "yyyy-MM-dd'T'HH:mm:ss.fff")
		{
			if (!from.HasValue)
				return this;

			((IRangeFilter)this).From = from.Value.ToString(format);
			((IRangeFilter)this).IncludeLower = true;
			return this;
		}
		/// <summary>
		/// Same as setting to and include_upper to false.
		/// </summary>
		public RangeFilterDescriptor<T> Lower(DateTime? to, string format = "yyyy-MM-dd'T'HH:mm:ss.fff")
		{
			if (!to.HasValue)
				return this;

			((IRangeFilter)this).To = to.Value.ToString(format);
			((IRangeFilter)this).IncludeUpper = false;
			return this;
		}
		/// <summary>
		/// Same as setting to and include_upper to true.
		/// </summary>
		public RangeFilterDescriptor<T> LowerOrEquals(DateTime? to, string format = "yyyy-MM-dd'T'HH:mm:ss.fff")
		{
			if (!to.HasValue)
				return this;

			((IRangeFilter)this).To = to.Value.ToString(format);
			((IRangeFilter)this).IncludeUpper = true;
			return this;
		}
		#endregion
	
	}
}
