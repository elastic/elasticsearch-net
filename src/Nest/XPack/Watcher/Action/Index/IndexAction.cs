using System;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public interface IIndexAction : IAction
	{
		[JsonProperty("doc_type")]
		TypeName DocType { get; set; }

		[JsonProperty("execution_time_field")]
		Field ExecutionTimeField { get; set; }

		[JsonProperty("index")]
		IndexName Index { get; set; }

		[JsonProperty("timeout")]
		Time Timeout { get; set; }
	}

	public class IndexAction : ActionBase, IIndexAction
	{
		public IndexAction(string name) : base(name) { }

		public override ActionType ActionType => ActionType.Index;

		public TypeName DocType { get; set; }

		public Field ExecutionTimeField { get; set; }

		public IndexName Index { get; set; }

		public Time Timeout { get; set; }
	}

	public class IndexActionDescriptor : ActionsDescriptorBase<IndexActionDescriptor, IIndexAction>, IIndexAction
	{
		public IndexActionDescriptor(string name) : base(name) { }

		protected override ActionType ActionType => ActionType.Index;
		TypeName IIndexAction.DocType { get; set; }
		Field IIndexAction.ExecutionTimeField { get; set; }
		IndexName IIndexAction.Index { get; set; }
		Time IIndexAction.Timeout { get; set; }

		public IndexActionDescriptor Index(IndexName index) => Assign(index, (a, v) => a.Index = v);

		public IndexActionDescriptor Index<T>() => Assign(typeof(T), (a, v) => a.Index = v);

		public IndexActionDescriptor DocType(TypeName type) => Assign(type, (a, v) => a.DocType = v);

		public IndexActionDescriptor DocType<T>() => Assign(typeof(T), (a, v) => a.DocType = v);

		public IndexActionDescriptor ExecutionTimeField(Field field) => Assign(field, (a, v) => a.ExecutionTimeField = v);

		public IndexActionDescriptor ExecutionTimeField<T>(Expression<Func<T, object>> objectPath) => Assign(objectPath, (a, v) => a.ExecutionTimeField = v);

		public IndexActionDescriptor Timeout(Time timeout) => Assign(timeout, (a, v) => a.Timeout = v);
	}
}
