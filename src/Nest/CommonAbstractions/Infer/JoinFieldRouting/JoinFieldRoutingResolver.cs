using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;

namespace Nest
{
	public class JoinFieldRoutingResolver
	{
		private static readonly ConcurrentDictionary<Type, Func<object, JoinField>> PropertyGetDelegates =
			new ConcurrentDictionary<Type, Func<object, JoinField>>();
		private static readonly MethodInfo MakeDelegateMethodInfo =
			typeof(JoinFieldRoutingResolver).GetMethod(nameof(MakeDelegate), BindingFlags.Static | BindingFlags.NonPublic);

		private readonly IConnectionSettingsValues _connectionSettings;
		private readonly IdResolver _idResolver;

		public JoinFieldRoutingResolver(IConnectionSettingsValues connectionSettings, IdResolver idResolver)
		{
			_connectionSettings = connectionSettings;
			_idResolver = idResolver;
		}

		internal static Func<object, object> MakeDelegate<T, TReturn>(MethodInfo @get)
		{
			var f = (Func<T, TReturn>)@get.CreateDelegate(typeof(Func<T, TReturn>));
			return t => f((T)t);
		}

		public string Resolve<T>(T @object) => @object == null ? null : Resolve(@object.GetType(), @object);

		public string Resolve(Type type, object @object)
		{
			var joinField = GetJoinFieldFromObject(type, @object);
			return joinField?.Match(p => _idResolver.Resolve(@object), c => ResolveId(c.Parent, _connectionSettings));
		}

		private string ResolveId(Id id, IConnectionSettingsValues nestSettings)
		{
			if (id.Document != null) return nestSettings.Inferrer.Id(id.Document);

			var s = id.Value as string;
			return s ?? id.Value?.ToString();
		}

		private static JoinField GetJoinFieldFromObject(Type type, object @object)
		{
			if (type == null || @object == null) return null;

			if (PropertyGetDelegates.TryGetValue(type, out var cachedLookup)) return cachedLookup(@object);

			var joinProperty = GetJoinFieldProperty(type);
			if (joinProperty == null)
			{
				PropertyGetDelegates.TryAdd(type, o => null);
				return null;
			}

			var getMethod = joinProperty.GetGetMethod();
			var generic = MakeDelegateMethodInfo.MakeGenericMethod(type, getMethod.ReturnType);
			var func = (Func<object, object>) generic.Invoke(null, new object[] {getMethod});
			cachedLookup = o =>
			{
				var v = func(o);
				return v as JoinField;
			};
			PropertyGetDelegates.TryAdd(type, cachedLookup);
			return cachedLookup(@object);
		}

		private static PropertyInfo GetJoinFieldProperty(Type type)
		{
			var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
			var joinField = properties.FirstOrDefault(p => p.PropertyType == typeof(JoinField));
			return joinField;
		}
	}
}
