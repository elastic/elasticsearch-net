using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class ScriptScoreFunction<T> : FunctionScoreFunction<T> where T : class
	{
		[JsonProperty(PropertyName = "script_score")]
		internal ScriptQueryDescriptor _ScriptScore { get; set; }

		public ScriptScoreFunction(Action<ScriptQueryDescriptor> scriptSelector)
		{
			var descriptor = new ScriptQueryDescriptor();
			if (scriptSelector != null)
				scriptSelector(descriptor);

			this._ScriptScore = descriptor;
		}
	}
}