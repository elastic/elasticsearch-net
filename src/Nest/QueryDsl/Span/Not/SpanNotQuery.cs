// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(SpanNotQuery))]
	public interface ISpanNotQuery : ISpanSubQuery
	{
		[DataMember(Name ="dist")]
		int? Dist { get; set; }

		[DataMember(Name ="exclude")]
		ISpanQuery Exclude { get; set; }

		[DataMember(Name ="include")]
		ISpanQuery Include { get; set; }

		[DataMember(Name ="post")]
		int? Post { get; set; }

		[DataMember(Name ="pre")]
		int? Pre { get; set; }
	}

	public class SpanNotQuery : QueryBase, ISpanNotQuery
	{
		public int? Dist { get; set; }
		public ISpanQuery Exclude { get; set; }
		public ISpanQuery Include { get; set; }
		public int? Post { get; set; }
		public int? Pre { get; set; }
		protected override bool Conditionless => IsConditionless(this);

		internal override void InternalWrapInContainer(IQueryContainer c) => c.SpanNot = this;

		internal static bool IsConditionless(ISpanNotQuery q)
		{
			var exclude = q.Exclude as IQuery;
			var include = q.Include as IQuery;

			return exclude == null && include == null
				|| include == null && exclude.Conditionless
				|| exclude == null && include.Conditionless
				|| exclude != null && exclude.Conditionless && include != null && include.Conditionless;
		}
	}

	public class SpanNotQueryDescriptor<T>
		: QueryDescriptorBase<SpanNotQueryDescriptor<T>, ISpanNotQuery>
			, ISpanNotQuery where T : class
	{
		protected override bool Conditionless => SpanNotQuery.IsConditionless(this);
		int? ISpanNotQuery.Dist { get; set; }
		ISpanQuery ISpanNotQuery.Exclude { get; set; }
		ISpanQuery ISpanNotQuery.Include { get; set; }
		int? ISpanNotQuery.Post { get; set; }
		int? ISpanNotQuery.Pre { get; set; }

		public SpanNotQueryDescriptor<T> Include(Func<SpanQueryDescriptor<T>, ISpanQuery> selector) =>
			Assign(selector(new SpanQueryDescriptor<T>()), (a, v) => a.Include = v);

		public SpanNotQueryDescriptor<T> Exclude(Func<SpanQueryDescriptor<T>, ISpanQuery> selector) =>
			Assign(selector(new SpanQueryDescriptor<T>()), (a, v) => a.Exclude = v);

		public SpanNotQueryDescriptor<T> Pre(int? pre) => Assign(pre, (a, v) => a.Pre = v);

		public SpanNotQueryDescriptor<T> Post(int? post) => Assign(post, (a, v) => a.Post = v);

		public SpanNotQueryDescriptor<T> Dist(int? dist) => Assign(dist, (a, v) => a.Dist = v);
	}
}
