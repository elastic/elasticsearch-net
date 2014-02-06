using System;
using Newtonsoft.Json;

namespace Nest
{
	public class AliasRemoveDescriptor : IAliasAction
	{
		public AliasRemoveDescriptor()
		{
			this.Remove = new AliasRemoveOperation();
		}

		[JsonProperty("remove")]
		internal AliasRemoveOperation Remove { get; private set; }

		public AliasRemoveDescriptor Index(string index)
		{
			this.Remove.Index = index;
			return this;
		}
		public AliasRemoveDescriptor Index(Type index)
		{
			this.Remove.Index = index;
			return this;
		}
		public AliasRemoveDescriptor Index<T>() where T : class
		{
			this.Remove.Index = typeof(T);
			return this;
		}
		public AliasRemoveDescriptor Alias(string alias)
		{
			this.Remove.Alias = alias;
			return this;
		}
	}
}