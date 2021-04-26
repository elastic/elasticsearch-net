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
using Nest.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	public interface IAliasAddAction : IAliasAction
	{
		[DataMember(Name ="add")]
		AliasAddOperation Add { get; set; }
	}

	public class AliasAddAction : IAliasAddAction
	{
		public AliasAddOperation Add { get; set; }
	}

	public class AliasAddDescriptor : DescriptorBase<AliasAddDescriptor, IAliasAddAction>, IAliasAddAction
	{
		public AliasAddDescriptor() => Self.Add = new AliasAddOperation();

		AliasAddOperation IAliasAddAction.Add { get; set; }

		/// <inheritdoc cref="AliasAddOperation.Index"/>
		public AliasAddDescriptor Index(string index)
		{
			Self.Add.Index = index;
			return this;
		}

		/// <inheritdoc cref="AliasAddOperation.Index"/>
		public AliasAddDescriptor Index(Type index)
		{
			Self.Add.Index = index;
			return this;
		}

		/// <inheritdoc cref="AliasAddOperation.Index"/>
		public AliasAddDescriptor Index<T>() where T : class
		{
			Self.Add.Index = typeof(T);
			return this;
		}

		/// <inheritdoc cref="AliasAddOperation.Indices"/>
		public AliasAddDescriptor Indices(Indices indices)
		{
			Self.Add.Indices = indices;
			return this;
		}

		/// <inheritdoc cref="AliasAddOperation.Alias"/>
		public AliasAddDescriptor Alias(string alias)
		{
			Self.Add.Alias = alias;
			return this;
		}

		/// <inheritdoc cref="AliasAddOperation.Aliases"/>
		public AliasAddDescriptor Aliases(IEnumerable<string> aliases)
		{
			Self.Add.Aliases = aliases;
			return this;
		}

		/// <inheritdoc cref="AliasAddOperation.Aliases"/>
		public AliasAddDescriptor Aliases(params string[] aliases)
		{
			Self.Add.Aliases = aliases;
			return this;
		}

		/// <inheritdoc cref="AliasAddOperation.Routing"/>
		public AliasAddDescriptor Routing(string routing)
		{
			Self.Add.Routing = routing;
			return this;
		}

		/// <inheritdoc cref="AliasAddOperation.IndexRouting"/>
		public AliasAddDescriptor IndexRouting(string indexRouting)
		{
			Self.Add.IndexRouting = indexRouting;
			return this;
		}

		/// <inheritdoc cref="AliasAddOperation.SearchRouting"/>
		public AliasAddDescriptor SearchRouting(string searchRouting)
		{
			Self.Add.SearchRouting = searchRouting;
			return this;
		}

		/// <inheritdoc cref="AliasAddOperation.IsWriteIndex"/>
		public AliasAddDescriptor IsWriteIndex(bool? isWriteIndex = true)
		{
			Self.Add.IsWriteIndex = isWriteIndex;
			return this;
		}

		/// <inheritdoc cref="AliasAddOperation.IsHidden"/>
		public AliasAddDescriptor IsHidden(bool? isHidden = true)
		{
			Self.Add.IsHidden = isHidden;
			return this;
		}

		/// <inheritdoc cref="AliasAddOperation.Filter"/>
		public AliasAddDescriptor Filter<T>(Func<QueryContainerDescriptor<T>, QueryContainer> filterSelector)
			where T : class
		{
			Self.Add.Filter = filterSelector?.Invoke(new QueryContainerDescriptor<T>());
			return this;
		}
	}
}
