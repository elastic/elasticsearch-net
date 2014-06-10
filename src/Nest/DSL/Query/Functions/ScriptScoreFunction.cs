using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class ScriptScoreFunction<T> : FunctionScoreFilteredFunction<T> where T : class
	{
		[JsonProperty(PropertyName = "script_score")]
		internal ScriptFilterDescriptor _ScriptScore { get; set; }

		public ScriptScoreFunction(Action<ScriptFilterDescriptor> scriptSelector)
		{
			var descriptor = new ScriptFilterDescriptor();
			if (scriptSelector != null)
				scriptSelector(descriptor);

			this._ScriptScore = descriptor;
		}
	}
}