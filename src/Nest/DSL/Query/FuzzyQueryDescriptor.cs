using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest.DSL.Query.Behaviour;
using Newtonsoft.Json;
using System.Linq.Expressions;
using System.Globalization;
using Newtonsoft.Json.Converters;
using Nest.Resolvers;
using Elasticsearch.Net;

namespace Nest
{

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IFuzzyQuery : IFieldNameQuery
	{
		PropertyPathMarker Field { get; set; }

		[JsonProperty(PropertyName = "boost")]
		double? Boost { get; set; }
		
		[JsonProperty(PropertyName = "fuzziness")]
		object Fuzziness { get; set; }

		[JsonProperty(PropertyName = "max_expansions")]
		int? MaxExpansions { get; set; }
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IStringFuzzyQuery : IFuzzyQuery
	{
		[JsonProperty(PropertyName = "prefix_length")]
		int? PrefixLength { get; set; }
		
		[JsonProperty(PropertyName = "value")]
		string Value { get; set; }
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class FuzzyQueryDescriptor<T> : IStringFuzzyQuery where T : class
	{
		PropertyPathMarker IFuzzyQuery.Field { get; set; }
		
		double? IFuzzyQuery.Boost { get; set; }
		
		object IFuzzyQuery.Fuzziness { get; set; }
		
		string IStringFuzzyQuery.Value { get; set; }
		
		int? IStringFuzzyQuery.PrefixLength { get; set; }

		int? IFuzzyQuery.MaxExpansions { get; set; }
		
		bool IQuery.IsConditionless
		{
			get
			{
				return ((IFuzzyQuery)this).Field.IsConditionless() || ((IStringFuzzyQuery)this).Value.IsNullOrEmpty();
			}
		}
		
		PropertyPathMarker IFieldNameQuery.GetFieldName()
		{
			return ((IFuzzyQuery)this).Field;
		}
		
		public FuzzyQueryDescriptor<T> OnField(string field)
		{
			((IFuzzyQuery)this).Field = field;
			return this;
		}

		public FuzzyQueryDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
		{
			((IFuzzyQuery)this).Field = objectPath;
			return this;
		}
		
		public FuzzyQueryDescriptor<T> Boost(double boost)
		{
			((IFuzzyQuery)this).Boost = boost;
			return this;
		}
		
		public FuzzyQueryDescriptor<T> Fuzziness(double fuzziness)
		{
			((IFuzzyQuery)this).Fuzziness = fuzziness;
			return this;
		}
		public FuzzyQueryDescriptor<T> Fuzziness(string fuzziness)
		{
			((IFuzzyQuery)this).Fuzziness = fuzziness;
			return this;
		}
		
		public FuzzyQueryDescriptor<T> MaxExpansions(int maxExpansions)
		{
			((IFuzzyQuery)this).MaxExpansions = maxExpansions;
			return this;
		}
		public FuzzyQueryDescriptor<T> PrefixLength(int prefixLength)
		{
			((IStringFuzzyQuery)this).PrefixLength = prefixLength;
			return this;
		}
		
		public FuzzyQueryDescriptor<T> Value(string value)
		{
			((IStringFuzzyQuery)this).Value = value;
			return this;
		}
	}
}
