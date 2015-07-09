using System;
using System.Collections.Generic;
using System.Linq;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<SpanNotQuery>))]
	public interface ISpanNotQuery : ISpanSubQuery
	{
		[JsonProperty(PropertyName = "include")]
		ISpanQuery Include { get; set; }

		[JsonProperty(PropertyName = "exclude")]
		ISpanQuery Exclude { get; set; }

		[JsonProperty(PropertyName = "pre")]
		int? Pre { get; set; }

		[JsonProperty(PropertyName = "post")]
		int? Post { get; set; }

		[JsonProperty(PropertyName = "dist")]
		int? Dist { get; set; }

	}

	public class SpanNotQuery : QueryBase, ISpanNotQuery
	{
		bool IQuery.Conditionless => IsConditionless(this);
		public ISpanQuery Include { get; set; }
		public ISpanQuery Exclude { get; set; }
		public int? Pre { get; set; }
		public int? Post { get; set; }
		public int? Dist { get; set; }

		protected override void WrapInContainer(IQueryContainer c) => c.SpanNot = this;
		internal static bool IsConditionless(ISpanNotQuery q)
		{
			var exclude = q.Exclude as IQuery;
			var include = q.Include as IQuery;

			return (exclude == null && include == null)
				|| (include == null && exclude.Conditionless)
				|| (exclude == null && include.Conditionless)
				|| (exclude != null && exclude.Conditionless && include != null && include.Conditionless);
		}
	}

	public class SpanNotQueryDescriptor<T>
		: QueryDescriptorBase<SpanNotQueryDescriptor<T>, ISpanNotQuery>
		, ISpanNotQuery where T : class
	{
		private ISpanNotQuery Self => this;
		bool IQuery.Conditionless => SpanNotQuery.IsConditionless(this);
		ISpanQuery ISpanNotQuery.Include { get; set; }
		ISpanQuery ISpanNotQuery.Exclude { get; set; }
		int? ISpanNotQuery.Pre { get; set; }
		int? ISpanNotQuery.Post { get; set; }
		int? ISpanNotQuery.Dist { get; set; }

		public SpanNotQueryDescriptor<T> Include(Func<SpanQueryDescriptor<T>, SpanQueryDescriptor<T>> selector)
		{
			if (selector == null) return this;
			var span = new SpanQueryDescriptor<T>();
			Self.Include = selector(span); ;
			return this;
		}

		public SpanNotQueryDescriptor<T> Exclude(Func<SpanQueryDescriptor<T>, SpanQueryDescriptor<T>> selector)
		{
			if (selector == null) return this;
			var span = new SpanQueryDescriptor<T>();
			Self.Exclude = selector(span);;
			return this;
		}

		public SpanNotQueryDescriptor<T> Pre(int pre)
		{
			Self.Pre = pre;
			return this;
		}

		public SpanNotQueryDescriptor<T> Post(int post)
		{
			Self.Post = post;
			return this;
		}

		public SpanNotQueryDescriptor<T> Dist(int dist)
		{
			Self.Dist = dist;
			return this;
		}
	}
}
