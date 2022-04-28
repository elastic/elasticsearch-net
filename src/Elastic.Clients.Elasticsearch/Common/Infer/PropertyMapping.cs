// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

namespace Elastic.Clients.Elasticsearch
{
	/// <summary>
	/// Represents meta data about a property which may be used by inferrence and during serialization.
	/// </summary>
	public readonly struct PropertyMapping
	{
		public static PropertyMapping Ignored = new() { Ignore = true };

		/// <summary>
		/// The property should be ignored during serialization.
		/// <para>
		/// NOTE: This only applies if a custom source serializer is registered which supports
		/// modifying the JSON contract. The <see cref="DefaultSourceSerializer"/> does not support
		/// ignoring properties via a <see cref="PropertyMapping"/>.
		/// </para>
		/// </summary>
		public bool Ignore { get; init; }

		/// <summary>
		/// The JSON name for the property.
		/// </summary>
		public string? Name { get; init; }

		public override bool Equals(object? obj) => obj is PropertyMapping mapping && Ignore == mapping.Ignore && Name == mapping.Name;

		public override int GetHashCode() => (Ignore, Name).GetHashCode();
	}
}
