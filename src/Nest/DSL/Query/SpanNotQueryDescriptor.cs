using System;
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

		[JsonProperty(PropertyName = "pre")]
		int? Pre { get; set; }

		[JsonProperty(PropertyName = "post")]
		int? Post { get; set; }

		[JsonProperty(PropertyName = "dist")]
		int? Dist { get; set; }

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
		public int? Pre { get; set; }
		public int? Post { get; set; }
		public int? Dist { get; set; }
	}

	public class SpanNotQuery<T> : ISpanNotQuery where T : class
	{
		private ISpanNotQuery Self { get { return this; } }

		ISpanQuery ISpanNotQuery.Include { get; set; }

		ISpanQuery ISpanNotQuery.Exclude { get; set; }
		double? ISpanNotQuery.Boost { get; set; }
		int? ISpanNotQuery.Pre { get; set; }
		int? ISpanNotQuery.Post { get; set; }
		int? ISpanNotQuery.Dist { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				var excludeQuery = Self.Exclude as IQuery;
				var includeQuery = Self.Include as IQuery;

				return excludeQuery == null && includeQuery == null
					|| (includeQuery == null && excludeQuery.IsConditionless)
					|| (excludeQuery == null && includeQuery.IsConditionless)
					|| (excludeQuery != null && excludeQuery.IsConditionless && includeQuery != null && includeQuery.IsConditionless);
			}
		}



		public SpanNotQuery<T> Include(Func<SpanQuery<T>, SpanQuery<T>> selector)
		{
			if (selector == null) return this;
			var span = new SpanQuery<T>();
			Self.Include = selector(span); ;
			return this;
		}

		public SpanNotQuery<T> Exclude(Func<SpanQuery<T>, SpanQuery<T>> selector)
		{
			if (selector == null) return this;
			var span = new SpanQuery<T>();
			Self.Exclude = selector(span);;
			return this;
		}

		public SpanNotQuery<T> Boost(double boost)
		{
			Self.Boost = boost;
			return this;
		}

		public SpanNotQuery<T> Pre(int pre)
		{
			Self.Pre = pre;
			return this;
		}

		public SpanNotQuery<T> Post(int post)
		{
			Self.Post = post;
			return this;
		}

		public SpanNotQuery<T> Dist(int dist)
		{
			Self.Dist = dist;
			return this;
		}

	}
}
