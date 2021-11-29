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

namespace Elastic.Clients.Elasticsearch
{
	public class Inferrer
	{
		private readonly IElasticsearchClientSettings _elasticsearchClientSettings;

		public Inferrer(IElasticsearchClientSettings elasticsearchClientSettings)
		{
			elasticsearchClientSettings.ThrowIfNull(nameof(elasticsearchClientSettings));
			_elasticsearchClientSettings = elasticsearchClientSettings;
			IdResolver = new IdResolver(elasticsearchClientSettings);
			IndexNameResolver = new IndexNameResolver(elasticsearchClientSettings);
			//RelationNameResolver = new RelationNameResolver(connectionSettings);
			FieldResolver = new FieldResolver(elasticsearchClientSettings);
			//RoutingResolver = new RoutingResolver(connectionSettings, IdResolver);

			//CreateMultiHitDelegates =
			//	new ConcurrentDictionary<Type,
			//		Action<MultiGetResponseFormatter.MultiHitTuple, IJsonFormatterResolver, ICollection<IMultiGetHit<object>>>>();
			//CreateSearchResponseDelegates =
			//	new ConcurrentDictionary<Type,
			//		Action<MultiSearchResponseFormatter.SearchHitTuple, IJsonFormatterResolver, IDictionary<string, IResponse>>>();
		}

		//internal ConcurrentDictionary<Type, Action<MultiGetResponseFormatter.MultiHitTuple, IJsonFormatterResolver, ICollection<IMultiGetHit<object>>>
		//	>
		//	CreateMultiHitDelegates { get; }

		//internal ConcurrentDictionary<Type,
		//		Action<MultiSearchResponseFormatter.SearchHitTuple, IJsonFormatterResolver, IDictionary<string, IResponse>>>
		//	CreateSearchResponseDelegates { get; }

		private FieldResolver FieldResolver { get; }

		private IdResolver IdResolver { get; }

		private IndexNameResolver IndexNameResolver { get; }
		//private RelationNameResolver RelationNameResolver { get; }
		//private RoutingResolver RoutingResolver { get; }

		public string Resolve(IUrlParameter urlParameter) => urlParameter.GetString(_elasticsearchClientSettings);

		public string Field(Field field) => FieldResolver.Resolve(field);

		public string PropertyName(PropertyName property) => FieldResolver.Resolve(property);

		public string IndexName<T>() where T : class => IndexNameResolver.Resolve<T>();

		public string IndexName(IndexName index) => IndexNameResolver.Resolve(index);

		public string Id<T>(T instance) where T : class => IdResolver.Resolve(instance);

		public string Id(Type type, object instance) => IdResolver.Resolve(type, instance);

		//public string RelationName<T>() where T : class => RelationNameResolver.Resolve<T>();

		//public string RelationName(RelationName type) => RelationNameResolver.Resolve(type);

		//public string Routing<T>(T document) => RoutingResolver.Resolve(document);

		//public string Routing(Type type, object instance) => RoutingResolver.Resolve(type, instance);
	}

	internal class FieldResolver
	{
		protected readonly ConcurrentDictionary<Field, string> Fields = new ConcurrentDictionary<Field, string>();
		protected readonly ConcurrentDictionary<PropertyName, string> Properties = new ConcurrentDictionary<PropertyName, string>();
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

	public class PropertyMapping
	{
		public static PropertyMapping Ignored = new PropertyMapping { Ignore = true };

		/// <inheritdoc />
		public bool Ignore { get; set; }

		/// <inheritdoc />
		public string Name { get; set; }
	}

	/// <inheritdoc />
	public class PropertyMappingProvider : IPropertyMappingProvider
	{
		protected readonly ConcurrentDictionary<string, PropertyMapping> Properties = new ConcurrentDictionary<string, PropertyMapping>();

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
			var dataMemberProperty = memberInfo.GetCustomAttribute<DataMemberAttribute>(true);
			var propertyName = memberInfo.GetCustomAttribute<PropertyNameAttribute>(true);
			var ignore = memberInfo.GetCustomAttribute<IgnoreForMappingAttribute>(true);
			if (ignore == null && propertyName == null && dataMemberProperty == null)
				return null;

			return new PropertyMapping
			{
				Name = propertyName?.Name ?? dataMemberProperty?.Name,
				Ignore = ignore != null || propertyName != null && propertyName.Ignore
			};
		}
	}

	internal class FieldExpressionVisitor : ExpressionVisitor
	{
		private readonly IElasticsearchClientSettings _settings;
		private readonly Stack<string> _stack = new Stack<string>();

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

			var att = ElasticsearchPropertyAttributeBase.From(info);
			if (att != null && !att.Name.IsNullOrEmpty())
				return att.Name;

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
		private readonly IElasticsearchClientSettings _connectionSettings;

		public IndexNameResolver(IElasticsearchClientSettings connectionSettings)
		{
			connectionSettings.ThrowIfNull(nameof(connectionSettings));
			_connectionSettings = connectionSettings;
		}

		public string Resolve<T>() where T : class => Resolve(typeof(T));

		public string Resolve(IndexName i)
		{
			if (string.IsNullOrEmpty(i?.Name))
				return PrefixClusterName(i, Resolve(i?.Type));

			ValidateIndexName(i.Name);
			return PrefixClusterName(i, i.Name);
		}

		public string Resolve(Type type)
		{
			var indexName = _connectionSettings.DefaultIndex;
			var defaultIndices = _connectionSettings.DefaultIndices;
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
