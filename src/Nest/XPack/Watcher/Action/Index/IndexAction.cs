using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	public interface IIndexAction : IAction
	{
		[DataMember(Name = "doc_type")]
		TypeName DocType { get; set; }

		[DataMember(Name = "execution_time_field")]
		Field ExecutionTimeField { get; set; }

		[DataMember(Name = "index")]
		IndexName Index { get; set; }

		[DataMember(Name = "timeout")]
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

		public IndexActionDescriptor Index(IndexName index) => Assign(a => a.Index = index);

		public IndexActionDescriptor Index<T>() => Assign(a => a.Index = typeof(T));

		public IndexActionDescriptor DocType(TypeName type) => Assign(a => a.DocType = type);

		public IndexActionDescriptor DocType<T>() => Assign(a => a.DocType = typeof(T));

		public IndexActionDescriptor ExecutionTimeField(Field field) => Assign(a => a.ExecutionTimeField = field);

		public IndexActionDescriptor ExecutionTimeField<T>(Expression<Func<T, object>> objectPath) => Assign(a => a.ExecutionTimeField = objectPath);

		public IndexActionDescriptor Timeout(Time timeout) => Assign(a => a.Timeout = timeout);
	}
}
