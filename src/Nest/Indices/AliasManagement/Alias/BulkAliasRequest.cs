using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IBulkAliasRequest 
	{
		[JsonProperty("actions")]
		IList<IAliasAction> Actions { get; set; }
	}

	public partial class BulkAliasRequest 
	{
		public IList<IAliasAction> Actions { get; set; }
	}


	[DescriptorFor("IndicesUpdateAliases")]
	public partial class BulkAliasDescriptor 
	{
		public BulkAliasDescriptor Add(IAliasAction action) => 
			Fluent.Assign<BulkAliasDescriptor, IBulkAliasRequest>(this, a=> a.Actions.AddIfNotNull(action));

		IList<IAliasAction> IBulkAliasRequest.Actions { get; set; } = new List<IAliasAction>();

		public BulkAliasDescriptor Add(Func<AliasAddDescriptor, IAliasAddAction> addSelector) => Add(addSelector?.Invoke(new AliasAddDescriptor()));

		public BulkAliasDescriptor Remove(Func<AliasRemoveDescriptor, IAliasRemoveAction> removeSelector)=> Add(removeSelector?.Invoke(new AliasRemoveDescriptor()));
	}
}
