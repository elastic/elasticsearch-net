// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Serialization;

/// <summary>
/// Provides functionality to link a <typeparamref name="TContext"/> to <see cref="JsonSerializerOptions"/> by
/// registering this context provider to the global <see cref="JsonSerializerOptions.Converters"/> list.
/// </summary>
/// <typeparam name="TContext">The type of the context data.</typeparam>
internal sealed class ContextProvider<TContext> :
	JsonConverterFactory
{
	private readonly Converter _converter;

	public ContextProvider(TContext context)
	{
		_converter = new Converter(context);
	}

	/// <summary>
	/// Retrieves the <typeparamref name="TContext"/> instance from the given <see cref="JsonSerializerOptions"/>.
	/// </summary>
	/// <param name="options">The <see cref="JsonSerializerOptions"/> to retrieve the context from.</param>
	/// <returns>The context linked to the given <see cref="JsonSerializerOptions"/>.</returns>
	/// <exception cref="InvalidOperationException">
	/// If no <see cref="ContextProvider{TContext}"/> for <typeparamref name="TContext"/> is registered to the given
	/// <see cref="JsonSerializerOptions"/> instance.
	/// </exception>
	public static TContext GetContext(JsonSerializerOptions options)
	{
#pragma warning disable IL2026, IL3050
		if (options.GetConverter(typeof(Marker)) is not Converter provider)
#pragma warning restore IL2026, IL3050
		{
			throw new InvalidOperationException($"No context provider for type '{typeof(TContext).Name}' is " +
												$"registered for the given 'JsonSerializerOptions' instance.");
		}

		return provider.Context;
	}

	/// <summary>
	/// Tries to retrieve the <typeparamref name="TContext"/> instance from the given <see cref="JsonSerializerOptions"/>.
	/// </summary>
	/// <param name="options">The <see cref="JsonSerializerOptions"/> to retrieve the context from.</param>
	/// <param name="context">The context linked to the given <see cref="JsonSerializerOptions"/>.</param>
	/// <returns>
	/// <see langword="true"/> if the context was successfully retrieved from the given <see cref="JsonSerializerOptions"/>
	/// or <see langword="false"/>, if not.
	/// </returns>
	public static bool TryGetContext(JsonSerializerOptions options, [MaybeNullWhen(false)] out TContext context)
	{
#pragma warning disable IL2026, IL3050
		if (options.GetConverter(typeof(Marker)) is not Converter provider)
#pragma warning restore IL2026, IL3050
		{
			context = default;
			return false;
		}

		context = provider.Context;
		return true;
	}

	public override bool CanConvert(Type typeToConvert)
	{
		return typeToConvert == typeof(Marker);
	}

	public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
	{
		return _converter;
	}

	private sealed class Marker;

	private sealed class Converter(TContext context) :
		JsonConverter<Marker>
	{
		public TContext Context { get; } = context;

		public override Marker Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			throw new InvalidOperationException();
		}

		public override void Write(Utf8JsonWriter writer, Marker value, JsonSerializerOptions options)
		{
			throw new InvalidOperationException();
		}
	}
}
