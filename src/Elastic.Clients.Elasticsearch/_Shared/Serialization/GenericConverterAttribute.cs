// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Reflection;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Serialization;

/// <summary>
/// A custom <see cref="JsonConverterAttribute"/> used to dynamically create <see cref="JsonConverter"/>
/// instances for generic classes and properties whose type arguments are unknown at compile time.
/// </summary>
internal class GenericConverterAttribute :
	JsonConverterAttribute
{
	private readonly int _parameterCount;

	/// <summary>
	/// The constructor.
	/// </summary>
	/// <param name="genericConverterType">The open generic type of the JSON converter class.</param>
	/// <param name="unwrap">
	///		Set <c>true</c> to unwrap the generic type arguments of the source/target type before using them to create
	///		the converter instance.
	///		<para>
	///			This is especially useful, if the base converter is e.g. defined as <c>MyBaseConverter{SomeType{T}}</c>
	///			but the annotated property already has the concrete type <c>SomeType{T}</c>. Unwrapping the generic
	///			arguments will make sure to not incorrectly instantiate a converter class of type
	///			<c>MyBaseConverter{SomeType{SomeType{T}}}</c>.
	///		</para>
	/// </param>
	/// <exception cref="ArgumentException">If <paramref name="genericConverterType"/> is not a compatible generic type definition.</exception>
	public GenericConverterAttribute(Type genericConverterType, bool unwrap = false)
	{
		if (!genericConverterType.IsGenericTypeDefinition)
		{
			throw new ArgumentException(
				$"The generic JSON converter type '{genericConverterType.Name}' is not a generic type definition.",
				nameof(genericConverterType));
		}

		GenericConverterType = genericConverterType;
		Unwrap = unwrap;

		_parameterCount = GenericConverterType.GetTypeInfo().GenericTypeParameters.Length;

		if (!unwrap && (_parameterCount != 1))
		{
			throw new ArgumentException(
				$"The generic JSON converter type '{genericConverterType.Name}' must accept exactly 1 generic type " +
				$"argument",
				nameof(genericConverterType));
		}
	}

	public Type GenericConverterType { get; }

	public bool Unwrap { get; }

	/// <inheritdoc cref="JsonConverterAttribute.CreateConverter"/>
	public override JsonConverter? CreateConverter(Type typeToConvert)
	{
		if (!Unwrap)
			return (JsonConverter)Activator.CreateInstance(GenericConverterType.MakeGenericType(typeToConvert));

		var arguments = typeToConvert.GetGenericArguments();
		if (arguments.Length != _parameterCount)
		{
			throw new ArgumentException(
				$"The generic JSON converter type '{GenericConverterType.Name}' is not compatible with the target " +
				$"type '{typeToConvert.Name}'.",
				nameof(typeToConvert));
		}

		return (JsonConverter)Activator.CreateInstance(GenericConverterType.MakeGenericType(arguments));
	}
}
