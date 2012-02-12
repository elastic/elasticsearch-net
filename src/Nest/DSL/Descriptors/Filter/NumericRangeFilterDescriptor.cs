using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace Nest
{
	[JsonObject(MemberSerialization=MemberSerialization.OptIn)]
	public class NumericRangeFilterDescriptor<T> where T : class
	{
		[JsonProperty("from")]
		internal int? _From { get; set;}
		[JsonProperty("to")]
		internal int? _To { get; set; }
		[JsonProperty("from_inclusive")]
		internal bool? _FromInclusive { get; set; }
		[JsonProperty("to_inclusive")]
		internal bool? _ToInclusive { get; set; }

		internal bool? _Cache { get; set; }
		internal string _Field { get; set; }


		public NumericRangeFilterDescriptor<T> OnField(string field)
		{
			this._Field = field;
			return this;
		}
		public NumericRangeFilterDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
		{
			var fieldName = ElasticClient.PropertyNameResolver.ResolveToSort(objectPath);
			return this.OnField(fieldName);
		}

		/// <summary>
		/// The lower bound. Defaults to start from the first.
		/// </summary>
		/// <returns></returns>
		public NumericRangeFilterDescriptor<T> From(int from)
		{
			this._From = from;
			return this;
		}
		/// <summary>
		/// Forces the 'From()' to be exclusive (which is inclusive by default).
		/// </summary>
		public NumericRangeFilterDescriptor<T> FromExclusive()
		{
			this._FromInclusive = false;
			return this;
		}
		/// <summary>
		/// The upper bound. Defaults to unbounded.
		/// </summary>
		public NumericRangeFilterDescriptor<T> To(int to)
		{
			this._To = to;
			return this;
		}
		/// <summary>
		/// Forces the 'To()' to be exclusive (which is inclusive by default).
		/// </summary>
		public NumericRangeFilterDescriptor<T> ToExclusive()
		{
			this._ToInclusive = false;
			return this;
		}
		/// <summary>
		/// Same as setting from and from_inclusive to false.
		/// </summary>
		public NumericRangeFilterDescriptor<T> Greater(int from)
		{
			this._From = from;
			this._FromInclusive = false;
			return this;
		}
		/// <summary>
		/// Same as setting from and from_inclusive to true.
		/// </summary>
		public NumericRangeFilterDescriptor<T> GreaterOrEquals(int from)
		{
			this._From = from;
			this._FromInclusive = true;
			return this;
		}
		/// <summary>
		/// Same as setting to and to_inclusive to false.
		/// </summary>
		public NumericRangeFilterDescriptor<T> Lower(int to)
		{
			this._To = to;
			this._ToInclusive = false;
			return this;
		}
		/// <summary>
		/// Same as setting to and to_inclusive to true.
		/// </summary>
		public NumericRangeFilterDescriptor<T> LowerOrEquals(int to)
		{
			this._To = to;
			this._ToInclusive = true;
			return this;
		}
		/// <summary>
		/// The result of the filter is not cached by default. The _cache can be set to true to cache the result of the filter. 
		/// This is handy when the same points parameters are used on several (many) other queries. Note, the process of caching the first execution is higher when caching (since it needs to satisfy different queries).
		/// If caching the result of the filter is desired (for example, using the same “teen” filter with ages between 10 and 20), then it is advisable to simply use the range filter.
		/// </summary>
		public NumericRangeFilterDescriptor<T> Cache()
		{
			this._Cache = true;
			return this;
		}
	}
}
