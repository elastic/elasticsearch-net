using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// Represents an aggregation on the request
	/// </summary>
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IAggregation
	{
		IDictionary<string, object> Meta { get; set; }
		string Name { get; set; }
	}

	public abstract class AggregationBase : IAggregation
	{
		internal AggregationBase() { }

		protected AggregationBase(string name) => ((IAggregation)this).Name = name;

		public IDictionary<string, object> Meta { get; set; }
		string IAggregation.Name { get; set; }

		internal abstract void WrapInContainer(AggregationContainer container);

		//always evaluate to false so that each side of && equation is evaluated
		public static bool operator false(AggregationBase a) => false;

		//always evaluate to false so that each side of && equation is evaluated
		public static bool operator true(AggregationBase a) => false;

		public static AggregationBase operator &(AggregationBase left, AggregationBase right) => new AggregationCombinator(null, left, right);
	}

	internal class AggregationCombinator : AggregationBase, IAggregation
	{
		public AggregationCombinator(string name, AggregationBase left, AggregationBase right) : base(name)
		{
			AddAggregation(left);
			AddAggregation(right);
		}

		internal List<AggregationBase> Aggregations { get; } = new List<AggregationBase>();

		internal override void WrapInContainer(AggregationContainer container) { }

		private void AddAggregation(AggregationBase agg)
		{
			if (agg == null) return;

			var combinator = agg as AggregationCombinator;
			if ((combinator?.Aggregations.HasAny()).GetValueOrDefault(false))
				Aggregations.AddRange(combinator.Aggregations);
			else Aggregations.Add(agg);
		}
	}
}
