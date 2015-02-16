using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Nest.Resolvers.Converters;
using Nest.Resolvers.Converters.Filters;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace Nest
{
	[JsonConverter(typeof(RangeFilterJsonConverter))]
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

		[JsonProperty("time_zone")]
		string TimeZone { get; set; }

		[JsonProperty("execution")]
		RangeExecution? Execution { get; set; }
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
		public string TimeZone { get; set; }
		public RangeExecution? Execution { get; set; }
		public PropertyPathMarker Field { get; set; }
	}

	public class RangeFilterDescriptor<T> : FilterBase, IRangeFilter where T : class
	{
		string IRangeFilter.GreaterThanOrEqualTo { get; set;  }
		
		string IRangeFilter.LowerThanOrEqualTo { get; set; }
		
		string IRangeFilter.GreaterThan { get; set; }
		
		string IRangeFilter.LowerThan { get; set; }

		string IRangeFilter.TimeZone { get; set; }

		RangeExecution? IRangeFilter.Execution { get; set; }

		PropertyPathMarker IFieldNameFilter.Field { get; set; }

		private IRangeFilter Self { get { return this;  } }

		bool IFilter.IsConditionless
		{
			get
			{
				return this.Self.Field.IsConditionless() || 
				(	this.Self.GreaterThanOrEqualTo.IsNullOrEmpty() 
					&& this.Self.LowerThanOrEqualTo.IsNullOrEmpty()
					&& this.Self.LowerThan.IsNullOrEmpty()
					&& this.Self.GreaterThan.IsNullOrEmpty()
				);
			}
		}

		public RangeFilterDescriptor<T> OnField(string field)
		{
			this.Self.Field = field;
			return this;
		}
		public RangeFilterDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
		{
			this.Self.Field = objectPath;
			return this;
		}

		public RangeFilterDescriptor<T> Greater(long? from)
		{
			this.Self.GreaterThan = from.HasValue ? from.Value.ToString(CultureInfo.InvariantCulture) : null;
			return this;
		}
	
		public RangeFilterDescriptor<T> GreaterOrEquals(long? from)
		{
			this.Self.GreaterThanOrEqualTo = from.HasValue ? from.Value.ToString(CultureInfo.InvariantCulture) : null;
			return this;
		}
	
		public RangeFilterDescriptor<T> Lower(long? to)
		{
			this.Self.LowerThan = to.HasValue ? to.Value.ToString(CultureInfo.InvariantCulture) : null;
			return this;
		}

		public RangeFilterDescriptor<T> LowerOrEquals(long? to)
		{
			this.Self.LowerThanOrEqualTo = to.HasValue ? to.Value.ToString(CultureInfo.InvariantCulture) : null;
			return this;
		}

		public RangeFilterDescriptor<T> Greater(double? from)
		{
			this.Self.GreaterThan = from.HasValue ? from.Value.ToString(CultureInfo.InvariantCulture) : null;
			return this;
		}
		
		public RangeFilterDescriptor<T> GreaterOrEquals(double? from)
		{
			this.Self.GreaterThanOrEqualTo = from.HasValue ? from.Value.ToString(CultureInfo.InvariantCulture) : null;
			return this;
		}
		
		public RangeFilterDescriptor<T> Lower(double? to)
		{
			this.Self.LowerThan = to.HasValue ? to.Value.ToString(CultureInfo.InvariantCulture) : null;
			return this;
		}
		/// <summary>
		/// Same as setting to and include_upper to true.
		/// </summary>
		public RangeFilterDescriptor<T> LowerOrEquals(double? to)
		{
			this.Self.LowerThanOrEqualTo = to.HasValue ? to.Value.ToString(CultureInfo.InvariantCulture) : null;
			return this;
		}
		

		public RangeFilterDescriptor<T> Greater(string from)
		{
			this.Self.GreaterThan = from;
			return this;
		}

		public RangeFilterDescriptor<T> GreaterOrEquals(string from)
		{
			this.Self.GreaterThanOrEqualTo = from;
			return this;
		}

		public RangeFilterDescriptor<T> Lower(string to)
		{
			this.Self.LowerThan = to;
			return this;
		}

		public RangeFilterDescriptor<T> LowerOrEquals(string to)
		{
			this.Self.LowerThanOrEqualTo = to;
			return this;
		}

		public RangeFilterDescriptor<T> Greater(DateTime? from, string format = "yyyy-MM-dd'T'HH:mm:ss.fff")
		{
			if (!from.HasValue) return this;
			this.Self.GreaterThan = from.Value.ToString(format, CultureInfo.InvariantCulture);
			return this;
		}
		
		public RangeFilterDescriptor<T> GreaterOrEquals(DateTime? from, string format = "yyyy-MM-dd'T'HH:mm:ss.fff")
		{
			if (!from.HasValue) return this;
			this.Self.GreaterThanOrEqualTo = from.Value.ToString(format, CultureInfo.InvariantCulture);
			return this;
		}

		public RangeFilterDescriptor<T> Lower(DateTime? to, string format = "yyyy-MM-dd'T'HH:mm:ss.fff")
		{
			if (!to.HasValue) return this;
			this.Self.LowerThan = to.Value.ToString(format, CultureInfo.InvariantCulture);
			return this;
		}

		public RangeFilterDescriptor<T> LowerOrEquals(DateTime? to, string format = "yyyy-MM-dd'T'HH:mm:ss.fff")
		{
			if (!to.HasValue) return this;
			this.Self.LowerThanOrEqualTo = to.Value.ToString(format, CultureInfo.InvariantCulture);
			return this;
		}
	
		public RangeFilterDescriptor<T> TimeZone(string timeZone)
		{
			this.Self.TimeZone = timeZone;
			return this;
		}

		public RangeFilterDescriptor<T> Execution(RangeExecution? execution)
		{
			this.Self.Execution = execution;
			return this;
		} 
	}
}
