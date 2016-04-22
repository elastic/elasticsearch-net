using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[ContractJsonConverter(typeof(ScoreFunctionJsonConverter))]
	public interface IScoreFunction
	{
		[JsonProperty("filter")]
		QueryContainer Filter { get; set; }

		[JsonProperty("weight")]
		double? Weight { get; set; }
	}

	public class FunctionScoreFunction : FunctionScoreFunctionBase { }
	public abstract class FunctionScoreFunctionBase : IScoreFunction
	{
		public QueryContainer Filter { get; set; }
		public double? Weight { get; set; }
	}

	public class FunctionScoreFunctionDescriptor<T> : FunctionScoreFunctionDescriptorBase<FunctionScoreFunctionDescriptor<T>, IScoreFunction, T>
		where T : class { } 

	public abstract class FunctionScoreFunctionDescriptorBase<TDescriptor, TInterface, T> : 
		DescriptorBase<TDescriptor, TInterface>, IScoreFunction
		where TDescriptor : FunctionScoreFunctionDescriptorBase<TDescriptor, TInterface, T>, TInterface, IScoreFunction
		where TInterface : class, IScoreFunction
		where T : class
	{
		QueryContainer IScoreFunction.Filter { get; set; }

		double? IScoreFunction.Weight { get; set; }

		public TDescriptor Filter(Func<QueryContainerDescriptor<T>, QueryContainer> filterSelector) =>
			Assign(a => a.Filter = filterSelector?.Invoke(new QueryContainerDescriptor<T>()));

		public TDescriptor Weight(double? weight) => Assign(a => a.Weight = weight);
	}
}