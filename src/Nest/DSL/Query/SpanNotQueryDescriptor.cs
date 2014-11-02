using System;
using System.Collections.Generic;
using System.Linq;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<SpanNotQuery<object>>))]
	public interface ISpanNotQuery : ISpanSubQuery
	{
		[JsonProperty(PropertyName = "include")]
		ISpanQuery Include { get; set; }

		[JsonProperty(PropertyName = "exclude")]
		ISpanQuery Exclude { get; set; }

        [JsonProperty(PropertyName = "boost")]
        double? Boost { get; set; }

	}

	public class SpanNotQuery : PlainQuery, ISpanNotQuery
	{
		protected override void WrapInContainer(IQueryContainer container)
		{
			container.SpanNot = this;
		}

		bool IQuery.IsConditionless { get { return false; } }
		public ISpanQuery Include { get; set; }
		public ISpanQuery Exclude { get; set; }
	    public double? Boost { get; set; }
	}

	public class SpanNotQuery<T> : ISpanNotQuery where T : class
	{
		ISpanQuery ISpanNotQuery.Include { get; set; }

		ISpanQuery ISpanNotQuery.Exclude { get; set; }
	    double? ISpanNotQuery.Boost { get; set; }

	    bool IQuery.IsConditionless
		{
			get
			{
				var excludeQuery = ((ISpanNotQuery)this).Exclude as IQuery;
				var includeQuery = ((ISpanNotQuery)this).Include as IQuery;

				return excludeQuery == null && includeQuery == null
					|| (includeQuery == null && excludeQuery.IsConditionless)
					|| (excludeQuery == null && includeQuery.IsConditionless)
					|| (excludeQuery != null && excludeQuery.IsConditionless && includeQuery != null && includeQuery.IsConditionless);
			}
		}



		public SpanNotQuery<T> Include(Func<SpanQuery<T>, SpanQuery<T>> selector)
		{
			if (selector == null)
				return this;
			var descriptors = new List<SpanQuery<T>>();
			var span = new SpanQuery<T>();
			var q = selector(span);
			((ISpanNotQuery)this).Include = q;
			return this;
		}
		public SpanNotQuery<T> Exclude(Func<SpanQuery<T>, SpanQuery<T>> selector)
		{
			if (selector == null)
				return this;
			var descriptors = new List<SpanQuery<T>>();
			var span = new SpanQuery<T>();
			var q = selector(span);
			((ISpanNotQuery)this).Exclude = q;
			return this;
		}

        public ISpanNotQuery Boost(double boost)
        {
            ((ISpanNotQuery)this).Boost = boost;
            return this;
        }
	}
}
