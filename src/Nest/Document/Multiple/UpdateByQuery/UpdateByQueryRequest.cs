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

	// ReSharper disable once UnusedMember.Global
	// ReSharper disable once UnusedTypeParameter
	public partial interface IUpdateByQueryRequest<TDocument> where TDocument : class { }

	public partial class UpdateByQueryRequest
	{
		public QueryContainer Query { get; set; }
		public IScript Script { get; set; }
	}

	// ReSharper disable once UnusedTypeParameter
	public partial class UpdateByQueryRequest<TDocument> where TDocument : class
	{
	}

	public partial class UpdateByQueryDescriptor<TDocument>
		where TDocument : class
	{
		QueryContainer IUpdateByQueryRequest.Query { get; set; }
		IScript IUpdateByQueryRequest.Script { get; set; }

		public UpdateByQueryDescriptor<TDocument> MatchAll() => Assign(new QueryContainerDescriptor<TDocument>().MatchAll(), (a, v) => a.Query = v);

		public UpdateByQueryDescriptor<TDocument> Query(Func<QueryContainerDescriptor<TDocument>, QueryContainer> querySelector) =>
			Assign(querySelector, (a, v) => a.Query = v?.Invoke(new QueryContainerDescriptor<TDocument>()));

		public UpdateByQueryDescriptor<TDocument> Script(string script) => Assign((InlineScript)script, (a, v) => a.Script = v);

		public UpdateByQueryDescriptor<TDocument> Script(Func<ScriptDescriptor, IScript> scriptSelector) =>
			Assign(scriptSelector, (a, v) => a.Script = v?.Invoke(new ScriptDescriptor()));
	}
}
