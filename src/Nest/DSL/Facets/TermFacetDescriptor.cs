using System;
using System.Collections.Generic;
using System.Linq;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Linq.Expressions;
using Nest.Resolvers;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<TermFacetRequest>))]
	public interface ITermFacetRequest : IFacetRequest
	{
		[JsonProperty(PropertyName = "field")]
		PropertyPathMarker Field { get; set; }

		[JsonProperty(PropertyName = "fields")]
		IEnumerable<PropertyPathMarker> Fields { get; set; }

		[JsonProperty(PropertyName = "size")]
		int? Size { get; set; }

		[JsonProperty(PropertyName = "shard_size")]
		int? ShardSize { get; set; }

		[JsonConverter(typeof (StringEnumConverter))]
		[JsonProperty(PropertyName = "order")]
		TermsOrder? Order { get; set; }

		[JsonProperty(PropertyName = "all_terms")]
		bool? AllTerms { get; set; }

		[JsonProperty(PropertyName = "exclude")]
		IEnumerable<string> Exclude { get; set; }

		[JsonProperty(PropertyName = "execution_hint")]
		string ExecutionHint { get; set; }

		[JsonProperty(PropertyName = "regex")]
		string Regex { get; set; }

		[JsonProperty(PropertyName = "regex_flags")]
		string RegexFlags { get; set; }

		[JsonProperty(PropertyName = "script")]
		string Script { get; set; }

		[JsonProperty(PropertyName = "script_field")]
		string ScriptField { get; set; }
	}

	public class TermFacetRequest : FacetRequest, ITermFacetRequest
	{
		public PropertyPathMarker Field { get; set; }
		public IEnumerable<PropertyPathMarker> Fields { get; set; }
		public int? Size { get; set; }
		public int? ShardSize { get; set; }
		public TermsOrder? Order { get; set; }
		public bool? AllTerms { get; set; }
		public IEnumerable<string> Exclude { get; set; }
		public string ExecutionHint { get; set; }
		public string Regex { get; set; }
		public string RegexFlags { get; set; }
		public string Script { get; set; }
		public string ScriptField { get; set; }
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class TermFacetDescriptor<T> : BaseFacetDescriptor<TermFacetDescriptor<T>,T>, ITermFacetRequest where T : class
	{
		protected ITermFacetRequest Self { get { return this; } }

		PropertyPathMarker ITermFacetRequest.Field { get; set; }

		IEnumerable<PropertyPathMarker> ITermFacetRequest.Fields { get; set; }
		
		int? ITermFacetRequest.Size { get; set; }
		
		int? ITermFacetRequest.ShardSize { get; set; }
		
		TermsOrder? ITermFacetRequest.Order { get; set; }
		
		bool? ITermFacetRequest.AllTerms { get; set; }
		
		IEnumerable<string> ITermFacetRequest.Exclude { get; set; }

		string ITermFacetRequest.ExecutionHint { get; set; }

		string ITermFacetRequest.Regex { get; set; }

		string ITermFacetRequest.RegexFlags { get; set; }

		string ITermFacetRequest.Script { get; set; }

		string ITermFacetRequest.ScriptField { get; set; }

		public TermFacetDescriptor<T> OnField(string field)
		{
			Self.Fields = null;
			Self.Field = field;
			return this;
		}
		public TermFacetDescriptor<T> OnFields(params string[] fields)
		{
			Self.Field = null;
			Self.Fields = fields.Select(f=>(PropertyPathMarker)f);
			return this;
		}
		public TermFacetDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
		{
			Self.Fields = null;
			Self.Field = objectPath;
			return this;
		}
		public TermFacetDescriptor<T> OnFields(params Expression<Func<T, object>>[] objectPaths)
		{
			Self.Field = null;
			Self.Fields = objectPaths.Select(e=>(PropertyPathMarker)e);
			return this;
		}
		public TermFacetDescriptor<T> Size(int size)
		{
			Self.Size = size;
			return this;
		}
		public TermFacetDescriptor<T> ShardSize(int shardSize)
		{
			Self.ShardSize = shardSize;
			return this;
		}
		public TermFacetDescriptor<T> Order(TermsOrder order)
		{
			Self.Order = order;
			return this;
		}
		public TermFacetDescriptor<T> Exclude(params string[] args)
		{
			Self.Exclude = args;
			return this;
		}
		public TermFacetDescriptor<T> AllTerms()
		{
			Self.AllTerms = true;
			return this;
		}
		public TermFacetDescriptor<T> Regex(string regex, string flags = null)
		{
			Self.Regex = regex;
			Self.RegexFlags = flags;
			return this;
		}

		[Obsolete("execution_hint is an undocumented elasticsearch property")]
		public TermFacetDescriptor<T> ExecutionHint(string executionHint)
		{
			Self.ExecutionHint = executionHint;
			return this;
		}

		public TermFacetDescriptor<T> Script(string script)
		{
			Self.Script = script;
			return this;
		}

		public TermFacetDescriptor<T> ScriptField(string scriptField)
		{
			Self.ScriptField = scriptField;
			return this;
		}

	}
}
