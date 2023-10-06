// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Elastic.Clients.Elasticsearch.Fluent;

namespace Elastic.Clients.Elasticsearch.Mapping;

public sealed partial class PropertiesDescriptor<TDocument>
		: IsADictionaryDescriptor<PropertiesDescriptor<TDocument>, Properties, PropertyName, IProperty>
{
	// SCALAR OVERLOADS
	// Scalar are manually added to a partial class which seems reasonable.

	// int
	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, int>> propertyName) =>
		AssignVariant<IntegerNumberPropertyDescriptor<TDocument>, IntegerNumberProperty>(propertyName, null);

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, int>> propertyName, Action<IntegerNumberPropertyDescriptor<TDocument>> configure) =>
		AssignVariant<IntegerNumberPropertyDescriptor<TDocument>, IntegerNumberProperty>(propertyName, configure);

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, IEnumerable<int>>> propertyName, Action<IntegerNumberPropertyDescriptor<TDocument>> configure) =>
		AssignVariant<IntegerNumberPropertyDescriptor<TDocument>, IntegerNumberProperty>(propertyName, configure);

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, int?>> propertyName, Action<IntegerNumberPropertyDescriptor<TDocument>> configure) =>
		AssignVariant<IntegerNumberPropertyDescriptor<TDocument>, IntegerNumberProperty>(propertyName, configure);

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, IEnumerable<int?>>> propertyName, Action<IntegerNumberPropertyDescriptor<TDocument>> configure) =>
		AssignVariant<IntegerNumberPropertyDescriptor<TDocument>, IntegerNumberProperty>(propertyName, configure);

	// float
	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, float>> propertyName) =>
		AssignVariant<FloatNumberPropertyDescriptor<TDocument>, FloatNumberProperty>(propertyName, null);

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, float>> propertyName, Action<FloatNumberPropertyDescriptor<TDocument>> configure) =>
		AssignVariant<FloatNumberPropertyDescriptor<TDocument>, FloatNumberProperty>(propertyName, configure);

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, IEnumerable<float>>> propertyName, Action<FloatNumberPropertyDescriptor<TDocument>> configure) =>
		AssignVariant<FloatNumberPropertyDescriptor<TDocument>, FloatNumberProperty>(propertyName, configure);

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, float?>> propertyName, Action<FloatNumberPropertyDescriptor<TDocument>> configure) =>
		AssignVariant<FloatNumberPropertyDescriptor<TDocument>, FloatNumberProperty>(propertyName, configure);

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, IEnumerable<float?>>> propertyName, Action<FloatNumberPropertyDescriptor<TDocument>> configure) =>
		AssignVariant<FloatNumberPropertyDescriptor<TDocument>, FloatNumberProperty>(propertyName, configure);

	// byte
	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, byte>> propertyName) =>
		AssignVariant<ByteNumberPropertyDescriptor<TDocument>, ByteNumberProperty>(propertyName, null);

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, byte>> propertyName, Action<ByteNumberPropertyDescriptor<TDocument>> configure) =>
		AssignVariant<ByteNumberPropertyDescriptor<TDocument>, ByteNumberProperty>(propertyName, configure);

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, IEnumerable<byte>>> propertyName, Action<ByteNumberPropertyDescriptor<TDocument>> configure) =>
		AssignVariant<ByteNumberPropertyDescriptor<TDocument>, ByteNumberProperty>(propertyName, configure);

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, byte?>> propertyName, Action<ByteNumberPropertyDescriptor<TDocument>> configure) =>
		AssignVariant<ByteNumberPropertyDescriptor<TDocument>, ByteNumberProperty>(propertyName, configure);

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, IEnumerable<byte?>>> propertyName, Action<ByteNumberPropertyDescriptor<TDocument>> configure) =>
		AssignVariant<ByteNumberPropertyDescriptor<TDocument>, ByteNumberProperty>(propertyName, configure);

	// short
	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, short>> propertyName) =>
		AssignVariant<ShortNumberPropertyDescriptor<TDocument>, ShortNumberProperty>(propertyName, null);

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, short>> propertyName, Action<ShortNumberPropertyDescriptor<TDocument>> configure) =>
		AssignVariant<ShortNumberPropertyDescriptor<TDocument>, ShortNumberProperty>(propertyName, configure);

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, IEnumerable<short>>> propertyName, Action<ShortNumberPropertyDescriptor<TDocument>> configure) =>
		AssignVariant<ShortNumberPropertyDescriptor<TDocument>, ShortNumberProperty>(propertyName, configure);

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, short?>> propertyName, Action<ShortNumberPropertyDescriptor<TDocument>> configure) =>
		AssignVariant<ShortNumberPropertyDescriptor<TDocument>, ShortNumberProperty>(propertyName, configure);

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, IEnumerable<short?>>> propertyName, Action<ShortNumberPropertyDescriptor<TDocument>> configure) =>
		AssignVariant<ShortNumberPropertyDescriptor<TDocument>, ShortNumberProperty>(propertyName, configure);

	// long
	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, long>> propertyName) =>
		AssignVariant<LongNumberPropertyDescriptor<TDocument>, LongNumberProperty>(propertyName, null);

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, long>> propertyName, Action<LongNumberPropertyDescriptor<TDocument>> configure) =>
		AssignVariant<LongNumberPropertyDescriptor<TDocument>, LongNumberProperty>(propertyName, configure);

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, IEnumerable<long>>> propertyName, Action<LongNumberPropertyDescriptor<TDocument>> configure) =>
		AssignVariant<LongNumberPropertyDescriptor<TDocument>, LongNumberProperty>(propertyName, configure);

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, long?>> propertyName, Action<LongNumberPropertyDescriptor<TDocument>> configure) =>
		AssignVariant<LongNumberPropertyDescriptor<TDocument>, LongNumberProperty>(propertyName, configure);

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, IEnumerable<long?>>> propertyName, Action<LongNumberPropertyDescriptor<TDocument>> configure) =>
		AssignVariant<LongNumberPropertyDescriptor<TDocument>, LongNumberProperty>(propertyName, configure);

	// decimal
	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, decimal>> propertyName) =>
		AssignVariant<DoubleNumberPropertyDescriptor<TDocument>, DoubleNumberProperty>(propertyName, null);

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, decimal>> propertyName, Action<DoubleNumberPropertyDescriptor<TDocument>> configure) =>
		AssignVariant<DoubleNumberPropertyDescriptor<TDocument>, DoubleNumberProperty>(propertyName, configure);

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, IEnumerable<decimal>>> propertyName, Action<DoubleNumberPropertyDescriptor<TDocument>> configure) =>
		AssignVariant<DoubleNumberPropertyDescriptor<TDocument>, DoubleNumberProperty>(propertyName, configure);

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, decimal?>> propertyName, Action<DoubleNumberPropertyDescriptor<TDocument>> configure) =>
		AssignVariant<DoubleNumberPropertyDescriptor<TDocument>, DoubleNumberProperty>(propertyName, configure);

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, IEnumerable<decimal?>>> propertyName, Action<DoubleNumberPropertyDescriptor<TDocument>> configure) =>
		AssignVariant<DoubleNumberPropertyDescriptor<TDocument>, DoubleNumberProperty>(propertyName, configure);

	// double
	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, double>> propertyName) =>
		AssignVariant<DoubleNumberPropertyDescriptor<TDocument>, DoubleNumberProperty>(propertyName, null);

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, double>> propertyName, Action<DoubleNumberPropertyDescriptor<TDocument>> configure) =>
		AssignVariant<DoubleNumberPropertyDescriptor<TDocument>, DoubleNumberProperty>(propertyName, configure);

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, IEnumerable<double>>> propertyName, Action<DoubleNumberPropertyDescriptor<TDocument>> configure) =>
		AssignVariant<DoubleNumberPropertyDescriptor<TDocument>, DoubleNumberProperty>(propertyName, configure);

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, double?>> propertyName, Action<DoubleNumberPropertyDescriptor<TDocument>> configure) =>
		AssignVariant<DoubleNumberPropertyDescriptor<TDocument>, DoubleNumberProperty>(propertyName, configure);

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, IEnumerable<double?>>> propertyName, Action<DoubleNumberPropertyDescriptor<TDocument>> configure) =>
		AssignVariant<DoubleNumberPropertyDescriptor<TDocument>, DoubleNumberProperty>(propertyName, configure);

	// date time
	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, DateTime>> propertyName) =>
		AssignVariant<DatePropertyDescriptor<TDocument>, DateProperty>(propertyName, null);

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, DateTime>> propertyName, Action<DatePropertyDescriptor<TDocument>> configure) =>
		AssignVariant<DatePropertyDescriptor<TDocument>, DateProperty>(propertyName, configure);

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, IEnumerable<DateTime>>> propertyName, Action<DatePropertyDescriptor<TDocument>> configure) =>
		AssignVariant<DatePropertyDescriptor<TDocument>, DateProperty>(propertyName, configure);

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, DateTime?>> propertyName, Action<DatePropertyDescriptor<TDocument>> configure) =>
		AssignVariant<DatePropertyDescriptor<TDocument>, DateProperty>(propertyName, configure);

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, IEnumerable<DateTime?>>> propertyName, Action<DatePropertyDescriptor<TDocument>> configure) =>
		AssignVariant<DatePropertyDescriptor<TDocument>, DateProperty>(propertyName, configure);

	// date time offset
	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, DateTimeOffset>> propertyName) =>
		AssignVariant<DatePropertyDescriptor<TDocument>, DateProperty>(propertyName, null);

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, DateTimeOffset>> propertyName, Action<DatePropertyDescriptor<TDocument>> configure) =>
		AssignVariant<DatePropertyDescriptor<TDocument>, DateProperty>(propertyName, configure);

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, IEnumerable<DateTimeOffset>>> propertyName, Action<DatePropertyDescriptor<TDocument>> configure) =>
		AssignVariant<DatePropertyDescriptor<TDocument>, DateProperty>(propertyName, configure);

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, DateTimeOffset?>> propertyName, Action<DatePropertyDescriptor<TDocument>> configure) =>
		AssignVariant<DatePropertyDescriptor<TDocument>, DateProperty>(propertyName, configure);

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, IEnumerable<DateTimeOffset?>>> propertyName, Action<DatePropertyDescriptor<TDocument>> configure) =>
		AssignVariant<DatePropertyDescriptor<TDocument>, DateProperty>(propertyName, configure);

	// bool
	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, bool>> propertyName) =>
		AssignVariant<BooleanPropertyDescriptor<TDocument>, BooleanProperty>(propertyName, null);

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, bool>> propertyName, Action<BooleanPropertyDescriptor<TDocument>> configure) =>
		AssignVariant<BooleanPropertyDescriptor<TDocument>, BooleanProperty>(propertyName, configure);

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, IEnumerable<bool>>> propertyName, Action<BooleanPropertyDescriptor<TDocument>> configure) =>
		AssignVariant<BooleanPropertyDescriptor<TDocument>, BooleanProperty>(propertyName, configure);

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, bool?>> propertyName, Action<BooleanPropertyDescriptor<TDocument>> configure) =>
		AssignVariant<BooleanPropertyDescriptor<TDocument>, BooleanProperty>(propertyName, configure);

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, IEnumerable<bool?>>> propertyName, Action<BooleanPropertyDescriptor<TDocument>> configure) =>
		AssignVariant<BooleanPropertyDescriptor<TDocument>, BooleanProperty>(propertyName, configure);

	// char
	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, char>> propertyName) =>
		AssignVariant<KeywordPropertyDescriptor<TDocument>, KeywordProperty>(propertyName, null);

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, char>> propertyName, Action<KeywordPropertyDescriptor<TDocument>> configure) =>
		AssignVariant<KeywordPropertyDescriptor<TDocument>, KeywordProperty>(propertyName, configure);

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, IEnumerable<char>>> propertyName, Action<KeywordPropertyDescriptor<TDocument>> configure) =>
		AssignVariant<KeywordPropertyDescriptor<TDocument>, KeywordProperty>(propertyName, configure);

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, char?>> propertyName, Action<KeywordPropertyDescriptor<TDocument>> configure) =>
		AssignVariant<KeywordPropertyDescriptor<TDocument>, KeywordProperty>(propertyName, configure);

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, IEnumerable<char?>>> propertyName, Action<KeywordPropertyDescriptor<TDocument>> configure) =>
		AssignVariant<KeywordPropertyDescriptor<TDocument>, KeywordProperty>(propertyName, configure);

	// GUID
	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, Guid>> propertyName) =>
		AssignVariant<KeywordPropertyDescriptor<TDocument>, KeywordProperty>(propertyName, null);

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, Guid>> propertyName, Action<KeywordPropertyDescriptor<TDocument>> configure) =>
		AssignVariant<KeywordPropertyDescriptor<TDocument>, KeywordProperty>(propertyName, configure);

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, IEnumerable<Guid>>> propertyName, Action<KeywordPropertyDescriptor<TDocument>> configure) =>
		AssignVariant<KeywordPropertyDescriptor<TDocument>, KeywordProperty>(propertyName, configure);

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, Guid?>> propertyName, Action<KeywordPropertyDescriptor<TDocument>> configure) =>
		AssignVariant<KeywordPropertyDescriptor<TDocument>, KeywordProperty>(propertyName, configure);

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, IEnumerable<Guid?>>> propertyName, Action<KeywordPropertyDescriptor<TDocument>> configure) =>
		AssignVariant<KeywordPropertyDescriptor<TDocument>, KeywordProperty>(propertyName, configure);

	// string

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, string>> propertyName) =>
		AssignVariant<TextPropertyDescriptor<TDocument>, TextProperty>(propertyName, null);

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, string>> propertyName, Action<TextPropertyDescriptor<TDocument>> configure) =>
		AssignVariant<TextPropertyDescriptor<TDocument>, TextProperty>(propertyName, configure);

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, IEnumerable<string>>> propertyName, Action<TextPropertyDescriptor<TDocument>> configure) =>
		AssignVariant<TextPropertyDescriptor<TDocument>, TextProperty>(propertyName, configure);

	// Skipping these for now
	//public PropertiesDescriptor<TDocument> Boolean<TValue>(Expression<Func<TDocument, TValue>> fieldName) =>
	//	AssignVariant<BooleanPropertyDescriptor<TDocument>, BooleanProperty>(fieldName, null);

	//public PropertiesDescriptor<TDocument> Boolean<TValue>(Expression<Func<TDocument, TValue>> fieldName, Action<BooleanPropertyDescriptor<TDocument>> configure) =>
	//	AssignVariant<BooleanPropertyDescriptor<TDocument>, BooleanProperty>(fieldName, configure);

	// This will remain non-code-generated
	protected override PropertiesDescriptor<TDocument> AssignVariant(PropertyName name, IProperty type)
	{
		type.ThrowIfNull(nameof(type));

		if (name.IsConditionless())
			throw new ArgumentException($"Could not get property name for {type.GetType().Name} mapping.");

		return Assign(name, type);
	}
}
