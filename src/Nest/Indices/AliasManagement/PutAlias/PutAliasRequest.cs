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
using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// A request to put an alias to one or more indices
	/// </summary>
	[MapsApi("indices.put_alias.json")]
	public partial interface IPutAliasRequest
	{
		/// <inheritdoc cref="IAlias.Filter"/>
		[DataMember(Name = "filter")]
		QueryContainer Filter { get; set; }

		/// <inheritdoc cref="IAlias.IndexRouting"/>
		[DataMember(Name = "index_routing")]
		Routing IndexRouting { get; set; }

		/// <inheritdoc cref="IAlias.IsWriteIndex" />
		[DataMember(Name = "is_write_index")]
		bool? IsWriteIndex { get; set; }

		/// <inheritdoc cref="IAlias.Routing"/>
		[DataMember(Name = "routing")]
		Routing Routing { get; set; }

		/// <inheritdoc cref="IAlias.SearchRouting"/>
		[DataMember(Name = "search_routing")]
		Routing SearchRouting { get; set; }
	}

	/// <inheritdoc cref="IPutAliasRequest"/>
	public partial class PutAliasRequest
	{
		/// <inheritdoc cref="IPutAliasRequest.Filter"/>
		public QueryContainer Filter { get; set; }
		/// <inheritdoc cref="IPutAliasRequest.IndexRouting"/>
		public Routing IndexRouting { get; set; }
		/// <inheritdoc cref="IPutAliasRequest.IsWriteIndex" />
		public bool? IsWriteIndex { get; set; }
		/// <inheritdoc cref="IPutAliasRequest.Routing"/>
		public Routing Routing { get; set; }
		/// <inheritdoc cref="IPutAliasRequest.SearchRouting"/>
		public Routing SearchRouting { get; set; }
	}

	public partial class PutAliasDescriptor
	{
		QueryContainer IPutAliasRequest.Filter { get; set; }
		Routing IPutAliasRequest.IndexRouting { get; set; }
		bool? IPutAliasRequest.IsWriteIndex { get; set; }
		Routing IPutAliasRequest.Routing { get; set; }
		Routing IPutAliasRequest.SearchRouting { get; set; }

		/// <inheritdoc cref="IPutAliasRequest.Routing"/>
		public PutAliasDescriptor Routing(Routing routing) => Assign(routing, (a, v) => a.Routing = v);

		/// <inheritdoc cref="IPutAliasRequest.IndexRouting"/>
		public PutAliasDescriptor IndexRouting(Routing routing) => Assign(routing, (a, v) => a.IndexRouting = v);

		/// <inheritdoc cref="IPutAliasRequest.SearchRouting"/>
		public PutAliasDescriptor SearchRouting(Routing routing) => Assign(routing, (a, v) => a.SearchRouting = v);

		/// <inheritdoc cref="IPutAliasRequest.IsWriteIndex" />
		public PutAliasDescriptor IsWriteIndex(bool? isWriteIndex = true) => Assign(isWriteIndex, (a, v) => a.IsWriteIndex = v);

		/// <inheritdoc cref="IPutAliasRequest.Filter"/>
		public PutAliasDescriptor Filter<T>(Func<QueryContainerDescriptor<T>, QueryContainer> filterSelector) where T : class =>
			Assign(filterSelector, (a, v) => a.Filter = v?.Invoke(new QueryContainerDescriptor<T>()));
	}
}
