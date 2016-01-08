using System;
using System.Collections.Generic;
using System.Linq;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	public class RandomScoreFunction<T> : FunctionScoreFunction<T> where T : class
	{
		[JsonProperty(PropertyName = "random_score")]
		internal RandomScoreFunction _RandomScore { get; set; }

		public RandomScoreFunction(int? seed = null)
		{
			var randomScore = seed.HasValue 
				? new RandomScoreFunction(seed.Value) 
				: new RandomScoreFunction();
			
			this._RandomScore = randomScore;
		}
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<RandomScoreFunction>))]
	public interface IRandomScoreFunction
	{
		[JsonProperty(PropertyName = "seed")]
		int? Seed { get; set; }
	}

	public class RandomScoreFunction : IRandomScoreFunction
	{
		int? IRandomScoreFunction.Seed { get; set; }

		public RandomScoreFunction(int seed)
		{
			((IRandomScoreFunction)this).Seed = seed;
		}

		public RandomScoreFunction()
		{
		}
	}
}