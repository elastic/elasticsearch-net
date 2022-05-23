// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;

namespace Elastic.Clients.Elasticsearch;

public sealed class SortDescriptor<TDocument> : PromiseDescriptor<SortDescriptor<TDocument>, SortCollection>, ISelfSerializable
{
	public SortDescriptor() : base(new SortCollection()) { }

	public SortDescriptor(Action<SortDescriptor<TDocument>> configure) : base(new SortCollection()) => configure(this);

	private List<Action<FieldSortDescriptor<TDocument>>> _fieldSortDescriptorActions;
	private List<Action<ScriptSortDescriptor<TDocument>>> _scriptSortDescriptorActions;
	private List<Action<GeoDistanceSortDescriptor<TDocument>>> _geoDistanceSortDescriptorActions;
	private readonly List<byte> _serializationOrderTracker = new();

	public SortDescriptor<TDocument> Ascending(Expression<Func<TDocument, object>> objectPath) =>
		Assign(objectPath, (a, v) => a.Add(new FieldSort(v) { Order = SortOrder.Asc }), a => a._serializationOrderTracker.Add(0));

	public SortDescriptor<TDocument> Descending(Expression<Func<TDocument, object>> objectPath) =>
		Assign(objectPath, (a, v) => a.Add(new FieldSort(v) { Order = SortOrder.Desc }), a => a._serializationOrderTracker.Add(0));

	public SortDescriptor<TDocument> Ascending(Field field) => Assign(field, (a, v) => { a.Add(new FieldSort(v) { Order = SortOrder.Asc }); }, a => a._serializationOrderTracker.Add(0));

	public SortDescriptor<TDocument> Descending(Field field) => Assign(field, (a, v) => a.Add(new FieldSort(v) { Order = SortOrder.Desc }), a => a._serializationOrderTracker.Add(0));

	public SortDescriptor<TDocument> Ascending(SortSpecialField field) =>
		Assign(field == SortSpecialField.Score ? "_score" : field == SortSpecialField.DocumentIndexOrder ? "_doc" : "_shard_doc", (a, v) => a.Add(new FieldSort(v) { Order = SortOrder.Asc }), a => a._serializationOrderTracker.Add(0));

	public SortDescriptor<TDocument> Descending(SortSpecialField field) =>
		Assign(field == SortSpecialField.Score ? "_score" : field == SortSpecialField.DocumentIndexOrder ? "_doc" : "_shard_doc", (a, v) => a.Add(new FieldSort(v) { Order = SortOrder.Desc }), a => a._serializationOrderTracker.Add(0));

	public SortDescriptor<TDocument> Field(Action<FieldSortDescriptor<TDocument>> configure)
	{
		//var descriptor = new FieldSortDescriptor();
		//configure?.Invoke(descriptor);

		if (_fieldSortDescriptorActions is null)
			_fieldSortDescriptorActions = new List<Action<FieldSortDescriptor<TDocument>>>();

		_fieldSortDescriptorActions.Add(configure);

		_serializationOrderTracker.Add(1);

		//return AddSort(descriptor.ToFieldSort());

		return this;
	}

	public SortDescriptor<TDocument> Field(Field field, SortOrder order) => AddSort(new FieldSort(field) { Order = order });

	public SortDescriptor<TDocument> Field<TValue>(Expression<Func<TDocument, TValue>> field, SortOrder order) =>
		AddSort(new FieldSort(field) { Order = order });

	public SortDescriptor<TDocument> Field<T, TValue>(Expression<Func<T, TValue>> field, SortOrder order) =>
	AddSort(new FieldSort(field) { Order = order });

	public SortDescriptor<TDocument> GeoDistance(Action<GeoDistanceSortDescriptor<TDocument>> configure)
	{
		if (_geoDistanceSortDescriptorActions is null)
			_geoDistanceSortDescriptorActions = new List<Action<GeoDistanceSortDescriptor<TDocument>>>();

		_geoDistanceSortDescriptorActions.Add(configure);

		_serializationOrderTracker.Add(2);

		return this;
	}

	public SortDescriptor<TDocument> Script(Action<ScriptSortDescriptor<TDocument>> configure)
	{
		if (_scriptSortDescriptorActions is null)
			_scriptSortDescriptorActions = new List<Action<ScriptSortDescriptor<TDocument>>>();

		_scriptSortDescriptorActions.Add(configure);

		_serializationOrderTracker.Add(3);

		return this;
	}

	private SortDescriptor<TDocument> AddSort(SortBase sort) => sort == null ? this : Assign(sort, (a, v) => a.Add(v), a => a._serializationOrderTracker.Add(0));

	void ISelfSerializable.Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartArray();

