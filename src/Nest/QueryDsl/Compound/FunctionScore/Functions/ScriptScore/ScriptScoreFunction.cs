using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IScriptScoreFunction : IFunctionScoreFunction
	{
		[JsonProperty(PropertyName = "script_score")]
		IScriptQuery ScriptScore { get; set; }
	}

	public class ScriptScoreFunction : FunctionScoreFunctionBase, IScriptScoreFunction
	{
		public IScriptQuery ScriptScore { get; set; }
	}

	public class ScriptScoreFunctionDescriptor<T> :
		FunctionScoreFunctionBaseDescriptor<ScriptScoreFunctionDescriptor<T>, IScriptScoreFunction, T> , IScriptScoreFunction
		where T : class
	{
		IScriptQuery IScriptScoreFunction.ScriptScore { get; set; }

		public ScriptScoreFunctionDescriptor<T> ScriptScore(Func<ScriptQueryDescriptor<T>, IScriptQuery> selector) =>
			Assign(a => a.ScriptScore = selector?.Invoke(new ScriptQueryDescriptor<T>()));
	}
}