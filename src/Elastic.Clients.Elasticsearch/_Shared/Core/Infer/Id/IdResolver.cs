// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using static System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes;

namespace Elastic.Clients.Elasticsearch;

public class IdResolver
{
	private static readonly ConcurrentDictionary<Type, Func<object, string?>?> GlobalDelegateCache = new();

	private readonly IElasticsearchClientSettings _settings;
	private readonly ConcurrentDictionary<Type, Func<object, string?>?> _localDelegateCache = new();

	public IdResolver(IElasticsearchClientSettings settings) => _settings = settings;

	[UnconditionalSuppressMessage("AOT", "IL3050:Calling members annotated with 'RequiresDynamicCodeAttribute'", Justification = "Call to object.GetType()")]
	[UnconditionalSuppressMessage("AOT", "IL2072:Calling members annotated with 'RequiresDynamicCodeAttribute'", Justification = "Call to object.GetType()")]
	public string? Resolve<[DynamicallyAccessedMembers(PublicProperties | NonPublicProperties)] T>(T instance)
	{
		if (_settings.DefaultDisableIdInference || (instance is null))
		{
			return null;
		}

		return Resolve(instance.GetType(), instance);
	}

	public string? Resolve([DynamicallyAccessedMembers(PublicProperties | NonPublicProperties)] Type type, object instance)
	{
		if (type is null)
		{
			throw new ArgumentNullException(nameof(type));
		}

		if (instance is null)
		{
			throw new ArgumentNullException(nameof(instance));
		}

		if (_settings.DefaultDisableIdInference || _settings.DisableIdInference.Contains(type))
		{
			return null;
		}

		var localIdPropertyName =
			_settings.IdProperties.TryGetValue(type, out var propertyName) && !string.IsNullOrEmpty(propertyName)
				? propertyName
				: null;
		var idPropertyName = localIdPropertyName ?? "Id";

		var delegateCache = string.IsNullOrEmpty(localIdPropertyName) ? GlobalDelegateCache : _localDelegateCache;
		if (delegateCache.TryGetValue(type, out var getterDelegate))
		{
			return getterDelegate?.Invoke(instance);
		}

		if (!DynamicPropertyAccessor.TryCreateGetterDelegate(type, idPropertyName, out getterDelegate, GetAndFormatAsString))
		{
			if (!string.IsNullOrEmpty(localIdPropertyName))
			{
				throw new ArgumentException($"Type '{type.Name}' does not have a public property with name '{localIdPropertyName}'.");
			}

			// Avoid reflection calls for subsequent invocations.
			delegateCache.TryAdd(type, null);

			return null;
		}

		return getterDelegate(instance);
	}

	private static string? GetAndFormatAsString(Func<object, object?> genericGetter, object instance)
	{
		var value = genericGetter(instance);

		return (value is IFormattable formattable)
			? formattable.ToString(null, CultureInfo.InvariantCulture)
			: value?.ToString();
	}
}
