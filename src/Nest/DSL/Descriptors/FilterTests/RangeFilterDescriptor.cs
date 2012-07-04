using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace Nest
{
	[JsonObject(MemberSerialization=MemberSerialization.OptIn)]
	public class RangeFilterDescriptor<T> : FilterBase where T : class
	{
		[JsonProperty("from")]
		internal string _From { get; set;}
		[JsonProperty("to")]
		internal string _To { get; set; }
		[JsonProperty("include_lower")]
		internal bool? _FromInclusive { get; set; }
		[JsonProperty("include_upper")]
		internal bool? _ToInclusive { get; set; }

		internal string _Field { get; set; }


		public RangeFilterDescriptor<T> OnField(string field)
		{
			this._Field = field;
			return this;
		}
		public RangeFilterDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
		{
			var fieldName = ElasticClient.PropertyNameResolver.ResolveToSort(objectPath);
			return this.OnField(fieldName);
		}

		/// <summary>
		/// The lower bound. Defaults to start from the first.
		/// </summary>
		/// <returns></returns>
		public RangeFilterDescriptor<T> From(string from)
		{
			this._From = from;
			return this;
		}
		/// <summary>
		/// Forces the 'From()' to be exclusive (which is inclusive by default).
		/// </summary>
		public RangeFilterDescriptor<T> FromExclusive()
		{
			this._FromInclusive = false;
			return this;
		}
		/// <summary>
		/// The upper bound. Defaults to unbounded.
		/// </summary>
		public RangeFilterDescriptor<T> To(string to)
		{
			this._To = to;
			return this;
		}
		/// <summary>
		/// Forces the 'To()' to be exclusive (which is inclusive by default).
		/// </summary>
		public RangeFilterDescriptor<T> ToExclusive()
		{
			this._ToInclusive = false;
			return this;
		}
		/// <summary>
		/// Same as setting from and from_inclusive to false.
		/// </summary>
		public RangeFilterDescriptor<T> Greater(string from)
		{
			this._From = from;
			this._FromInclusive = false;
			return this;
		}
		/// <summary>
		/// Same as setting from and from_inclusive to true.
		/// </summary>
		public RangeFilterDescriptor<T> GreaterOrEquals(string from)
		{
			this._From = from;
			this._FromInclusive = true;
			return this;
		}
		/// <summary>
		/// Same as setting to and to_inclusive to false.
		/// </summary>
		public RangeFilterDescriptor<T> Lower(string to)
		{
			this._To = to;
			this._ToInclusive = false;
			return this;
		}
		/// <summary>
		/// Same as setting to and to_inclusive to true.
		/// </summary>
		public RangeFilterDescriptor<T> LowerOrEquals(string to)
		{
			this._To = to;
			this._ToInclusive = true;
			return this;
		}
	}
}
