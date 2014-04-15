using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Nest.Resolvers;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Nest.Resolvers.Converters;
using Elasticsearch.Net;

namespace Nest
{
	public interface ITermQuery : IQuery
	{
		PropertyPathMarker Field { get; set; }
		object Value { get; set; }
		double? Boost { get; set; }
	}

	public class TermQueryDescriptorBase<TDescriptor, T> : ITermQuery, ICustomJson
		where TDescriptor : TermQueryDescriptorBase<TDescriptor, T>
		where T : class
	{
		PropertyPathMarker ITermQuery.Field { get; set; }
		object ITermQuery.Value { get; set; }
		double? ITermQuery.Boost { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				var tq = ((ITermQuery)this);
				return tq.Value == null || tq.Value.ToString().IsNullOrEmpty() || tq.Field.IsConditionless();
			}
		}

		public TDescriptor OnField(string field)
		{
			((ITermQuery)this).Field = field;
			return (TDescriptor)this;
		}
		public TDescriptor OnField(Expression<Func<T, object>> field)
		{
			((ITermQuery)this).Field = field;
			return (TDescriptor)this;
		}
		public TDescriptor OnField<K>(Expression<Func<T, K>> field)
		{
			((ITermQuery)this).Field = field;
			return (TDescriptor)this;
		}
		public TDescriptor Value(object value)
		{
			((ITermQuery)this).Value = value;
			return (TDescriptor)this;
		}
		public TDescriptor Boost(double boost)
		{
			((ITermQuery)this).Boost = boost;
			return (TDescriptor)this;
		}

		object ICustomJson.GetCustomJson()
		{
			var tq = ((ITermQuery)this);
			return new Dictionary<object, object>
			{
				{
					tq.Field, new Dictionary<string, object>
					{
						{ "value", tq.Value },
						{ "boost", tq.Boost },
					}
				}
			};
		}
	}


	[JsonConverter(typeof(CustomJsonConverter))]
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class TermQueryDescriptor<T> : TermQueryDescriptorBase<TermQueryDescriptor<T>, T>
		where T : class
	{
		
	}
}
