using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Nest.DSL.Query.Behaviour;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IRangeQuery : IFieldNameQuery
	{
		[JsonProperty("gte")]
		string GreaterThanOrEqualTo { get; set; }

		[JsonProperty("lte")]
		string LowerThanOrEqualTo { get; set; }
		
		[JsonProperty("gt")]
		string GreaterThan { get; set; }

		[JsonProperty("lt")]
		string LowerThan { get; set; }

		[JsonProperty(PropertyName = "boost")]
		double? Boost { get; set; }

		[JsonProperty(PropertyName = "_cache")]
		[Obsolete("scheduled to be removed in 2.o copy paste errror from filters")]
		bool? Cache { get; set; }

		[JsonProperty("time_zone")]
		string TimeZone { get; set; }

		PropertyPathMarker Field { get; set; }
	}
	public class RangeQuery : PlainQuery, IRangeQuery
	{
		protected override void WrapInContainer(IQueryContainer container)
		{
			container.Range = this;
		}

		bool IQuery.IsConditionless { get { return false; } }
		PropertyPathMarker IFieldNameQuery.GetFieldName()
		{
			return this.Field;
		}

		void IFieldNameQuery.SetFieldName(string fieldName)
		{
			this.Field = fieldName;
		}

		public string GreaterThanOrEqualTo { get; set; }
		public string LowerThanOrEqualTo { get; set; }
		public string GreaterThan { get; set; }
		public string LowerThan { get; set; }
		public double? Boost { get; set; }
		public bool? Cache { get; set; }
		public string Name { get; set; }
		public string TimeZone { get; set; }
		public PropertyPathMarker Field { get; set; }
	}
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class RangeQueryDescriptor<T> : IRangeQuery where T : class
	{
		private IRangeQuery Self { get { return this; } }

		string IRangeQuery.GreaterThanOrEqualTo { get; set; }
	
		string IRangeQuery.LowerThanOrEqualTo { get; set; }
		
		string IRangeQuery.GreaterThan { get; set; }
	
		string IRangeQuery.LowerThan { get; set; }
		
		double? IRangeQuery.Boost { get; set; }
		
		bool? IRangeQuery.Cache { get; set; }

		string IRangeQuery.TimeZone { get; set; }

		PropertyPathMarker IRangeQuery.Field { get; set; }
		
		bool IQuery.IsConditionless
		{
			get
			{
				return this.Self.Field.IsConditionless() 
					|| (
						this.Self.GreaterThanOrEqualTo == null
						&& this.Self.LowerThanOrEqualTo == null
						&& this.Self.GreaterThan == null
						&& this.Self.LowerThan == null
					);
			}
		}

		string IQuery.Name { get; set; }

		void IFieldNameQuery.SetFieldName(string fieldName)
		{
			this.Self.Field = fieldName;
		}

		PropertyPathMarker IFieldNameQuery.GetFieldName()
		{
			return this.Self.Field;
		}

		public RangeQueryDescriptor<T> Name(string name)
		{
			Self.Name = name;
			return this;
		}
		public RangeQueryDescriptor<T> OnField(string field)
		{
			this.Self.Field = field;
			return this;
		}

		public RangeQueryDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
		{
			this.Self.Field = objectPath;
			return this;
		}
		
		/// <summary>
		/// Boosts the range query by the specified boost factor
		/// </summary>
		/// <param name="boost">Boost factor</param>
		public RangeQueryDescriptor<T> Boost(double boost)
		{
			this.Self.Boost = boost;
			return this;
		}

		public RangeQueryDescriptor<T> Greater(double? from)
		{
			this.Self.GreaterThan = from.HasValue ? from.Value.ToString(CultureInfo.InvariantCulture) : null;
			return this;
		}
		public RangeQueryDescriptor<T> GreaterOrEquals(double? from)
		{
			this.Self.GreaterThanOrEqualTo = from.HasValue ? from.Value.ToString(CultureInfo.InvariantCulture) : null;
			return this;
		}
		public RangeQueryDescriptor<T> Lower(double? to)
		{
			this.Self.LowerThan = to.HasValue ? to.Value.ToString(CultureInfo.InvariantCulture) : null;
			return this;
		}
		public RangeQueryDescriptor<T> LowerOrEquals(double? to)
		{
			this.Self.LowerThanOrEqualTo = to.HasValue ? to.Value.ToString(CultureInfo.InvariantCulture) : null;
			return this;
		}

		public RangeQueryDescriptor<T> Greater(string from)
		{
			this.Self.GreaterThan = from;
			return this;
		}

		public RangeQueryDescriptor<T> GreaterOrEquals(string from)
		{
			this.Self.GreaterThanOrEqualTo = from;
			return this;
		}
		public RangeQueryDescriptor<T> Lower(string to)
		{
			this.Self.LowerThan = to;
			return this;
		}
		public RangeQueryDescriptor<T> LowerOrEquals(string to)
		{
			this.Self.LowerThanOrEqualTo = to;
			return this;
		}

		public RangeQueryDescriptor<T> Greater(DateTime? from, string format = "yyyy-MM-dd'T'HH:mm:ss.fff")
		{
			this.Self.GreaterThan = from.HasValue ? from.Value.ToString(format) : null;
			return this;
		}

		public RangeQueryDescriptor<T> GreaterOrEquals(DateTime? from, string format = "yyyy-MM-dd'T'HH:mm:ss.fff")
		{
			this.Self.GreaterThanOrEqualTo = from.HasValue ? from.Value.ToString(format) : null;
			return this;
		}

		public RangeQueryDescriptor<T> Lower(DateTime? to, string format = "yyyy-MM-dd'T'HH:mm:ss.fff")
		{
			this.Self.LowerThan = to.HasValue ? to.Value.ToString(format) : null;
			return this;
		}

		public RangeQueryDescriptor<T> LowerOrEquals(DateTime? to, string format = "yyyy-MM-dd'T'HH:mm:ss.fff")
		{
			this.Self.LowerThanOrEqualTo = to.HasValue ? to.Value.ToString(format) : null;
			return this;
		}

		public RangeQueryDescriptor<T> TimeZone(string timeZone)
		{
			this.Self.TimeZone = timeZone;
			return this;
		}

	}
}
