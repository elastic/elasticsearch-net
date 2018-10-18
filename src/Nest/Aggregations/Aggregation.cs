using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	/// <summary>
	/// Represents an aggregation on the request
	/// </summary>
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IAggregation
	{
		/// <summary>
		/// name of the aggregation
		/// </summary>
		string Name { get; set; }

		/// <summary>
		/// metadata to associate with the individual aggregation at request time that
		/// will be returned in place at response time
		/// </summary>
		IDictionary<string, object> Meta { get; set; }
	}

	/// <inheritdoc />
	public abstract class AggregationBase : IAggregation
	{
		/// <inheritdoc />
		string IAggregation.Name { get; set; }

		/// <inheritdoc />
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

		public static AggregationBase operator &(AggregationBase left, AggregationBase right) =>
			new AggregationCombinator(null, left, right);
	}

	/// <summary>
	/// Combines aggregations into a single list of aggregations
	/// </summary>
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
			switch (agg)
			{
				case null:
					return;
				case AggregationCombinator combinator when combinator.Aggregations.Any():
					this.Aggregations.AddRange(combinator.Aggregations);
					break;
				default:
					this.Aggregations.Add(agg);
					break;
			}
		}
	}
}
