// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch;

public sealed class ScriptSort : SortBase
{
	[JsonConverter(typeof(ScriptBaseConverter))]
	public ScriptBase Script { get; set; }

	public ScriptSortType? Type { get; set; }

	public NestedSort? Nested { get; set; }
}

public sealed class ScriptSortDescriptor : SortDescriptorBase<ScriptSortDescriptor>
{
	private SortMode? _sortMode;
	private NestedSort _nestedSort;
	private NestedSortDescriptor _nestedSortDescriptor;
	private Action<NestedSortDescriptor> _nestedSortDescriptorAction;
	private SortOrder? _order;
	private ScriptSortType? _scriptSortType;
	private ScriptBase _script;
	private InlineScriptDescriptor _inlineScriptDescriptor;
	private Action<InlineScriptDescriptor> _inlineScriptDescriptorAction;

	/// <summary>
	/// Sorts by ascending sort order.
	/// </summary>
	public ScriptSortDescriptor Ascending() => Assign(SortOrder.Asc, (a, v) => Self._order = v);

	/// <summary>
	/// Sorts by descending sort order.
	/// </summary>
	public ScriptSortDescriptor Descending() => Assign(SortOrder.Desc, (a, v) => a._order = v);

	public ScriptSortDescriptor Mode(SortMode? mode) => Assign(mode, (a, v) => a._sortMode = v);

	public ScriptSortDescriptor Nested(NestedSort nestedSort) => Assign(nestedSort, (a, v) => a._nestedSort = v);

	public ScriptSortDescriptor Nested(NestedSortDescriptor descriptor) =>
		Assign(descriptor, (a, v) => a._nestedSortDescriptor = v);

	public ScriptSortDescriptor Nested(Action<NestedSortDescriptor> configure) =>
		Assign(configure, (a, v) => a._nestedSortDescriptorAction = v);

	public ScriptSortDescriptor Order(SortOrder? order) => Assign(order, (a, v) => a._order = v);

	public ScriptSortDescriptor Script(ScriptBase script) => Assign(script, (a, v) => a._script = v);

	// TODO - Decide on naming.
	// If we have multiple descriptors with the same method name, they may conflict

	public ScriptSortDescriptor InlineScript(InlineScriptDescriptor descriptor) =>
		Assign(descriptor, (a, v) => a._inlineScriptDescriptor = v);

	public ScriptSortDescriptor InlineScript(Action<InlineScriptDescriptor> configure) =>
		Assign(configure, (a, v) => a._inlineScriptDescriptorAction = v);

	public ScriptSortDescriptor InlineScript(Action<ScriptSortDescriptor> configure) => this;

	// TODO - Stored Script?

	public ScriptSortDescriptor Type(ScriptSortType? type) => Assign(type, (a, v) => a._scriptSortType = v);

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		writer.WritePropertyName("_script");
		writer.WriteStartObject();

		if (_order.HasValue)
		{
			writer.WritePropertyName("order");
			JsonSerializer.Serialize(writer, _order.Value, options);
		}

		if (_sortMode.HasValue)
		{
			writer.WritePropertyName("mode");
			JsonSerializer.Serialize(writer, _sortMode.Value, options);
		}

		if (_nestedSort is not null)
		{
			writer.WritePropertyName("nested");
			JsonSerializer.Serialize(writer, _nestedSort, options);
		}
		else if (_nestedSortDescriptor is not null)
		{
			writer.WritePropertyName("nested");
			JsonSerializer.Serialize(writer, _nestedSortDescriptor, options);
		}
		else if (_nestedSortDescriptorAction is not null)
		{
			writer.WritePropertyName("nested");
			var descriptor = new NestedSortDescriptor();
			_nestedSortDescriptorAction(descriptor);
			JsonSerializer.Serialize(writer, descriptor, options);
		}

		if (_script is not null)
		{
			writer.WritePropertyName("script");
			JsonSerializer.Serialize(writer, _script, options);
		}
		else if (_inlineScriptDescriptor is not null)
		{
			writer.WritePropertyName("script");
			JsonSerializer.Serialize(writer, _inlineScriptDescriptor, options);
		}
		else if (_inlineScriptDescriptorAction is not null)
		{
			writer.WritePropertyName("script");
			var descriptor = new InlineScriptDescriptor();
			_inlineScriptDescriptorAction(descriptor);
			JsonSerializer.Serialize(writer, descriptor, options);
		}

		if (_scriptSortType.HasValue)
		{
			writer.WritePropertyName("type");
			JsonSerializer.Serialize(writer, _scriptSortType.Value, options);
		}

		writer.WriteEndObject();
		writer.WriteEndObject();
	}

	//internal FieldSort ToFieldSort() => null;
}

