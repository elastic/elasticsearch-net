// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[JsonFormatter(typeof(PropertiesFormatter))]
	public interface IProperties : IIsADictionary<PropertyName, IProperty> { }

	public class Properties : IsADictionaryBase<PropertyName, IProperty>, IProperties
	{
		private readonly IConnectionSettingsValues _settings;

		public Properties() { }

		public Properties(IDictionary<PropertyName, IProperty> container) : base(container) { }

		public Properties(Dictionary<PropertyName, IProperty> container) : base(container) { }

		internal Properties(IConnectionSettingsValues values) => _settings = values;

		public void Add(PropertyName name, IProperty property) => BackingDictionary.Add(Sanitize(name), property);

		protected override PropertyName Sanitize(PropertyName key) => _settings?.Inferrer.PropertyName(key) ?? key;
	}

	public class Properties<T> : IsADictionaryBase<PropertyName, IProperty>, IProperties
	{
		public Properties() { }

		public Properties(IDictionary<PropertyName, IProperty> container) : base(container) { }

		public Properties(IProperties properties) : base(properties) { }

		public Properties(Dictionary<PropertyName, IProperty> container) : base(container) { }

		public void Add(PropertyName name, IProperty property) => BackingDictionary.Add(name, property);

		public void Add<TValue>(Expression<Func<T, TValue>> name, IProperty property) => BackingDictionary.Add(name, property);
	}

	public partial interface IPropertiesDescriptor<T, out TReturnType>
		where T : class
		where TReturnType : class
	{
		/// <inheritdoc cref="ITextProperty"/>
		TReturnType Text(Func<TextPropertyDescriptor<T>, ITextProperty> selector);

		/// <inheritdoc cref="IKeywordProperty"/>
		TReturnType Keyword(Func<KeywordPropertyDescriptor<T>, IKeywordProperty> selector);

		/// <summary>
		/// Number introduces a numeric mapping that defaults to `float`. Use .Type() to set the right type if needed or use
		/// Scalar instead of <see cref="Number" />
		/// </summary>
		TReturnType Number(Func<NumberPropertyDescriptor<T>, INumberProperty> selector);

		/// <inheritdoc cref="ITokenCountProperty"/>
		TReturnType TokenCount(Func<TokenCountPropertyDescriptor<T>, ITokenCountProperty> selector);

		/// <inheritdoc cref="IDateProperty"/>
		TReturnType Date(Func<DatePropertyDescriptor<T>, IDateProperty> selector);

		/// <inheritdoc cref="IDateNanosProperty"/>
		TReturnType DateNanos(Func<DateNanosPropertyDescriptor<T>, IDateNanosProperty> selector);

		/// <inheritdoc cref="IBooleanProperty"/>
		TReturnType Boolean(Func<BooleanPropertyDescriptor<T>, IBooleanProperty> selector);

		/// <inheritdoc cref="IBinaryProperty"/>
		TReturnType Binary(Func<BinaryPropertyDescriptor<T>, IBinaryProperty> selector);

		/// <inheritdoc cref="IObjectProperty"/>
		TReturnType Object<TChild>(Func<ObjectTypeDescriptor<T, TChild>, IObjectProperty> selector)
			where TChild : class;

		/// <inheritdoc cref="INestedProperty"/>
		TReturnType Nested<TChild>(Func<NestedPropertyDescriptor<T, TChild>, INestedProperty> selector)
			where TChild : class;

		/// <inheritdoc cref="IIpProperty"/>
		TReturnType Ip(Func<IpPropertyDescriptor<T>, IIpProperty> selector);

		/// <inheritdoc cref="IGeoPointProperty"/>
		TReturnType GeoPoint(Func<GeoPointPropertyDescriptor<T>, IGeoPointProperty> selector);

		/// <inheritdoc cref="IGeoShapeProperty"/>
		TReturnType GeoShape(Func<GeoShapePropertyDescriptor<T>, IGeoShapeProperty> selector);

		/// <inheritdoc cref="IShapeProperty"/>
		TReturnType Shape(Func<ShapePropertyDescriptor<T>, IShapeProperty> selector);

		/// <inheritdoc cref="IPointProperty"/>
		TReturnType Point(Func<PointPropertyDescriptor<T>, IPointProperty> selector);

		/// <inheritdoc cref="ICompletionProperty"/>
		TReturnType Completion(Func<CompletionPropertyDescriptor<T>, ICompletionProperty> selector);

		/// <inheritdoc cref="IMurmur3HashProperty"/>
		TReturnType Murmur3Hash(Func<Murmur3HashPropertyDescriptor<T>, IMurmur3HashProperty> selector);

		/// <inheritdoc cref="IPercolatorProperty"/>
		TReturnType Percolator(Func<PercolatorPropertyDescriptor<T>, IPercolatorProperty> selector);

		/// <inheritdoc cref="IDateRangeProperty"/>
		TReturnType DateRange(Func<DateRangePropertyDescriptor<T>, IDateRangeProperty> selector);

		/// <inheritdoc cref="IDoubleRangeProperty"/>
		TReturnType DoubleRange(Func<DoubleRangePropertyDescriptor<T>, IDoubleRangeProperty> selector);

		/// <inheritdoc cref="IFloatRangeProperty"/>
		TReturnType FloatRange(Func<FloatRangePropertyDescriptor<T>, IFloatRangeProperty> selector);

		/// <inheritdoc cref="IIntegerRangeProperty"/>
		TReturnType IntegerRange(Func<IntegerRangePropertyDescriptor<T>, IIntegerRangeProperty> selector);

		/// <inheritdoc cref="ILongRangeProperty"/>
		TReturnType LongRange(Func<LongRangePropertyDescriptor<T>, ILongRangeProperty> selector);

		/// <inheritdoc cref="IIpRangeProperty"/>
		TReturnType IpRange(Func<IpRangePropertyDescriptor<T>, IIpRangeProperty> selector);

		/// <inheritdoc cref="IJoinProperty"/>
		TReturnType Join(Func<JoinPropertyDescriptor<T>, IJoinProperty> selector);

		/// <inheritdoc cref="IHistogramProperty"/>
		TReturnType Histogram(Func<HistogramPropertyDescriptor<T>, IHistogramProperty> selector);

		/// <inheritdoc cref="IFieldAliasProperty"/>
		TReturnType FieldAlias(Func<FieldAliasPropertyDescriptor<T>, IFieldAliasProperty> selector);

		/// <inheritdoc cref="IRankFeatureProperty"/>
		TReturnType RankFeature(Func<RankFeaturePropertyDescriptor<T>, IRankFeatureProperty> selector);

		/// <inheritdoc cref="IRankFeaturesProperty"/>
		TReturnType RankFeatures(Func<RankFeaturesPropertyDescriptor<T>, IRankFeaturesProperty> selector);

		/// <inheritdoc cref="IFlattenedProperty"/>
		TReturnType Flattened(Func<FlattenedPropertyDescriptor<T>, IFlattenedProperty> selector);

		/// <inheritdoc cref="ISearchAsYouTypeProperty"/>
		TReturnType SearchAsYouType(Func<SearchAsYouTypePropertyDescriptor<T>, ISearchAsYouTypeProperty> selector);

		/// <inheritdoc cref="IConstantKeywordProperty"/>
		TReturnType ConstantKeyword(Func<ConstantKeywordPropertyDescriptor<T>, IConstantKeywordProperty> selector);

		/// <inheritdoc cref="IWildcardProperty"/>
		TReturnType Wildcard(Func<WildcardPropertyDescriptor<T>, IWildcardProperty> selector);
	}

	public partial class PropertiesDescriptor<T> where T : class
	{
		public PropertiesDescriptor() : base(new Properties<T>()) { }

		public PropertiesDescriptor(IProperties properties) : base(properties ?? new Properties<T>()) { }

		/// <inheritdoc cref="IBinaryProperty"/>
		public PropertiesDescriptor<T> Binary(Func<BinaryPropertyDescriptor<T>, IBinaryProperty> selector) => SetProperty(selector);

		/// <inheritdoc cref="IBooleanProperty"/>
		public PropertiesDescriptor<T> Boolean(Func<BooleanPropertyDescriptor<T>, IBooleanProperty> selector) => SetProperty(selector);

		/// <inheritdoc cref="ICompletionProperty"/>
		public PropertiesDescriptor<T> Completion(Func<CompletionPropertyDescriptor<T>, ICompletionProperty> selector) => SetProperty(selector);

		/// <inheritdoc cref="IDateProperty"/>
		public PropertiesDescriptor<T> Date(Func<DatePropertyDescriptor<T>, IDateProperty> selector) => SetProperty(selector);

		/// <inheritdoc cref="IDateNanosProperty"/>
		public PropertiesDescriptor<T> DateNanos(Func<DateNanosPropertyDescriptor<T>, IDateNanosProperty> selector) => SetProperty(selector);

		/// <inheritdoc cref="IDateRangeProperty"/>
		public PropertiesDescriptor<T> DateRange(Func<DateRangePropertyDescriptor<T>, IDateRangeProperty> selector) => SetProperty(selector);

		/// <inheritdoc cref="IDoubleRangeProperty"/>
		public PropertiesDescriptor<T> DoubleRange(Func<DoubleRangePropertyDescriptor<T>, IDoubleRangeProperty> selector) => SetProperty(selector);

		/// <inheritdoc cref="IFloatRangeProperty"/>
		public PropertiesDescriptor<T> FloatRange(Func<FloatRangePropertyDescriptor<T>, IFloatRangeProperty> selector) => SetProperty(selector);

		/// <inheritdoc cref="IGeoPointProperty"/>
		public PropertiesDescriptor<T> GeoPoint(Func<GeoPointPropertyDescriptor<T>, IGeoPointProperty> selector) => SetProperty(selector);

		/// <inheritdoc cref="IGeoShapeProperty"/>
		public PropertiesDescriptor<T> GeoShape(Func<GeoShapePropertyDescriptor<T>, IGeoShapeProperty> selector) => SetProperty(selector);

		/// <inheritdoc cref="IShapeProperty"/>
		public PropertiesDescriptor<T> Shape(Func<ShapePropertyDescriptor<T>, IShapeProperty> selector) => SetProperty(selector);

		/// <inheritdoc cref="IPointProperty"/>
		public PropertiesDescriptor<T> Point(Func<PointPropertyDescriptor<T>, IPointProperty> selector) => SetProperty(selector);

		/// <inheritdoc cref="IIntegerRangeProperty"/>
		public PropertiesDescriptor<T> IntegerRange(Func<IntegerRangePropertyDescriptor<T>, IIntegerRangeProperty> selector) => SetProperty(selector);

		/// <inheritdoc cref="IIpProperty"/>
		public PropertiesDescriptor<T> Ip(Func<IpPropertyDescriptor<T>, IIpProperty> selector) => SetProperty(selector);

		/// <inheritdoc cref="IIpRangeProperty"/>
		public PropertiesDescriptor<T> IpRange(Func<IpRangePropertyDescriptor<T>, IIpRangeProperty> selector) => SetProperty(selector);

		/// <inheritdoc cref="IJoinProperty"/>
		public PropertiesDescriptor<T> Join(Func<JoinPropertyDescriptor<T>, IJoinProperty> selector) => SetProperty(selector);

		/// <inheritdoc cref="IKeywordProperty"/>
		public PropertiesDescriptor<T> Keyword(Func<KeywordPropertyDescriptor<T>, IKeywordProperty> selector) => SetProperty(selector);

		/// <inheritdoc cref="ILongRangeProperty"/>
		public PropertiesDescriptor<T> LongRange(Func<LongRangePropertyDescriptor<T>, ILongRangeProperty> selector) => SetProperty(selector);

		/// <inheritdoc cref="IMurmur3HashProperty"/>
		public PropertiesDescriptor<T> Murmur3Hash(Func<Murmur3HashPropertyDescriptor<T>, IMurmur3HashProperty> selector) => SetProperty(selector);

		/// <inheritdoc cref="INestedProperty"/>
		public PropertiesDescriptor<T> Nested<TChild>(Func<NestedPropertyDescriptor<T, TChild>, INestedProperty> selector)
			where TChild : class => SetProperty(selector);

		/// <summary>
		/// Number introduces a numeric mapping that defaults to <c>float</c>. use
		/// <see cref="IProperty.Type" /> to set the right type if needed, or use .Scalar()
		/// instead of <see cref="Number" />
		/// </summary>
		public PropertiesDescriptor<T> Number(Func<NumberPropertyDescriptor<T>, INumberProperty> selector) => SetProperty(selector);

		/// <inheritdoc cref="IObjectProperty"/>
		public PropertiesDescriptor<T> Object<TChild>(Func<ObjectTypeDescriptor<T, TChild>, IObjectProperty> selector)
			where TChild : class => SetProperty(selector);

		/// <inheritdoc cref="IPercolatorProperty"/>
		public PropertiesDescriptor<T> Percolator(Func<PercolatorPropertyDescriptor<T>, IPercolatorProperty> selector) => SetProperty(selector);

		/// <inheritdoc cref="ITextProperty"/>
		public PropertiesDescriptor<T> Text(Func<TextPropertyDescriptor<T>, ITextProperty> selector) => SetProperty(selector);

		/// <inheritdoc cref="ISearchAsYouTypeProperty"/>
		public PropertiesDescriptor<T> SearchAsYouType(Func<SearchAsYouTypePropertyDescriptor<T>, ISearchAsYouTypeProperty> selector) => SetProperty(selector);

		/// <inheritdoc cref="ITokenCountProperty"/>
		public PropertiesDescriptor<T> TokenCount(Func<TokenCountPropertyDescriptor<T>, ITokenCountProperty> selector) => SetProperty(selector);

		/// <inheritdoc cref="IFieldAliasProperty"/>
		public PropertiesDescriptor<T> FieldAlias(Func<FieldAliasPropertyDescriptor<T>, IFieldAliasProperty> selector) => SetProperty(selector);

		/// <inheritdoc cref="IRankFeatureProperty"/>
		public PropertiesDescriptor<T> RankFeature(Func<RankFeaturePropertyDescriptor<T>, IRankFeatureProperty> selector) => SetProperty(selector);

		/// <inheritdoc cref="IRankFeaturesProperty"/>
		public PropertiesDescriptor<T> RankFeatures(Func<RankFeaturesPropertyDescriptor<T>, IRankFeaturesProperty> selector) => SetProperty(selector);

		/// <inheritdoc cref="IFlattenedProperty"/>
		public PropertiesDescriptor<T> Flattened(Func<FlattenedPropertyDescriptor<T>, IFlattenedProperty> selector) => SetProperty(selector);

		/// <inheritdoc cref="IHistogramProperty"/>
		public PropertiesDescriptor<T> Histogram(Func<HistogramPropertyDescriptor<T>, IHistogramProperty> selector) => SetProperty(selector);

		/// <inheritdoc cref="IConstantKeywordProperty"/>
		public PropertiesDescriptor<T> ConstantKeyword(Func<ConstantKeywordPropertyDescriptor<T>, IConstantKeywordProperty> selector) =>
			SetProperty(selector);

		/// <inheritdoc cref="IWildcardProperty"/>
		public PropertiesDescriptor<T> Wildcard(Func<WildcardPropertyDescriptor<T>, IWildcardProperty> selector) =>
			SetProperty(selector);

		/// <summary>
		/// Map a custom property.
		/// </summary>
		public PropertiesDescriptor<T> Custom(IProperty customType) => SetProperty(customType);

		private PropertiesDescriptor<T> SetProperty<TDescriptor, TInterface>(Func<TDescriptor, TInterface> selector)
			where TDescriptor : class, TInterface, new()
			where TInterface : IProperty
		{
			selector.ThrowIfNull(nameof(selector));
			var type = selector(new TDescriptor());
			return SetProperty(type);
		}

		private PropertiesDescriptor<T> SetProperty(IProperty type)
		{
			type.ThrowIfNull(nameof(type));
			var typeName = type.GetType().Name;
			if (type.Name.IsConditionless())
				throw new ArgumentException($"Could not get field name for {typeName} mapping");

			return Assign(type, (a, v) => a[v.Name] = v);
		}
	}

	internal static class PropertiesExtensions
	{
		internal static IProperties AutoMap(this IProperties existingProperties, Type documentType, IPropertyVisitor visitor = null,
			int maxRecursion = 0
		)
		{
			var properties = new Properties();
			var autoProperties = new PropertyWalker(documentType, visitor, maxRecursion).GetProperties();
			foreach (var autoProperty in autoProperties)
				properties[autoProperty.Key] = autoProperty.Value;

			if (existingProperties == null) return properties;

			// Existing/manually mapped properties always take precedence
			foreach (var existing in existingProperties)
				properties[existing.Key] = existing.Value;

			return properties;
		}

		internal static IProperties AutoMap<T>(this IProperties existingProperties, IPropertyVisitor visitor = null, int maxRecursion = 0)
			where T : class => existingProperties.AutoMap(typeof(T), visitor, maxRecursion);
	}
}
