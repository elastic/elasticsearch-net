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
using Elasticsearch.Net.Utf8Json;

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