public sealed class ScriptSortDescriptor<TDocument> : SortDescriptorBase<ScriptSortDescriptor<TDocument>>
{
	private SortMode? _sortMode;
	private NestedSort _nestedSort;
	private NestedSortDescriptor<TDocument> _nestedSortDescriptor;
	private Action<NestedSortDescriptor<TDocument>> _nestedSortDescriptorAction;
	private SortOrder? _order;
	private ScriptSortType? _scriptSortType;
	private ScriptBase _script;
	private InlineScriptDescriptor _inlineScriptDescriptor;
	private Action<InlineScriptDescriptor> _inlineScriptDescriptorAction;

	/// <summary>
	/// Sorts by ascending sort order.
	/// </summary>
	public ScriptSortDescriptor<TDocument> Ascending() => Assign(SortOrder.Asc, (a, v) => Self._order = v);

	/// <summary>
	/// Sorts by descending sort order.
	/// </summary>
	public ScriptSortDescriptor<TDocument> Descending() => Assign(SortOrder.Desc, (a, v) => a._order = v);

	public ScriptSortDescriptor<TDocument> Mode(SortMode? mode) => Assign(mode, (a, v) => a._sortMode = v);

	public ScriptSortDescriptor<TDocument> Nested(NestedSort nestedSort) => Assign(nestedSort, (a, v) => a._nestedSort = v);

	public ScriptSortDescriptor<TDocument> Nested(NestedSortDescriptor<TDocument> descriptor) =>
		Assign(descriptor, (a, v) => a._nestedSortDescriptor = v);

	public ScriptSortDescriptor<TDocument> Nested(Action<NestedSortDescriptor<TDocument>> configure) =>
		Assign(configure, (a, v) => a._nestedSortDescriptorAction = v);

	public ScriptSortDescriptor<TDocument> Order(SortOrder? order) => Assign(order, (a, v) => a._order = v);

	public ScriptSortDescriptor<TDocument> Script(ScriptBase script) => Assign(script, (a, v) => a._script = v);

	// TODO - Decide on naming.
	// If we have multiple descriptors with the same method name, they may conflict

	public ScriptSortDescriptor<TDocument> InlineScript(InlineScriptDescriptor descriptor) =>
		Assign(descriptor, (a, v) => a._inlineScriptDescriptor = v);

	public ScriptSortDescriptor<TDocument> InlineScript(Action<InlineScriptDescriptor> configure) =>
		Assign(configure, (a, v) => a._inlineScriptDescriptorAction = v);

	public ScriptSortDescriptor<TDocument> InlineScript(Action<ScriptSortDescriptor> configure) => this;

	// TODO - Stored Script?

	public ScriptSortDescriptor<TDocument> Type(ScriptSortType? type) => Assign(type, (a, v) => a._scriptSortType = v);

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		writer.WritePropertyName("_script");
		writer.WriteStartObject();

		if (_order.HasValue)
		{
			writer.WritePropertyName("order");
			JsonSerializer.Serialize(writer, _order.Value, options);
		}

		if (_sortMode.HasValue)
		{
			writer.WritePropertyName("mode");
			JsonSerializer.Serialize(writer, _sortMode.Value, options);
		}

		if (_nestedSort is not null)
		{
			writer.WritePropertyName("nested");
			JsonSerializer.Serialize(writer, _nestedSort, options);
		}
		else if (_nestedSortDescriptor is not null)
		{
			writer.WritePropertyName("nested");
			JsonSerializer.Serialize(writer, _nestedSortDescriptor, options);
		}
		else if (_nestedSortDescriptorAction is not null)
		{
			writer.WritePropertyName("nested");
			var descriptor = new NestedSortDescriptor<TDocument>();
			_nestedSortDescriptorAction(descriptor);
			JsonSerializer.Serialize(writer, descriptor, options);
		}

		if (_script is not null)
		{
			writer.WritePropertyName("script");
			JsonSerializer.Serialize(writer, _script, options);
		}
		else if (_inlineScriptDescriptor is not null)
		{
			writer.WritePropertyName("script");
			JsonSerializer.Serialize(writer, _inlineScriptDescriptor, options);
		}
		else if (_inlineScriptDescriptorAction is not null)
		{
			writer.WritePropertyName("script");
			var descriptor = new InlineScriptDescriptor();
			_inlineScriptDescriptorAction(descriptor);
			JsonSerializer.Serialize(writer, descriptor, options);
		}

		if (_scriptSortType.HasValue)
		{
			writer.WritePropertyName("type");
			JsonSerializer.Serialize(writer, _scriptSortType.Value, options);
		}

		writer.WriteEndObject();
		writer.WriteEndObject();
	}

	//internal FieldSort ToFieldSort() => null;
}
