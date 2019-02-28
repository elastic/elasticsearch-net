using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[InterfaceDataContract]
	public interface IWeightFunction : IScoreFunction { }

	public class WeightFunction : FunctionScoreFunctionBase, IWeightFunction { }

	public class WeightFunctionDescriptor<T> : FunctionScoreFunctionDescriptorBase<WeightFunctionDescriptor<T>, IWeightFunction, T>, IWeightFunction
		where T : class { }
}
