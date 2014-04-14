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
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IRangeQuery
	{
		[JsonProperty("from")]
		object _From { get; set; }

		[JsonProperty("to")]
		object _To { get; set; }

		[JsonProperty("include_lower")]
		bool? _FromInclusive { get; set; }

		[JsonProperty("include_upper")]
		bool? _ToInclusive { get; set; }

		[JsonProperty(PropertyName = "boost")]
		double? _Boost { get; set; }

		[JsonProperty(PropertyName = "_cache")]
		bool? _Cache { get; set; }

		[JsonProperty(PropertyName = "_name")]
		string _Name { get; set; }

		PropertyPathMarker _Field { get; set; }
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class RangeQueryDescriptor<T> : IQuery, IRangeQuery where T : class
	{
		object IRangeQuery._From { get; set; }
	
		object IRangeQuery._To { get; set; }
		
		bool? IRangeQuery._FromInclusive { get; set; }
		
		bool? IRangeQuery._ToInclusive { get; set; }
		
		double? IRangeQuery._Boost { get; set; }
		
		bool? IRangeQuery._Cache { get; set; }
		
		string IRangeQuery._Name { get; set; }

		PropertyPathMarker IRangeQuery._Field { get; set; }
		
		bool IQuery.IsConditionless
		{
			get
			{
				return ((IRangeQuery)this)._Field.IsConditionless() || (((IRangeQuery)this)._From == null && ((IRangeQuery)this)._To == null);
			}
		}


		public RangeQueryDescriptor<T> OnField(string field)
		{
			((IRangeQuery)this)._Field = field;
			return this;
		}
		public RangeQueryDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
		{
			((IRangeQuery)this)._Field = objectPath;
			return this;
		}
		/// <summary>
		/// Forces the 'To()' to be exclusive (which is inclusive by default).
		/// </summary>
		public RangeQueryDescriptor<T> ToExclusive()
		{
			((IRangeQuery)this)._ToInclusive = false;
			return this;
		}
		/// <summary>
		/// Forces the 'From()' to be exclusive (which is inclusive by default).
		/// </summary>
		public RangeQueryDescriptor<T> FromExclusive()
		{
			((IRangeQuery)this)._FromInclusive = false;
			return this;
		}
		
		/// <summary>
		/// Boosts the range query by the specified boost factor
		/// </summary>
		/// <param name="boost">Boost factor</param>
		public RangeQueryDescriptor<T> Boost(double boost)
		{
			((IRangeQuery)this)._Boost = boost;
			return this;
		}


		#region int
		/// <summary>
		/// The upper bound. Defaults to unbounded.
		/// </summary>
		public RangeQueryDescriptor<T> To(int? to)
		{
			((IRangeQuery)this)._To = to;
			return this;
		}
		/// <summary>
		/// The lower bound. Defaults to start from the first.
		/// </summary>
		/// <returns></returns>
		public RangeQueryDescriptor<T> From(int? from)
		{
			((IRangeQuery)this)._From = from;
			return this;
		}

		/// <summary>
		/// Same as setting from and include_lower to false.
		/// </summary>
		public RangeQueryDescriptor<T> Greater(int? from)
		{
			((IRangeQuery)this)._From = from;
			((IRangeQuery)this)._FromInclusive = false;
			return this;
		}
		/// <summary>
		/// Same as setting from and include_lower to true.
		/// </summary>
		public RangeQueryDescriptor<T> GreaterOrEquals(int? from)
		{
			((IRangeQuery)this)._From = from;
			((IRangeQuery)this)._FromInclusive = from.HasValue ? new Nullable<bool>(true) : null;
			return this;
		}
		/// <summary>
		/// Same as setting to and include_upper to false.
		/// </summary>
		public RangeQueryDescriptor<T> Lower(int? to)
		{
			((IRangeQuery)this)._To = to;
			((IRangeQuery)this)._ToInclusive = to.HasValue ? new Nullable<bool>(false) : null;
			return this;
		}
		/// <summary>
		/// Same as setting to and include_upper to true.
		/// </summary>
		public RangeQueryDescriptor<T> LowerOrEquals(int? to)
		{
			((IRangeQuery)this)._To = to;
			((IRangeQuery)this)._ToInclusive = to.HasValue ? new Nullable<bool>(true) : null;
			return this;
		}
		#endregion

		#region double
		/// <summary>
		/// The upper bound. Defaults to unbounded.
		/// </summary>
		public RangeQueryDescriptor<T> To(double? to)
		{
			((IRangeQuery)this)._To = to;
			return this;
		}
		/// <summary>
		/// The lower bound. Defaults to start from the first.
		/// </summary>
		/// <returns></returns>
		public RangeQueryDescriptor<T> From(double? from)
		{
			((IRangeQuery)this)._From = from;
			return this;
		}

		/// <summary>
		/// Same as setting from and include_lower to false.
		/// </summary>
		public RangeQueryDescriptor<T> Greater(double? from)
		{
			((IRangeQuery)this)._From = from;
			((IRangeQuery)this)._FromInclusive = from.HasValue ? new Nullable<bool>(false) : null;
			return this;
		}
		/// <summary>
		/// Same as setting from and include_lower to true.
		/// </summary>
		public RangeQueryDescriptor<T> GreaterOrEquals(double? from)
		{
			((IRangeQuery)this)._From = from;
			((IRangeQuery)this)._FromInclusive = from.HasValue ? new Nullable<bool>(true) : null;
			return this;
		}
		/// <summary>
		/// Same as setting to and include_upper to false.
		/// </summary>
		public RangeQueryDescriptor<T> Lower(double? to)
		{
			((IRangeQuery)this)._To = to;
			((IRangeQuery)this)._ToInclusive = to.HasValue ? new Nullable<bool>(false) : null;
			return this;
		}
		/// <summary>
		/// Same as setting to and include_upper to true.
		/// </summary>
		public RangeQueryDescriptor<T> LowerOrEquals(double? to)
		{
			((IRangeQuery)this)._To = to;
			((IRangeQuery)this)._ToInclusive = to.HasValue ? new Nullable<bool>(true) : null;
			return this;
		}
		#endregion

		#region string
		/// <summary>
		/// The upper bound. Defaults to unbounded.
		/// </summary>
		public RangeQueryDescriptor<T> To(string to)
		{
			((IRangeQuery)this)._To = to;
			return this;
		}
		/// <summary>
		/// The lower bound. Defaults to start from the first.
		/// </summary>
		/// <returns></returns>
		public RangeQueryDescriptor<T> From(string from)
		{
			((IRangeQuery)this)._From = from;
			return this;
		}

		/// <summary>
		/// Same as setting from and include_lower to false.
		/// </summary>
		public RangeQueryDescriptor<T> Greater(string from)
		{
			((IRangeQuery)this)._From = from;
			((IRangeQuery)this)._FromInclusive = !from.IsNullOrEmpty() ? new Nullable<bool>(false) : null;
			return this;
		}
		/// <summary>
		/// Same as setting from and include_lower to true.
		/// </summary>
		public RangeQueryDescriptor<T> GreaterOrEquals(string from)
		{
			((IRangeQuery)this)._From = from;
			((IRangeQuery)this)._FromInclusive = !from.IsNullOrEmpty() ? new Nullable<bool>(true) : null;
			return this;
		}
		/// <summary>
		/// Same as setting to and include_upper to false.
		/// </summary>
		public RangeQueryDescriptor<T> Lower(string to)
		{
			((IRangeQuery)this)._To = to;
			((IRangeQuery)this)._ToInclusive = !to.IsNullOrEmpty() ? new Nullable<bool>(false) : null;
			return this;
		}
		/// <summary>
		/// Same as setting to and include_upper to true.
		/// </summary>
		public RangeQueryDescriptor<T> LowerOrEquals(string to)
		{
			((IRangeQuery)this)._To = to;
			((IRangeQuery)this)._ToInclusive = !to.IsNullOrEmpty() ? new Nullable<bool>(true) : null;
			return this;
		}
		#endregion

		#region DateTime
		/// <summary>
		/// The upper bound. Defaults to unbounded.
		/// </summary>
		public RangeQueryDescriptor<T> To(DateTime? to, string format = "yyyy-MM-dd'T'HH:mm:ss")
		{
			if (!to.HasValue)
				return this;
			((IRangeQuery)this)._To = to.Value.ToString(format);
			return this;
		}
		/// <summary>
		/// The lower bound. Defaults to start from the first.
		/// </summary>
		/// <returns></returns>
		public RangeQueryDescriptor<T> From(DateTime? from, string format = "yyyy-MM-dd'T'HH:mm:ss")
		{
			if (!from.HasValue)
				return this;

			((IRangeQuery)this)._From = from.Value.ToString(format);
			return this;
		}

		/// <summary>
		/// Same as setting from and include_lower to false.
		/// </summary>
		public RangeQueryDescriptor<T> Greater(DateTime? from, string format = "yyyy-MM-dd'T'HH:mm:ss")
		{
			if (!from.HasValue)
				return this;

			((IRangeQuery)this)._From = from.Value.ToString(format);
			((IRangeQuery)this)._FromInclusive = false;
			return this;
		}
		/// <summary>
		/// Same as setting from and include_lower to true.
		/// </summary>
		public RangeQueryDescriptor<T> GreaterOrEquals(DateTime? from, string format = "yyyy-MM-dd'T'HH:mm:ss")
		{
			if (!from.HasValue)
				return this;

			((IRangeQuery)this)._From = from.Value.ToString(format);
			((IRangeQuery)this)._FromInclusive = true;
			return this;
		}
		/// <summary>
		/// Same as setting to and include_upper to false.
		/// </summary>
		public RangeQueryDescriptor<T> Lower(DateTime? to, string format = "yyyy-MM-dd'T'HH:mm:ss")
		{
			if (!to.HasValue)
				return this;

			((IRangeQuery)this)._To = to.Value.ToString(format);
			((IRangeQuery)this)._ToInclusive = false;
			return this;
		}
		/// <summary>
		/// Same as setting to and include_upper to true.
		/// </summary>
		public RangeQueryDescriptor<T> LowerOrEquals(DateTime? to, string format = "yyyy-MM-dd'T'HH:mm:ss")
		{
			if (!to.HasValue)
				return this;

			((IRangeQuery)this)._To = to.Value.ToString(format);
			((IRangeQuery)this)._ToInclusive = true;
			return this;
		}
		#endregion



	}
}
