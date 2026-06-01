// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace Elastic.Clients.Elasticsearch.Serialization;

/// <summary>
/// A per-<see cref="IElasticsearchClientSettings"/> registry that lets callers plug in their own CLR types for
/// plugin-defined discriminator values of non-exhaustive variant families.
/// </summary>
public sealed class VariantRegistry
{
	private sealed class RegisteredVariantWriter(string discriminator, Delegate writer)
	{
		public string Discriminator { get; } = discriminator;
		public Delegate Writer { get; } = writer;
	}

	// Keyed by (family, discriminator) / (family, CLR type). The "family" is the union interface type for tagged and
	// external variants, or the container type for container variants. Keying by an explicit family type keeps the
	// same discriminator string from clashing across different families (notably across containers).
	private readonly ConcurrentDictionary<(Type Family, string Discriminator), Delegate> _readers = new();
	private readonly ConcurrentDictionary<(Type Family, Type Clr), RegisteredVariantWriter> _writers = new();

	/// <summary>
	/// Registers a CLR type <typeparamref name="TImplementation"/> for a plugin-defined <paramref name="discriminator"/> value
	/// of the non-exhaustive variant family represented by the union interface <typeparamref name="TVariantFamily"/>.
	/// </summary>
	/// <typeparam name="TVariantFamily">The variant family's union interface (e.g. <c>IAnalyzer</c>).</typeparam>
	/// <typeparam name="TImplementation">The CLR type to (de-)serialize for the given discriminator.</typeparam>
	/// <param name="discriminator">The JSON discriminator value the type is registered for.</param>
	/// <returns>This registry instance, to allow chaining multiple registrations.</returns>
	public VariantRegistry Register<TVariantFamily, TImplementation>(string discriminator)
		where TImplementation : TVariantFamily
	{
		if (discriminator is null)
			throw new ArgumentNullException(nameof(discriminator));

		_readers[(typeof(TVariantFamily), discriminator)] =
			(VariantReader<TVariantFamily>)(static (ref Utf8JsonReader r, JsonSerializerOptions o) => r.ReadValue<TImplementation>(o));
		_writers[(typeof(TVariantFamily), typeof(TImplementation))] =
			new RegisteredVariantWriter(
				discriminator,
				(VariantWriter<TVariantFamily>)(static (Utf8JsonWriter w, TVariantFamily v, JsonSerializerOptions o) => w.WriteValue<TImplementation>(o, (TImplementation)v!)));

		return this;
	}

	/// <summary>
	/// Registers a CLR type <typeparamref name="TVariant"/> for a plugin-defined <paramref name="variantName"/> of the
	/// non-exhaustive container type <typeparamref name="TContainer"/>.
	/// </summary>
	/// <typeparam name="TContainer">The container type the variant belongs to.</typeparam>
	/// <typeparam name="TVariant">The CLR type to (de-)serialize for the given variant name.</typeparam>
	/// <param name="variantName">The JSON property name the variant is registered for.</param>
	/// <returns>This registry instance, to allow chaining multiple registrations.</returns>
	public VariantRegistry RegisterContainer<TContainer, TVariant>(string variantName)
	{
		if (variantName is null)
			throw new ArgumentNullException(nameof(variantName));

		_readers[(typeof(TContainer), variantName)] =
			(VariantReader<object>)(static (ref Utf8JsonReader r, JsonSerializerOptions o) => (object?)r.ReadValue<TVariant>(o));
		_writers[(typeof(TContainer), typeof(TVariant))] =
			new RegisteredVariantWriter(
				variantName,
				(VariantWriter<object>)(static (Utf8JsonWriter w, object v, JsonSerializerOptions o) => w.WriteValue<TVariant>(o, (TVariant)v!)));

		return this;
	}

	internal bool TryGetReader<TVariantFamily>(string discriminator, [NotNullWhen(true)] out VariantReader<TVariantFamily>? reader)
	{
		if (_readers.TryGetValue((typeof(TVariantFamily), discriminator), out var del))
		{
			reader = (VariantReader<TVariantFamily>)del;
			return true;
		}

		reader = null;
		return false;
	}

	internal bool TryGetWriter<TVariantFamily>(Type clrType, out VariantWriterRegistration<TVariantFamily> writer)
	{
		var family = typeof(TVariantFamily);
		if (TryGetWriter(family, clrType, out var registration))
		{
			writer = new VariantWriterRegistration<TVariantFamily>(registration.Discriminator, (VariantWriter<TVariantFamily>)registration.Writer);
			return true;
		}

		writer = default;
		return false;
	}

	internal bool TryGetContainerReader(Type container, string variantName, [NotNullWhen(true)] out VariantReader<object>? reader)
	{
		if (_readers.TryGetValue((container, variantName), out var del))
		{
			reader = (VariantReader<object>)del;
			return true;
		}

		reader = null;
		return false;
	}

	internal bool TryGetContainerWriter(Type container, Type clrType, out VariantWriterRegistration<object> writer)
	{
		if (TryGetWriter(container, clrType, out var registration))
		{
			writer = new VariantWriterRegistration<object>(registration.Discriminator, (VariantWriter<object>)registration.Writer);
			return true;
		}

		writer = default;
		return false;
	}

	private bool TryGetWriter(Type family, Type clrType, [NotNullWhen(true)] out RegisteredVariantWriter? writer)
	{
		if (_writers.TryGetValue((family, clrType), out writer))
		{
			return true;
		}

		foreach (var pair in _writers)
		{
			if (pair.Key.Family == family && pair.Key.Clr.IsAssignableFrom(clrType))
			{
				writer = pair.Value;
				return true;
			}
		}

		writer = null;
		return false;
	}
}
