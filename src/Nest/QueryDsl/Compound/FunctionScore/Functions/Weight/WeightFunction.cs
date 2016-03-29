using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IWeightFunction : IScoreFunction
	{
	}

	public class WeightFunction : FunctionScoreFunctionBase, IWeightFunction { }

	public class WeightFunctionDescriptor<T> : FunctionScoreFunctionDescriptorBase<WeightFunctionDescriptor<T>, IWeightFunction,T> , IWeightFunction
		where T : class
	{
	}
}
