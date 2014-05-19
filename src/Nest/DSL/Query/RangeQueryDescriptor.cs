using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest.DSL.Query.Behaviour;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Nest.Resolvers;
using Elasticsearch.Net;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IRangeQuery : IFieldNameQuery
	{
		[JsonProperty("from")]
		object From { get; set; }

		[JsonProperty("to")]
		object To { get; set; }

		[JsonProperty("include_lower")]
		bool? FromInclusive { get; set; }

		[JsonProperty("include_upper")]
		bool? ToInclusive { get; set; }

		[JsonProperty(PropertyName = "boost")]
		double? Boost { get; set; }

		[JsonProperty(PropertyName = "_cache")]
		bool? Cache { get; set; }

		[JsonProperty(PropertyName = "_name")]
		string Name { get; set; }

		PropertyPathMarker Field { get; set; }
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class RangeQueryDescriptor<T> : IRangeQuery where T : class
	{
		object IRangeQuery.From { get; set; }
	
		object IRangeQuery.To { get; set; }
		
		bool? IRangeQuery.FromInclusive { get; set; }
		
		bool? IRangeQuery.ToInclusive { get; set; }
		
		double? IRangeQuery.Boost { get; set; }
		
		bool? IRangeQuery.Cache { get; set; }
		
		string IRangeQuery.Name { get; set; }

		PropertyPathMarker IRangeQuery.Field { get; set; }
		
		bool IQuery.IsConditionless
		{
			get
			{
				return ((IRangeQuery)this).Field.IsConditionless() || (((IRangeQuery)this).From == null && ((IRangeQuery)this).To == null);
			}
		}
		void IFieldNameQuery.SetFieldName(string fieldName)
		{
			((IRangeQuery)this).Field = fieldName;
		}
		PropertyPathMarker IFieldNameQuery.GetFieldName()
		{
			return ((IRangeQuery)this).Field;
		}


		public RangeQueryDescriptor<T> OnField(string field)
		{
			((IRangeQuery)this).Field = field;
			return this;
		}
		public RangeQueryDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
		{
			((IRangeQuery)this).Field = objectPath;
			return this;
		}
		/// <summary>
		/// Forces the 'To()' to be exclusive (which is inclusive by default).
		/// </summary>
		public RangeQueryDescriptor<T> ToExclusive()
		{
			((IRangeQuery)this).ToInclusive = false;
			return this;
		}
		/// <summary>
		/// Forces the 'From()' to be exclusive (which is inclusive by default).
		/// </summary>
		public RangeQueryDescriptor<T> FromExclusive()
		{
			((IRangeQuery)this).FromInclusive = false;
			return this;
		}
		
		/// <summary>
		/// Boosts the range query by the specified boost factor
		/// </summary>
		/// <param name="boost">Boost factor</param>
		public RangeQueryDescriptor<T> Boost(double boost)
		{
			((IRangeQuery)this).Boost = boost;
			return this;
		}


		#region int
		/// <summary>
		/// The upper bound. Defaults to unbounded.
		/// </summary>
		public RangeQueryDescriptor<T> To(int? to)
		{
			((IRangeQuery)this).To = to;
			return this;
		}
		/// <summary>
		/// The lower bound. Defaults to start from the first.
		/// </summary>
		/// <returns></returns>
		public RangeQueryDescriptor<T> From(int? from)
		{
			((IRangeQuery)this).From = from;
			return this;
		}

		/// <summary>
		/// Same as setting from and include_lower to false.
		/// </summary>
		public RangeQueryDescriptor<T> Greater(int? from)
		{
			((IRangeQuery)this).From = from;
			((IRangeQuery)this).FromInclusive = false;
			return this;
		}
		/// <summary>
		/// Same as setting from and include_lower to true.
		/// </summary>
		public RangeQueryDescriptor<T> GreaterOrEquals(int? from)
		{
			((IRangeQuery)this).From = from;
			((IRangeQuery)this).FromInclusive = from.HasValue ? new Nullable<bool>(true) : null;
			return this;
		}
		/// <summary>
		/// Same as setting to and include_upper to false.
		/// </summary>
		public RangeQueryDescriptor<T> Lower(int? to)
		{
			((IRangeQuery)this).To = to;
			((IRangeQuery)this).ToInclusive = to.HasValue ? new Nullable<bool>(false) : null;
			return this;
		}
		/// <summary>
		/// Same as setting to and include_upper to true.
		/// </summary>
		public RangeQueryDescriptor<T> LowerOrEquals(int? to)
		{
			((IRangeQuery)this).To = to;
			((IRangeQuery)this).ToInclusive = to.HasValue ? new Nullable<bool>(true) : null;
			return this;
		}
		#endregion

		#region double
		/// <summary>
		/// The upper bound. Defaults to unbounded.
		/// </summary>
		public RangeQueryDescriptor<T> To(double? to)
		{
			((IRangeQuery)this).To = to;
			return this;
		}
		/// <summary>
		/// The lower bound. Defaults to start from the first.
		/// </summary>
		/// <returns></returns>
		public RangeQueryDescriptor<T> From(double? from)
		{
			((IRangeQuery)this).From = from;
			return this;
		}

		/// <summary>
		/// Same as setting from and include_lower to false.
		/// </summary>
		public RangeQueryDescriptor<T> Greater(double? from)
		{
			((IRangeQuery)this).From = from;
			((IRangeQuery)this).FromInclusive = from.HasValue ? new Nullable<bool>(false) : null;
			return this;
		}
		/// <summary>
		/// Same as setting from and include_lower to true.
		/// </summary>
		public RangeQueryDescriptor<T> GreaterOrEquals(double? from)
		{
			((IRangeQuery)this).From = from;
			((IRangeQuery)this).FromInclusive = from.HasValue ? new Nullable<bool>(true) : null;
			return this;
		}
		/// <summary>
		/// Same as setting to and include_upper to false.
		/// </summary>
		public RangeQueryDescriptor<T> Lower(double? to)
		{
			((IRangeQuery)this).To = to;
			((IRangeQuery)this).ToInclusive = to.HasValue ? new Nullable<bool>(false) : null;
			return this;
		}
		/// <summary>
		/// Same as setting to and include_upper to true.
		/// </summary>
		public RangeQueryDescriptor<T> LowerOrEquals(double? to)
		{
			((IRangeQuery)this).To = to;
			((IRangeQuery)this).ToInclusive = to.HasValue ? new Nullable<bool>(true) : null;
			return this;
		}
		#endregion

		#region string
		/// <summary>
		/// The upper bound. Defaults to unbounded.
		/// </summary>
		public RangeQueryDescriptor<T> To(string to)
		{
			((IRangeQuery)this).To = to;
			return this;
		}
		/// <summary>
		/// The lower bound. Defaults to start from the first.
		/// </summary>
		/// <returns></returns>
		public RangeQueryDescriptor<T> From(string from)
		{
			((IRangeQuery)this).From = from;
			return this;
		}

		/// <summary>
		/// Same as setting from and include_lower to false.
		/// </summary>
		public RangeQueryDescriptor<T> Greater(string from)
		{
			((IRangeQuery)this).From = from;
			((IRangeQuery)this).FromInclusive = !from.IsNullOrEmpty() ? new Nullable<bool>(false) : null;
			return this;
		}
		/// <summary>
		/// Same as setting from and include_lower to true.
		/// </summary>
		public RangeQueryDescriptor<T> GreaterOrEquals(string from)
		{
			((IRangeQuery)this).From = from;
			((IRangeQuery)this).FromInclusive = !from.IsNullOrEmpty() ? new Nullable<bool>(true) : null;
			return this;
		}
		/// <summary>
		/// Same as setting to and include_upper to false.
		/// </summary>
		public RangeQueryDescriptor<T> Lower(string to)
		{
			((IRangeQuery)this).To = to;
			((IRangeQuery)this).ToInclusive = !to.IsNullOrEmpty() ? new Nullable<bool>(false) : null;
			return this;
		}
		/// <summary>
		/// Same as setting to and include_upper to true.
		/// </summary>
		public RangeQueryDescriptor<T> LowerOrEquals(string to)
		{
			((IRangeQuery)this).To = to;
			((IRangeQuery)this).ToInclusive = !to.IsNullOrEmpty() ? new Nullable<bool>(true) : null;
			return this;
		}
		#endregion

		#region DateTime
		/// <summary>
		/// The upper bound. Defaults to unbounded.
		/// </summary>
		public RangeQueryDescriptor<T> To(DateTime? to, string format = "yyyy-MM-dd'T'HH:mm:ss.fff")
		{
			if (!to.HasValue)
				return this;
			((IRangeQuery)this).To = to.Value.ToString(format);
			return this;
		}
		/// <summary>
		/// The lower bound. Defaults to start from the first.
		/// </summary>
		/// <returns></returns>
		public RangeQueryDescriptor<T> From(DateTime? from, string format = "yyyy-MM-dd'T'HH:mm:ss.fff")
		{
			if (!from.HasValue)
				return this;

			((IRangeQuery)this).From = from.Value.ToString(format);
			return this;
		}

		/// <summary>
		/// Same as setting from and include_lower to false.
		/// </summary>
		public RangeQueryDescriptor<T> Greater(DateTime? from, string format = "yyyy-MM-dd'T'HH:mm:ss.fff")
		{
			if (!from.HasValue)
				return this;

			((IRangeQuery)this).From = from.Value.ToString(format);
			((IRangeQuery)this).FromInclusive = false;
			return this;
		}
		/// <summary>
		/// Same as setting from and include_lower to true.
		/// </summary>
		public RangeQueryDescriptor<T> GreaterOrEquals(DateTime? from, string format = "yyyy-MM-dd'T'HH:mm:ss.fff")
		{
			if (!from.HasValue)
				return this;

			((IRangeQuery)this).From = from.Value.ToString(format);
			((IRangeQuery)this).FromInclusive = true;
			return this;
		}
		/// <summary>
		/// Same as setting to and include_upper to false.
		/// </summary>
		public RangeQueryDescriptor<T> Lower(DateTime? to, string format = "yyyy-MM-dd'T'HH:mm:ss.fff")
		{
			if (!to.HasValue)
				return this;

			((IRangeQuery)this).To = to.Value.ToString(format);
			((IRangeQuery)this).ToInclusive = false;
			return this;
		}
		/// <summary>
		/// Same as setting to and include_upper to true.
		/// </summary>
		public RangeQueryDescriptor<T> LowerOrEquals(DateTime? to, string format = "yyyy-MM-dd'T'HH:mm:ss.fff")
		{
			if (!to.HasValue)
				return this;

			((IRangeQuery)this).To = to.Value.ToString(format);
			((IRangeQuery)this).ToInclusive = true;
			return this;
		}
		#endregion



	}
}
