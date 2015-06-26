using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class ScriptScoreFunction<T> : FunctionScoreFunction<T> where T : class
	{
		[JsonProperty(PropertyName = "script_score")]
		internal ScriptQueryDescriptor<T> _ScriptScore { get; set; }

		public ScriptScoreFunction(Action<ScriptQueryDescriptor<T>> scriptSelector)
		{
			var descriptor = new ScriptQueryDescriptor<T>();
			if (scriptSelector != null)
				scriptSelector(descriptor);

			this._ScriptScore = descriptor;
		}
	}
}