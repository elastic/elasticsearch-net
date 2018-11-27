using System;
using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public interface IAliasRemoveAction : IAliasAction
	{
		[DataMember(Name ="remove")]
		AliasRemoveOperation Remove { get; set; }
	}

	public class AliasRemoveAction : IAliasRemoveAction
	{
		public AliasRemoveOperation Remove { get; set; }
	}

	public class AliasRemoveDescriptor : DescriptorBase<AliasRemoveDescriptor, IAliasRemoveAction>, IAliasRemoveAction
	{
		public AliasRemoveDescriptor() => Self.Remove = new AliasRemoveOperation();

		AliasRemoveOperation IAliasRemoveAction.Remove { get; set; }

		public AliasRemoveDescriptor Index(string index)
		{
			Self.Remove.Index = index;
			return this;
		}

		public AliasRemoveDescriptor Index(Type index)
		{
			Self.Remove.Index = index;
			return this;
		}

		public AliasRemoveDescriptor Index<T>() where T : class
		{
			Self.Remove.Index = typeof(T);
			return this;
		}

		public AliasRemoveDescriptor Alias(string alias)
		{
			Self.Remove.Alias = alias;
			return this;
		}
	}
}
