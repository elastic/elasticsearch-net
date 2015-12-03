using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<RandomScoreFunction>))]
	public interface IRandomScoreFunction : IScoreFunction
	{
		[JsonProperty(PropertyName = "seed")]
		int? Seed { get; set; }
	}

	public class RandomScoreFunction: FunctionScoreFunctionBase, IRandomScoreFunction
	{
		public int? Seed { get; set; }
	}

	public class RandomScoreFunctionDescriptor<T> : FunctionScoreFunctionBaseDescriptor<RandomScoreFunctionDescriptor<T>, IRandomScoreFunction,T>, IRandomScoreFunction
		where T : class
	{
		int? IRandomScoreFunction.Seed { get; set; }

		public RandomScoreFunctionDescriptor<T> Seed(int? seed) => Assign(a => a.Seed = seed);

	}
}