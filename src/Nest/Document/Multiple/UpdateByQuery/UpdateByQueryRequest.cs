using System;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IUpdateByQueryRequest
	{
		[JsonProperty("query")]
		QueryContainer Query { get; set; }

		[JsonProperty("script")]
		IScript Script { get; set; }
	}

	public interface IUpdateByQueryRequest<T> : IUpdateByQueryRequest where T : class { }

	public partial class UpdateByQueryRequest
	{
		public QueryContainer Query { get; set; }
		public IScript Script { get; set; }
	}

	public partial class UpdateByQueryRequest<T> : IUpdateByQueryRequest<T>
		where T : class
	{
		public QueryContainer Query { get; set; }
		public IScript Script { get; set; }
	}

	public partial class UpdateByQueryDescriptor<T> : IUpdateByQueryRequest<T>
		where T : class
	{
		QueryContainer IUpdateByQueryRequest.Query { get; set; }
		IScript IUpdateByQueryRequest.Script { get; set; }

		public UpdateByQueryDescriptor<T> MatchAll() => Assign(new QueryContainerDescriptor<T>().MatchAll(), (a, v) => a.Query = v);

		public UpdateByQueryDescriptor<T> Query(Func<QueryContainerDescriptor<T>, QueryContainer> querySelector) =>
			Assign(querySelector, (a, v) => a.Query = v?.Invoke(new QueryContainerDescriptor<T>()));

		public UpdateByQueryDescriptor<T> Script(string script) => Assign((InlineScript)script, (a, v) => a.Script = v);

		public UpdateByQueryDescriptor<T> Script(Func<ScriptDescriptor, IScript> scriptSelector) =>
			Assign(scriptSelector, (a, v) => a.Script = v?.Invoke(new ScriptDescriptor()));
	}
}
