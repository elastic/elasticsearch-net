// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using Elastic.Transport;
using System.Collections.Concurrent;
using System.Reflection;
using System.Globalization;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Runtime.Serialization;
using System.Collections;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace Elastic.Clients.Elasticsearch
{
	public sealed class Inferrer
	{
		private readonly IElasticsearchClientSettings _elasticsearchClientSettings;

		public Inferrer(IElasticsearchClientSettings elasticsearchClientSettings)
		{
			elasticsearchClientSettings.ThrowIfNull(nameof(elasticsearchClientSettings));
			_elasticsearchClientSettings = elasticsearchClientSettings;
			IdResolver = new IdResolver(elasticsearchClientSettings);
			IndexNameResolver = new IndexNameResolver(elasticsearchClientSettings);
			RelationNameResolver = new RelationNameResolver(elasticsearchClientSettings);
			FieldResolver = new FieldResolver(elasticsearchClientSettings);
			RoutingResolver = new RoutingResolver(elasticsearchClientSettings, IdResolver);

			//CreateMultiHitDelegates =
			//	new ConcurrentDictionary<Type,
			//		Action<MultiGetResponseFormatter.MultiHitTuple, IJsonFormatterResolver, ICollection<IMultiGetHit<object>>>>();
			//CreateSearchResponseDelegates =
			//	new ConcurrentDictionary<Type,
			//		Action<MultiSearchResponseFormatter.SearchHitTuple, IJsonFormatterResolver, IDictionary<string, IElasticsearchResponse>>>();
		}

		//internal ConcurrentDictionary<Type, Action<MultiGetResponseFormatter.MultiHitTuple, IJsonFormatterResolver, ICollection<IMultiGetHit<object>>>
		//	>
		//	CreateMultiHitDelegates { get; }

		//internal ConcurrentDictionary<Type,
		//		Action<MultiSearchResponseFormatter.SearchHitTuple, IJsonFormatterResolver, IDictionary<string, IElasticsearchResponse>>>
		//	CreateSearchResponseDelegates { get; }

		private FieldResolver FieldResolver { get; }
		private IdResolver IdResolver { get; }
		private IndexNameResolver IndexNameResolver { get; }
		private RelationNameResolver RelationNameResolver { get; }
		private RoutingResolver RoutingResolver { get; }

		public string Resolve(IUrlParameter urlParameter) => urlParameter.GetString(_elasticsearchClientSettings);

		public string Field(Field field) => FieldResolver.Resolve(field);

		public string PropertyName(PropertyName property) => FieldResolver.Resolve(property);

		public string IndexName<T>() => IndexNameResolver.Resolve<T>();

		public string IndexName(IndexName index) => IndexNameResolver.Resolve(index);

		public string Id<T>(T instance) => IdResolver.Resolve(instance);

		public string Id(Type type, object instance) => IdResolver.Resolve(type, instance);

		public string RelationName<T>() => RelationNameResolver.Resolve<T>();

		public string RelationName(RelationName type) => RelationNameResolver.Resolve(type);

		public string Routing<T>(T document) => RoutingResolver.Resolve(document);

		public string Routing(Type type, object instance) => RoutingResolver.Resolve(type, instance);
	}

	

	public class RoutingResolver
	{
		private static readonly ConcurrentDictionary<Type, Func<object, JoinField>> PropertyGetDelegates =
			new();

		private static readonly MethodInfo MakeDelegateMethodInfo =
			typeof(RoutingResolver).GetMethod(nameof(MakeDelegate), BindingFlags.Static | BindingFlags.NonPublic);


		private readonly IElasticsearchClientSettings _transportClientSettings;
		private readonly IdResolver _idResolver;

		private readonly ConcurrentDictionary<Type, Func<object, string>>
			_localRouteDelegates = new();

		public RoutingResolver(IElasticsearchClientSettings connectionSettings, IdResolver idResolver)
		{
			_transportClientSettings = connectionSettings;
			_idResolver = idResolver;
		}

		private PropertyInfo GetPropertyCaseInsensitive(Type type, string fieldName) =>
			type.GetProperty(fieldName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);

		internal static Func<object, object> MakeDelegate<T, TReturn>(MethodInfo @get)
		{
			var f = (Func<T, TReturn>)@get.CreateDelegate(typeof(Func<T, TReturn>));
			return t => f((T)t);
		}

		public string Resolve<T>(T @object) => @object == null ? null : Resolve(@object.GetType(), @object);

		public string Resolve(Type type, object @object)
		{
			if (TryConnectionSettingsRoute(type, @object, out var route))
				return route;

			var joinField = GetJoinFieldFromObject(type, @object);
			return joinField?.Match(p => _idResolver.Resolve(@object), c => ResolveId(c.ParentId, _transportClientSettings));
		}

		private bool TryConnectionSettingsRoute(Type type, object @object, out string route)
		{
			route = null;
			if (!_transportClientSettings.RouteProperties.TryGetValue(type, out var propertyName))
				return false;

			if (_localRouteDelegates.TryGetValue(type, out var cachedLookup))
			{
				route = cachedLookup(@object);
				return true;
			}
			var property = GetPropertyCaseInsensitive(type, propertyName);
			var func = CreateGetterFunc(type, property);
			cachedLookup = o =>
			{
				var v = func(o);
				return v?.ToString();
			};
			_localRouteDelegates.TryAdd(type, cachedLookup);
			route = cachedLookup(@object);
			return true;
		}

		private string ResolveId(Id id, IElasticsearchClientSettings settings) =>
			id.Document != null ? settings.Inferrer.Id(id.Document) : id.StringOrLongValue;

		private static JoinField GetJoinFieldFromObject(Type type, object @object)
		{
			if (type == null || @object == null)
				return null;

			if (PropertyGetDelegates.TryGetValue(type, out var cachedLookup))
				return cachedLookup(@object);

			var joinProperty = GetJoinFieldProperty(type);
			if (joinProperty == null)
			{
				PropertyGetDelegates.TryAdd(type, o => null);
				return null;
			}

			var func = CreateGetterFunc(type, joinProperty);
			cachedLookup = o =>
			{
				var v = func(o);
				return v as JoinField;
			};
			PropertyGetDelegates.TryAdd(type, cachedLookup);
			return cachedLookup(@object);
		}

		private static Func<object, object> CreateGetterFunc(Type type, PropertyInfo joinProperty)
		{
			var getMethod = joinProperty.GetMethod;
			var generic = MakeDelegateMethodInfo.MakeGenericMethod(type, getMethod.ReturnType);
			var func = (Func<object, object>)generic.Invoke(null, new object[] { getMethod });
			return func;
		}

		private static PropertyInfo GetJoinFieldProperty(Type type)
		{
			var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
			try
			{
				var joinField = properties.SingleOrDefault(p => p.PropertyType == typeof(JoinField));
				return joinField;
			}
			catch (InvalidOperationException e)
			{
				throw new ArgumentException($"{type.Name} has more than one JoinField property", e);
			}
		}
	}

	[JsonConverter(typeof(JoinFieldConverter))]
	public class JoinField
	{
		internal Child ChildOption { get; }
		internal Parent ParentOption { get; }
		internal int Tag { get; }

		public JoinField(Parent parentName)
		{
			ParentOption = parentName;
			Tag = 0;
		}

		public JoinField(Child child)
		{
			ChildOption = child;
			Tag = 1;
		}

		public static JoinField Root<TParent>() => new Parent(typeof(TParent));

		public static JoinField Root(RelationName parent) => new Parent(parent);

		public static JoinField Link(RelationName child, Id parentId) => new Child(child, parentId);

		public static JoinField Link<TChild, TParentDocument>(TParentDocument parent) where TParentDocument : class =>
			new Child(typeof(TChild), Id.From(parent));

		public static JoinField Link<TChild>(Id parentId) => new Child(typeof(TChild), parentId);

		public static implicit operator JoinField(Parent parent) => new(parent);

		public static implicit operator JoinField(string parentName) => new(new Parent(parentName));

		public static implicit operator JoinField(Type parentType) => new(new Parent(parentType));

		public static implicit operator JoinField(Child child) => new(child);

		public T Match<T>(Func<Parent, T> first, Func<Child, T> second)
		{
			switch (Tag)
			{
				case 0:
					return first(ParentOption);
				case 1:
					return second(ChildOption);
				default:
					throw new Exception($"Unrecognized tag value: {Tag}");
			}
		}

		public void Match(Action<Parent> first, Action<Child> second)
		{
			switch (Tag)
			{
				case 0:
					first(ParentOption);
					break;
				case 1:
					second(ChildOption);
					break;
				default:
					throw new Exception($"Unrecognized tag value: {Tag}");
			}
		}

		public class Parent
		{
			public Parent(RelationName name) => Name = name;

			public RelationName Name { get; }
		}

		public class Child
		{
			public Child(RelationName name, Id parent)
			{
				Name = name;
				ParentId = parent;
			}

			public RelationName Name { get; }
			public Id ParentId { get; }
		}
	}

	internal sealed class JoinFieldConverter : JsonConverter<JoinField>
	{
		public override JoinField? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
		public override void Write(Utf8JsonWriter writer, JoinField value, JsonSerializerOptions options)
		{
			switch (value.Tag)
			{
				case 0:
					JsonSerializer.Serialize(writer, value.ParentOption.Name, options);
					break; 
			}

			// TODO - Finish this
		}
	}

	

	
	internal class FieldResolver
	{
		protected readonly ConcurrentDictionary<Field, string> Fields = new();
		protected readonly ConcurrentDictionary<PropertyName, string> Properties = new();
		private readonly IElasticsearchClientSettings _settings;

		public FieldResolver(IElasticsearchClientSettings settings)
		{
			settings.ThrowIfNull(nameof(settings));
			_settings = settings;
		}

		public string Resolve(Field field)
		{
			var name = ResolveFieldName(field);
			if (field.Boost.HasValue)
				name += $"^{field.Boost.Value.ToString(CultureInfo.InvariantCulture)}";
			return name;
		}

		private string ResolveFieldName(Field field)
		{
			//if (field.IsConditionless())
			//	return null;
			if (!field.Name.IsNullOrEmpty())
				return field.Name;
			if (field.Expression != null && !field.CachableExpression)
				return Resolve(field.Expression, field.Property);

			if (Fields.TryGetValue(field, out var fieldName))
				return fieldName;

			fieldName = Resolve(field.Expression, field.Property);
			Fields.TryAdd(field, fieldName);
			return fieldName;
		}

		public string Resolve(PropertyName property)
		{
			//if (property.IsConditionless())
			//	return null;
			if (!property.Name.IsNullOrEmpty())
				return property.Name;

			if (property.Expression != null && !property.CacheableExpression)
				return Resolve(property.Expression, property.Property);

			if (Properties.TryGetValue(property, out var propertyName))
				return propertyName;

			propertyName = Resolve(property.Expression, property.Property, true);
			Properties.TryAdd(property, propertyName);
			return propertyName;
		}

		private string Resolve(Expression expression, MemberInfo member, bool toLastToken = false)
		{
			var visitor = new FieldExpressionVisitor(_settings);
			var name = expression != null
				? visitor.Resolve(expression, toLastToken)
				: member != null
					? visitor.Resolve(member)
					: null;

			if (name == null)
				throw new ArgumentException("Name resolved to null for the given Expression or MemberInfo.");

			return name;
		}
	}

	/// <summary>
	/// Provides mappings for CLR types
	/// </summary>
	public interface IPropertyMappingProvider
	{
		/// <summary>
		/// Creates an <see cref="PropertyMapping" /> for a <see cref="MemberInfo" />
		/// </summary>
		PropertyMapping CreatePropertyMapping(MemberInfo memberInfo);
	}

	public interface IPropertyMapping
	{
		string Name { get; }

		bool Ignore { get; }
	}

	public class PropertyMapping : IPropertyMapping
	{
		public static PropertyMapping Ignored = new() { Ignore = true };

		///// <inheritdoc />
		public bool Ignore { get; set; }

		/// <inheritdoc />
		public string Name { get; set; }
	}

	/// <inheritdoc />
	public class PropertyMappingProvider : IPropertyMappingProvider
	{
		protected readonly ConcurrentDictionary<string, PropertyMapping> Properties = new();

		/// <inheritdoc />
		public virtual PropertyMapping CreatePropertyMapping(MemberInfo memberInfo)
		{
			var memberInfoString = $"{memberInfo.DeclaringType?.FullName}.{memberInfo.Name}";
			if (Properties.TryGetValue(memberInfoString, out var mapping))
				return mapping;

			mapping = PropertyMappingFromAttributes(memberInfo);
			Properties.TryAdd(memberInfoString, mapping);
			return mapping;
		}

		private static PropertyMapping PropertyMappingFromAttributes(MemberInfo memberInfo)
		{
			var jsonPropertyName = memberInfo.GetCustomAttribute<JsonPropertyNameAttribute>(true);
			//var dataMemberProperty = memberInfo.GetCustomAttribute<DataMemberAttribute>(true);

			//var propertyName = memberInfo.GetCustomAttribute<PropertyNameAttribute>(true);
			//var ignore = memberInfo.GetCustomAttribute<IgnoreForMappingAttribute>(true);

			if (jsonPropertyName == null)
				return null;

			return new PropertyMapping
			{
				Name = jsonPropertyName?.Name
			};
		}
	}

	internal class FieldExpressionVisitor : ExpressionVisitor
	{
		private readonly IElasticsearchClientSettings _settings;
		private readonly Stack<string> _stack = new();

		public FieldExpressionVisitor(IElasticsearchClientSettings settings) => _settings = settings;

		public string Resolve(Expression expression, bool toLastToken = false)
		{
			Visit(expression);
			if (toLastToken)
				return _stack.Last();

			var builder = new StringBuilder(_stack.Sum(s => s.Length) + (_stack.Count - 1));

			return _stack
				.Aggregate(
					builder,
					(sb, name) =>
						(sb.Length > 0 ? sb.Append(".") : sb).Append(name))
				.ToString();
		}

		public string Resolve(MemberInfo info)
		{
			if (info == null)
				return null;

			var name = info.Name;

			if (_settings.PropertyMappings.TryGetValue(info, out var propertyMapping))
				return propertyMapping.Name;

			//var att = ElasticsearchPropertyAttributeBase.From(info);
			//if (att != null && !att.Name.IsNullOrEmpty())
			//	return att.Name;

			return _settings.PropertyMappingProvider?.CreatePropertyMapping(info)?.Name ?? _settings.DefaultFieldNameInferrer(name);
		}

		protected override Expression VisitMember(MemberExpression expression)
		{
			if (_stack == null)
				return base.VisitMember(expression);

			var name = Resolve(expression.Member);
			_stack.Push(name);
			return base.VisitMember(expression);
		}

		protected override Expression VisitMethodCall(MethodCallExpression methodCall)
		{
			if (methodCall.Method.Name == nameof(SuffixExtensions.Suffix) && methodCall.Arguments.Any())
			{
				VisitConstantOrVariable(methodCall, _stack);
				var callingMember = new ReadOnlyCollection<Expression>(
					new List<Expression> { { methodCall.Arguments.First() } }
				);
				Visit(callingMember);
				return methodCall;
			}
			else if (methodCall.Method.Name == "get_Item" && methodCall.Arguments.Any())
			{
				var t = methodCall.Object.Type;
				var isDict =
					typeof(IDictionary).IsAssignableFrom(t)
					|| typeof(IDictionary<,>).IsAssignableFrom(t)
					|| t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IDictionary<,>);

				if (!isDict)
					return base.VisitMethodCall(methodCall);

				VisitConstantOrVariable(methodCall, _stack);
				Visit(methodCall.Object);
				return methodCall;
			}
			else if (IsLinqOperator(methodCall.Method))
			{
				for (var i = 1; i < methodCall.Arguments.Count; i++)
					Visit(methodCall.Arguments[i]);
				Visit(methodCall.Arguments[0]);
				return methodCall;
			}
			return base.VisitMethodCall(methodCall);
		}

		private static void VisitConstantOrVariable(MethodCallExpression methodCall, Stack<string> stack)
		{
			var lastArg = methodCall.Arguments.Last();
			var value = lastArg is ConstantExpression constantExpression
				? constantExpression.Value.ToString()
				: Expression.Lambda(lastArg).Compile().DynamicInvoke().ToString();
			stack.Push(value);
		}

		private static bool IsLinqOperator(MethodInfo methodInfo)
		{
			if (methodInfo.DeclaringType != typeof(Queryable) && methodInfo.DeclaringType != typeof(Enumerable))
				return false;

			return methodInfo.GetCustomAttribute<ExtensionAttribute>() != null;
		}
	}

	internal class IndexNameResolver
	{
		private readonly IElasticsearchClientSettings _transportClientSettings;

		public IndexNameResolver(IElasticsearchClientSettings connectionSettings)
		{
			connectionSettings.ThrowIfNull(nameof(connectionSettings));
			_transportClientSettings = connectionSettings;
		}

		public string Resolve<T>() => Resolve(typeof(T));

		public string Resolve(IndexName i)
		{
			if (string.IsNullOrEmpty(i?.Name))
				return PrefixClusterName(i, Resolve(i?.Type));

			ValidateIndexName(i.Name);
			return PrefixClusterName(i, i.Name);
		}

		public string Resolve(Type type)
		{
			var indexName = _transportClientSettings.DefaultIndex;
			var defaultIndices = _transportClientSettings.DefaultIndices;
			if (defaultIndices != null && type != null)
			{
				if (defaultIndices.TryGetValue(type, out var value) && !string.IsNullOrEmpty(value))
					indexName = value;
			}
			ValidateIndexName(indexName);
			return indexName;
		}

		private static string PrefixClusterName(IndexName i, string name) => i.Cluster.IsNullOrEmpty() ? name : $"{i.Cluster}:{name}";

		// ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local
		private static void ValidateIndexName(string indexName)
		{
			if (string.IsNullOrWhiteSpace(indexName))
				throw new ArgumentException(
					"Index name is null for the given type and no default index is set. "
					+ "Map an index name using ConnectionSettings.DefaultMappingFor<TDocument>() "
					+ "or set a default index using ConnectionSettings.DefaultIndex()."
				);
		}
	}
}
