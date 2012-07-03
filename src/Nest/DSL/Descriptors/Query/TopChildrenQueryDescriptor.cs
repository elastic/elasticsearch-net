using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonObject(MemberSerialization=MemberSerialization.OptIn)]
	public class TopChildrenQueryDescriptor<T>  where T : class
	{
		public TopChildrenQueryDescriptor()
		{
			this._Type = ElasticClient.GetTypeNameFor<T>();
		}
		[JsonProperty("type")]
		internal string _Type { get; set; }

		[JsonProperty("_scope")]
		internal string _Scope { get; set;}

		[JsonProperty("score"), JsonConverter(typeof(StringEnumConverter))]
		internal TopChildrenScore? _Score { get; set; }

		[JsonProperty("factor")]
		internal int? _Factor { get; set; }

		[JsonProperty("incremental_factor")]
		internal int? _IncrementalFactor { get; set; }

		[JsonProperty("query")]
		internal QueryDescriptor<T> _QueryDescriptor { get; set;}

    [JsonProperty(PropertyName = "_cache")]
		internal bool? _Cache { get; set; }

		[JsonProperty(PropertyName = "_name")]
		internal string _Name { get; set; }

		public TopChildrenQueryDescriptor<T> Query(Action<QueryDescriptor<T>> querySelector)
		{
			this._QueryDescriptor = new QueryDescriptor<T>();
			querySelector(this._QueryDescriptor);
			return this;
		}
		public TopChildrenQueryDescriptor<T> Scope(string scope)
		{
			this._Scope = scope;
			return this;
		}
		public TopChildrenQueryDescriptor<T> Factor(int factor)
		{
			this._Factor= factor;
			return this;
		}
		public TopChildrenQueryDescriptor<T> Score(TopChildrenScore score)
		{
			this._Score = score;
			return this;
		}
		public TopChildrenQueryDescriptor<T> IncrementalFactor(int factor)
		{
			this._IncrementalFactor = factor;
			return this;
		}
		public TopChildrenQueryDescriptor<T> Type(string type)
		{
			this._Type = type;
			return this;
		}
	}
}
