using System;
using System.Collections.Generic;
using System.Linq;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<SpanOrQueryDescriptor<object>>))]
	public interface ISpanOrQuery : ISpanSubQuery
	{
		[JsonProperty(PropertyName = "clauses")]
		IEnumerable<ISpanQuery> Clauses { get; set; }
        [JsonProperty(PropertyName = "boost")]
        double? Boost { get; set; }

	}

	public class SpanOrQuery : PlainQuery, ISpanOrQuery
	{
		protected override void WrapInContainer(IQueryContainer container)
		{
			container.SpanOr = this;
		}

		public string Name { get; set; }
		bool IQuery.IsConditionless { get { return false; } }
		public IEnumerable<ISpanQuery> Clauses { get; set; }
        public double? Boost { get; set; }
	}

	public class SpanOrQueryDescriptor<T> : ISpanOrQuery where T : class
	{
		private ISpanOrQuery Self { get { return this; } }

		IEnumerable<ISpanQuery> ISpanOrQuery.Clauses { get; set; }

        double? ISpanOrQuery.Boost { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				return !Self.Clauses.HasAny() 
					|| Self.Clauses.Cast<IQuery>().All(q => q.IsConditionless);
			}
		}

		string IQuery.Name { get; set; }

		public SpanOrQueryDescriptor<T> Name(string name)
		{
			Self.Name = name;
			return this;
		}

		public SpanOrQueryDescriptor<T> Clauses(params Func<SpanQuery<T>, SpanQuery<T>>[] selectors)
		{
			selectors.ThrowIfNull("selector");
			var descriptors = (
				from selector in selectors 
				let span = new SpanQuery<T>() 
				select selector(span) into q 
				where !(q as IQuery).IsConditionless 
				select q
			).ToList();
			Self.Clauses = descriptors.HasAny() ? descriptors : null;
			return this;
		}

        public ISpanOrQuery Boost(double boost)
        {
            Self.Boost = boost;
            return this;
        }
	}
}