		var sortsIndex = 0;
		var fieldsDescriptorIndex = 0;
		var scriptsDescriptorIndex = 0;
		var geoDescriptorIndex = 0;

		foreach (var item in _serializationOrderTracker)
		{
			if (item == 0)
			{
				var sort = PromisedValue[sortsIndex++];

				if (sort is null)
					continue;

				if (sort is FieldSort fieldSort)
				{
					SortSerializationHelpers.WriteFieldSort(writer, fieldSort, options, settings);
					continue;
				}

				if (sort is ScriptSort scriptSort)
				{
					SortSerializationHelpers.WriteScriptSort(writer, scriptSort, options);
					continue;
				}

				if (sort is GeoDistanceSort geoDistanceSort)
				{
					SortSerializationHelpers.WriteGeoDistanceSort(writer, geoDistanceSort, options, settings);
					continue;
				}

				// TODO - Other types
				throw new NotImplementedException("The sort type is not currently supported in this release.");
			}
			else if (item == 1)
			{
				var action = _fieldSortDescriptorActions[fieldsDescriptorIndex++];
				var descriptor = new FieldSortDescriptor<TDocument>();
				action(descriptor);
				JsonSerializer.Serialize(writer, descriptor, options);
			}
			else if (item == 2)
			{
				var action = _geoDistanceSortDescriptorActions[geoDescriptorIndex++];
				var descriptor = new GeoDistanceSortDescriptor<TDocument>();
				action(descriptor);
				JsonSerializer.Serialize(writer, descriptor, options);
			}
			else if (item == 3)
			{
				var action = _scriptSortDescriptorActions[scriptsDescriptorIndex++];
				var descriptor = new ScriptSortDescriptor<TDocument>();
				action(descriptor);
				JsonSerializer.Serialize(writer, descriptor, options);
			}
		}

		writer.WriteEndArray();
	}
}

public sealed class SortDescriptor : PromiseDescriptor<SortDescriptor, SortCollection>, ISelfSerializable
{
	public SortDescriptor() : base(new SortCollection()) { }

	public SortDescriptor(Action<SortDescriptor> configure) : base(new SortCollection()) => configure(this);

	private List<Action<FieldSortDescriptor>> _fieldSortDescriptorActions;
	private List<Action<ScriptSortDescriptor>> _scriptSortDescriptorActions;
	private List<Action<GeoDistanceSortDescriptor>> _geoDistanceSortDescriptorActions;
	private readonly List<byte> _serializationOrderTracker = new();

	public SortDescriptor Ascending<TDocument>(Expression<Func<TDocument, object>> objectPath) =>
		Assign(objectPath, (a, v) => a.Add(new FieldSort(v) { Order = SortOrder.Asc }), a => a._serializationOrderTracker.Add(0));

	public SortDescriptor Descending<TDocument>(Expression<Func<TDocument, object>> objectPath) =>
		Assign(objectPath, (a, v) => a.Add(new FieldSort(v) { Order = SortOrder.Desc }), a => a._serializationOrderTracker.Add(0));

	public SortDescriptor Ascending(Field field) => Assign(field, (a, v) => {  a.Add(new FieldSort(v) { Order = SortOrder.Asc }); }, a => a._serializationOrderTracker.Add(0));

	public SortDescriptor Descending(Field field) => Assign(field, (a, v) => a.Add(new FieldSort(v) { Order = SortOrder.Desc }), a => a._serializationOrderTracker.Add(0));

	public SortDescriptor Ascending(SortSpecialField field) =>
		Assign(field == SortSpecialField.Score ? "_score" : field == SortSpecialField.DocumentIndexOrder ? "_doc" : "_shard_doc", (a, v) => a.Add(new FieldSort(v) { Order = SortOrder.Asc }), a => a._serializationOrderTracker.Add(0));

	public SortDescriptor Descending(SortSpecialField field) =>
		Assign(field == SortSpecialField.Score ? "_score" : field == SortSpecialField.DocumentIndexOrder ? "_doc" : "_shard_doc", (a, v) => a.Add(new FieldSort(v) { Order = SortOrder.Desc }), a => a._serializationOrderTracker.Add(0));

	public SortDescriptor Field(Action<FieldSortDescriptor> configure)
	{
		//var descriptor = new FieldSortDescriptor();
		//configure?.Invoke(descriptor);

		if (_fieldSortDescriptorActions is null)
			_fieldSortDescriptorActions = new List<Action<FieldSortDescriptor>>();

		_fieldSortDescriptorActions.Add(configure);

		_serializationOrderTracker.Add(1);

		//return AddSort(descriptor.ToFieldSort());

		return this;
	}

	public SortDescriptor Field(Field field, SortOrder order) => AddSort(new FieldSort(field) { Order = order });

