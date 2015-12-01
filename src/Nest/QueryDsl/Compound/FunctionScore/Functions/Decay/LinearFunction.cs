using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface ILinearFunction : IFunctionScoreFunction
	{
		[JsonProperty(PropertyName = "linear")]
		IDictionary<Field, IFunctionScoreDecayField> Linear { get; set; }
	}

	public class LinearFunction : FunctionScoreFunctionBase
	{
		public IDictionary<Field, IFunctionScoreDecayField> Linear { get; set; }
	}
	public class LinearFunctionDescriptor<T> : FunctionScoreFunctionBaseDescriptor<LinearFunctionDescriptor<T>, ILinearFunction, T>, ILinearFunction
		where T : class
	{
		IDictionary<Field, IFunctionScoreDecayField> ILinearFunction.Linear { get; set; }

		public LinearFunctionDescriptor<T> Exp(Func<FluentDictionary<Field, IFunctionScoreDecayField>, FluentDictionary<Field, IFunctionScoreDecayField>> selector) =>
			Assign(a => a.Linear = selector?.Invoke(new FluentDictionary<Field, IFunctionScoreDecayField>()));

	}
}