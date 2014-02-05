using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Nest.Resolvers;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class RangeQueryDescriptor<T> : IQuery where T : class
	{
		[JsonProperty("from")]
		internal object _From { get; set; }
		[JsonProperty("to")]
		internal object _To { get; set; }
		[JsonProperty("include_lower")]
		internal bool? _FromInclusive { get; set; }
		[JsonProperty("include_upper")]
		internal bool? _ToInclusive { get; set; }
		[JsonProperty(PropertyName = "boost")]
		internal double? _Boost { get; set; }
		[JsonProperty(PropertyName = "_cache")]
		internal bool? _Cache { get; set; }
		[JsonProperty(PropertyName = "_name")]
		internal string _Name { get; set; }

		internal PropertyPathMarker _Field { get; set; }
		bool IQuery.IsConditionless
		{
			get
			{
				return this._Field.IsConditionless() || (this._From == null && this._To == null);
			}
		}


		public RangeQueryDescriptor<T> OnField(string field)
		{
			this._Field = field;
			return this;
		}
		public RangeQueryDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
		{
			this._Field = objectPath;
			return this;
		}
		/// <summary>
		/// Forces the 'To()' to be exclusive (which is inclusive by default).
		/// </summary>
		public RangeQueryDescriptor<T> ToExclusive()
		{
			this._ToInclusive = false;
			return this;
		}
		/// <summary>
		/// Forces the 'From()' to be exclusive (which is inclusive by default).
		/// </summary>
		public RangeQueryDescriptor<T> FromExclusive()
		{
			this._FromInclusive = false;
			return this;
		}
		
		/// <summary>
		/// Boosts the range query by the specified boost factor
		/// </summary>
		/// <param name="boost">Boost factor</param>
		public RangeQueryDescriptor<T> Boost(double boost)
		{
			this._Boost = boost;
			return this;
		}


		#region int
		/// <summary>
		/// The upper bound. Defaults to unbounded.
		/// </summary>
		public RangeQueryDescriptor<T> To(int? to)
		{
			this._To = to;
			return this;
		}
		/// <summary>
		/// The lower bound. Defaults to start from the first.
		/// </summary>
		/// <returns></returns>
		public RangeQueryDescriptor<T> From(int? from)
		{
			this._From = from;
			return this;
		}

		/// <summary>
		/// Same as setting from and include_lower to false.
		/// </summary>
		public RangeQueryDescriptor<T> Greater(int? from)
		{
			this._From = from;
			this._FromInclusive = false;
			return this;
		}
		/// <summary>
		/// Same as setting from and include_lower to true.
		/// </summary>
		public RangeQueryDescriptor<T> GreaterOrEquals(int? from)
		{
			this._From = from;
			this._FromInclusive = from.HasValue ? new Nullable<bool>(true) : null;
			return this;
		}
		/// <summary>
		/// Same as setting to and include_upper to false.
		/// </summary>
		public RangeQueryDescriptor<T> Lower(int? to)
		{
			this._To = to;
			this._ToInclusive = to.HasValue ? new Nullable<bool>(false) : null;
			return this;
		}
		/// <summary>
		/// Same as setting to and include_upper to true.
		/// </summary>
		public RangeQueryDescriptor<T> LowerOrEquals(int? to)
		{
			this._To = to;
			this._ToInclusive = to.HasValue ? new Nullable<bool>(true) : null;
			return this;
		}
		#endregion

		#region double
		/// <summary>
		/// The upper bound. Defaults to unbounded.
		/// </summary>
		public RangeQueryDescriptor<T> To(double? to)
		{
			this._To = to;
			return this;
		}
		/// <summary>
		/// The lower bound. Defaults to start from the first.
		/// </summary>
		/// <returns></returns>
		public RangeQueryDescriptor<T> From(double? from)
		{
			this._From = from;
			return this;
		}

		/// <summary>
		/// Same as setting from and include_lower to false.
		/// </summary>
		public RangeQueryDescriptor<T> Greater(double? from)
		{
			this._From = from;
			this._FromInclusive = from.HasValue ? new Nullable<bool>(false) : null;
			return this;
		}
		/// <summary>
		/// Same as setting from and include_lower to true.
		/// </summary>
		public RangeQueryDescriptor<T> GreaterOrEquals(double? from)
		{
			this._From = from;
			this._FromInclusive = from.HasValue ? new Nullable<bool>(true) : null;
			return this;
		}
		/// <summary>
		/// Same as setting to and include_upper to false.
		/// </summary>
		public RangeQueryDescriptor<T> Lower(double? to)
		{
			this._To = to;
			this._ToInclusive = to.HasValue ? new Nullable<bool>(false) : null;
			return this;
		}
		/// <summary>
		/// Same as setting to and include_upper to true.
		/// </summary>
		public RangeQueryDescriptor<T> LowerOrEquals(double? to)
		{
			this._To = to;
			this._ToInclusive = to.HasValue ? new Nullable<bool>(true) : null;
			return this;
		}
		#endregion

		#region string
		/// <summary>
		/// The upper bound. Defaults to unbounded.
		/// </summary>
		public RangeQueryDescriptor<T> To(string to)
		{
			this._To = to;
			return this;
		}
		/// <summary>
		/// The lower bound. Defaults to start from the first.
		/// </summary>
		/// <returns></returns>
		public RangeQueryDescriptor<T> From(string from)
		{
			this._From = from;
			return this;
		}

		/// <summary>
		/// Same as setting from and include_lower to false.
		/// </summary>
		public RangeQueryDescriptor<T> Greater(string from)
		{
			this._From = from;
			this._FromInclusive = !from.IsNullOrEmpty() ? new Nullable<bool>(false) : null;
			return this;
		}
		/// <summary>
		/// Same as setting from and include_lower to true.
		/// </summary>
		public RangeQueryDescriptor<T> GreaterOrEquals(string from)
		{
			this._From = from;
			this._FromInclusive = !from.IsNullOrEmpty() ? new Nullable<bool>(true) : null;
			return this;
		}
		/// <summary>
		/// Same as setting to and include_upper to false.
		/// </summary>
		public RangeQueryDescriptor<T> Lower(string to)
		{
			this._To = to;
			this._ToInclusive = !to.IsNullOrEmpty() ? new Nullable<bool>(false) : null;
			return this;
		}
		/// <summary>
		/// Same as setting to and include_upper to true.
		/// </summary>
		public RangeQueryDescriptor<T> LowerOrEquals(string to)
		{
			this._To = to;
			this._ToInclusive = !to.IsNullOrEmpty() ? new Nullable<bool>(true) : null;
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
			this._To = to.Value.ToString(format);
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

			this._From = from.Value.ToString(format);
			return this;
		}

		/// <summary>
		/// Same as setting from and include_lower to false.
		/// </summary>
		public RangeQueryDescriptor<T> Greater(DateTime? from, string format = "yyyy-MM-dd'T'HH:mm:ss")
		{
			if (!from.HasValue)
				return this;

			this._From = from.Value.ToString(format);
			this._FromInclusive = false;
			return this;
		}
		/// <summary>
		/// Same as setting from and include_lower to true.
		/// </summary>
		public RangeQueryDescriptor<T> GreaterOrEquals(DateTime? from, string format = "yyyy-MM-dd'T'HH:mm:ss")
		{
			if (!from.HasValue)
				return this;

			this._From = from.Value.ToString(format);
			this._FromInclusive = true;
			return this;
		}
		/// <summary>
		/// Same as setting to and include_upper to false.
		/// </summary>
		public RangeQueryDescriptor<T> Lower(DateTime? to, string format = "yyyy-MM-dd'T'HH:mm:ss")
		{
			if (!to.HasValue)
				return this;

			this._To = to.Value.ToString(format);
			this._ToInclusive = false;
			return this;
		}
		/// <summary>
		/// Same as setting to and include_upper to true.
		/// </summary>
		public RangeQueryDescriptor<T> LowerOrEquals(DateTime? to, string format = "yyyy-MM-dd'T'HH:mm:ss")
		{
			if (!to.HasValue)
				return this;

			this._To = to.Value.ToString(format);
			this._ToInclusive = true;
			return this;
		}
		#endregion



	}
}