	public SortDescriptor Field<TDocument, TValue>(Expression<Func<TDocument, TValue>> field, SortOrder order) =>
		AddSort(new FieldSort(field) { Order = order });

	public SortDescriptor GeoDistance(Action<GeoDistanceSortDescriptor> configure)
	{
		if (_geoDistanceSortDescriptorActions is null)
			_geoDistanceSortDescriptorActions = new List<Action<GeoDistanceSortDescriptor>>();

		_geoDistanceSortDescriptorActions.Add(configure);

		_serializationOrderTracker.Add(2);

		return this;
	}

	public SortDescriptor Script(Action<ScriptSortDescriptor> configure)
	{
		if (_scriptSortDescriptorActions is null)
			_scriptSortDescriptorActions = new List<Action<ScriptSortDescriptor>>();

		_scriptSortDescriptorActions.Add(configure);

		_serializationOrderTracker.Add(3);

		return this;
	}

	private SortDescriptor AddSort(SortBase sort) => sort == null ? this : Assign(sort, (a, v) => a.Add(v), a => a._serializationOrderTracker.Add(0));

	public void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartArray();

		var sortsIndex = 0;
		var fieldsDescriptorIndex = 0;
		var scriptsDescriptorIndex = 0;
		var geoDescriptorIndex = 0;

		foreach (var item in _serializationOrderTracker)
		{
			if (item == 0)
			{
				var sort = PromisedValue[sortsIndex++];

				if (sort is null)
					continue;

				if (sort is FieldSort fieldSort)
				{
					SortSerializationHelpers.WriteFieldSort(writer, fieldSort, options, settings);
					continue;
				}

				if (sort is ScriptSort scriptSort)
				{
					SortSerializationHelpers.WriteScriptSort(writer, scriptSort, options);
					continue;
				}

				if (sort is GeoDistanceSort geoDistanceSort)
				{
					SortSerializationHelpers.WriteGeoDistanceSort(writer, geoDistanceSort, options, settings);
					continue;
				}

				// TODO - Other types
				throw new NotImplementedException("The sort type is not currently supported in this release.");
			}
			else if (item == 1)
			{
				var action = _fieldSortDescriptorActions[fieldsDescriptorIndex++];
				var descriptor = new FieldSortDescriptor();
				action(descriptor);
				JsonSerializer.Serialize(writer, descriptor, options);
			}
			else if (item == 2)
			{
				var action = _geoDistanceSortDescriptorActions[geoDescriptorIndex++];
				var descriptor = new GeoDistanceSortDescriptor();
				action(descriptor);
				JsonSerializer.Serialize(writer, descriptor, options);
			}
			else if (item == 3)
			{
				var action = _scriptSortDescriptorActions[scriptsDescriptorIndex++];
				var descriptor = new ScriptSortDescriptor();
				action(descriptor);
				JsonSerializer.Serialize(writer, descriptor, options);
			}
		}

		//foreach (var sort in PromisedValue)
		//{
		//	if (sort is null)
		//		continue;

		//	if (sort is FieldSort fieldSort)
		//	{
		//		SortSerializationHelpers.WriteFieldSort(writer, fieldSort, options, settings);
		//		continue;
		//	}

		//	if (sort is ScriptSort scriptSort)
		//	{
		//		SortSerializationHelpers.WriteScriptSort(writer, scriptSort, options);
		//		continue;
		//	}

		//	if (sort is GeoDistanceSort geoDistanceSort)
		//	{
		//		SortSerializationHelpers.WriteGeoDistanceSort(writer, geoDistanceSort, options, settings);
		//		continue;
		//	}

		//	// TODO - Other types
		//	throw new NotImplementedException("The sort type is not currently supported in this release.");
		//}

		//if (_fieldSortDescriptorActions is not null)
		//{
		//	foreach (var action in _fieldSortDescriptorActions)
		//	{
		//		var descriptor = new FieldSortDescriptor();
		//		action(descriptor);
		//		JsonSerializer.Serialize(writer, descriptor, options);
		//	}
		//}

		//if (_geoDistanceSortDescriptorActions is not null)
		//{
		//	foreach (var action in _geoDistanceSortDescriptorActions)
		//	{
		//		var descriptor = new GeoDistanceSortDescriptor();
		//		action(descriptor);
		//		JsonSerializer.Serialize(writer, descriptor, options);
		//	}
		//}

		//if (_scriptSortDescriptorActions is not null)
		//{
		//	foreach (var action in _scriptSortDescriptorActions)
		//	{
		//		var descriptor = new ScriptSortDescriptor();
		//		action(descriptor);
		//		JsonSerializer.Serialize(writer, descriptor, options);
		//	}
		//}

		writer.WriteEndArray();
	}
}
