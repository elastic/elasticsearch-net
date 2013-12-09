using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Nest.Resolvers;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class HasParentQueryDescriptor<T> : IQuery where T : class
	{
		bool IQuery.IsConditionless
		{
			get
			{
				return this._QueryDescriptor == null || this._QueryDescriptor.IsConditionless;
			}
		}

		public HasParentQueryDescriptor()
		{
			this._Type = TypeNameMarker.Create<T>();
		}
		[JsonProperty("type")]
		internal TypeNameMarker _Type { get; set; }

		[JsonProperty("_scope")]
		internal string _Scope { get; set; }

		[JsonProperty("score_type")]
		[JsonConverter(typeof(StringEnumConverter))]
		internal ParentScoreType? _ScoreType { get; set; }

		[JsonProperty("query")]
		internal BaseQuery _QueryDescriptor { get; set; }

		public HasParentQueryDescriptor<T> Query(Func<QueryDescriptor<T>, BaseQuery> querySelector)
		{
			var q = new QueryDescriptor<T>();
			this._QueryDescriptor = querySelector(q);
			return this;
		}
		public HasParentQueryDescriptor<T> Scope(string scope)
		{
			this._Scope = scope;
			return this;
		}
		public HasParentQueryDescriptor<T> Type(string type)
		{
			this._Type = type;
			return this;
		}

		public HasParentQueryDescriptor<T> Score(ParentScoreType? scoreType = ParentScoreType.score)
		{
			_ScoreType = scoreType;
			return this;
		}

		[JsonProperty(PropertyName = "_name")]
		internal string _Name { get; set; }
	}
}
