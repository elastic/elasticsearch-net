using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IExpFunction : IFunctionScoreFunction
	{
		[JsonProperty(PropertyName = "exp")]
		IDictionary<Field, IFunctionScoreDecayField> Exp { get; set; }
	}

	public class ExpFunction : FunctionScoreFunctionBase
	{
		public IDictionary<Field, IFunctionScoreDecayField> Exp { get; set; }
	}

	public class ExpFunctionDescriptor<T> : FunctionScoreFunctionBaseDescriptor<ExpFunctionDescriptor<T>, IExpFunction, T>, IExpFunction
		where T : class
	{

		IDictionary<Field, IFunctionScoreDecayField> IExpFunction.Exp { get; set; }

		public ExpFunctionDescriptor<T> Exp(Func<FluentDictionary<Field, IFunctionScoreDecayField>, FluentDictionary<Field, IFunctionScoreDecayField>> selector) =>
			Assign(a => a.Exp = selector?.Invoke(new FluentDictionary<Field, IFunctionScoreDecayField>()));

	}
}