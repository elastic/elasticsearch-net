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
	public interface IFuzzyDateQuery : IFuzzyQuery
	{
		[JsonProperty(PropertyName = "value")]
		DateTime? Value { get; set; }
	}
	
	public class FuzzyDateQuery : PlainQuery, IFuzzyDateQuery
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
		public DateTime? Value { get; set; }
	}

	public class FuzzyDateQueryDescriptor<T> : IFuzzyDateQuery where T : class
	{
		private IFuzzyDateQuery Self { get { return this; } }

		PropertyPathMarker IFuzzyQuery.Field { get; set; }
		
		double? IFuzzyQuery.Boost { get; set; }
		
		int? IFuzzyQuery.MaxExpansions { get; set; }
		
		string IFuzzyQuery.Fuzziness { get; set; }
		
		DateTime? IFuzzyDateQuery.Value { get; set; }

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

		void IFieldNameQuery.SetFieldName(string fieldName)
		{
			Self.Field = fieldName;
		}

		PropertyPathMarker IFieldNameQuery.GetFieldName()
		{
			return Self.Field;
		}

		public FuzzyDateQueryDescriptor<T> Name(string name)
		{
			Self.Name = name;
			return this;
		}

		public FuzzyDateQueryDescriptor<T> OnField(string field)
		{
			Self.Field = field;
			return this;
		}
		public FuzzyDateQueryDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
		{
			Self.Field = objectPath;
			return this;
		}
		public FuzzyDateQueryDescriptor<T> Boost(double boost)
		{
			Self.Boost = boost;
			return this;
		}
			
		public FuzzyDateQueryDescriptor<T> Fuzziness(double fuzziness)
		{
			Self.Fuzziness = fuzziness.ToString(CultureInfo.InvariantCulture);
			return this;
		}
		public FuzzyDateQueryDescriptor<T> Fuzziness(string fuzziness)
		{
			Self.Fuzziness = fuzziness;
			return this;
		}
		public FuzzyDateQueryDescriptor<T> Transpositions(bool enable = true)
		{
			Self.Transpositions = enable;
			return this;
		}
		public FuzzyDateQueryDescriptor<T> UnicodeAware(bool enable = true)
		{
			Self.UnicodeAware = enable;
			return this;
		}
		
		public FuzzyDateQueryDescriptor<T> Rewrite(RewriteMultiTerm rewrite)
		{
			Self.Rewrite = rewrite;
			return this;
		}

		public FuzzyDateQueryDescriptor<T> MaxExpansions(int maxExpansions)
		{
			Self.MaxExpansions = maxExpansions;
			return this;
		}

		public FuzzyDateQueryDescriptor<T> Value(DateTime? value)
		{
			Self.Value = value;
			return this;
		}
	}
}
