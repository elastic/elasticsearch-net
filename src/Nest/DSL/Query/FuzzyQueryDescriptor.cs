using Elasticsearch.Net;
using Nest.DSL.Query.Behaviour;
using Nest.Resolvers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Nest
{

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IFuzzyQuery : IFieldNameQuery 
	{
		PropertyPathMarker Field { get; set; }

		[JsonProperty(PropertyName = "boost")]
		double? Boost { get; set; }
		
		[JsonProperty(PropertyName = "fuzziness")]
		string Fuzziness { get; set; }

		[JsonProperty(PropertyName = "rewrite")]
		RewriteMultiTerm? Rewrite { get; set; }

		[JsonProperty(PropertyName = "max_expansions")]
		int? MaxExpansions { get; set; }

		[JsonProperty(PropertyName = "transpositions")]
		bool? Transpositions { get; set; }

		[JsonProperty(PropertyName = "unicode_aware")]
		bool? UnicodeAware { get; set; }
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IStringFuzzyQuery : IFuzzyQuery
	{
		[JsonProperty(PropertyName = "prefix_length")]
		int? PrefixLength { get; set; }
		
		[JsonProperty(PropertyName = "value")]
		string Value { get; set; }
	}

	public class FuzzyStringQuery : PlainQuery, IStringFuzzyQuery
	{
		protected override void WrapInContainer(IQueryContainer container)
		{
			container.Fuzzy = this;
		}
		bool IQuery.IsConditionless { get { return false; } }
		PropertyPathMarker IFieldNameQuery.GetFieldName() { return this.Field; }
		void IFieldNameQuery.SetFieldName(string fieldName) { this.Field = fieldName; }

		public PropertyPathMarker Field { get; set; }
		public double? Boost { get; set; }
		public string Fuzziness { get; set; }
		public RewriteMultiTerm? Rewrite { get; set; }
		public int? MaxExpansions { get; set; }
		public bool? Transpositions { get; set; }
		public bool? UnicodeAware { get; set; }
		public int? PrefixLength { get; set; }
		public string Value { get; set; }
	}

	public class FuzzyQueryDescriptor<T> : IStringFuzzyQuery where T : class
	{
		PropertyPathMarker IFuzzyQuery.Field { get; set; }
		
		double? IFuzzyQuery.Boost { get; set; }
		
		string IFuzzyQuery.Fuzziness { get; set; }
		
		string IStringFuzzyQuery.Value { get; set; }
		
		int? IStringFuzzyQuery.PrefixLength { get; set; }

		int? IFuzzyQuery.MaxExpansions { get; set; }

		bool? IFuzzyQuery.Transpositions { get; set; }

		bool? IFuzzyQuery.UnicodeAware { get; set; }

		RewriteMultiTerm? IFuzzyQuery.Rewrite { get; set; }
		
		bool IQuery.IsConditionless
		{
			get
			{
				return ((IFuzzyQuery)this).Field.IsConditionless() || ((IStringFuzzyQuery)this).Value.IsNullOrEmpty();
			}
		}
		
		void IFieldNameQuery.SetFieldName(string fieldName)
		{
			((IFuzzyQuery)this).Field = fieldName;
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
			((IFuzzyQuery)this).Fuzziness = fuzziness.ToString(CultureInfo.InvariantCulture);
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

		public FuzzyQueryDescriptor<T> Transpositions(bool enable = true)
		{
			((IFuzzyQuery)this).Transpositions = enable;
			return this;
		}

		public FuzzyQueryDescriptor<T> UnicodeAware(bool enable = true)
		{
			((IFuzzyQuery)this).UnicodeAware = enable;
			return this;
		}
		
		public FuzzyQueryDescriptor<T> Rewrite(RewriteMultiTerm rewrite)
		{
			((IFuzzyQuery)this).Rewrite = rewrite;
			return this;
		}
		public FuzzyQueryDescriptor<T> Value(string value)
		{
			((IStringFuzzyQuery)this).Value = value;
			return this;
		}
	}
}
