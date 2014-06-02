using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Nest.Resolvers.Converters;
using Nest.Resolvers.Converters.Filters;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Nest.Resolvers;
using Elasticsearch.Net;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IRangeFilter : IFieldNameFilter
	{
		[JsonProperty("gte")]
		[JsonConverter(typeof(ForceStringReader))]
		string GreaterThanOrEqualTo { get; set; }

		[JsonProperty("lte")]
		[JsonConverter(typeof(ForceStringReader))]
		string LowerThanOrEqualTo { get; set; }

		[JsonProperty("gt")]
		[JsonConverter(typeof(ForceStringReader))]
		string GreaterThan { get; set; }

		[JsonProperty("lt")]
		[JsonConverter(typeof(ForceStringReader))]
		string LowerThan { get; set; }
	}

	public class RangeFilter : PlainFilter, IRangeFilter
	{
		protected internal override void WrapInContainer(IFilterContainer container)
		{
			container.Range = this;
		}
		public string GreaterThanOrEqualTo { get; set; }
		public string LowerThanOrEqualTo { get; set; }
		public string GreaterThan { get; set; }
		public string LowerThan { get; set; }
		public PropertyPathMarker Field { get; set; }
	}

	public class RangeFilterDescriptor<T> : FilterBase, IRangeFilter where T : class
	{
		string IRangeFilter.GreaterThanOrEqualTo { get; set;  }
		
		string IRangeFilter.LowerThanOrEqualTo { get; set; }
		
		string IRangeFilter.GreaterThan { get; set; }
		
		string IRangeFilter.LowerThan { get; set; }

		PropertyPathMarker IFieldNameFilter.Field { get; set; }

		private IRangeFilter _ { get { return this;  } }

		bool IFilter.IsConditionless
		{
			get
			{
				return _.Field.IsConditionless() || 
				(	_.GreaterThanOrEqualTo.IsNullOrEmpty() 
					&& _.LowerThanOrEqualTo.IsNullOrEmpty()
					&& _.LowerThan.IsNullOrEmpty()
					&& _.GreaterThan.IsNullOrEmpty()
				);
			}
		}
		
		public RangeFilterDescriptor<T> OnField(string field)
		{
			_.Field = field;
			return this;
		}
		public RangeFilterDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
		{
			_.Field = objectPath;
			return this;
		}

		public RangeFilterDescriptor<T> Greater(long? from)
		{
			_.GreaterThan = from.HasValue ? from.Value.ToString(CultureInfo.InvariantCulture) : null;
			return this;
		}
	
		public RangeFilterDescriptor<T> GreaterOrEquals(long? from)
		{
			_.GreaterThanOrEqualTo = from.HasValue ? from.Value.ToString(CultureInfo.InvariantCulture) : null;
			return this;
		}
	
		public RangeFilterDescriptor<T> Lower(long? to)
		{
			_.LowerThan = to.HasValue ? to.Value.ToString(CultureInfo.InvariantCulture) : null;
			return this;
		}

		public RangeFilterDescriptor<T> LowerOrEquals(long? to)
		{
			_.LowerThanOrEqualTo = to.HasValue ? to.Value.ToString(CultureInfo.InvariantCulture) : null;
			return this;
		}

		public RangeFilterDescriptor<T> Greater(double? from)
		{
			_.GreaterThan = from.HasValue ? from.Value.ToString(CultureInfo.InvariantCulture) : null;
			return this;
		}
		
		public RangeFilterDescriptor<T> GreaterOrEquals(double? from)
		{
			_.GreaterThanOrEqualTo = from.HasValue ? from.Value.ToString(CultureInfo.InvariantCulture) : null;
			return this;
		}
		
		public RangeFilterDescriptor<T> Lower(double? to)
		{
			_.LowerThan = to.HasValue ? to.Value.ToString(CultureInfo.InvariantCulture) : null;
			return this;
		}
		/// <summary>
		/// Same as setting to and include_upper to true.
		/// </summary>
		public RangeFilterDescriptor<T> LowerOrEquals(double? to)
		{
			_.LowerThanOrEqualTo = to.HasValue ? to.Value.ToString(CultureInfo.InvariantCulture) : null;
			return this;
		}
		

		public RangeFilterDescriptor<T> Greater(string from)
		{
			_.GreaterThan = from;
			return this;
		}

		public RangeFilterDescriptor<T> GreaterOrEquals(string from)
		{
			_.GreaterThanOrEqualTo = from;
			return this;
		}

		public RangeFilterDescriptor<T> Lower(string to)
		{
			_.LowerThan = to;
			return this;
		}

		public RangeFilterDescriptor<T> LowerOrEquals(string to)
		{
			_.LowerThanOrEqualTo = to;
			return this;
		}

		public RangeFilterDescriptor<T> Greater(DateTime? from, string format = "yyyy-MM-dd'T'HH:mm:ss.fff")
		{
			if (!from.HasValue) return this;
			_.GreaterThan = from.Value.ToString(format, CultureInfo.InvariantCulture);
			return this;
		}
		
		public RangeFilterDescriptor<T> GreaterOrEquals(DateTime? from, string format = "yyyy-MM-dd'T'HH:mm:ss.fff")
		{
			if (!from.HasValue) return this;
			_.GreaterThanOrEqualTo = from.Value.ToString(format, CultureInfo.InvariantCulture);
			return this;
		}

		public RangeFilterDescriptor<T> Lower(DateTime? to, string format = "yyyy-MM-dd'T'HH:mm:ss.fff")
		{
			if (!to.HasValue) return this;
			_.LowerThan = to.Value.ToString(format, CultureInfo.InvariantCulture);
			return this;
		}

		public RangeFilterDescriptor<T> LowerOrEquals(DateTime? to, string format = "yyyy-MM-dd'T'HH:mm:ss.fff")
		{
			if (!to.HasValue) return this;
			_.LowerThanOrEqualTo = to.Value.ToString(format, CultureInfo.InvariantCulture);
			return this;
		}
	
	}
}
