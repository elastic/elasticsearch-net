// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	public interface IAliasRemoveIndexAction : IAliasAction
	{
		[DataMember(Name ="remove_index")]
		AliasRemoveIndexOperation RemoveIndex { get; set; }
	}

	public class AliasRemoveIndexAction : IAliasRemoveIndexAction
	{
		public AliasRemoveIndexOperation RemoveIndex { get; set; }
	}

	public class AliasRemoveIndexDescriptor : DescriptorBase<AliasRemoveIndexDescriptor, IAliasRemoveIndexAction>, IAliasRemoveIndexAction
	{
		public AliasRemoveIndexDescriptor() => Self.RemoveIndex = new AliasRemoveIndexOperation();

		AliasRemoveIndexOperation IAliasRemoveIndexAction.RemoveIndex { get; set; }

		public AliasRemoveIndexDescriptor Index(IndexName index)
		{
			Self.RemoveIndex.Index = index;
			return this;
		}

		public AliasRemoveIndexDescriptor Index(Type index)
		{
			Self.RemoveIndex.Index = index;
			return this;
		}

		public AliasRemoveIndexDescriptor Index<T>() where T : class
		{
			Self.RemoveIndex.Index = typeof(T);
			return this;
		}
	}
}
