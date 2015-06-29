using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Linq.Expressions;
using System.Globalization;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IFuzzyNumericQuery : IFuzzyQuery
	{
		[JsonProperty(PropertyName = "value")]
		double? Value { get; set; }
	}

	public class FuzzyNumericQuery : FieldNameQuery, IFuzzyNumericQuery
	{
		bool IQuery.Conditionless => IsConditionless(this);
		public double? Boost { get; set; }
		public string Fuzziness { get; set; }
		public RewriteMultiTerm? Rewrite { get; set; }
		public int? MaxExpansions { get; set; }
		public bool? Transpositions { get; set; }
		public bool? UnicodeAware { get; set; }
		public double? Value { get; set; }

		protected override void WrapInContainer(IQueryContainer q) => q.Fuzzy = this;
		internal static bool IsConditionless(IFuzzyNumericQuery q) => q.Field.IsConditionless() || q.Value == null;
	}

	public class FuzzyNumericQueryDescriptor<T> : IFuzzyNumericQuery where T : class
	{
		private IFuzzyNumericQuery Self => this;
		string IQuery.Name { get; set; }
		bool IQuery.Conditionless => FuzzyNumericQuery.IsConditionless(this);
		PropertyPathMarker IFieldNameQuery.Field { get; set; }
		double? IFuzzyQuery.Boost { get; set; }
		int? IFuzzyQuery.MaxExpansions { get; set; }
		string IFuzzyQuery.Fuzziness { get; set; }
		double? IFuzzyNumericQuery.Value { get; set; }
		bool? IFuzzyQuery.Transpositions { get; set; }
		bool? IFuzzyQuery.UnicodeAware { get; set; }
		RewriteMultiTerm? IFuzzyQuery.Rewrite { get; set; }

		public FuzzyNumericQueryDescriptor<T> Name(string name)
		{
			Self.Name = name;
			return this;
		}
		
		public FuzzyNumericQueryDescriptor<T> OnField(string field)
		{
			Self.Field = field;
			return this;
		}

		public FuzzyNumericQueryDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
		{
			Self.Field = objectPath;
			return this;
		}

		public FuzzyNumericQueryDescriptor<T> Boost(double boost)
		{
			Self.Boost = boost;
			return this;
		}

		public FuzzyNumericQueryDescriptor<T> Fuzziness(double fuzziness)
		{
			Self.Fuzziness = fuzziness.ToString(CultureInfo.InvariantCulture);
			return this;
		}

		public FuzzyNumericQueryDescriptor<T> Fuzziness(string fuzziness)
		{
			Self.Fuzziness = fuzziness;
			return this;
		}
		
		public FuzzyNumericQueryDescriptor<T> MaxExpansions(int maxExpansions)
		{
			Self.MaxExpansions = maxExpansions;
			return this;
		}
	
		public FuzzyNumericQueryDescriptor<T> Transpositions(bool enable = true)
		{
			Self.Transpositions = enable;
			return this;
		}

		public FuzzyNumericQueryDescriptor<T> UnicodeAware(bool enable = true)
		{
			Self.UnicodeAware = enable;
			return this;
		}

		public FuzzyNumericQueryDescriptor<T> Rewrite(RewriteMultiTerm rewrite)
		{
			Self.Rewrite = rewrite;
			return this;
		}

		public FuzzyNumericQueryDescriptor<T> Value(int? value)
		{
			Self.Value = value;
			return this;
		}
	}
}
