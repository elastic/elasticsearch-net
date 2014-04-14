using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Newtonsoft.Json;
using Elasticsearch.Net;

namespace Nest
{
	public interface ISpanOrQuery
	{
		[JsonProperty(PropertyName = "clauses")]
		IEnumerable<ISpanQuery> _SpanQueryDescriptors { get; set; }
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class SpanOrQueryDescriptor<T> : ISpanSubQuery, IQuery, ISpanOrQuery where T : class
	{
		[JsonProperty(PropertyName = "clauses")]
		IEnumerable<ISpanQuery> ISpanOrQuery._SpanQueryDescriptors { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				return !((ISpanOrQuery)this)._SpanQueryDescriptors.HasAny() 
					|| ((ISpanOrQuery)this)._SpanQueryDescriptors.Cast<IQuery>().All(q => q.IsConditionless);
			}
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
			((ISpanOrQuery)this)._SpanQueryDescriptors = descriptors.HasAny() ? descriptors : null;
			return this;
		}
	}
}
