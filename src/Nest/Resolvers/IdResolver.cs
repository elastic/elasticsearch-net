using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Concurrent;
using System.Reflection;

namespace Nest.Resolvers
{
	public class IdResolver
	{
		private readonly IConnectionSettingsValues _connectionSettings;
		private static ConcurrentDictionary<Type, Func<object, string>> IdDelegates = new ConcurrentDictionary<Type, Func<object, string>>();
		private static MethodInfo MakeDelegateMethodInfo = typeof(IdResolver).GetMethod("MakeDelegate", BindingFlags.Static | BindingFlags.NonPublic);

		public IdResolver(IConnectionSettingsValues connectionSettings)
		{
			_connectionSettings = connectionSettings;
		}


		internal Func<T, string> CreateIdSelector<T>() where T : class
		{
			Func<T, string> idSelector = (@object) => this.GetIdFor(@object);
			return idSelector;
		}

		internal static Func<T, object> MakeDelegate<T, U>(MethodInfo @get)
		{
			var f = (Func<T, U>)Delegate.CreateDelegate(typeof(Func<T, U>), @get);

			return t => f(t);
		}

		[Obsolete("Scheduled to be removed in 2.0, is not cached and not used internally")]
		public string GetIdFor<T>(T @object, string idProperty)
		{
			try
			{
				var value = @object
					.GetType()
					.GetProperty(idProperty)
					.GetValue(@object, null);

				return value.ToString();
			}
			catch
			{
				return null;
			}
		}

		public string GetIdFor<T>(T @object)
		{
			if (@object == null)
				return null;

			var type = typeof(T);
			Func<object, string> cachedLookup;
			if (IdDelegates.TryGetValue(type, out cachedLookup))
				return cachedLookup(@object);

			var idProperty = GetInferredId(type);
			if (idProperty == null)
			{
				return null;
			}
			try
			{
				var getMethod = idProperty.GetGetMethod();
				var generic = MakeDelegateMethodInfo.MakeGenericMethod(type, getMethod.ReturnType);
				var func = (Func<T, object>)generic.Invoke(null, new[] { getMethod });
				cachedLookup = o =>
				{
					T obj = (T)o;
					var v = func(obj);
					return v != null ? v.ToString() : null;
				};
				IdDelegates.TryAdd(type, cachedLookup);
				return cachedLookup(@object);
			}
			catch 
			{
				var value = idProperty.GetValue(@object, null);
				return value.ToString();
			}
		}

		private PropertyInfo GetInferredId(Type type)
		{
			// if the type specifies through ElasticAttribute what the id prop is 
			// use that no matter what

			string propertyName;
			this._connectionSettings.IdProperties.TryGetValue(type, out propertyName);
			if (!propertyName.IsNullOrEmpty())
				return GetPropertyCaseInsensitive(type, propertyName);

			var esTypeAtt = ElasticAttributes.Type(type);
			if (esTypeAtt != null && !string.IsNullOrWhiteSpace(esTypeAtt.IdProperty))
				return GetPropertyCaseInsensitive(type, esTypeAtt.IdProperty);

			propertyName = "Id";
			//Try Id on its own case insensitive
			var idProperty = GetPropertyCaseInsensitive(type, propertyName);
			if (idProperty != null)
				return idProperty;

			//TODO remove in 2.0 ?
			//Try TypeNameId case insensitive
			idProperty = GetPropertyCaseInsensitive(type, type.Name + propertyName);
			if (idProperty != null)
				return idProperty;

			//TODO remove in 2.0 ?
			//Try TypeName_Id case insensitive
			idProperty = GetPropertyCaseInsensitive(type, type.Name + "_" + propertyName);

			return idProperty;
		}

		PropertyInfo GetPropertyCaseInsensitive(Type type, string propertyName)
		{
			return type.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
		}
	}
}
