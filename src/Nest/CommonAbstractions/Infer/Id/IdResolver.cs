// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Concurrent;
using System.Reflection;

namespace Nest
{
	public class IdResolver
	{
		private static readonly ConcurrentDictionary<Type, Func<object, string>> IdDelegates = new ConcurrentDictionary<Type, Func<object, string>>();

		private static readonly MethodInfo MakeDelegateMethodInfo =
			typeof(IdResolver).GetMethod(nameof(MakeDelegate), BindingFlags.Static | BindingFlags.NonPublic);

		private readonly IConnectionSettingsValues _connectionSettings;
		private readonly ConcurrentDictionary<Type, Func<object, string>> _localIdDelegates = new ConcurrentDictionary<Type, Func<object, string>>();

		public IdResolver(IConnectionSettingsValues connectionSettings) => _connectionSettings = connectionSettings;

		private PropertyInfo GetPropertyCaseInsensitive(Type type, string fieldName)
			=> type.GetProperty(fieldName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);

		internal Func<T, string> CreateIdSelector<T>() where T : class
		{
			Func<T, string> idSelector = Resolve;
			return idSelector;
		}

		internal static Func<object, object> MakeDelegate<T, TReturn>(MethodInfo @get)
		{
			var f = (Func<T, TReturn>)@get.CreateDelegate(typeof(Func<T, TReturn>));
			return t => f((T)t);
		}

		public string Resolve<T>(T @object) where T : class =>
			_connectionSettings.DefaultDisableIdInference || @object == null ? null : Resolve(@object.GetType(), @object);

		public string Resolve(Type type, object @object)
		{
			if (type == null || @object == null) return null;
			if (_connectionSettings.DefaultDisableIdInference || _connectionSettings.DisableIdInference.Contains(type))
				return null;

			var preferLocal = _connectionSettings.IdProperties.TryGetValue(type, out _);

			if (_localIdDelegates.TryGetValue(type, out var cachedLookup))
				return cachedLookup(@object);

			if (!preferLocal && IdDelegates.TryGetValue(type, out cachedLookup))
				return cachedLookup(@object);

			var idProperty = GetInferredId(type);
			if (idProperty == null) return null;

			var getMethod = idProperty.GetMethod;
			var generic = MakeDelegateMethodInfo.MakeGenericMethod(type, getMethod.ReturnType);
			var func = (Func<object, object>)generic.Invoke(null, new object[] { getMethod });
			cachedLookup = o =>
			{
				var v = func(o);
				return v?.ToString();
			};
			if (preferLocal)
				_localIdDelegates.TryAdd(type, cachedLookup);
			else
				IdDelegates.TryAdd(type, cachedLookup);
			return cachedLookup(@object);
		}


		private PropertyInfo GetInferredId(Type type)
		{
			// if the type specifies through ElasticAttribute what the id prop is
			// use that no matter what

			_connectionSettings.IdProperties.TryGetValue(type, out var propertyName);
			if (!propertyName.IsNullOrEmpty())
				return GetPropertyCaseInsensitive(type, propertyName);

			var esTypeAtt = ElasticsearchTypeAttribute.From(type);
			propertyName = esTypeAtt?.IdProperty.IsNullOrEmpty() ?? true ? "Id" : esTypeAtt.IdProperty;

			return GetPropertyCaseInsensitive(type, propertyName);
		}
	}
}
