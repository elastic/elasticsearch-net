using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IAliasRemoveIndexAction : IAliasAction
	{
		[JsonProperty("remove_index")]
		AliasRemoveIndexOperation RemoveIndex { get; set; }
	}

	public class AliasRemoveIndexAction : IAliasRemoveIndexAction
	{
		public AliasRemoveIndexOperation RemoveIndex { get; set; }
	}

	public class AliasRemoveIndexDescriptor : DescriptorBase<AliasRemoveIndexDescriptor, IAliasRemoveIndexAction>, IAliasRemoveIndexAction
	{
		AliasRemoveIndexOperation IAliasRemoveIndexAction.RemoveIndex { get; set; }

		public AliasRemoveIndexDescriptor()
		{
			Self.RemoveIndex = new AliasRemoveIndexOperation();
		}

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
