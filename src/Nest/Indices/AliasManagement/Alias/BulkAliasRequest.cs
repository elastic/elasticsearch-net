/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

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
