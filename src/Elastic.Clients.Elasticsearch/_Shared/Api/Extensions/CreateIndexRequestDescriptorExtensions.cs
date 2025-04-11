// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;

namespace Elastic.Clients.Elasticsearch.IndexManagement;

public static class CreateIndexRequestDescriptorExtensions
{
	/// <summary>
	/// Add multiple aliases to the index at creation time.
	/// </summary>
	/// <param name="descriptor">A descriptor for an index request.</param>
	/// <param name="aliasName">The name of the alias.</param>
	/// <returns>The <see cref="CreateIndexRequestDescriptor"/> to allow fluent chaining of calls to configure the indexing request.</returns>
	[Obsolete("Use 'Aliases(string)' instead.")]
	public static CreateIndexRequestDescriptor WithAlias(this CreateIndexRequestDescriptor descriptor, string aliasName)
	{
#if NET8_0_OR_GREATER
		ArgumentException.ThrowIfNullOrEmpty(aliasName);
#else
		if (string.IsNullOrEmpty(aliasName))
			throw new ArgumentNullException(nameof(aliasName));
#endif

		descriptor.Aliases(aliasName);
		return descriptor;
	}

	/// <summary>
	/// Adds an alias to the index at creation time.
	/// </summary>
	/// <typeparam name="TDocument">The type representing documents stored in this index.</typeparam>
	/// <param name="descriptor">A fluent descriptor for an index request.</param>
	/// <param name="aliasName">The name of the alias.</param>
	/// <returns>The <see cref="CreateIndexRequestDescriptor{TDocument}"/> to allow fluent chaining of calls to configure the indexing request.</returns>
	[Obsolete("Use 'Aliases(string)' instead.")]
	public static CreateIndexRequestDescriptor<TDocument> WithAlias<TDocument>(this CreateIndexRequestDescriptor<TDocument> descriptor, string aliasName)
	{
#if NET8_0_OR_GREATER
		ArgumentException.ThrowIfNullOrEmpty(aliasName);
#else
		if (string.IsNullOrEmpty(aliasName))
			throw new ArgumentNullException(nameof(aliasName));
#endif

		descriptor.Aliases(aliasName);
		return descriptor;
	}
}
