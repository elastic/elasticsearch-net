using System;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public interface IIndexAction : IAction
	{
		[JsonProperty("index")]
		IndexName Index { get; set; }

		[JsonProperty("doc_type")]
		TypeName DocType { get; set; }

		[JsonProperty("execution_time_field")]
		Field ExecutionTimeField { get; set; }

		[JsonProperty("timeout")]
		Time Timeout { get; set; }
	}

	public class IndexAction : ActionBase, IIndexAction
	{
		public override ActionType ActionType => ActionType.Index;

		public IndexName Index { get; set; }

		public TypeName DocType { get; set; }

		public Field ExecutionTimeField { get; set; }

		public Time Timeout { get; set; }

		public IndexAction(string name) : base(name) {}
	}

	public class IndexActionDescriptor : ActionsDescriptorBase<IndexActionDescriptor, IIndexAction>, IIndexAction
	{
		IndexName IIndexAction.Index { get; set; }
		TypeName IIndexAction.DocType { get; set; }
		Field IIndexAction.ExecutionTimeField { get; set; }
		Time IIndexAction.Timeout { get; set; }

		protected override ActionType ActionType => ActionType.Index;

		public IndexActionDescriptor(string name) : base(name) {}

		public IndexActionDescriptor Index(IndexName index) => Assign(a => a.Index = index);

		public IndexActionDescriptor Index<T>() => Assign(a => a.Index = typeof(T));

		public IndexActionDescriptor DocType(TypeName type) => Assign(a => a.DocType = type);

		public IndexActionDescriptor DocType<T>() => Assign(a => a.DocType = typeof(T));

		public IndexActionDescriptor ExecutionTimeField(Field field) => Assign(a => a.ExecutionTimeField = field);

		public IndexActionDescriptor ExecutionTimeField<T>(Expression<Func<T, object>> objectPath) => Assign(a => a.ExecutionTimeField = objectPath);

		public IndexActionDescriptor Timeout(Time timeout) => Assign(a => a.Timeout = timeout);
	}
}
