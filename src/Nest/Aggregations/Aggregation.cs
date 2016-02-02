using Newtonsoft.Json;
using System.Collections.Generic;

namespace Nest
{
	/// <summary>
	/// Represents an aggregation on the request
	/// </summary>
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IAggregation 
	{
		string Name { get; set; }
		IDictionary<string, object> Meta { get; set; }
	}

	public abstract class AggregationBase : IAggregation
	{
		string IAggregation.Name { get; set; }
		
		public IDictionary<string, object> Meta { get; set; }

		internal AggregationBase() { }

		protected AggregationBase(string name)
		{
			((IAggregation)this).Name = name;
		}

		internal abstract void WrapInContainer(AggregationContainer container);
		
		//always evaluate to false so that each side of && equation is evaluated
		public static bool operator false(AggregationBase a) => false;

		//always evaluate to false so that each side of && equation is evaluated
		public static bool operator true(AggregationBase a) => false;

		public static AggregationBase operator &(AggregationBase left, AggregationBase right)
		{
			return new AggregationCombinator(null, left, right);
		}
	}

	internal class AggregationCombinator : AggregationBase, IAggregation
	{
		internal List<AggregationBase> Aggregations { get; } = new List<AggregationBase>();

		internal override void WrapInContainer(AggregationContainer container) { }

		public AggregationCombinator(string name, AggregationBase left, AggregationBase right) : base(name)
		{
			this.AddAggregation(left);
			this.AddAggregation(right);
		}

		private void AddAggregation(AggregationBase agg)
		{
			if (agg == null) return;
			var combinator = agg as AggregationCombinator;
			if ((combinator?.Aggregations.HasAny()).GetValueOrDefault(false))
			{
				this.Aggregations.AddRange(combinator.Aggregations);
			}
			else this.Aggregations.Add(agg);
		}
	}
}