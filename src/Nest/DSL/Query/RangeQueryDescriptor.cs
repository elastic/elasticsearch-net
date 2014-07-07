using System;
using System.Globalization;
using Nest.DSL.Query.Behaviour;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Nest.Resolvers;

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
		bool? Cache { get; set; }

		[JsonProperty(PropertyName = "_name")]
		string Name { get; set; }

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
		public PropertyPathMarker Field { get; set; }
	}
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class RangeQueryDescriptor<T> : IRangeQuery where T : class
	{
		string IRangeQuery.GreaterThanOrEqualTo { get; set; }
	
		string IRangeQuery.LowerThanOrEqualTo { get; set; }
		
		string IRangeQuery.GreaterThan { get; set; }
	
		string IRangeQuery.LowerThan { get; set; }
		
		double? IRangeQuery.Boost { get; set; }
		
		bool? IRangeQuery.Cache { get; set; }
		
		string IRangeQuery.Name { get; set; }

		PropertyPathMarker IRangeQuery.Field { get; set; }
		
		bool IQuery.IsConditionless
		{
			get
			{
				var rangeQuery = ((IRangeQuery)this);
				return rangeQuery.Field.IsConditionless() 
					|| (
						rangeQuery.GreaterThanOrEqualTo == null 
						&& rangeQuery.LowerThanOrEqualTo == null
						&& rangeQuery.GreaterThan == null
						&& rangeQuery.LowerThan == null
					);
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
		/// Boosts the range query by the specified boost factor
		/// </summary>
		/// <param name="boost">Boost factor</param>
		public RangeQueryDescriptor<T> Boost(double boost)
		{
			((IRangeQuery)this).Boost = boost;
			return this;
		}


		public RangeQueryDescriptor<T> Greater(double? from)
		{
			((IRangeQuery)this).GreaterThan = from.HasValue ? from.Value.ToString(CultureInfo.InvariantCulture) : null;
			return this;
		}
		public RangeQueryDescriptor<T> GreaterOrEquals(double? from)
		{
			((IRangeQuery)this).GreaterThanOrEqualTo = from.HasValue ? from.Value.ToString(CultureInfo.InvariantCulture) : null;
			return this;
		}
		public RangeQueryDescriptor<T> Lower(double? to)
		{
			((IRangeQuery)this).LowerThan = to.HasValue ? to.Value.ToString(CultureInfo.InvariantCulture) : null;
			return this;
		}
		public RangeQueryDescriptor<T> LowerOrEquals(double? to)
		{
			((IRangeQuery)this).LowerThanOrEqualTo = to.HasValue ? to.Value.ToString(CultureInfo.InvariantCulture) : null;
			return this;
		}

		public RangeQueryDescriptor<T> Greater(string from)
		{
			((IRangeQuery)this).GreaterThan = from;
			return this;
		}

		public RangeQueryDescriptor<T> GreaterOrEquals(string from)
		{
			((IRangeQuery)this).GreaterThanOrEqualTo = from;
			return this;
		}
		public RangeQueryDescriptor<T> Lower(string to)
		{
			((IRangeQuery)this).LowerThan = to;
			return this;
		}
		public RangeQueryDescriptor<T> LowerOrEquals(string to)
		{
			((IRangeQuery)this).LowerThanOrEqualTo = to;
			return this;
		}

		public RangeQueryDescriptor<T> Greater(DateTime? from, string format = "yyyy-MM-dd'T'HH:mm:ss.fff")
		{
			((IRangeQuery)this).GreaterThan = from.HasValue ? from.Value.ToString(format) : null;
			return this;
		}

		public RangeQueryDescriptor<T> GreaterOrEquals(DateTime? from, string format = "yyyy-MM-dd'T'HH:mm:ss.fff")
		{
			((IRangeQuery)this).GreaterThanOrEqualTo = from.HasValue ? from.Value.ToString(format) : null;
			return this;
		}

		public RangeQueryDescriptor<T> Lower(DateTime? to, string format = "yyyy-MM-dd'T'HH:mm:ss.fff")
		{
			((IRangeQuery)this).LowerThan = to.HasValue ? to.Value.ToString(format) : null;
			return this;
		}

		public RangeQueryDescriptor<T> LowerOrEquals(DateTime? to, string format = "yyyy-MM-dd'T'HH:mm:ss.fff")
		{
			((IRangeQuery)this).LowerThanOrEqualTo = to.HasValue ? to.Value.ToString(format) : null;
			return this;
		}

	}
}
