using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;

namespace Nest
{
	internal static class TypeExtensions
	{
		private static readonly MethodInfo GetActivatorMethodInfo =
			typeof(TypeExtensions).GetMethod(nameof(GetActivator), BindingFlags.Static | BindingFlags.NonPublic);

		private static readonly ConcurrentDictionary<string, ObjectActivator<object>> CachedActivators =
			new ConcurrentDictionary<string, ObjectActivator<object>>();

		private static readonly ConcurrentDictionary<Type, Func<object>> CachedDefaultValues =
			new ConcurrentDictionary<Type, Func<object>>();

		private static readonly ConcurrentDictionary<string, Type> CachedGenericClosedTypes =
			new ConcurrentDictionary<string, Type>();

		private static readonly ConcurrentDictionary<Type, IList<CachedPropertyInfo>> CachedTypeProperties =
			new ConcurrentDictionary<Type, IList<CachedPropertyInfo>>();

		private static readonly ConcurrentDictionary<Type, IList<PropertyInfo>> CachedTypePropertyInfos =
			new ConcurrentDictionary<Type, IList<PropertyInfo>>();


		internal static object CreateGenericInstance(this Type t, Type closeOver, params object[] args) =>
			t.CreateGenericInstance(new[] { closeOver }, args);

		internal static object CreateGenericInstance(this Type t, Type[] closeOver, params object[] args)
		{
			var argKey = closeOver.Aggregate(new StringBuilder(), (sb, gt) => sb.Append("--" + gt.FullName), sb => sb.ToString());
			var key = t.FullName + argKey;
			if (!CachedGenericClosedTypes.TryGetValue(key, out var closedType))
			{
				closedType = t.MakeGenericType(closeOver);
				CachedGenericClosedTypes.TryAdd(key, closedType);
			}
			return closedType.CreateInstance(args);
		}

		internal static T CreateInstance<T>(this Type t, params object[] args) => (T)t.CreateInstance(args);

		internal static object CreateInstance(this Type t, params object[] args)
		{
			var argKey = args.Length;
			var key = argKey + "--" + t.FullName;
			if (CachedActivators.TryGetValue(key, out var activator))
				return activator(args);

			var generic = GetActivatorMethodInfo.MakeGenericMethod(t);
			var constructors = from c in t.GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
				let p = c.GetParameters()
				let k = string.Join(",", p.Select(a => a.ParameterType.Name))
				where p.Length == args.Length
				select c;

			var ctor = constructors.FirstOrDefault();
			if (ctor == null)
				throw new Exception($"Cannot create an instance of {t.FullName} because it has no constructor taking {args.Length} arguments");

			activator = (ObjectActivator<object>)generic.Invoke(null, new object[] { ctor });
			CachedActivators.TryAdd(key, activator);
			return activator(args);
		}

		internal static object DefaultValue(this Type type) =>
			type.IsValueType()
				? CachedDefaultValues.GetOrAdd(type, t =>
						Expression.Lambda<Func<object>>(
								Expression.Convert(Expression.Default(type), typeof(object))
							)
							.Compile()
					)
					.Invoke()
				: null;

		//do not remove this is referenced through GetActivatorMethod
		private static ObjectActivator<T> GetActivator<T>(ConstructorInfo ctor)
		{
			var type = ctor.DeclaringType;
			var paramsInfo = ctor.GetParameters();

			//create a single param of type object[]
			var param = Expression.Parameter(typeof(object[]), "args");

			var argsExp = new Expression[paramsInfo.Length];

			//pick each arg from the params array
			//and create a typed expression of them
			for (var i = 0; i < paramsInfo.Length; i++)
			{
				var index = Expression.Constant(i);
				var paramType = paramsInfo[i].ParameterType;
				var paramAccessorExp = Expression.ArrayIndex(param, index);
				var paramCastExp = Expression.Convert(paramAccessorExp, paramType);
				argsExp[i] = paramCastExp;
			}

			//make a NewExpression that calls the
			//ctor with the args we just created
			var newExp = Expression.New(ctor, argsExp);

			//create a lambda with the New
			//Expression as body and our param object[] as arg
			var lambda = Expression.Lambda(typeof(ObjectActivator<T>), newExp, param);

			//compile it
			var compiled = (ObjectActivator<T>)lambda.Compile();
			return compiled;
		}

		internal static IList<CachedPropertyInfo> GetCachedObjectProperties(this Type t, Func<string, string> inferrer, bool optInSerialization = true
		)
		{
			if (CachedTypeProperties.TryGetValue(t, out var propertyDictionary))
				return propertyDictionary;

			propertyDictionary = PropertiesOfAll(t, inferrer, optInSerialization);
			CachedTypeProperties.TryAdd(t, propertyDictionary);
			return propertyDictionary;
		}

