// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(SpanWithinQuery))]
	public interface ISpanWithinQuery : ISpanSubQuery
	{
		[DataMember(Name ="big")]
		ISpanQuery Big { get; set; }

		[DataMember(Name ="little")]
		ISpanQuery Little { get; set; }
	}

	public class SpanWithinQuery : QueryBase, ISpanWithinQuery
	{
		public ISpanQuery Big { get; set; }
		public ISpanQuery Little { get; set; }
		protected override bool Conditionless => IsConditionless(this);

		internal override void InternalWrapInContainer(IQueryContainer c) => c.SpanWithin = this;

		internal static bool IsConditionless(ISpanWithinQuery q)
		{
			var exclude = q.Little as IQuery;
			var include = q.Big as IQuery;

			return exclude == null && include == null
				|| include == null && exclude.Conditionless
				|| exclude == null && include.Conditionless
				|| exclude != null && exclude.Conditionless && include != null && include.Conditionless;
		}
	}

	public class SpanWithinQueryDescriptor<T>
		: QueryDescriptorBase<SpanWithinQueryDescriptor<T>, ISpanWithinQuery>
			, ISpanWithinQuery where T : class
	{
		protected override bool Conditionless => SpanWithinQuery.IsConditionless(this);
		ISpanQuery ISpanWithinQuery.Big { get; set; }
		ISpanQuery ISpanWithinQuery.Little { get; set; }

		public SpanWithinQueryDescriptor<T> Little(Func<SpanQueryDescriptor<T>, ISpanQuery> selector) =>
			Assign(selector(new SpanQueryDescriptor<T>()), (a, v) => a.Little = v);

		public SpanWithinQueryDescriptor<T> Big(Func<SpanQueryDescriptor<T>, ISpanQuery> selector) =>
			Assign(selector(new SpanQueryDescriptor<T>()), (a, v) => a.Big = v);
	}
}
