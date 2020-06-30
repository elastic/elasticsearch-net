// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	public interface IAliasRemoveAction : IAliasAction
	{
		[DataMember(Name = "remove")]
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

		/// <inheritdoc cref="AliasRemoveOperation.Index"/>
		public AliasRemoveDescriptor Index(string index)
		{
			Self.Remove.Index = index;
			return this;
		}

		/// <inheritdoc cref="AliasRemoveOperation.Index"/>
		public AliasRemoveDescriptor Index(Type index)
		{
			Self.Remove.Index = index;
			return this;
		}

		/// <inheritdoc cref="AliasRemoveOperation.Index"/>
		public AliasRemoveDescriptor Index<T>() where T : class
		{
			Self.Remove.Index = typeof(T);
			return this;
		}

		/// <inheritdoc cref="AliasRemoveOperation.Indices"/>
		public AliasRemoveDescriptor Indices(Indices indices)
		{
			Self.Remove.Indices = indices;
			return this;
		}

		/// <inheritdoc cref="AliasRemoveOperation.Alias"/>
		public AliasRemoveDescriptor Alias(string alias)
		{
			Self.Remove.Alias = alias;
			return this;
		}

		/// <inheritdoc cref="AliasRemoveOperation.Aliases"/>
		public AliasRemoveDescriptor Aliases(IEnumerable<string> aliases)
		{
			Self.Remove.Aliases = aliases;
			return this;
		}

		/// <inheritdoc cref="AliasRemoveOperation.Aliases"/>
		public AliasRemoveDescriptor Aliases(params string[] aliases)
		{
			Self.Remove.Aliases = aliases;
			return this;
		}
	}
}
