using System;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<RandomScoreFunction>))]
	public interface IRandomScoreFunction : IScoreFunction
	{
		[JsonProperty("field")]
		Field Field { get; set; }

		[JsonProperty("seed")]
		Union<long, string> Seed { get; set; }
	}

	public class RandomScoreFunction : FunctionScoreFunctionBase, IRandomScoreFunction
	{
		public Field Field { get; set; }
		public Union<long, string> Seed { get; set; }
	}

	public class RandomScoreFunctionDescriptor<T>
		: FunctionScoreFunctionDescriptorBase<RandomScoreFunctionDescriptor<T>, IRandomScoreFunction, T>, IRandomScoreFunction
		where T : class
	{
		Field IRandomScoreFunction.Field { get; set; }
		Union<long, string> IRandomScoreFunction.Seed { get; set; }

		public RandomScoreFunctionDescriptor<T> Seed(long? seed) => Assign(seed, (a, v) => a.Seed = v);

		public RandomScoreFunctionDescriptor<T> Seed(string seed) => Assign(seed, (a, v) => a.Seed = v);

		public RandomScoreFunctionDescriptor<T> Field(Field field) => Assign(field, (a, v) => a.Field = v);

		public RandomScoreFunctionDescriptor<T> Field(Expression<Func<T, object>> objectPath) =>
			Assign(objectPath, (a, v) => a.Field = v);
	}
}
