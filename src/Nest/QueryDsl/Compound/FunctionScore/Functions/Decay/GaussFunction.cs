using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IGaussFunction : IFunctionScoreFunction
	{
		[JsonProperty(PropertyName = "gauss")]
		IDictionary<Field, IFunctionScoreDecayField> Gauss { get; set; }
	}

	public class GaussFunction : FunctionScoreFunctionBase
	{
		public IDictionary<Field, IFunctionScoreDecayField> Gauss { get; set; }
	}

	public class GaussFunctionDescriptor<T> : FunctionScoreFunctionBaseDescriptor<GaussFunctionDescriptor<T>, IGaussFunction, T>, IGaussFunction
		where T : class
	{
		IDictionary<Field, IFunctionScoreDecayField> IGaussFunction.Gauss { get; set; }

		public GaussFunctionDescriptor<T> Exp(Func<FluentDictionary<Field, IFunctionScoreDecayField>, FluentDictionary<Field, IFunctionScoreDecayField>> selector) =>
			Assign(a => a.Gauss = selector?.Invoke(new FluentDictionary<Field, IFunctionScoreDecayField>()));


	}
}