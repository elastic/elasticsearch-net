// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Elastic.Clients.Elasticsearch.Mapping;

public readonly partial struct PropertiesDescriptor<TDocument>
{
	// SCALAR OVERLOADS
	// Scalar are manually added to a partial class which seems reasonable.

	// int
	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, int>> propertyName)
	{
		_items.Add(propertyName, IntegerNumberPropertyDescriptor<TDocument>.Build(null));
		return this;
	}

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, int>> propertyName, Action<IntegerNumberPropertyDescriptor<TDocument>> configure)
	{
		_items.Add(propertyName, IntegerNumberPropertyDescriptor<TDocument>.Build(configure));
		return this;
	}

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, IEnumerable<int>>> propertyName, Action<IntegerNumberPropertyDescriptor<TDocument>> configure)
	{
		_items.Add(propertyName, IntegerNumberPropertyDescriptor<TDocument>.Build(configure));
		return this;
	}

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, int?>> propertyName, Action<IntegerNumberPropertyDescriptor<TDocument>> configure)
	{
		_items.Add(propertyName, IntegerNumberPropertyDescriptor<TDocument>.Build(configure));
		return this;
	}

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, IEnumerable<int?>>> propertyName, Action<IntegerNumberPropertyDescriptor<TDocument>> configure)
	{
		_items.Add(propertyName, IntegerNumberPropertyDescriptor<TDocument>.Build(configure));
		return this;
	}

	// float
	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, float>> propertyName)
	{
		_items.Add(propertyName, FloatNumberPropertyDescriptor<TDocument>.Build(null));
		return this;
	}

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, float>> propertyName, Action<FloatNumberPropertyDescriptor<TDocument>> configure)
	{
		_items.Add(propertyName, FloatNumberPropertyDescriptor<TDocument>.Build(configure));
		return this;
	}

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, IEnumerable<float>>> propertyName, Action<FloatNumberPropertyDescriptor<TDocument>> configure)
	{
		_items.Add(propertyName, FloatNumberPropertyDescriptor<TDocument>.Build(configure));
		return this;
	}

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, float?>> propertyName, Action<FloatNumberPropertyDescriptor<TDocument>> configure)
	{
		_items.Add(propertyName, FloatNumberPropertyDescriptor<TDocument>.Build(configure));
		return this;
	}

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, IEnumerable<float?>>> propertyName, Action<FloatNumberPropertyDescriptor<TDocument>> configure)
	{
		_items.Add(propertyName, FloatNumberPropertyDescriptor<TDocument>.Build(configure));
		return this;
	}

	// byte
	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, byte>> propertyName)
	{
		_items.Add(propertyName, ByteNumberPropertyDescriptor<TDocument>.Build(null));
		return this;
	}

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, byte>> propertyName, Action<ByteNumberPropertyDescriptor<TDocument>> configure)
	{
		_items.Add(propertyName, ByteNumberPropertyDescriptor<TDocument>.Build(configure));
		return this;
	}

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, IEnumerable<byte>>> propertyName, Action<ByteNumberPropertyDescriptor<TDocument>> configure)
	{
		_items.Add(propertyName, ByteNumberPropertyDescriptor<TDocument>.Build(configure));
		return this;
	}

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, byte?>> propertyName, Action<ByteNumberPropertyDescriptor<TDocument>> configure)
	{
		_items.Add(propertyName, ByteNumberPropertyDescriptor<TDocument>.Build(configure));
		return this;
	}

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, IEnumerable<byte?>>> propertyName, Action<ByteNumberPropertyDescriptor<TDocument>> configure)
	{
		_items.Add(propertyName, ByteNumberPropertyDescriptor<TDocument>.Build(configure));
		return this;
	}

	// short
	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, short>> propertyName)
	{
		_items.Add(propertyName, ShortNumberPropertyDescriptor<TDocument>.Build(null));
		return this;
	}

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, short>> propertyName, Action<ShortNumberPropertyDescriptor<TDocument>> configure)
	{
		_items.Add(propertyName, ShortNumberPropertyDescriptor<TDocument>.Build(configure));
		return this;
	}

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, IEnumerable<short>>> propertyName, Action<ShortNumberPropertyDescriptor<TDocument>> configure)
	{
		_items.Add(propertyName, ShortNumberPropertyDescriptor<TDocument>.Build(configure));
		return this;
	}

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, short?>> propertyName, Action<ShortNumberPropertyDescriptor<TDocument>> configure)
	{
		_items.Add(propertyName, ShortNumberPropertyDescriptor<TDocument>.Build(configure));
		return this;
	}

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, IEnumerable<short?>>> propertyName, Action<ShortNumberPropertyDescriptor<TDocument>> configure)
	{
		_items.Add(propertyName, ShortNumberPropertyDescriptor<TDocument>.Build(configure));
		return this;
	}

	// long
	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, long>> propertyName)
	{
		_items.Add(propertyName, LongNumberPropertyDescriptor<TDocument>.Build(null));
		return this;
	}

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, long>> propertyName, Action<LongNumberPropertyDescriptor<TDocument>> configure)
	{
		_items.Add(propertyName, LongNumberPropertyDescriptor<TDocument>.Build(configure));
		return this;
	}

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, IEnumerable<long>>> propertyName, Action<LongNumberPropertyDescriptor<TDocument>> configure)
	{
		_items.Add(propertyName, LongNumberPropertyDescriptor<TDocument>.Build(configure));
		return this;
	}

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, long?>> propertyName, Action<LongNumberPropertyDescriptor<TDocument>> configure)
	{
		_items.Add(propertyName, LongNumberPropertyDescriptor<TDocument>.Build(configure));
		return this;
	}

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, IEnumerable<long?>>> propertyName, Action<LongNumberPropertyDescriptor<TDocument>> configure)
	{
		_items.Add(propertyName, LongNumberPropertyDescriptor<TDocument>.Build(configure));
		return this;
	}

	// decimal
	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, decimal>> propertyName)
	{
		_items.Add(propertyName, DoubleNumberPropertyDescriptor<TDocument>.Build(null));
		return this;
	}

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, decimal>> propertyName, Action<DoubleNumberPropertyDescriptor<TDocument>> configure)
	{
		_items.Add(propertyName, DoubleNumberPropertyDescriptor<TDocument>.Build(configure));
		return this;
	}

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, IEnumerable<decimal>>> propertyName, Action<DoubleNumberPropertyDescriptor<TDocument>> configure)
	{
		_items.Add(propertyName, DoubleNumberPropertyDescriptor<TDocument>.Build(configure));
		return this;
	}

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, decimal?>> propertyName, Action<DoubleNumberPropertyDescriptor<TDocument>> configure)
	{
		_items.Add(propertyName, DoubleNumberPropertyDescriptor<TDocument>.Build(configure));
		return this;
	}

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, IEnumerable<decimal?>>> propertyName, Action<DoubleNumberPropertyDescriptor<TDocument>> configure)
	{
		_items.Add(propertyName, DoubleNumberPropertyDescriptor<TDocument>.Build(configure));
		return this;
	}

	// double
	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, double>> propertyName)
	{
		_items.Add(propertyName, DoubleNumberPropertyDescriptor<TDocument>.Build(null));
		return this;
	}

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, double>> propertyName, Action<DoubleNumberPropertyDescriptor<TDocument>> configure)
	{
		_items.Add(propertyName, DoubleNumberPropertyDescriptor<TDocument>.Build(configure));
		return this;
	}

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, IEnumerable<double>>> propertyName, Action<DoubleNumberPropertyDescriptor<TDocument>> configure)
	{
		_items.Add(propertyName, DoubleNumberPropertyDescriptor<TDocument>.Build(configure));
		return this;
	}

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, double?>> propertyName, Action<DoubleNumberPropertyDescriptor<TDocument>> configure)
	{
		_items.Add(propertyName, DoubleNumberPropertyDescriptor<TDocument>.Build(configure));
		return this;
	}

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, IEnumerable<double?>>> propertyName, Action<DoubleNumberPropertyDescriptor<TDocument>> configure)
	{
		_items.Add(propertyName, DoubleNumberPropertyDescriptor<TDocument>.Build(configure));
		return this;
	}

	// date time
	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, DateTime>> propertyName)
	{
		_items.Add(propertyName, DatePropertyDescriptor<TDocument>.Build(null));
		return this;
	}

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, DateTime>> propertyName, Action<DatePropertyDescriptor<TDocument>> configure)
	{
		_items.Add(propertyName, DatePropertyDescriptor<TDocument>.Build(configure));
		return this;
	}

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, IEnumerable<DateTime>>> propertyName, Action<DatePropertyDescriptor<TDocument>> configure)
	{
		_items.Add(propertyName, DatePropertyDescriptor<TDocument>.Build(configure));
		return this;
	}

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, DateTime?>> propertyName, Action<DatePropertyDescriptor<TDocument>> configure)
	{
		_items.Add(propertyName, DatePropertyDescriptor<TDocument>.Build(configure));
		return this;
	}

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, IEnumerable<DateTime?>>> propertyName, Action<DatePropertyDescriptor<TDocument>> configure)
	{
		_items.Add(propertyName, DatePropertyDescriptor<TDocument>.Build(configure));
		return this;
	}

	// date time offset
	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, DateTimeOffset>> propertyName)
	{
		_items.Add(propertyName, DatePropertyDescriptor<TDocument>.Build(null));
		return this;
	}

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, DateTimeOffset>> propertyName, Action<DatePropertyDescriptor<TDocument>> configure)
	{
		_items.Add(propertyName, DatePropertyDescriptor<TDocument>.Build(configure));
		return this;
	}

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, IEnumerable<DateTimeOffset>>> propertyName, Action<DatePropertyDescriptor<TDocument>> configure)
	{
		_items.Add(propertyName, DatePropertyDescriptor<TDocument>.Build(configure));
		return this;
	}

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, DateTimeOffset?>> propertyName, Action<DatePropertyDescriptor<TDocument>> configure)
	{
		_items.Add(propertyName, DatePropertyDescriptor<TDocument>.Build(configure));
		return this;
	}

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, IEnumerable<DateTimeOffset?>>> propertyName, Action<DatePropertyDescriptor<TDocument>> configure)
	{
		_items.Add(propertyName, DatePropertyDescriptor<TDocument>.Build(configure));
		return this;
	}

	// bool
	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, bool>> propertyName)
	{
		_items.Add(propertyName, BooleanPropertyDescriptor<TDocument>.Build(null));
		return this;
	}

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, bool>> propertyName, Action<BooleanPropertyDescriptor<TDocument>> configure)
	{
		_items.Add(propertyName, BooleanPropertyDescriptor<TDocument>.Build(configure));
		return this;
	}

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, IEnumerable<bool>>> propertyName, Action<BooleanPropertyDescriptor<TDocument>> configure)
	{
		_items.Add(propertyName, BooleanPropertyDescriptor<TDocument>.Build(configure));
		return this;
	}

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, bool?>> propertyName, Action<BooleanPropertyDescriptor<TDocument>> configure)
	{
		_items.Add(propertyName, BooleanPropertyDescriptor<TDocument>.Build(configure));
		return this;
	}

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, IEnumerable<bool?>>> propertyName, Action<BooleanPropertyDescriptor<TDocument>> configure)
	{
		_items.Add(propertyName, BooleanPropertyDescriptor<TDocument>.Build(configure));
		return this;
	}

	// char
	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, char>> propertyName)
	{
		_items.Add(propertyName, KeywordPropertyDescriptor<TDocument>.Build(null));
		return this;
	}

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, char>> propertyName, Action<KeywordPropertyDescriptor<TDocument>> configure)
	{
		_items.Add(propertyName, KeywordPropertyDescriptor<TDocument>.Build(configure));
		return this;
	}

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, IEnumerable<char>>> propertyName, Action<KeywordPropertyDescriptor<TDocument>> configure)
	{
		_items.Add(propertyName, KeywordPropertyDescriptor<TDocument>.Build(configure));
		return this;
	}

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, char?>> propertyName, Action<KeywordPropertyDescriptor<TDocument>> configure)
	{
		_items.Add(propertyName, KeywordPropertyDescriptor<TDocument>.Build(configure));
		return this;
	}

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, IEnumerable<char?>>> propertyName, Action<KeywordPropertyDescriptor<TDocument>> configure)
	{
		_items.Add(propertyName, KeywordPropertyDescriptor<TDocument>.Build(configure));
		return this;
	}

	// GUID
	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, Guid>> propertyName)
	{
		_items.Add(propertyName, KeywordPropertyDescriptor<TDocument>.Build(null));
		return this;
	}

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, Guid>> propertyName, Action<KeywordPropertyDescriptor<TDocument>> configure)
	{
		_items.Add(propertyName, KeywordPropertyDescriptor<TDocument>.Build(configure));
		return this;
	}

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, IEnumerable<Guid>>> propertyName, Action<KeywordPropertyDescriptor<TDocument>> configure)
	{
		_items.Add(propertyName, KeywordPropertyDescriptor<TDocument>.Build(configure));
		return this;
	}

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, Guid?>> propertyName, Action<KeywordPropertyDescriptor<TDocument>> configure)

	{
		_items.Add(propertyName, KeywordPropertyDescriptor<TDocument>.Build(configure));
		return this;
	}

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, IEnumerable<Guid?>>> propertyName, Action<KeywordPropertyDescriptor<TDocument>> configure)
	{
		_items.Add(propertyName, KeywordPropertyDescriptor<TDocument>.Build(configure));
		return this;
	}

	// string

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, string>> propertyName)
	{
		_items.Add(propertyName, TextPropertyDescriptor<TDocument>.Build(null));
		return this;
	}

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, string>> propertyName, Action<TextPropertyDescriptor<TDocument>> configure)
	{
		_items.Add(propertyName, TextPropertyDescriptor<TDocument>.Build(configure));
		return this;
	}

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, IEnumerable<string>>> propertyName, Action<TextPropertyDescriptor<TDocument>> configure)
	{
		_items.Add(propertyName, TextPropertyDescriptor<TDocument>.Build(configure));
		return this;
	}
}
