using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Newtonsoft.Json.Converters;
using Nest.Resolvers;

namespace Nest
{
	public interface ITopChildrenQuery
	{
		[JsonProperty("type")]
		TypeNameMarker _Type { get; set; }

		[JsonProperty("_scope")]
		string _Scope { get; set; }

		[JsonProperty("score"), JsonConverter(typeof (StringEnumConverter))]
		TopChildrenScore? _Score { get; set; }

		[JsonProperty("factor")]
		int? _Factor { get; set; }

		[JsonProperty("incremental_factor")]
		int? _IncrementalFactor { get; set; }

		[JsonProperty("query")]
		BaseQuery _QueryDescriptor { get; set; }

		[JsonProperty(PropertyName = "_cache")]
		bool? _Cache { get; set; }

		[JsonProperty(PropertyName = "_name")]
		string _Name { get; set; }
	}

	/// <summary>
	/// The top_children query runs the child query with an estimated hits size, and out of the hit docs, 
	/// aggregates it into parent docs. If there aren’t enough parent docs matching the 
	/// requested from/size search request, then it is run again with a wider (more hits) search.
	/// </summary>
	/// <typeparam name="T">Type used to strongly type parts of this query</typeparam>
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class TopChildrenQueryDescriptor<T> : IQuery, ITopChildrenQuery where T : class
	{
		bool IQuery.IsConditionless
		{
			get
			{
				return ((ITopChildrenQuery)this)._QueryDescriptor == null || ((ITopChildrenQuery)this)._QueryDescriptor.IsConditionless;
			}
		}

		public TopChildrenQueryDescriptor()
		{
			((ITopChildrenQuery)this)._Type = TypeNameMarker.Create<T>();
			
		}

		[JsonProperty("type")]
		TypeNameMarker ITopChildrenQuery._Type { get; set; }

		[JsonProperty("_scope")]
		string ITopChildrenQuery._Scope { get; set; }

		[JsonProperty("score"), JsonConverter(typeof(StringEnumConverter))]
		TopChildrenScore? ITopChildrenQuery._Score { get; set; }

		[JsonProperty("factor")]
		int? ITopChildrenQuery._Factor { get; set; }

		[JsonProperty("incremental_factor")]
		int? ITopChildrenQuery._IncrementalFactor { get; set; }

		[JsonProperty("query")]
		BaseQuery ITopChildrenQuery._QueryDescriptor { get; set; }

		[JsonProperty(PropertyName = "_cache")]
		bool? ITopChildrenQuery._Cache { get; set; }

		[JsonProperty(PropertyName = "_name")]
		string ITopChildrenQuery._Name { get; set; }

		/// <summary>
		/// Provide a child query for the top_children query
		/// </summary>
		/// <param name="querySelector">Describe the child query to be executed</param>
		public TopChildrenQueryDescriptor<T> Query(Func<QueryDescriptor<T>, BaseQuery> querySelector)
		{
			var q = new QueryDescriptor<T>();
			((ITopChildrenQuery)this)._QueryDescriptor = querySelector(q);
			return this;
		}
		
		/// <summary>
		/// A _scope can be defined on the query allowing to run facets on the same scope name that will work 
		/// against the child documents. 
		/// </summary>
		/// <param name="scope">The name of the scope</param>
		public TopChildrenQueryDescriptor<T> Scope(string scope)
		{
			((ITopChildrenQuery)this)._Scope = scope;
			return this;
		}
		
		/// <summary>
		/// How many hits are asked for in the first child query run is controlled using the factor parameter (defaults to 5).
		/// </summary>
		/// <param name="factor">The factor that controls how many hits are asked for</param>
		public TopChildrenQueryDescriptor<T> Factor(int factor)
		{
			((ITopChildrenQuery)this)._Factor = factor;
			return this;
		}
		
		/// <summary>
		/// Provide a scoring mode for the child hits
		/// </summary>
		/// <param name="score">max, sum or avg</param>
		public TopChildrenQueryDescriptor<T> Score(TopChildrenScore score)
		{
			((ITopChildrenQuery)this)._Score = score;
			return this;
		}
		
		/// <summary>
		/// If the initial fetch did not result in enough parent documents this factor will be used to determine
		/// the next pagesize
		/// </summary>
		/// <param name="factor">Multiplier for the original factor parameter</param>
		public TopChildrenQueryDescriptor<T> IncrementalFactor(int factor)
		{
			((ITopChildrenQuery)this)._IncrementalFactor = factor;
			return this;
		}
		
		/// <summary>
		/// The type of the children to query, defaults to the inferred typename for the T
		/// that was used on the TopChildren call
		/// </summary>
		public TopChildrenQueryDescriptor<T> Type(string type)
		{
			((ITopChildrenQuery)this)._Type = type;
			return this;
		}
	}
}
