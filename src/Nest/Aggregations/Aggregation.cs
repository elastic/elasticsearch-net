using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	///     Represents an aggregation on the request
	/// </summary>
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IAggregation
	{
		/// <summary>
		///     metadata to associate with the individual aggregation at request time that
		///     will be returned in place at response time
		/// </summary>
		IDictionary<string, object> Meta { get; set; }

		/// <summary>
		///     name of the aggregation
		/// </summary>
		string Name { get; set; }
	}

	/// <inheritdoc />
	public abstract class AggregationBase : IAggregation
	{
		internal AggregationBase() { }

		protected AggregationBase(string name) => ((IAggregation)this).Name = name;

		/// <inheritdoc />
		public IDictionary<string, object> Meta { get; set; }

		/// <inheritdoc />
		string IAggregation.Name { get; set; }

		public static AggregationBase operator &(AggregationBase left, AggregationBase right) =>
			new AggregationCombinator(null, left, right);

		//always evaluate to false so that each side of && equation is evaluated
		public static bool operator false(AggregationBase a) => false;

		//always evaluate to false so that each side of && equation is evaluated
		public static bool operator true(AggregationBase a) => false;

		internal abstract void WrapInContainer(AggregationContainer container);
	}

	/// <summary>
	///     Combines aggregations into a single list of aggregations
	/// </summary>
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
			switch (agg)
			{
				case null:
					return;
				case AggregationCombinator combinator when combinator.Aggregations.Any():
					Aggregations.AddRange(combinator.Aggregations);
					break;
				default:
					Aggregations.Add(agg);
					break;
			}
		}
	}
}
