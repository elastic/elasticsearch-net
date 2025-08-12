// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

#if NETCOREAPP3_0_OR_GREATER
using System.Runtime.CompilerServices;
#endif

namespace Elastic.Clients.Elasticsearch;

internal static class DynamicPropertyAccessor
{
	public static Func<object, TResult?> CreateGetterDelegate<TResult>(
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)] Type type,
		PropertyInfo property,
		Func<Func<object, object?>, object, TResult?>? retrieverFunc = null)
	{
		if (type is null)
		{
			throw new ArgumentNullException(nameof(type));
		}

		if (property is null)
		{
			throw new ArgumentNullException(nameof(property));
		}

		var getterMethod = property.GetMethod;
		if (getterMethod is null)
		{
			throw new ArgumentException($"Property '{property.Name}' of '{type.Name}' does declare a getter method.");
		}

		retrieverFunc ??= static (genericGetter, instance) => (TResult?)genericGetter(instance);

#if NETCOREAPP3_0_OR_GREATER

		if (!RuntimeFeature.IsDynamicCodeSupported || !RuntimeFeature.IsDynamicCodeCompiled)
		{
			// Fall back to reflection based getter access.

			return instance => retrieverFunc(instance => getterMethod.Invoke(instance, []), instance);
		}

#endif

		// Build compiled getter delegate.

#pragma warning disable IL3050
#pragma warning disable IL2060
		var getterDelegateFactory = MakeDelegateMethodInfo.MakeGenericMethod(type, getterMethod.ReturnType);
#pragma warning restore IL3050
#pragma warning restore IL2060
		var genericGetterDelegate = (Func<object, object>)getterDelegateFactory.Invoke(null, [getterMethod])!;

		return instance => retrieverFunc(genericGetterDelegate, instance);
	}

	public static Func<object, TResult?> CreateGetterDelegate<TResult>(
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)] Type type,
		string propertyName,
		Func<Func<object, object?>, object, TResult?>? retrieverFunc = null)
	{
		return TryCreateGetterDelegate(type, propertyName, out var result, retrieverFunc)
			? result
			: throw new ArgumentException($"Type '{type.Name}' does not have a public property with name '{propertyName}'.");
	}

	public static bool TryCreateGetterDelegate<TResult>(
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)] Type type,
		string propertyName,
		[NotNullWhen(true)] out Func<object, TResult?>? result,
		Func<Func<object, object?>, object, TResult?>? retrieverFunc = null)
	{
		if (type is null)
		{
			throw new ArgumentNullException(nameof(type));
		}

		if (propertyName is null)
		{
			throw new ArgumentNullException(nameof(propertyName));
		}

		var property = type.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);

		if (property is null)
		{
			result = null;
			return false;
		}

		result = CreateGetterDelegate(type, property, retrieverFunc);
		return true;
	}

	private static readonly MethodInfo MakeDelegateMethodInfo = typeof(DynamicPropertyAccessor).GetMethod(nameof(MakeDelegate), BindingFlags.Static | BindingFlags.NonPublic)!;

	private static Func<object, object?> MakeDelegate<T, TReturn>(MethodInfo getterMethod)
	{
#if NET5_0_OR_GREATER
		var func = getterMethod.CreateDelegate<Func<T, TReturn>>(); //(Func<T, TReturn>)getterMethod.CreateDelegate(typeof(Func<T, TReturn>));
#else
		var func = (Func<T, TReturn>)getterMethod.CreateDelegate(typeof(Func<T, TReturn>));
#endif

		return type => func((T)type);
	}
}
