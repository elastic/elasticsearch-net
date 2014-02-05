using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Linq.Expressions;
using Nest.Resolvers;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class TermFacetDescriptor<T> : BaseFacetDescriptor<T> where T : class
	{
		[JsonProperty(PropertyName = "field")]
		internal PropertyPathMarker _Field { get; set; }
		[JsonProperty(PropertyName = "fields")]
		internal IEnumerable<PropertyPathMarker> _Fields { get; set; }
		[JsonProperty(PropertyName = "size")]
		internal int? _Size { get; set; }
		[JsonProperty(PropertyName = "shard_size")]
		internal int? _ShardSize { get; set; }
		[JsonConverter(typeof(StringEnumConverter))]
		[JsonProperty(PropertyName = "order")]
		internal TermsOrder? _FacetOrder { get; set; }
		[JsonProperty(PropertyName = "all_terms")]
		internal bool? _AllTerms { get; set; }
		[JsonProperty(PropertyName = "exclude")]
		internal IEnumerable<string> _Exclude { get; set; }

		[JsonProperty(PropertyName = "execution_hint")]
		internal string _ExecutionHint { get; set; }

		[JsonProperty(PropertyName = "regex")]
		internal string _Regex { get; set; }

		[JsonConverter(typeof(StringEnumConverter))]
		[JsonProperty(PropertyName = "regex_flags")]
		internal EsRegexFlags? _RegexFlags { get; set; }

		[JsonProperty(PropertyName = "script")]
		internal string _Script { get; set; }
		[JsonProperty(PropertyName = "script_field")]
		internal string _ScriptField { get; set; }

		public TermFacetDescriptor<T> OnField(string field)
		{
			this._Fields = null;
			this._Field = field;
			return this;
		}
		public TermFacetDescriptor<T> OnFields(params string[] fields)
		{
			this._Field = null;
			this._Fields = fields.Select(f=>(PropertyPathMarker)f);
			return this;
		}
		public TermFacetDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
		{
			this._Fields = null;
			this._Field = objectPath;
			return this;
		}
		public TermFacetDescriptor<T> OnFields(params Expression<Func<T, object>>[] objectPaths)
		{
			this._Field = null;
			this._Fields = objectPaths.Select(e=>(PropertyPathMarker)e);
			return this;
		}
		public TermFacetDescriptor<T> Size(int size)
		{
			this._Size = size;
			return this;
		}
		public TermFacetDescriptor<T> ShardSize(int shardSize)
		{
			this._ShardSize = shardSize;
			return this;
		}
		public TermFacetDescriptor<T> Order(TermsOrder order)
		{
			this._FacetOrder = order;
			return this;
		}
		public TermFacetDescriptor<T> Exclude(params string[] args)
		{
			this._Exclude = args;
			return this;
		}
		public TermFacetDescriptor<T> AllTerms()
		{
			this._AllTerms = true;
			return this;
		}
		public TermFacetDescriptor<T> Regex(string regex, EsRegexFlags? Flags = null)
		{
			this._Regex = regex;
			this._RegexFlags = Flags;
			return this;
		}
		[Obsolete("execution_hint is an undocumented elasticsearch property")]
		public TermFacetDescriptor<T> ExecutionHint(string executionHint)
		{
			this._ExecutionHint = executionHint;
			return this;
		}
		public TermFacetDescriptor<T> Script(string script)
		{
			this._Script = script;
			return this;
		}
		public TermFacetDescriptor<T> ScriptField(string scriptField)
		{
			this._ScriptField = scriptField;
			return this;
		}


		public new TermFacetDescriptor<T> Global()
		{
			this._IsGlobal = true;
			return this;
		}
		public TermFacetDescriptor<T> FacetFilter(Func<FilterDescriptor<T>, BaseFilter> facetFilter)
		{
			var filter = new FilterDescriptor<T>();
			var f = facetFilter(filter);
			if (f.IsConditionless)
				f = null;
			this._FacetFilter = f;
			return this;
		}
		public new TermFacetDescriptor<T> Nested(string nested)
		{
			this._Nested = nested;
			return this;
		}
		public new TermFacetDescriptor<T> Nested(Expression<Func<T, object>> objectPath)
		{
			this._Nested = objectPath;
			return this;
		}
		public new TermFacetDescriptor<T> Scope(string scope)
		{
			this._Scope = scope;
			return this;
		}

	}
}
