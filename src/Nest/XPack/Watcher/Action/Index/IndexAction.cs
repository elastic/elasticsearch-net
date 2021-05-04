// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	public interface IIndexAction : IAction
	{
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

		public Field ExecutionTimeField { get; set; }

		public IndexName Index { get; set; }

		public Time Timeout { get; set; }
	}

	public class IndexActionDescriptor : ActionsDescriptorBase<IndexActionDescriptor, IIndexAction>, IIndexAction
	{
		public IndexActionDescriptor(string name) : base(name) { }

		protected override ActionType ActionType => ActionType.Index;
		Field IIndexAction.ExecutionTimeField { get; set; }
		IndexName IIndexAction.Index { get; set; }
		Time IIndexAction.Timeout { get; set; }

		public IndexActionDescriptor Index(IndexName index) => Assign(index, (a, v) => a.Index = v);

		public IndexActionDescriptor Index<T>() => Assign(typeof(T), (a, v) => a.Index = v);

		public IndexActionDescriptor ExecutionTimeField(Field field) => Assign(field, (a, v) => a.ExecutionTimeField = v);

		public IndexActionDescriptor ExecutionTimeField<T, TValue>(Expression<Func<T, TValue>> objectPath) => Assign(objectPath, (a, v) => a.ExecutionTimeField = v);

		public IndexActionDescriptor Timeout(Time timeout) => Assign(timeout, (a, v) => a.Timeout = v);
	}
}
