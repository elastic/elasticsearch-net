using System;
using Nest.DSL.Query.Behaviour;
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

	public class FuzzyNumericQuery : PlainQuery, IFuzzyNumericQuery
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
		public double? Value { get; set; }
	}

	public class FuzzyNumericQueryDescriptor<T> : IFuzzyNumericQuery where T : class
	{
		PropertyPathMarker IFuzzyQuery.Field { get; set; }
		
		double? IFuzzyQuery.Boost { get; set; }
		
		int? IFuzzyQuery.MaxExpansions { get; set; }
		
		string IFuzzyQuery.Fuzziness { get; set; }
	
		double? IFuzzyNumericQuery.Value { get; set; }

		bool? IFuzzyQuery.Transpositions { get; set; }

		bool? IFuzzyQuery.UnicodeAware { get; set; }

		RewriteMultiTerm? IFuzzyQuery.Rewrite { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				return ((IFuzzyNumericQuery)this).Field.IsConditionless() || ((IFuzzyNumericQuery)this).Value == null;
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
		
		public FuzzyNumericQueryDescriptor<T> OnField(string field)
		{
			((IFuzzyNumericQuery)this).Field = field;
			return this;
		}
		public FuzzyNumericQueryDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
		{
			((IFuzzyNumericQuery)this).Field = objectPath;
			return this;
		}
		public FuzzyNumericQueryDescriptor<T> Boost(double boost)
		{
			((IFuzzyNumericQuery)this).Boost = boost;
			return this;
		}
		public FuzzyNumericQueryDescriptor<T> Fuzziness(double fuzziness)
		{
			((IFuzzyQuery)this).Fuzziness = fuzziness.ToString(CultureInfo.InvariantCulture);
			return this;
		}
		public FuzzyNumericQueryDescriptor<T> Fuzziness(string fuzziness)
		{
			((IFuzzyQuery)this).Fuzziness = fuzziness;
			return this;
		}
		
		public FuzzyNumericQueryDescriptor<T> MaxExpansions(int maxExpansions)
		{
			((IFuzzyQuery)this).MaxExpansions = maxExpansions;
			return this;
		}
	
		public FuzzyNumericQueryDescriptor<T> Transpositions(bool enable = true)
		{
			((IFuzzyQuery)this).Transpositions = enable;
			return this;
		}
		public FuzzyNumericQueryDescriptor<T> UnicodeAware(bool enable = true)
		{
			((IFuzzyQuery)this).UnicodeAware = enable;
			return this;
		}
		public FuzzyNumericQueryDescriptor<T> Rewrite(RewriteMultiTerm rewrite)
		{
			((IFuzzyQuery)this).Rewrite = rewrite;
			return this;
		}
		public FuzzyNumericQueryDescriptor<T> Value(int? value)
		{
			((IFuzzyNumericQuery)this).Value = value;
			return this;
		}
	}
}
