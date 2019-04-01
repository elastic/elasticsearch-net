using System;
using System.Runtime.Serialization;

namespace Nest
{
	public partial interface IUpdateByQueryRequest
	{
		[DataMember(Name ="query")]
		QueryContainer Query { get; set; }

		[DataMember(Name ="script")]
		IScript Script { get; set; }
	}

	public partial interface IUpdateByQueryRequest<T> where T : class { }

	public partial class UpdateByQueryRequest
	{
		public QueryContainer Query { get; set; }
		public IScript Script { get; set; }
	}

	public partial class UpdateByQueryRequest<T> where T : class
	{
	}

	public partial class UpdateByQueryDescriptor<T>
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
