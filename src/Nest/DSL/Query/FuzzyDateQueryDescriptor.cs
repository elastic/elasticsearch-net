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

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class FuzzyDateQueryDescriptor<T> : IFuzzyDateQuery where T : class
	{
		PropertyPathMarker IFuzzyQuery.Field { get; set; }
		
		double? IFuzzyQuery.Boost { get; set; }
		
		int? IFuzzyQuery.MaxExpansions { get; set; }
		
		string IFuzzyQuery.Fuzziness { get; set; }
		
		DateTime? IFuzzyDateQuery.Value { get; set; }

		bool? IFuzzyQuery.Transpositions { get; set; }

		bool? IFuzzyQuery.UnicodeAware { get; set; }

		RewriteMultiTerm? IFuzzyQuery.Rewrite { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				return ((IFuzzyDateQuery)this).Field.IsConditionless() || ((IFuzzyDateQuery)this).Value == null;
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
		
		public FuzzyDateQueryDescriptor<T> OnField(string field)
		{
			((IFuzzyDateQuery)this).Field = field;
			return this;
		}
		public FuzzyDateQueryDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
		{
			((IFuzzyDateQuery)this).Field = objectPath;
			return this;
		}
		public FuzzyDateQueryDescriptor<T> Boost(double boost)
		{
			((IFuzzyDateQuery)this).Boost = boost;
			return this;
		}
			
		public FuzzyDateQueryDescriptor<T> Fuzziness(double fuzziness)
		{
			((IFuzzyQuery)this).Fuzziness = fuzziness.ToString(CultureInfo.InvariantCulture);
			return this;
		}
		public FuzzyDateQueryDescriptor<T> Fuzziness(string fuzziness)
		{
			((IFuzzyQuery)this).Fuzziness = fuzziness;
			return this;
		}
		public FuzzyDateQueryDescriptor<T> Transpositions(bool enable = true)
		{
			((IFuzzyQuery)this).Transpositions = enable;
			return this;
		}
		public FuzzyDateQueryDescriptor<T> UnicodeAware(bool enable = true)
		{
			((IFuzzyQuery)this).UnicodeAware = enable;
			return this;
		}
		
		public FuzzyDateQueryDescriptor<T> Rewrite(RewriteMultiTerm rewrite)
		{
			((IFuzzyQuery)this).Rewrite = rewrite;
			return this;
		}

		public FuzzyDateQueryDescriptor<T> MaxExpansions(int maxExpansions)
		{
			((IFuzzyQuery)this).MaxExpansions = maxExpansions;
			return this;
		}

		public FuzzyDateQueryDescriptor<T> Value(DateTime? value)
		{
			((IFuzzyDateQuery)this).Value = value;
			return this;
		}
	}
}
