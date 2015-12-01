using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IBoostFactorFunction : IFunctionScoreFunction
	{
		[JsonProperty(PropertyName = "boost_factor")]
		double? BoostFactor { get; set; }
	}

	public class BoostFactorFunction : FunctionScoreFunctionBase, IBoostFactorFunction
	{
		public double? BoostFactor { get; set; }
	}


	public class BoostFactorFunctionDescriptor<T> 
		: FunctionScoreFunctionBaseDescriptor<BoostFactorFunctionDescriptor<T>, IBoostFactorFunction, T>, IBoostFactorFunction
		where T : class
	{
		double? IBoostFactorFunction.BoostFactor { get; set; }

		public BoostFactorFunctionDescriptor<T> BoostFactor(double? boostFactor) => Assign(a => a.BoostFactor = boostFactor);

	}
}