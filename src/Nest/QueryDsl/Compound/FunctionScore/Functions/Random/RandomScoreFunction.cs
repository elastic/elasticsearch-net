using System;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<RandomScoreFunction>))]
	public interface IRandomScoreFunction : IScoreFunction
	{
		[JsonProperty("seed")]
		Union<long, string> Seed { get; set; }

		[JsonProperty("field")]
		Field Field { get; set; }
	}

	public class RandomScoreFunction: FunctionScoreFunctionBase, IRandomScoreFunction
	{
		public Union<long, string> Seed { get; set; }

		public Field Field { get; set; }
	}

	public class RandomScoreFunctionDescriptor<T> : FunctionScoreFunctionDescriptorBase<RandomScoreFunctionDescriptor<T>, IRandomScoreFunction,T>, IRandomScoreFunction
		where T : class
	{
		Union<long, string> IRandomScoreFunction.Seed { get; set; }
		Field IRandomScoreFunction.Field { get; set; }

		public RandomScoreFunctionDescriptor<T> Seed(long? seed) => Assign(a => a.Seed = seed);
		public RandomScoreFunctionDescriptor<T> Seed(string seed) => Assign(a => a.Seed = seed);
		public RandomScoreFunctionDescriptor<T> Field(Field field) => Assign(a => a.Field = field);
		public RandomScoreFunctionDescriptor<T> Field(Expression<Func<T, object>> objectPath) =>
			Assign(a => a.Field = objectPath);

	}
}
