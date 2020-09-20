// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Nest
{
	public class SingleMappingSelector<T> : SelectorBase, IPropertiesDescriptor<T, IProperty> where T : class
	{
		/// <inheritdoc />
		public IProperty Binary(Func<BinaryPropertyDescriptor<T>, IBinaryProperty> selector) =>
			selector?.Invoke(new BinaryPropertyDescriptor<T>());

		/// <inheritdoc />
		public IProperty Boolean(Func<BooleanPropertyDescriptor<T>, IBooleanProperty> selector) =>
			selector?.Invoke(new BooleanPropertyDescriptor<T>());

		/// <inheritdoc />
		public IProperty Completion(Func<CompletionPropertyDescriptor<T>, ICompletionProperty> selector) =>
			selector?.Invoke(new CompletionPropertyDescriptor<T>());

		/// <inheritdoc />
		public IProperty Date(Func<DatePropertyDescriptor<T>, IDateProperty> selector) =>
			selector?.Invoke(new DatePropertyDescriptor<T>());

		/// <inheritdoc />
		public IProperty DateNanos(Func<DateNanosPropertyDescriptor<T>, IDateNanosProperty> selector) =>
			selector?.Invoke(new DateNanosPropertyDescriptor<T>());

		/// <inheritdoc />
		public IProperty DateRange(Func<DateRangePropertyDescriptor<T>, IDateRangeProperty> selector) =>
			selector?.Invoke(new DateRangePropertyDescriptor<T>());

		/// <inheritdoc />
		public IProperty DoubleRange(Func<DoubleRangePropertyDescriptor<T>, IDoubleRangeProperty> selector) =>
			selector?.Invoke(new DoubleRangePropertyDescriptor<T>());

		/// <inheritdoc />
		public IProperty FloatRange(Func<FloatRangePropertyDescriptor<T>, IFloatRangeProperty> selector) =>
			selector?.Invoke(new FloatRangePropertyDescriptor<T>());

		/// <inheritdoc />
		public IProperty GeoPoint(Func<GeoPointPropertyDescriptor<T>, IGeoPointProperty> selector) =>
			selector?.Invoke(new GeoPointPropertyDescriptor<T>());

		/// <inheritdoc />
		public IProperty GeoShape(Func<GeoShapePropertyDescriptor<T>, IGeoShapeProperty> selector) =>
			selector?.Invoke(new GeoShapePropertyDescriptor<T>());

		/// <inheritdoc />
		public IProperty Shape(Func<ShapePropertyDescriptor<T>, IShapeProperty> selector) =>
			selector?.Invoke(new ShapePropertyDescriptor<T>());

		/// <inheritdoc />
		public IProperty Point(Func<PointPropertyDescriptor<T>, IPointProperty> selector) =>
			selector?.Invoke(new PointPropertyDescriptor<T>());

		/// <inheritdoc />
		public IProperty IntegerRange(Func<IntegerRangePropertyDescriptor<T>, IIntegerRangeProperty> selector) =>
			selector?.Invoke(new IntegerRangePropertyDescriptor<T>());

		/// <inheritdoc />
		public IProperty Ip(Func<IpPropertyDescriptor<T>, IIpProperty> selector) =>
			selector?.Invoke(new IpPropertyDescriptor<T>());

		/// <inheritdoc />
		public IProperty IpRange(Func<IpRangePropertyDescriptor<T>, IIpRangeProperty> selector) =>
			selector?.Invoke(new IpRangePropertyDescriptor<T>());

		/// <inheritdoc />
		public IProperty Join(Func<JoinPropertyDescriptor<T>, IJoinProperty> selector) =>
			selector?.Invoke(new JoinPropertyDescriptor<T>());

		/// <inheritdoc />
		public IProperty Histogram(Func<HistogramPropertyDescriptor<T>, IHistogramProperty> selector) =>
			selector?.Invoke(new HistogramPropertyDescriptor<T>());

    /// <inheritdoc />
		public IProperty FieldAlias(Func<FieldAliasPropertyDescriptor<T>, IFieldAliasProperty> selector) =>
			selector?.Invoke(new FieldAliasPropertyDescriptor<T>());

		/// <inheritdoc />
		public IProperty RankFeature(Func<RankFeaturePropertyDescriptor<T>, IRankFeatureProperty> selector) =>
			selector?.Invoke(new RankFeaturePropertyDescriptor<T>());

		/// <inheritdoc />
		public IProperty RankFeatures(Func<RankFeaturesPropertyDescriptor<T>, IRankFeaturesProperty> selector) =>
			selector?.Invoke(new RankFeaturesPropertyDescriptor<T>());

		/// <inheritdoc />
		public IProperty Flattened(Func<FlattenedPropertyDescriptor<T>, IFlattenedProperty> selector) =>
			selector?.Invoke(new FlattenedPropertyDescriptor<T>());

		/// <inheritdoc />
		public IProperty Keyword(Func<KeywordPropertyDescriptor<T>, IKeywordProperty> selector) =>
			selector?.Invoke(new KeywordPropertyDescriptor<T>());

		/// <inheritdoc />
		public IProperty LongRange(Func<LongRangePropertyDescriptor<T>, ILongRangeProperty> selector) =>
			selector?.Invoke(new LongRangePropertyDescriptor<T>());

		/// <inheritdoc />
		public IProperty Murmur3Hash(Func<Murmur3HashPropertyDescriptor<T>, IMurmur3HashProperty> selector) =>
			selector?.Invoke(new Murmur3HashPropertyDescriptor<T>());

		/// <inheritdoc />
		public IProperty Nested<TChild>(Func<NestedPropertyDescriptor<T, TChild>, INestedProperty> selector)
			where TChild : class => selector?.Invoke(new NestedPropertyDescriptor<T, TChild>());

		/// <summary>
		/// Number introduces a numeric mapping that defaults to `float` use .Type() to set the right type if needed or use
		/// Scalar instead of <see cref="Number" />
		/// </summary>
		public IProperty Number(Func<NumberPropertyDescriptor<T>, INumberProperty> selector) =>
			selector?.Invoke(new NumberPropertyDescriptor<T>());

		/// <inheritdoc />
		public IProperty Object<TChild>(Func<ObjectTypeDescriptor<T, TChild>, IObjectProperty> selector)
			where TChild : class => selector?.Invoke(new ObjectTypeDescriptor<T, TChild>());

		/// <inheritdoc />
		public IProperty Percolator(Func<PercolatorPropertyDescriptor<T>, IPercolatorProperty> selector) =>
			selector?.Invoke(new PercolatorPropertyDescriptor<T>());

		/// <inheritdoc />
		public IProperty Text(Func<TextPropertyDescriptor<T>, ITextProperty> selector) =>
			selector?.Invoke(new TextPropertyDescriptor<T>());

		/// <inheritdoc />
		public IProperty TokenCount(Func<TokenCountPropertyDescriptor<T>, ITokenCountProperty> selector) =>
			selector?.Invoke(new TokenCountPropertyDescriptor<T>());

		/// <inheritdoc />
		public IProperty Generic(Func<GenericPropertyDescriptor<T>, IGenericProperty> selector) =>
			selector?.Invoke(new GenericPropertyDescriptor<T>());

		/// <inheritdoc />
		public IProperty SearchAsYouType(Func<SearchAsYouTypePropertyDescriptor<T>, ISearchAsYouTypeProperty> selector) =>
			selector?.Invoke(new SearchAsYouTypePropertyDescriptor<T>());

		/// <inheritdoc />
		public IProperty ConstantKeyword(Func<ConstantKeywordPropertyDescriptor<T>, IConstantKeywordProperty> selector) =>
			selector?.Invoke(new ConstantKeywordPropertyDescriptor<T>());

		/// <inheritdoc />
		public IProperty Wildcard(Func<WildcardPropertyDescriptor<T>, IWildcardProperty> selector) =>
			selector?.Invoke(new WildcardPropertyDescriptor<T>());

#pragma warning disable CS3001 // Argument type is not CLS-compliant
		public IProperty Scalar(Expression<Func<T, int>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector = null) =>
			selector.InvokeOrDefault(new NumberPropertyDescriptor<T>().Name(field).Type(NumberType.Integer));

		public IProperty Scalar(Expression<Func<T, IEnumerable<int>>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector = null) =>
			selector.InvokeOrDefault(new NumberPropertyDescriptor<T>().Name(field).Type(NumberType.Integer));

		public IProperty Scalar(Expression<Func<T, int?>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector = null) =>
			selector.InvokeOrDefault(new NumberPropertyDescriptor<T>().Name(field).Type(NumberType.Integer));

		public IProperty Scalar(Expression<Func<T, IEnumerable<int?>>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector = null) =>
			selector.InvokeOrDefault(new NumberPropertyDescriptor<T>().Name(field).Type(NumberType.Integer));

		public IProperty Scalar(Expression<Func<T, float>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector = null) =>
			selector.InvokeOrDefault(new NumberPropertyDescriptor<T>().Name(field).Type(NumberType.Float));

		public IProperty Scalar(Expression<Func<T, IEnumerable<float>>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector = null) =>
			selector.InvokeOrDefault(new NumberPropertyDescriptor<T>().Name(field).Type(NumberType.Float));

		public IProperty Scalar(Expression<Func<T, float?>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector = null) =>
			selector.InvokeOrDefault(new NumberPropertyDescriptor<T>().Name(field).Type(NumberType.Float));

		public IProperty Scalar(Expression<Func<T, IEnumerable<float?>>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector = null) =>
			selector.InvokeOrDefault(new NumberPropertyDescriptor<T>().Name(field).Type(NumberType.Float));

		public IProperty Scalar(Expression<Func<T, sbyte>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector = null) =>
			selector.InvokeOrDefault(new NumberPropertyDescriptor<T>().Name(field).Type(NumberType.Byte));

		public IProperty Scalar(Expression<Func<T, sbyte?>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector = null) =>
			selector.InvokeOrDefault(new NumberPropertyDescriptor<T>().Name(field).Type(NumberType.Byte));

		public IProperty Scalar(Expression<Func<T, IEnumerable<sbyte>>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector = null) =>
			selector.InvokeOrDefault(new NumberPropertyDescriptor<T>().Name(field).Type(NumberType.Byte));

		public IProperty Scalar(Expression<Func<T, IEnumerable<sbyte?>>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector = null) =>
			selector.InvokeOrDefault(new NumberPropertyDescriptor<T>().Name(field).Type(NumberType.Byte));

		public IProperty Scalar(Expression<Func<T, short>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector = null) =>
			selector.InvokeOrDefault(new NumberPropertyDescriptor<T>().Name(field).Type(NumberType.Short));

		public IProperty Scalar(Expression<Func<T, short?>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector = null) =>
			selector.InvokeOrDefault(new NumberPropertyDescriptor<T>().Name(field).Type(NumberType.Short));

		public IProperty Scalar(Expression<Func<T, IEnumerable<short>>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector = null) =>
			selector.InvokeOrDefault(new NumberPropertyDescriptor<T>().Name(field).Type(NumberType.Short));

		public IProperty Scalar(Expression<Func<T, IEnumerable<short?>>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector = null) =>
			selector.InvokeOrDefault(new NumberPropertyDescriptor<T>().Name(field).Type(NumberType.Short));

		public IProperty Scalar(Expression<Func<T, byte>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector = null) =>
			selector.InvokeOrDefault(new NumberPropertyDescriptor<T>().Name(field).Type(NumberType.Short));

		public IProperty Scalar(Expression<Func<T, byte?>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector = null) =>
			selector.InvokeOrDefault(new NumberPropertyDescriptor<T>().Name(field).Type(NumberType.Short));

		public IProperty Scalar(Expression<Func<T, IEnumerable<byte>>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector = null) =>
			selector.InvokeOrDefault(new NumberPropertyDescriptor<T>().Name(field).Type(NumberType.Short));

		public IProperty Scalar(Expression<Func<T, IEnumerable<byte?>>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector = null) =>
			selector.InvokeOrDefault(new NumberPropertyDescriptor<T>().Name(field).Type(NumberType.Short));

		public IProperty Scalar(Expression<Func<T, long>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector = null) =>
			selector.InvokeOrDefault(new NumberPropertyDescriptor<T>().Name(field).Type(NumberType.Long));

		public IProperty Scalar(Expression<Func<T, long?>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector = null) =>
			selector.InvokeOrDefault(new NumberPropertyDescriptor<T>().Name(field).Type(NumberType.Long));

		public IProperty Scalar(Expression<Func<T, IEnumerable<long>>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector = null) =>
			selector.InvokeOrDefault(new NumberPropertyDescriptor<T>().Name(field).Type(NumberType.Long));

		public IProperty Scalar(Expression<Func<T, IEnumerable<long?>>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector = null) =>
			selector.InvokeOrDefault(new NumberPropertyDescriptor<T>().Name(field).Type(NumberType.Long));

		public IProperty Scalar(Expression<Func<T, uint>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector = null) =>
			selector.InvokeOrDefault(new NumberPropertyDescriptor<T>().Name(field).Type(NumberType.Long));

		public IProperty Scalar(Expression<Func<T, uint?>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector = null) =>
			selector.InvokeOrDefault(new NumberPropertyDescriptor<T>().Name(field).Type(NumberType.Long));

		public IProperty Scalar(Expression<Func<T, IEnumerable<uint>>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector = null) =>
			selector.InvokeOrDefault(new NumberPropertyDescriptor<T>().Name(field).Type(NumberType.Long));

		public IProperty Scalar(Expression<Func<T, IEnumerable<uint?>>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector = null) =>
			selector.InvokeOrDefault(new NumberPropertyDescriptor<T>().Name(field).Type(NumberType.Long));

		public IProperty Scalar(Expression<Func<T, TimeSpan>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector = null) =>
			selector.InvokeOrDefault(new NumberPropertyDescriptor<T>().Name(field).Type(NumberType.Long));

		public IProperty Scalar(Expression<Func<T, TimeSpan?>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector = null) =>
			selector.InvokeOrDefault(new NumberPropertyDescriptor<T>().Name(field).Type(NumberType.Long));

		public IProperty Scalar(Expression<Func<T, IEnumerable<TimeSpan>>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector = null
		) =>
			selector.InvokeOrDefault(new NumberPropertyDescriptor<T>().Name(field).Type(NumberType.Long));

		public IProperty Scalar(Expression<Func<T, IEnumerable<TimeSpan?>>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector = null
		) =>
			selector.InvokeOrDefault(new NumberPropertyDescriptor<T>().Name(field).Type(NumberType.Long));

		public IProperty Scalar(Expression<Func<T, decimal>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector = null) =>
			selector.InvokeOrDefault(new NumberPropertyDescriptor<T>().Name(field).Type(NumberType.Double));

		public IProperty Scalar(Expression<Func<T, decimal?>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector = null) =>
			selector.InvokeOrDefault(new NumberPropertyDescriptor<T>().Name(field).Type(NumberType.Double));

		public IProperty Scalar(Expression<Func<T, IEnumerable<decimal>>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector = null
		) =>
			selector.InvokeOrDefault(new NumberPropertyDescriptor<T>().Name(field).Type(NumberType.Double));

		public IProperty Scalar(Expression<Func<T, IEnumerable<decimal?>>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector = null
		) =>
			selector.InvokeOrDefault(new NumberPropertyDescriptor<T>().Name(field).Type(NumberType.Double));

		public IProperty Scalar(Expression<Func<T, ulong>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector = null) =>
			selector.InvokeOrDefault(new NumberPropertyDescriptor<T>().Name(field).Type(NumberType.Double));

		public IProperty Scalar(Expression<Func<T, ulong?>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector = null) =>
			selector.InvokeOrDefault(new NumberPropertyDescriptor<T>().Name(field).Type(NumberType.Double));

		public IProperty Scalar(Expression<Func<T, IEnumerable<ulong>>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector = null) =>
			selector.InvokeOrDefault(new NumberPropertyDescriptor<T>().Name(field).Type(NumberType.Double));

		public IProperty Scalar(Expression<Func<T, IEnumerable<ulong?>>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector = null) =>
			selector.InvokeOrDefault(new NumberPropertyDescriptor<T>().Name(field).Type(NumberType.Double));

		public IProperty Scalar(Expression<Func<T, double>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector = null) =>
			selector.InvokeOrDefault(new NumberPropertyDescriptor<T>().Name(field).Type(NumberType.Double));

		public IProperty Scalar(Expression<Func<T, double?>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector = null) =>
			selector.InvokeOrDefault(new NumberPropertyDescriptor<T>().Name(field).Type(NumberType.Double));

		public IProperty Scalar(Expression<Func<T, IEnumerable<double>>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector = null) =>
			selector.InvokeOrDefault(new NumberPropertyDescriptor<T>().Name(field).Type(NumberType.Double));

		public IProperty Scalar(Expression<Func<T, IEnumerable<double?>>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector = null
		) =>
			selector.InvokeOrDefault(new NumberPropertyDescriptor<T>().Name(field).Type(NumberType.Double));

		public IProperty Scalar(Expression<Func<T, Enum>> field, Func<NumberPropertyDescriptor<T>, INumberProperty> selector = null) =>
			selector.InvokeOrDefault(new NumberPropertyDescriptor<T>().Name(field).Type(NumberType.Integer));

		public IProperty Scalar(Expression<Func<T, DateTime>> field, Func<DatePropertyDescriptor<T>, IDateProperty> selector = null) =>
			selector.InvokeOrDefault(new DatePropertyDescriptor<T>().Name(field));

		public IProperty Scalar(Expression<Func<T, DateTime?>> field, Func<DatePropertyDescriptor<T>, IDateProperty> selector = null) =>
			selector.InvokeOrDefault(new DatePropertyDescriptor<T>().Name(field));

		public IProperty Scalar(Expression<Func<T, IEnumerable<DateTime>>> field, Func<DatePropertyDescriptor<T>, IDateProperty> selector = null) =>
			selector.InvokeOrDefault(new DatePropertyDescriptor<T>().Name(field));

		public IProperty Scalar(Expression<Func<T, IEnumerable<DateTime?>>> field, Func<DatePropertyDescriptor<T>, IDateProperty> selector = null) =>
			selector.InvokeOrDefault(new DatePropertyDescriptor<T>().Name(field));

		public IProperty Scalar(Expression<Func<T, DateTimeOffset>> field, Func<DatePropertyDescriptor<T>, IDateProperty> selector = null) =>
			selector.InvokeOrDefault(new DatePropertyDescriptor<T>().Name(field));

		public IProperty Scalar(Expression<Func<T, DateTimeOffset?>> field, Func<DatePropertyDescriptor<T>, IDateProperty> selector = null) =>
			selector.InvokeOrDefault(new DatePropertyDescriptor<T>().Name(field));

		public IProperty Scalar(Expression<Func<T, IEnumerable<DateTimeOffset>>> field, Func<DatePropertyDescriptor<T>, IDateProperty> selector = null
		) =>
			selector.InvokeOrDefault(new DatePropertyDescriptor<T>().Name(field));

		public IProperty Scalar(Expression<Func<T, IEnumerable<DateTimeOffset?>>> field,
			Func<DatePropertyDescriptor<T>, IDateProperty> selector = null
		) =>
			selector.InvokeOrDefault(new DatePropertyDescriptor<T>().Name(field));

		public IProperty Scalar(Expression<Func<T, bool>> field, Func<BooleanPropertyDescriptor<T>, IBooleanProperty> selector = null) =>
			selector.InvokeOrDefault(new BooleanPropertyDescriptor<T>().Name(field));

		public IProperty Scalar(Expression<Func<T, bool?>> field, Func<BooleanPropertyDescriptor<T>, IBooleanProperty> selector = null) =>
			selector.InvokeOrDefault(new BooleanPropertyDescriptor<T>().Name(field));

		public IProperty Scalar(Expression<Func<T, IEnumerable<bool>>> field, Func<BooleanPropertyDescriptor<T>, IBooleanProperty> selector = null) =>
			selector.InvokeOrDefault(new BooleanPropertyDescriptor<T>().Name(field));

		public IProperty Scalar(Expression<Func<T, IEnumerable<bool?>>> field, Func<BooleanPropertyDescriptor<T>, IBooleanProperty> selector = null
		) =>
			selector.InvokeOrDefault(new BooleanPropertyDescriptor<T>().Name(field));

		public IProperty Scalar(Expression<Func<T, char>> field, Func<KeywordPropertyDescriptor<T>, IKeywordProperty> selector = null) =>
			selector.InvokeOrDefault(new KeywordPropertyDescriptor<T>().Name(field));

		public IProperty Scalar(Expression<Func<T, char?>> field, Func<KeywordPropertyDescriptor<T>, IKeywordProperty> selector = null) =>
			selector.InvokeOrDefault(new KeywordPropertyDescriptor<T>().Name(field));

		public IProperty Scalar(Expression<Func<T, IEnumerable<char>>> field, Func<KeywordPropertyDescriptor<T>, IKeywordProperty> selector = null) =>
			selector.InvokeOrDefault(new KeywordPropertyDescriptor<T>().Name(field));

		public IProperty Scalar(Expression<Func<T, IEnumerable<char?>>> field, Func<KeywordPropertyDescriptor<T>, IKeywordProperty> selector = null
		) =>
			selector.InvokeOrDefault(new KeywordPropertyDescriptor<T>().Name(field));

		public IProperty Scalar(Expression<Func<T, Guid>> field, Func<KeywordPropertyDescriptor<T>, IKeywordProperty> selector = null) =>
			selector.InvokeOrDefault(new KeywordPropertyDescriptor<T>().Name(field));

		public IProperty Scalar(Expression<Func<T, Guid?>> field, Func<KeywordPropertyDescriptor<T>, IKeywordProperty> selector = null) =>
			selector.InvokeOrDefault(new KeywordPropertyDescriptor<T>().Name(field));

		public IProperty Scalar(Expression<Func<T, IEnumerable<Guid>>> field, Func<KeywordPropertyDescriptor<T>, IKeywordProperty> selector = null) =>
			selector.InvokeOrDefault(new KeywordPropertyDescriptor<T>().Name(field));

		public IProperty Scalar(Expression<Func<T, IEnumerable<Guid?>>> field, Func<KeywordPropertyDescriptor<T>, IKeywordProperty> selector = null
		) =>
			selector.InvokeOrDefault(new KeywordPropertyDescriptor<T>().Name(field));

		public IProperty Scalar(Expression<Func<T, string>> field, Func<TextPropertyDescriptor<T>, ITextProperty> selector = null) =>
			selector.InvokeOrDefault(new TextPropertyDescriptor<T>().Name(field));

		public IProperty Scalar(Expression<Func<T, IEnumerable<string>>> field, Func<TextPropertyDescriptor<T>, ITextProperty> selector = null) =>
			selector.InvokeOrDefault(new TextPropertyDescriptor<T>().Name(field));

		public IProperty Scalar(Expression<Func<T, DateRange>> field, Func<DateRangePropertyDescriptor<T>, IDateRangeProperty> selector = null) =>
			selector.InvokeOrDefault(new DateRangePropertyDescriptor<T>().Name(field));

		public IProperty Scalar(Expression<Func<T, DoubleRange>> field, Func<DoubleRangePropertyDescriptor<T>, IDoubleRangeProperty> selector = null
		) =>
			selector.InvokeOrDefault(new DoubleRangePropertyDescriptor<T>().Name(field));

		public IProperty Scalar(Expression<Func<T, LongRange>> field, Func<LongRangePropertyDescriptor<T>, ILongRangeProperty> selector = null) =>
			selector.InvokeOrDefault(new LongRangePropertyDescriptor<T>().Name(field));

		public IProperty Scalar(Expression<Func<T, IntegerRange>> field,
			Func<IntegerRangePropertyDescriptor<T>, IIntegerRangeProperty> selector = null
		) =>
			selector.InvokeOrDefault(new IntegerRangePropertyDescriptor<T>().Name(field));

		public IProperty Scalar(Expression<Func<T, FloatRange>> field, Func<FloatRangePropertyDescriptor<T>, IFloatRangeProperty> selector = null) =>
			selector.InvokeOrDefault(new FloatRangePropertyDescriptor<T>().Name(field));

		public IProperty Scalar(Expression<Func<T, IpAddressRange>> field, Func<IpRangePropertyDescriptor<T>, IIpRangeProperty> selector = null) =>
			selector.InvokeOrDefault(new IpRangePropertyDescriptor<T>().Name(field));
#pragma warning restore CS3001 // Argument type is not CLS-compliant
	}
}
