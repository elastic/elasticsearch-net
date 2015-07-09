using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class ScriptScoreFunction<T> : FunctionScoreFunction<T> where T : class
	{
		[JsonProperty(PropertyName = "script_score")]
		internal IScriptQuery _ScriptScore { get; set; }

		public ScriptScoreFunction(Func<ScriptQueryDescriptor<T>, IScriptQuery> scriptSelector)
		{
			this._ScriptScore = scriptSelector(new ScriptQueryDescriptor<T>());
		}
	}
}