using System;
using Newtonsoft.Json;
using System.Linq.Expressions;
using System.Globalization;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IFuzzyQuery : IFieldNameQuery
	{
		[JsonProperty(PropertyName = "prefix_length")]
		int? PrefixLength { get; set; }
		
		[JsonProperty(PropertyName = "value")]
		object Value { get; set; }

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

	public class FuzzyQuery : FieldNameQuery, IFuzzyQuery
	{
		bool IQuery.Conditionless => IsConditionless(this);
		public double? Boost { get; set; }
		public string Fuzziness { get; set; }
		public RewriteMultiTerm? Rewrite { get; set; }
		public int? MaxExpansions { get; set; }
		public bool? Transpositions { get; set; }
		public bool? UnicodeAware { get; set; }
		public int? PrefixLength { get; set; }
		public object Value { get; set; }

		protected override void WrapInContainer(IQueryContainer c) => c.Fuzzy = this;
		internal static bool IsConditionless(IFuzzyQuery q) => q.Field.IsConditionless() || q.Value == null;
	}

	public class FuzzyQueryDescriptor<T> : IFuzzyQuery where T : class
	{
		private IFuzzyQuery Self => this;
		string IQuery.Name { get; set; }
		bool IQuery.Conditionless => FuzzyQuery.IsConditionless(this);
		FieldName IFieldNameQuery.Field { get; set; }
		double? IFuzzyQuery.Boost { get; set; }
		string IFuzzyQuery.Fuzziness { get; set; }
		object IFuzzyQuery.Value { get; set; }
		int? IFuzzyQuery.PrefixLength { get; set; }
		int? IFuzzyQuery.MaxExpansions { get; set; }
		bool? IFuzzyQuery.Transpositions { get; set; }
		bool? IFuzzyQuery.UnicodeAware { get; set; }
		RewriteMultiTerm? IFuzzyQuery.Rewrite { get; set; }
		
		public FuzzyQueryDescriptor<T> OnField(string field)
		{
			Self.Field = field;
			return this;
		}

		public FuzzyQueryDescriptor<T> Name(string name)
		{
			Self.Name = name;
			return this;
		}

		public FuzzyQueryDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
		{
			Self.Field = objectPath;
			return this;
		}
		
		public FuzzyQueryDescriptor<T> Boost(double boost)
		{
			Self.Boost = boost;
			return this;
		}
		
		public FuzzyQueryDescriptor<T> Fuzziness(double fuzziness)
		{
			Self.Fuzziness = fuzziness.ToString(CultureInfo.InvariantCulture);
			return this;
		}

		public FuzzyQueryDescriptor<T> Fuzziness(string fuzziness)
		{
			Self.Fuzziness = fuzziness;
			return this;
		}
		
		public FuzzyQueryDescriptor<T> MaxExpansions(int maxExpansions)
		{
			Self.MaxExpansions = maxExpansions;
			return this;
		}

		public FuzzyQueryDescriptor<T> PrefixLength(int prefixLength)
		{
			Self.PrefixLength = prefixLength;
			return this;
		}

		public FuzzyQueryDescriptor<T> Transpositions(bool enable = true)
		{
			Self.Transpositions = enable;
			return this;
		}

		public FuzzyQueryDescriptor<T> UnicodeAware(bool enable = true)
		{
			Self.UnicodeAware = enable;
			return this;
		}
		
		public FuzzyQueryDescriptor<T> Rewrite(RewriteMultiTerm rewrite)
		{
			Self.Rewrite = rewrite;
			return this;
		}

		public FuzzyQueryDescriptor<T> Value(string value)
		{
			Self.Value = value;
			return this;
		}

		public FuzzyQueryDescriptor<T> Value(double value)
		{
			Self.Value = value;
			return this;
		}

		public FuzzyQueryDescriptor<T> Value(DateTime value)
		{
			Self.Value = value;
			return this;
		}
	}
}
