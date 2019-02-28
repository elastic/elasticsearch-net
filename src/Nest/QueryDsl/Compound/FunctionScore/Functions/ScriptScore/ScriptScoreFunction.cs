using System;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[InterfaceDataContract]
	public interface IScriptScoreFunction : IScoreFunction
	{
		[DataMember(Name = "script")]
		IScriptQuery Script { get; set; }
	}

	public class ScriptScoreFunction : FunctionScoreFunctionBase, IScriptScoreFunction
	{
		public IScriptQuery Script { get; set; }
	}

	public class ScriptScoreFunctionDescriptor<T>
		: FunctionScoreFunctionDescriptorBase<ScriptScoreFunctionDescriptor<T>, IScriptScoreFunction, T>, IScriptScoreFunction
		where T : class
	{
		IScriptQuery IScriptScoreFunction.Script { get; set; }

		public ScriptScoreFunctionDescriptor<T> Script(Func<ScriptQueryDescriptor<T>, IScriptQuery> selector) =>
			Assign(a => a.Script = selector?.Invoke(new ScriptQueryDescriptor<T>()));
	}
}
