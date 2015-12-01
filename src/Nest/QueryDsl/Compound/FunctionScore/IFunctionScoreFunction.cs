using Newtonsoft.Json;
using System;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<FunctionScoreFunction>))]
	public interface IFunctionScoreFunction
	{
		[JsonProperty("filter")]
		QueryContainer Filter { get; set; }

		[JsonProperty("weight")]
		double? Weight { get; set; }
	}

	public class FunctionScoreFunction : FunctionScoreFunctionBase { }
	public abstract class FunctionScoreFunctionBase : IFunctionScoreFunction
	{
		public QueryContainer Filter { get; set; }
		public double? Weight { get; set; }
	}

	public class FunctionScoreFunctionDescriptor<T> : FunctionScoreFunctionBaseDescriptor<FunctionScoreFunctionDescriptor<T>, IFunctionScoreFunction, T>
		where T : class { } 

	public abstract class FunctionScoreFunctionBaseDescriptor<TDescriptor, TInterface, T> : 
		DescriptorBase<TDescriptor, TInterface>, IFunctionScoreFunction
		where TDescriptor : FunctionScoreFunctionBaseDescriptor<TDescriptor, TInterface, T>, TInterface, IFunctionScoreFunction
		where TInterface : class, IFunctionScoreFunction
		where T : class
	{
		QueryContainer IFunctionScoreFunction.Filter { get; set; }

		double? IFunctionScoreFunction.Weight { get; set; }

		public TDescriptor Filter(Func<QueryContainerDescriptor<T>, QueryContainer> filterSelector) =>
			Assign(a => a.Filter = filterSelector?.Invoke(new QueryContainerDescriptor<T>()));

		public TDescriptor Weight(double? weight) => Assign(a => a.Weight = weight);
	}
}