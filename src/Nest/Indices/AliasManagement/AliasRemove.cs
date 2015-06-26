using System;
using Newtonsoft.Json;

namespace Nest
{

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IAliasRemoveAction : IAliasAction
	{
		[JsonProperty("remove")]
		AliasRemoveOperation Remove { get; set; }
	}

	public class AliasRemoveAction : IAliasRemoveAction
	{
		public AliasRemoveOperation Remove { get; set; }
	}

	public class AliasRemoveDescriptor : IAliasRemoveAction
	{
		private IAliasRemoveAction Self { get { return this; } }
		AliasRemoveOperation IAliasRemoveAction.Remove { get; set; }

		public AliasRemoveDescriptor()
		{
			Self.Remove = new AliasRemoveOperation();
		}


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