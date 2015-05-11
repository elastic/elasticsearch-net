using System;
using System.Collections.Generic;
using System.Linq;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<TopChildrenQueryDescriptor<object>>))]
	public interface ITopChildrenQuery : IQuery
	{
		[JsonProperty("type")]
		TypeNameMarker Type { get; set; }

		[JsonProperty("score"), JsonConverter(typeof (StringEnumConverter))]
		TopChildrenScore? Score { get; set; }

		[JsonProperty("factor")]
		int? Factor { get; set; }

		[JsonProperty("incremental_factor")]
		int? IncrementalFactor { get; set; }

		[JsonProperty("query")]
		[JsonConverter(typeof(CompositeJsonConverter<ReadAsTypeConverter<QueryDescriptor<object>>, CustomJsonConverter>))]
		IQueryContainer Query { get; set; }

		[JsonProperty(PropertyName = "_cache")]
		[Obsolete("invalid mapping scheduled to be removed in 2.0")]
		bool? Cache { get; set; }

	}

	public class TopChildrenQuery : PlainQuery, ITopChildrenQuery
	{
		protected override void WrapInContainer(IQueryContainer container)
		{
			container.TopChildren = this;
		}

		bool IQuery.IsConditionless { get { return false; } }
		public TypeNameMarker Type { get; set; }
		public TopChildrenScore? Score { get; set; }
		public int? Factor { get; set; }
		public int? IncrementalFactor { get; set; }
		public IQueryContainer Query { get; set; }
		[Obsolete("invalid mapping scheduled to be removed in 2.0")]
		public bool? Cache { get; set; }
		public string Name { get; set; }
	}

	/// <summary>
	/// The top_children query runs the child query with an estimated hits size, and out of the hit docs, 
	/// aggregates it into parent docs. If there aren’t enough parent docs matching the 
	/// requested from/size search request, then it is run again with a wider (more hits) search.
	/// </summary>
	/// <typeparam name="T">Type used to strongly type parts of this query</typeparam>
	public class TopChildrenQueryDescriptor<T> : ITopChildrenQuery where T : class
	{
		private ITopChildrenQuery Self { get { return this; }}

		bool IQuery.IsConditionless
		{
			get
			{
				return Self.Query == null || Self.Query.IsConditionless;
			}
		}

		string IQuery.Name { get; set; }

		public TopChildrenQueryDescriptor<T> Name(string name)
		{
			Self.Name = name;
			return this;
		}

		public TopChildrenQueryDescriptor()
		{
			Self.Type = TypeNameMarker.Create<T>();
			
		}

		TypeNameMarker ITopChildrenQuery.Type { get; set; }

		TopChildrenScore? ITopChildrenQuery.Score { get; set; }

		int? ITopChildrenQuery.Factor { get; set; }

		int? ITopChildrenQuery.IncrementalFactor { get; set; }

		IQueryContainer ITopChildrenQuery.Query { get; set; }

		bool? ITopChildrenQuery.Cache { get; set; }

		/// <summary>
		/// Provide a child query for the top_children query
		/// </summary>
		/// <param name="querySelector">Describe the child query to be executed</param>
		public TopChildrenQueryDescriptor<T> Query(Func<QueryDescriptor<T>, QueryContainer> querySelector)
		{
			var q = new QueryDescriptor<T>();
			((ITopChildrenQuery)this).Query = querySelector(q);
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