		internal static IList<PropertyInfo> AllPropertiesCached(this Type t)
		{
			if (CachedTypePropertyInfos.TryGetValue(t, out var propertyInfos))
				return propertyInfos;

			propertyInfos = t.AllPropertiesNotCached().ToList();
			CachedTypePropertyInfos.TryAdd(t, propertyInfos);
			return propertyInfos;
		}

		private static IList<CachedPropertyInfo> CreateProperties(Type t, Func<string, string> inferrer, bool optInSerialization = true)
		{
			var properties = t.AllPropertiesNotCached()
				.Select(propertyInfo => new CachedPropertyInfo(propertyInfo, inferrer));

			return (optInSerialization
				? properties.Where(p => p.HasMemberAttribute)
				: properties).ToList();
		}

		public static IList<CachedPropertyInfo> PropertiesOfAllInterfaces(Type t, Func<string, string> inferrer, bool optInSerialization = true) =>
			(
				from i in t.GetInterfaces()
				select CreateProperties(i, inferrer, optInSerialization)
			)
			.SelectMany(interfaceProps => interfaceProps)
			.DistinctBy(p => p.PropertyName)
			.ToList();

		public static IList<CachedPropertyInfo> PropertiesOfInterface<TInterface>(Type t, Func<string, string> inferrer,
			bool optInSerialization = true
		)
			where TInterface : class => (
				from i in t.GetInterfaces().Where(i => typeof(TInterface).IsAssignableFrom(i))
				select CreateProperties(i, inferrer, optInSerialization)
			)
			.SelectMany(interfaceProps => interfaceProps)
			.DistinctBy(p => p.PropertyName)
			.ToList();

		public static IList<CachedPropertyInfo> PropertiesOfAll(Type t, Func<string, string> inferrer, bool optInSerialization = true) =>
			CreateProperties(t, inferrer, optInSerialization)
				.Concat(PropertiesOfAllInterfaces(t, inferrer, optInSerialization))
				.DistinctBy(p => p.PropertyName)
				.ToList();

		/// <summary>
		/// Returns inherited properties with reflectedType set to base type
		/// </summary>
		private static IEnumerable<PropertyInfo> AllPropertiesNotCached(this Type type)
		{
			var propertiesByName = new Dictionary<string, PropertyInfo>();
			do
			{
				foreach (var propertyInfo in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
				{
					if (propertiesByName.ContainsKey(propertyInfo.Name))
					{
						if (IsHidingMember(propertyInfo)) propertiesByName[propertyInfo.Name] = propertyInfo;
					}
					else
						propertiesByName.Add(propertyInfo.Name, propertyInfo);
				}
				type = type.GetTypeInfo()?.BaseType;
			} while (type?.GetTypeInfo()?.BaseType != null);
			return propertiesByName.Values;
		}

		/// <summary>
		/// Determines if a property is overriding an inherited property of its base class
		/// </summary>
		private static bool IsHidingMember(PropertyInfo propertyInfo)
		{
			var baseType = propertyInfo.DeclaringType?.GetTypeInfo()?.BaseType;
			var baseProperty = baseType?.GetProperty(propertyInfo.Name);
			if (baseProperty == null) return false;

			var derivedGetMethod = propertyInfo.GetGetMethod().GetBaseDefinition();
			return derivedGetMethod?.ReturnType != propertyInfo.PropertyType;
		}

		private delegate T ObjectActivator<out T>(params object[] args);
	}

	// TODO: Temporary shim for JsonProperty
	internal class CachedPropertyInfo
	{
		public CachedPropertyInfo(PropertyInfo propertyInfo, Func<string, string> inferrer)
		{
			PropertyInfo = propertyInfo;
			AttributeProvider = new PropertyAttributeProvider(propertyInfo);
			var dataMemberAttribute = propertyInfo.GetCustomAttribute<DataMemberAttribute>();
			var propertyNameAttribute = propertyInfo.GetCustomAttribute<PropertyNameAttribute>();
			HasMemberAttribute = dataMemberAttribute != null;

			if (propertyNameAttribute != null)
				PropertyName = propertyNameAttribute.Name;
			else if (dataMemberAttribute != null)
				PropertyName = dataMemberAttribute.Name;
			else if (inferrer != null)
				PropertyName = inferrer(propertyInfo.Name);
			else
				PropertyName = propertyInfo.Name.ToCamelCase();
		}

		public PropertyAttributeProvider AttributeProvider { get; }

		public bool HasMemberAttribute { get; }
		public PropertyInfo PropertyInfo { get; }

		public string PropertyName { get; }

		internal class PropertyAttributeProvider
		{
			private readonly PropertyInfo _propertyInfo;

			public PropertyAttributeProvider(PropertyInfo propertyInfo) => _propertyInfo = propertyInfo;

			public IList<Attribute> GetAttributes(bool inherit) =>
				Attribute.GetCustomAttributes(_propertyInfo, inherit);

			public IList<Attribute> GetAttributes(Type attributeType, bool inherit) =>
				Attribute.GetCustomAttributes(_propertyInfo, attributeType, inherit);
		}
	}
}
