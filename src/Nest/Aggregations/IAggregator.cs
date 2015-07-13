using System;
using System.Collections.Generic;

namespace Nest
{
	/// <summary>
	/// Describes an aggregation at request time when its being build
	/// </summary>
	public interface IAggregator { }

	/// <summary>
	/// Base for the OIS Aggregation DSL
	/// </summary>
	public interface IAggregatorBase : IAggregator
	{
		string Name { get; set; }
	}

	public abstract class AggregatorBase : IAggregatorBase
	{
		string IAggregatorBase.Name { get; set; }
		
		protected AggregatorBase(string name)
		{
			((IAggregatorBase) this).Name = name;
		}

		internal abstract void WrapInContainer(AggregationContainer container);
		
		//always evaluate to false so that each side of && equation is evaluated
		public static bool operator false(AggregatorBase a) => false;

		//always evaluate to false so that each side of && equation is evaluated
		public static bool operator true(AggregatorBase a) => false;

		public static AggregatorBase operator &(AggregatorBase left, AggregatorBase right)
		{
			return new AggregatorCombinator(null, left, right);
		}
	}

	internal class AggregatorCombinator : AggregatorBase, IAggregatorBase
	{
		string IAggregatorBase.Name { get; set; }

		internal List<AggregatorBase> Aggregations { get; } = new List<AggregatorBase>();

		internal override void WrapInContainer(AggregationContainer container) { }

		public AggregatorCombinator(string name, AggregatorBase left, AggregatorBase right) : base(name)
		{
			this.AddAggregator(left);
			this.AddAggregator(right);
		}

		private void AddAggregator(AggregatorBase agg)
		{
			if (agg == null) return;
			var combinator = agg as AggregatorCombinator;
			if ((combinator?.Aggregations.HasAny()).GetValueOrDefault(false))
			{
				this.Aggregations.AddRange(combinator.Aggregations);
			}
			else this.Aggregations.Add(agg);
		}
	}


}