using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using static System.Reflection.BindingFlags;

namespace Nest
{
	internal static class SerializerTypeExtensions
	{
		public struct Jsonable
		{
			public MemberInfo MemberInfo { get; }
			public string JsonProperty { get; }

			public Jsonable(MemberInfo info, string prop)
			{
				this.JsonProperty = prop;
				this.MemberInfo = info;
			}
		}

		public static bool IsNestType(this Type t) =>
			t.FullName != null && t.FullName.StartsWith("Nest.", StringComparison.OrdinalIgnoreCase);

		private static ConcurrentDictionary<Type, List<Jsonable>> FullSerializabeMembersCache { get; }
			= new ConcurrentDictionary<Type, List<Jsonable>>();

		public static IList<Jsonable> GetSerializeMembers(this Type t)
		{
			if (FullSerializabeMembersCache.TryGetValue(t, out var list)) return list;
			var jsonables =
				(from i in t.AllInterfacesAndSelf() select GetPropertiesWithJsonProperty(i))
				.SelectMany(j => j)
				.DistinctBy(j => j.JsonProperty)
				.ToList();

			FullSerializabeMembersCache.TryAdd(t, jsonables);
			return jsonables;
		}

		public static IEnumerable<Type> AllInterfacesAndSelf(this Type t)
		{
			yield return t;
			foreach (var i in t.GetInterfaces()) yield return i;
		}

		private static ConcurrentDictionary<Type, List<Jsonable>> PerTypeSerializabeMembersCache { get; }
			= new ConcurrentDictionary<Type, List<Jsonable>>();

		private static IList<Jsonable> GetPropertiesWithJsonProperty(this Type t)
		{
			if (PerTypeSerializabeMembersCache.TryGetValue(t, out var list)) return list;
			var props = t.GetProperties(Public | DeclaredOnly | Instance);
			var properties = (
				from p in props
				let jsonAtt = p.GetCustomAttribute<JsonPropertyAttribute>()
				let variableFieldAtt = p.GetCustomAttribute<VariableFieldAttribute>()
				where !(string.IsNullOrWhiteSpace(jsonAtt?.PropertyName) && variableFieldAtt == null)
				select new Jsonable(p, variableFieldAtt?.FieldName ?? jsonAtt?.PropertyName)
				).ToList();

			PerTypeSerializabeMembersCache.TryAdd(t, properties);
			return properties;
		}

		private static ConcurrentDictionary<Type, Jsonable> VariableFieldCache { get; }
			= new ConcurrentDictionary<Type, Jsonable>();

		private static Jsonable GetVariableFieldProperty(this Type t)
		{
			if (VariableFieldCache.TryGetValue(t, out var prop)) return prop;
			var props = t.GetProperties(Public | DeclaredOnly | Instance);
			prop = (
				from p in props
				let jsonAtt = p.GetCustomAttribute<JsonPropertyAttribute>()
				where jsonAtt != null && !string.IsNullOrWhiteSpace(jsonAtt.PropertyName)
				select new Jsonable(p, jsonAtt.PropertyName)
				).FirstOrDefault();

			VariableFieldCache.TryAdd(t, prop);
			return prop;
		}

	}
}
