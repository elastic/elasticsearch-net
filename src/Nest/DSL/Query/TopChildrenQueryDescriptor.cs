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
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface ITopChildrenQuery : IQuery
	{
		[JsonProperty("type")]
		TypeNameMarker Type { get; set; }

		[JsonProperty("_scope")]
		string Scope { get; set; }

		[JsonProperty("score"), JsonConverter(typeof (StringEnumConverter))]
		TopChildrenScore? Score { get; set; }

		[JsonProperty("factor")]
		int? Factor { get; set; }

		[JsonProperty("incremental_factor")]
		int? IncrementalFactor { get; set; }

		[JsonProperty("query")]
		IQueryDescriptor Query { get; set; }

		[JsonProperty(PropertyName = "_cache")]
		bool? Cache { get; set; }

		[JsonProperty(PropertyName = "_name")]
		string Name { get; set; }
	}

	/// <summary>
	/// The top_children query runs the child query with an estimated hits size, and out of the hit docs, 
	/// aggregates it into parent docs. If there aren’t enough parent docs matching the 
	/// requested from/size search request, then it is run again with a wider (more hits) search.
	/// </summary>
	/// <typeparam name="T">Type used to strongly type parts of this query</typeparam>
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class TopChildrenQueryDescriptor<T> : ITopChildrenQuery where T : class
	{
		bool IQuery.IsConditionless
		{
			get
			{
				return ((ITopChildrenQuery)this).Query == null || ((ITopChildrenQuery)this).Query.IsConditionless;
			}
		}

		public TopChildrenQueryDescriptor()
		{
			((ITopChildrenQuery)this).Type = TypeNameMarker.Create<T>();
			
		}

		TypeNameMarker ITopChildrenQuery.Type { get; set; }

		string ITopChildrenQuery.Scope { get; set; }

		TopChildrenScore? ITopChildrenQuery.Score { get; set; }

		int? ITopChildrenQuery.Factor { get; set; }

		int? ITopChildrenQuery.IncrementalFactor { get; set; }

		IQueryDescriptor ITopChildrenQuery.Query { get; set; }

		bool? ITopChildrenQuery.Cache { get; set; }

		string ITopChildrenQuery.Name { get; set; }

		/// <summary>
		/// Provide a child query for the top_children query
		/// </summary>
		/// <param name="querySelector">Describe the child query to be executed</param>
		public TopChildrenQueryDescriptor<T> Query(Func<QueryDescriptor<T>, BaseQuery> querySelector)
		{
			var q = new QueryDescriptor<T>();
			((ITopChildrenQuery)this).Query = querySelector(q);
			return this;
		}
		
		/// <summary>
		/// A _scope can be defined on the query allowing to run facets on the same scope name that will work 
		/// against the child documents. 
		/// </summary>
		/// <param name="scope">The name of the scope</param>
		public TopChildrenQueryDescriptor<T> Scope(string scope)
		{
			((ITopChildrenQuery)this).Scope = scope;
			return this;
		}
		
		/// <summary>
		/// How many hits are asked for in the first child query run is controlled using the factor parameter (defaults to 5).
		/// </summary>
		/// <param name="factor">The factor that controls how many hits are asked for</param>
		public TopChildrenQueryDescriptor<T> Factor(int factor)
		{
			((ITopChildrenQuery)this).Factor = factor;
			return this;
		}
		
		/// <summary>
		/// Provide a scoring mode for the child hits
		/// </summary>
		/// <param name="score">max, sum or avg</param>
		public TopChildrenQueryDescriptor<T> Score(TopChildrenScore score)
		{
			((ITopChildrenQuery)this).Score = score;
			return this;
		}
		
		/// <summary>
		/// If the initial fetch did not result in enough parent documents this factor will be used to determine
		/// the next pagesize
		/// </summary>
		/// <param name="factor">Multiplier for the original factor parameter</param>
		public TopChildrenQueryDescriptor<T> IncrementalFactor(int factor)
		{
			((ITopChildrenQuery)this).IncrementalFactor = factor;
			return this;
		}
		
		/// <summary>
		/// The type of the children to query, defaults to the inferred typename for the T
		/// that was used on the TopChildren call
		/// </summary>
		public TopChildrenQueryDescriptor<T> Type(string type)
		{
			((ITopChildrenQuery)this).Type = type;
			return this;
		}
	}
}
