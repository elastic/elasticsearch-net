// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	[MapsApi("indices.update_aliases.json")]
	public partial interface IBulkAliasRequest
	{
		[DataMember(Name ="actions")]
		IList<IAliasAction> Actions { get; set; }
	}

	public partial class BulkAliasRequest
	{
		public IList<IAliasAction> Actions { get; set; }
	}

	public partial class BulkAliasDescriptor
	{
		IList<IAliasAction> IBulkAliasRequest.Actions { get; set; } = new List<IAliasAction>();

		public BulkAliasDescriptor Add(IAliasAction action) =>
			Fluent.Assign<BulkAliasDescriptor, IBulkAliasRequest, IAliasAction>(this, action, (a, v) => a.Actions.AddIfNotNull(v));

		public BulkAliasDescriptor Add(Func<AliasAddDescriptor, IAliasAddAction> addSelector) => Add(addSelector?.Invoke(new AliasAddDescriptor()));

		public BulkAliasDescriptor Remove(Func<AliasRemoveDescriptor, IAliasRemoveAction> removeSelector) =>
			Add(removeSelector?.Invoke(new AliasRemoveDescriptor()));

		public BulkAliasDescriptor RemoveIndex(Func<AliasRemoveIndexDescriptor, IAliasRemoveIndexAction> removeIndexSelector) =>
			Add(removeIndexSelector?.Invoke(new AliasRemoveIndexDescriptor()));
	}
}
