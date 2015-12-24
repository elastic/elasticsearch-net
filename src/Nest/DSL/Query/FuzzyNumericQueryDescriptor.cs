using System;
using System.Collections.Generic;
using System.Linq;
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
		public string Name { get; set; }
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
		private IFuzzyNumericQuery Self { get { return this; } }

		PropertyPathMarker IFuzzyQuery.Field { get; set; }
		
		double? IFuzzyQuery.Boost { get; set; }
		
		int? IFuzzyQuery.MaxExpansions { get; set; }
		
		string IFuzzyQuery.Fuzziness { get; set; }
	
		double? IFuzzyNumericQuery.Value { get; set; }

		bool? IFuzzyQuery.Transpositions { get; set; }

		bool? IFuzzyQuery.UnicodeAware { get; set; }

		RewriteMultiTerm? IFuzzyQuery.Rewrite { get; set; }

		string IQuery.Name { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				return Self.Field.IsConditionless() || Self.Value == null;
			}
		}

		public FuzzyNumericQueryDescriptor<T> Name(string name)
		{
			Self.Name = name;
			return this;
		}

		void IFieldNameQuery.SetFieldName(string fieldName)
		{
			Self.Field = fieldName;
		}
		PropertyPathMarker IFieldNameQuery.GetFieldName()
		{
			return Self.Field;
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
