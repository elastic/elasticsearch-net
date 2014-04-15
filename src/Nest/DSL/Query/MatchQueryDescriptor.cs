using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest.DSL.Query.Behaviour;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Linq.Expressions;
using Nest.Resolvers;
using Elasticsearch.Net;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IMatchQuery : IFieldNameQuery 
	{
		[JsonProperty(PropertyName = "type")]
		string Type { get; }

		[JsonProperty(PropertyName = "query")]
		string Query { get; set; }

		[JsonProperty(PropertyName = "analyzer")]
		string Analyzer { get; set; }

		[JsonProperty(PropertyName = "rewrite")]
		[JsonConverter(typeof (StringEnumConverter))]
		RewriteMultiTerm? Rewrite { get; set; }

		[JsonProperty(PropertyName = "fuzziness")]
		double? Fuzziness { get; set; }

		[JsonProperty(PropertyName = "cutoff_frequency")]
		double? CutoffFrequency { get; set; }

		[JsonProperty(PropertyName = "prefix_length")]
		int? PrefixLength { get; set; }

		[JsonProperty(PropertyName = "max_expansions")]
		int? MaxExpansions { get; set; }

		[JsonProperty(PropertyName = "slop")]
		int? Slop { get; set; }

		[JsonProperty(PropertyName = "boost")]
		double? Boost { get; set; }

		[JsonProperty(PropertyName = "lenient")]
		bool? Lenient { get; set; }
		
		[JsonProperty(PropertyName = "operator")]
		[JsonConverter(typeof (StringEnumConverter))]
		Operator? Operator { get; set; }

		PropertyPathMarker Field { get; set; }
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class MatchQueryDescriptor<T> : IMatchQuery where T : class
	{
		protected virtual string _type { get { return null; } }

		string IMatchQuery.Type { get { return _type; } }

		string IMatchQuery.Query { get; set; }

		string IMatchQuery.Analyzer { get; set; }

		RewriteMultiTerm? IMatchQuery.Rewrite { get; set; }

		double? IMatchQuery.Fuzziness { get; set; }

		double? IMatchQuery.CutoffFrequency { get; set; }

		int? IMatchQuery.PrefixLength { get; set; }

		int? IMatchQuery.MaxExpansions { get; set; }

		int? IMatchQuery.Slop { get; set; }

		double? IMatchQuery.Boost { get; set; }

		bool? IMatchQuery.Lenient { get; set; }
		
		Operator? IMatchQuery.Operator { get; set; }

		PropertyPathMarker IMatchQuery.Field { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				return ((IMatchQuery)this).Field.IsConditionless() || ((IMatchQuery)this).Query.IsNullOrEmpty();
			}
		}
		PropertyPathMarker IFieldNameQuery.GetFieldName()
		{
			return ((IMatchQuery)this).Field;
		}

		public MatchQueryDescriptor<T> OnField(string field)
		{
			((IMatchQuery)this).Field = field;
			return this;
		}
		public MatchQueryDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
		{
			((IMatchQuery)this).Field = objectPath;
			return this;
		}

		public MatchQueryDescriptor<T> Query(string query)
		{
			((IMatchQuery)this).Query = query;
			return this;
		}
		public MatchQueryDescriptor<T> Lenient(bool lenient = true)
		{
			((IMatchQuery)this).Lenient = lenient;
			return this;
		}
		public MatchQueryDescriptor<T> Analyzer(string analyzer)
		{
			((IMatchQuery)this).Analyzer = analyzer;
			return this;
		}
		public MatchQueryDescriptor<T> Fuzziness(double fuzziness)
		{
			((IMatchQuery)this).Fuzziness = fuzziness;
			return this;
		}
		public MatchQueryDescriptor<T> CutoffFrequency(double cutoffFrequency)
		{
			((IMatchQuery)this).CutoffFrequency = cutoffFrequency;
			return this;
		}

		public MatchQueryDescriptor<T> Rewrite(RewriteMultiTerm rewrite)
		{
			((IMatchQuery)this).Rewrite = rewrite;
			return this;
		}

		public MatchQueryDescriptor<T> Boost(double boost)
		{
			((IMatchQuery)this).Boost = boost;
			return this;
		}
		public MatchQueryDescriptor<T> PrefixLength(int prefixLength)
		{
			((IMatchQuery)this).PrefixLength = prefixLength;
			return this;
		}
		public MatchQueryDescriptor<T> MaxExpansions(int maxExpansions)
		{
			((IMatchQuery)this).MaxExpansions = maxExpansions;
			return this;
		}
		public MatchQueryDescriptor<T> Slop(int slop)
		{
			((IMatchQuery)this).Slop = slop;
			return this;
		}
		public MatchQueryDescriptor<T> Operator(Operator op)
		{
			((IMatchQuery)this).Operator = op;
			return this;
		}
	}
}
