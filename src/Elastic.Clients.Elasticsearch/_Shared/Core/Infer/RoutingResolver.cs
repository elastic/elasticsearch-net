// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Linq;
using System.Globalization;
using static System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes;

namespace Elastic.Clients.Elasticsearch;

public class RoutingResolver
{
	private static readonly ConcurrentDictionary<Type, Func<object, JoinField>?> JoinFieldDelegateCache = new();

	private readonly IElasticsearchClientSettings _settings;
	private readonly ConcurrentDictionary<Type, Func<object, string?>?> _routeDelegateCache = new();

	public RoutingResolver(IElasticsearchClientSettings settings, IdResolver idResolver)
	{
		_settings = settings;
	}

	[UnconditionalSuppressMessage("AOT", "IL3050:Calling members annotated with 'RequiresDynamicCodeAttribute'", Justification = "Call to object.GetType()")]
	[UnconditionalSuppressMessage("AOT", "IL2072:Calling members annotated with 'RequiresDynamicCodeAttribute'", Justification = "Call to object.GetType()")]
	public string? Resolve<[DynamicallyAccessedMembers(PublicProperties | None | NonPublicProperties)] T>(T instance)
	{
		if (instance is null)
		{
			return null;
		}

		return Resolve(instance.GetType(), instance);
	}

	[UnconditionalSuppressMessage("AOT", "IL3050:Calling members annotated with 'RequiresDynamicCodeAttribute'", Justification = "Call to object.GetType()")]
	public string? Resolve([DynamicallyAccessedMembers(PublicProperties | None | NonPublicProperties)] Type type, object instance)
	{
		if (type is null)
		{
			throw new ArgumentNullException(nameof(type));
		}

		if (instance is null)
		{
			throw new ArgumentNullException(nameof(instance));
		}

		if (TryGetConnectionSettingsRoute(type, instance, out var route))
		{
			return route;
		}

		return GetJoinFieldFromObject(type, instance)?.Match(_ => _settings.Inferrer.Id(instance), child => ResolveId(_settings, child.ParentId));

		static string? ResolveId(IElasticsearchClientSettings settings, Id id)
		{
			return (id.Document is not null) ? settings.Inferrer.Id(id.Document) : id.StringOrLongValue;
		}
	}

	private bool TryGetConnectionSettingsRoute(
		[DynamicallyAccessedMembers(PublicProperties | NonPublicProperties)] Type type,
		object instance,
		out string? route)
	{
		if (!_settings.RouteProperties.TryGetValue(type, out var propertyName))
		{
			route = null;
			return false;
		}

		if (_routeDelegateCache.TryGetValue(type, out var getterDelegate))
		{
			route = getterDelegate?.Invoke(instance);
			return true;
		}

		getterDelegate = DynamicPropertyAccessor.CreateGetterDelegate(type, propertyName, GetAndFormatAsString);
		_routeDelegateCache.TryAdd(type, getterDelegate);

		route = getterDelegate(instance);
		return true;
	}

	private static JoinField? GetJoinFieldFromObject(
		[DynamicallyAccessedMembers(PublicProperties | NonPublicProperties)] Type type,
		object instance)
	{
		if (JoinFieldDelegateCache.TryGetValue(type, out var getterDelegate))
		{
			return getterDelegate?.Invoke(instance);
		}

		var joinProperty = GetJoinFieldProperty(type);
		if (joinProperty is null)
		{
			// Avoid reflection calls for subsequent invocations.
			JoinFieldDelegateCache.TryAdd(type, null);

			return null;
		}

		getterDelegate = DynamicPropertyAccessor.CreateGetterDelegate<JoinField?>(type, joinProperty);
		JoinFieldDelegateCache.TryAdd(type, getterDelegate);

		return getterDelegate(instance);

		static PropertyInfo? GetJoinFieldProperty([DynamicallyAccessedMembers(PublicProperties)] Type type)
		{
			var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
			try
			{
				return properties.SingleOrDefault(p => p.PropertyType == typeof(JoinField));
			}
			catch (InvalidOperationException e)
			{
				throw new ArgumentException($"Type '{type.Name}' has more than one '{nameof(JoinField)}' property.", e);
			}
		}
	}

	private static string? GetAndFormatAsString(Func<object, object?> genericGetter, object instance)
	{
		var value = genericGetter(instance);

		return (value is IFormattable formattable)
			? formattable.ToString(null, CultureInfo.InvariantCulture)
			: value?.ToString();
	}
}
