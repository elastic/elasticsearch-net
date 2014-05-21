using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Linq.Expressions;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using Elasticsearch.Net;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<SpanOrQueryDescriptor<object>>))]
	public interface ISpanOrQuery : ISpanSubQuery
	{
		[JsonProperty(PropertyName = "clauses")]
		IEnumerable<ISpanQuery> Clauses { get; set; }
	}

	public class SpanOrQuery : PlainQuery, ISpanOrQuery
	{
		protected override void WrapInContainer(IQueryContainer container)
		{
			container.SpanOr = this;
		}

		bool IQuery.IsConditionless { get { return false; } }
		public IEnumerable<ISpanQuery> Clauses { get; set; }
	}

	public class SpanOrQueryDescriptor<T> : ISpanOrQuery where T : class
	{
		IEnumerable<ISpanQuery> ISpanOrQuery.Clauses { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				return !((ISpanOrQuery)this).Clauses.HasAny() 
					|| ((ISpanOrQuery)this).Clauses.Cast<IQuery>().All(q => q.IsConditionless);
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
			((ISpanOrQuery)this).Clauses = descriptors.HasAny() ? descriptors : null;
			return this;
		}
	}
}
