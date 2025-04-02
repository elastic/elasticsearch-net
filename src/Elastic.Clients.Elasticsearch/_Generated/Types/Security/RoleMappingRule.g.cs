// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.
//
// ███╗   ██╗ ██████╗ ████████╗██╗ ██████╗███████╗
// ████╗  ██║██╔═══██╗╚══██╔══╝██║██╔════╝██╔════╝
// ██╔██╗ ██║██║   ██║   ██║   ██║██║     █████╗
// ██║╚██╗██║██║   ██║   ██║   ██║██║     ██╔══╝
// ██║ ╚████║╚██████╔╝   ██║   ██║╚██████╗███████╗
// ╚═╝  ╚═══╝ ╚═════╝    ╚═╝   ╚═╝ ╚═════╝╚══════╝
// ------------------------------------------------
//
// This file is automatically generated.
// Please do not edit these files manually.
//
// ------------------------------------------------

#nullable restore

using System;
using System.Linq;
using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch.Security;

internal sealed partial class RoleMappingRuleConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Security.RoleMappingRule>
{
	private static readonly System.Text.Json.JsonEncodedText VariantAll = System.Text.Json.JsonEncodedText.Encode("all");
	private static readonly System.Text.Json.JsonEncodedText VariantAny = System.Text.Json.JsonEncodedText.Encode("any");
	private static readonly System.Text.Json.JsonEncodedText VariantExcept = System.Text.Json.JsonEncodedText.Encode("except");
	private static readonly System.Text.Json.JsonEncodedText VariantField = System.Text.Json.JsonEncodedText.Encode("field");

	public override Elastic.Clients.Elasticsearch.Security.RoleMappingRule Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		var variantType = string.Empty;
		object? variant = null;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (reader.ValueTextEquals(VariantAll))
			{
				variantType = VariantAll.Value;
				reader.Read();
				variant = reader.ReadValue<System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Security.RoleMappingRule>>(options, static System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Security.RoleMappingRule> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.Security.RoleMappingRule>(o, null)!);
				continue;
			}

			if (reader.ValueTextEquals(VariantAny))
			{
				variantType = VariantAny.Value;
				reader.Read();
				variant = reader.ReadValue<System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Security.RoleMappingRule>>(options, static System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Security.RoleMappingRule> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.Security.RoleMappingRule>(o, null)!);
				continue;
			}

			if (reader.ValueTextEquals(VariantExcept))
			{
				variantType = VariantExcept.Value;
				reader.Read();
				variant = reader.ReadValue<Elastic.Clients.Elasticsearch.Security.RoleMappingRule>(options, null);
				continue;
			}

			if (reader.ValueTextEquals(VariantField))
			{
				variantType = VariantField.Value;
				reader.Read();
				variant = reader.ReadValue<System.Collections.Generic.KeyValuePair<Elastic.Clients.Elasticsearch.Field, System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.FieldValue>>>(options, static System.Collections.Generic.KeyValuePair<Elastic.Clients.Elasticsearch.Field, System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.FieldValue>> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadKeyValuePairValue<Elastic.Clients.Elasticsearch.Field, System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.FieldValue>>(o, null, static System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.FieldValue> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadSingleOrManyCollectionValue<Elastic.Clients.Elasticsearch.FieldValue>(o, null)!));
				continue;
			}

			if (options.UnmappedMemberHandling is System.Text.Json.Serialization.JsonUnmappedMemberHandling.Skip)
			{
				reader.Skip();
				continue;
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new Elastic.Clients.Elasticsearch.Security.RoleMappingRule(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			VariantType = variantType,
			Variant = variant
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Security.RoleMappingRule value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		switch (value.VariantType)
		{
			case "":
				break;
			case "all":
				writer.WriteProperty(options, value.VariantType, (System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Security.RoleMappingRule>)value.Variant, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Security.RoleMappingRule> v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.Security.RoleMappingRule>(o, v, null));
				break;
			case "any":
				writer.WriteProperty(options, value.VariantType, (System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Security.RoleMappingRule>)value.Variant, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Security.RoleMappingRule> v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.Security.RoleMappingRule>(o, v, null));
				break;
			case "except":
				writer.WriteProperty(options, value.VariantType, (Elastic.Clients.Elasticsearch.Security.RoleMappingRule)value.Variant, null, null);
				break;
			case "field":
				writer.WriteProperty(options, value.VariantType, (System.Collections.Generic.KeyValuePair<Elastic.Clients.Elasticsearch.Field, System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.FieldValue>>)value.Variant, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.KeyValuePair<Elastic.Clients.Elasticsearch.Field, System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.FieldValue>> v) => w.WriteKeyValuePairValue<Elastic.Clients.Elasticsearch.Field, System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.FieldValue>>(o, v, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.FieldValue> v) => w.WriteSingleOrManyCollectionValue<Elastic.Clients.Elasticsearch.FieldValue>(o, v, null)));
				break;
			default:
				throw new System.Text.Json.JsonException($"Variant '{value.VariantType}' is not supported for type '{nameof(Elastic.Clients.Elasticsearch.Security.RoleMappingRule)}'.");
		}

		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Security.RoleMappingRuleConverter))]
public sealed partial class RoleMappingRule
{
	public string VariantType { get; internal set; } = string.Empty;
	public object? Variant { get; internal set; }
#if NET7_0_OR_GREATER
	public RoleMappingRule()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public RoleMappingRule()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal RoleMappingRule(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Security.RoleMappingRule>? All { get => GetVariant<System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Security.RoleMappingRule>>("all"); set => SetVariant("all", value); }
	public System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Security.RoleMappingRule>? Any { get => GetVariant<System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Security.RoleMappingRule>>("any"); set => SetVariant("any", value); }
	public Elastic.Clients.Elasticsearch.Security.RoleMappingRule? Except { get => GetVariant<Elastic.Clients.Elasticsearch.Security.RoleMappingRule>("except"); set => SetVariant("except", value); }
	public System.Collections.Generic.KeyValuePair<Elastic.Clients.Elasticsearch.Field, System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.FieldValue>>? Field { get => GetVariant<System.Collections.Generic.KeyValuePair<Elastic.Clients.Elasticsearch.Field, System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.FieldValue>>>("field"); set => SetVariant("field", value); }

	public static implicit operator Elastic.Clients.Elasticsearch.Security.RoleMappingRule(System.Collections.Generic.KeyValuePair<Elastic.Clients.Elasticsearch.Field, System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.FieldValue>> value) => new Elastic.Clients.Elasticsearch.Security.RoleMappingRule { Field = value };

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	private T? GetVariant<T>(string type)
	{
		if (string.Equals(VariantType, type, System.StringComparison.Ordinal) && Variant is T result)
		{
			return result;
		}

		return default;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	private void SetVariant<T>(string type, T? value)
	{
		VariantType = type;
		Variant = value;
	}
}

public readonly partial struct RoleMappingRuleDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.Security.RoleMappingRule Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public RoleMappingRuleDescriptor(Elastic.Clients.Elasticsearch.Security.RoleMappingRule instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public RoleMappingRuleDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Security.RoleMappingRule(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Security.RoleMappingRuleDescriptor<TDocument>(Elastic.Clients.Elasticsearch.Security.RoleMappingRule instance) => new Elastic.Clients.Elasticsearch.Security.RoleMappingRuleDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Security.RoleMappingRule(Elastic.Clients.Elasticsearch.Security.RoleMappingRuleDescriptor<TDocument> descriptor) => descriptor.Instance;

	public Elastic.Clients.Elasticsearch.Security.RoleMappingRuleDescriptor<TDocument> All(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Security.RoleMappingRule>? value)
	{
		Instance.All = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.RoleMappingRuleDescriptor<TDocument> All()
	{
		Instance.All = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfRoleMappingRule<TDocument>.Build(null);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.RoleMappingRuleDescriptor<TDocument> All(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfRoleMappingRule<TDocument>>? action)
	{
		Instance.All = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfRoleMappingRule<TDocument>.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.RoleMappingRuleDescriptor<TDocument> All(params Elastic.Clients.Elasticsearch.Security.RoleMappingRule[] values)
	{
		Instance.All = [.. values];
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.RoleMappingRuleDescriptor<TDocument> All(params System.Action<Elastic.Clients.Elasticsearch.Security.RoleMappingRuleDescriptor<TDocument>>[] actions)
	{
		var items = new System.Collections.Generic.List<Elastic.Clients.Elasticsearch.Security.RoleMappingRule>();
		foreach (var action in actions)
		{
			items.Add(Elastic.Clients.Elasticsearch.Security.RoleMappingRuleDescriptor<TDocument>.Build(action));
		}

		Instance.All = items;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.RoleMappingRuleDescriptor<TDocument> Any(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Security.RoleMappingRule>? value)
	{
		Instance.Any = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.RoleMappingRuleDescriptor<TDocument> Any()
	{
		Instance.Any = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfRoleMappingRule<TDocument>.Build(null);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.RoleMappingRuleDescriptor<TDocument> Any(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfRoleMappingRule<TDocument>>? action)
	{
		Instance.Any = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfRoleMappingRule<TDocument>.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.RoleMappingRuleDescriptor<TDocument> Any(params Elastic.Clients.Elasticsearch.Security.RoleMappingRule[] values)
	{
		Instance.Any = [.. values];
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.RoleMappingRuleDescriptor<TDocument> Any(params System.Action<Elastic.Clients.Elasticsearch.Security.RoleMappingRuleDescriptor<TDocument>>[] actions)
	{
		var items = new System.Collections.Generic.List<Elastic.Clients.Elasticsearch.Security.RoleMappingRule>();
		foreach (var action in actions)
		{
			items.Add(Elastic.Clients.Elasticsearch.Security.RoleMappingRuleDescriptor<TDocument>.Build(action));
		}

		Instance.Any = items;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.RoleMappingRuleDescriptor<TDocument> Except(Elastic.Clients.Elasticsearch.Security.RoleMappingRule? value)
	{
		Instance.Except = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.RoleMappingRuleDescriptor<TDocument> Except(System.Action<Elastic.Clients.Elasticsearch.Security.RoleMappingRuleDescriptor<TDocument>> action)
	{
		Instance.Except = Elastic.Clients.Elasticsearch.Security.RoleMappingRuleDescriptor<TDocument>.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.RoleMappingRuleDescriptor<TDocument> Field(System.Collections.Generic.KeyValuePair<Elastic.Clients.Elasticsearch.Field, System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.FieldValue>>? value)
	{
		Instance.Field = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.RoleMappingRuleDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field key, System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.FieldValue> value)
	{
		Instance.Field = new System.Collections.Generic.KeyValuePair<Elastic.Clients.Elasticsearch.Field, System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.FieldValue>>(key, value);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.RoleMappingRuleDescriptor<TDocument> Field(System.Linq.Expressions.Expression<System.Func<TDocument, object?>> key, System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.FieldValue> value)
	{
		Instance.Field = new System.Collections.Generic.KeyValuePair<Elastic.Clients.Elasticsearch.Field, System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.FieldValue>>(key, value);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.RoleMappingRuleDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field key)
	{
		Instance.Field = new System.Collections.Generic.KeyValuePair<Elastic.Clients.Elasticsearch.Field, System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.FieldValue>>(key, Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfFieldValue.Build(null));
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.RoleMappingRuleDescriptor<TDocument> Field(System.Linq.Expressions.Expression<System.Func<TDocument, object?>> key)
	{
		Instance.Field = new System.Collections.Generic.KeyValuePair<Elastic.Clients.Elasticsearch.Field, System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.FieldValue>>(key, Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfFieldValue.Build(null));
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.RoleMappingRuleDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field key, System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfFieldValue>? action)
	{
		Instance.Field = new System.Collections.Generic.KeyValuePair<Elastic.Clients.Elasticsearch.Field, System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.FieldValue>>(key, Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfFieldValue.Build(action));
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.RoleMappingRuleDescriptor<TDocument> Field(System.Linq.Expressions.Expression<System.Func<TDocument, object?>> key, System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfFieldValue>? action)
	{
		Instance.Field = new System.Collections.Generic.KeyValuePair<Elastic.Clients.Elasticsearch.Field, System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.FieldValue>>(key, Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfFieldValue.Build(action));
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.RoleMappingRuleDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field key, params Elastic.Clients.Elasticsearch.FieldValue[] values)
	{
		Instance.Field = new System.Collections.Generic.KeyValuePair<Elastic.Clients.Elasticsearch.Field, System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.FieldValue>>(key, [.. values]);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.RoleMappingRuleDescriptor<TDocument> Field(System.Linq.Expressions.Expression<System.Func<TDocument, object?>> key, params Elastic.Clients.Elasticsearch.FieldValue[] values)
	{
		Instance.Field = new System.Collections.Generic.KeyValuePair<Elastic.Clients.Elasticsearch.Field, System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.FieldValue>>(key, [.. values]);
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Security.RoleMappingRule Build(System.Action<Elastic.Clients.Elasticsearch.Security.RoleMappingRuleDescriptor<TDocument>> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Security.RoleMappingRuleDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.Security.RoleMappingRule(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}

public readonly partial struct RoleMappingRuleDescriptor
{
	internal Elastic.Clients.Elasticsearch.Security.RoleMappingRule Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public RoleMappingRuleDescriptor(Elastic.Clients.Elasticsearch.Security.RoleMappingRule instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public RoleMappingRuleDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Security.RoleMappingRule(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Security.RoleMappingRuleDescriptor(Elastic.Clients.Elasticsearch.Security.RoleMappingRule instance) => new Elastic.Clients.Elasticsearch.Security.RoleMappingRuleDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Security.RoleMappingRule(Elastic.Clients.Elasticsearch.Security.RoleMappingRuleDescriptor descriptor) => descriptor.Instance;

	public Elastic.Clients.Elasticsearch.Security.RoleMappingRuleDescriptor All(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Security.RoleMappingRule>? value)
	{
		Instance.All = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.RoleMappingRuleDescriptor All()
	{
		Instance.All = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfRoleMappingRule.Build(null);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.RoleMappingRuleDescriptor All(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfRoleMappingRule>? action)
	{
		Instance.All = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfRoleMappingRule.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.RoleMappingRuleDescriptor All<T>(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfRoleMappingRule<T>>? action)
	{
		Instance.All = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfRoleMappingRule<T>.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.RoleMappingRuleDescriptor All(params Elastic.Clients.Elasticsearch.Security.RoleMappingRule[] values)
	{
		Instance.All = [.. values];
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.RoleMappingRuleDescriptor All(params System.Action<Elastic.Clients.Elasticsearch.Security.RoleMappingRuleDescriptor>[] actions)
	{
		var items = new System.Collections.Generic.List<Elastic.Clients.Elasticsearch.Security.RoleMappingRule>();
		foreach (var action in actions)
		{
			items.Add(Elastic.Clients.Elasticsearch.Security.RoleMappingRuleDescriptor.Build(action));
		}

		Instance.All = items;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.RoleMappingRuleDescriptor All<T>(params System.Action<Elastic.Clients.Elasticsearch.Security.RoleMappingRuleDescriptor<T>>[] actions)
	{
		var items = new System.Collections.Generic.List<Elastic.Clients.Elasticsearch.Security.RoleMappingRule>();
		foreach (var action in actions)
		{
			items.Add(Elastic.Clients.Elasticsearch.Security.RoleMappingRuleDescriptor<T>.Build(action));
		}

		Instance.All = items;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.RoleMappingRuleDescriptor Any(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Security.RoleMappingRule>? value)
	{
		Instance.Any = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.RoleMappingRuleDescriptor Any()
	{
		Instance.Any = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfRoleMappingRule.Build(null);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.RoleMappingRuleDescriptor Any(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfRoleMappingRule>? action)
	{
		Instance.Any = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfRoleMappingRule.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.RoleMappingRuleDescriptor Any<T>(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfRoleMappingRule<T>>? action)
	{
		Instance.Any = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfRoleMappingRule<T>.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.RoleMappingRuleDescriptor Any(params Elastic.Clients.Elasticsearch.Security.RoleMappingRule[] values)
	{
		Instance.Any = [.. values];
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.RoleMappingRuleDescriptor Any(params System.Action<Elastic.Clients.Elasticsearch.Security.RoleMappingRuleDescriptor>[] actions)
	{
		var items = new System.Collections.Generic.List<Elastic.Clients.Elasticsearch.Security.RoleMappingRule>();
		foreach (var action in actions)
		{
			items.Add(Elastic.Clients.Elasticsearch.Security.RoleMappingRuleDescriptor.Build(action));
		}

		Instance.Any = items;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.RoleMappingRuleDescriptor Any<T>(params System.Action<Elastic.Clients.Elasticsearch.Security.RoleMappingRuleDescriptor<T>>[] actions)
	{
		var items = new System.Collections.Generic.List<Elastic.Clients.Elasticsearch.Security.RoleMappingRule>();
		foreach (var action in actions)
		{
			items.Add(Elastic.Clients.Elasticsearch.Security.RoleMappingRuleDescriptor<T>.Build(action));
		}

		Instance.Any = items;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.RoleMappingRuleDescriptor Except(Elastic.Clients.Elasticsearch.Security.RoleMappingRule? value)
	{
		Instance.Except = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.RoleMappingRuleDescriptor Except(System.Action<Elastic.Clients.Elasticsearch.Security.RoleMappingRuleDescriptor> action)
	{
		Instance.Except = Elastic.Clients.Elasticsearch.Security.RoleMappingRuleDescriptor.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.RoleMappingRuleDescriptor Except<T>(System.Action<Elastic.Clients.Elasticsearch.Security.RoleMappingRuleDescriptor<T>> action)
	{
		Instance.Except = Elastic.Clients.Elasticsearch.Security.RoleMappingRuleDescriptor<T>.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.RoleMappingRuleDescriptor Field(System.Collections.Generic.KeyValuePair<Elastic.Clients.Elasticsearch.Field, System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.FieldValue>>? value)
	{
		Instance.Field = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.RoleMappingRuleDescriptor Field(Elastic.Clients.Elasticsearch.Field key, System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.FieldValue> value)
	{
		Instance.Field = new System.Collections.Generic.KeyValuePair<Elastic.Clients.Elasticsearch.Field, System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.FieldValue>>(key, value);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.RoleMappingRuleDescriptor Field<T>(System.Linq.Expressions.Expression<System.Func<T, object?>> key, System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.FieldValue> value)
	{
		Instance.Field = new System.Collections.Generic.KeyValuePair<Elastic.Clients.Elasticsearch.Field, System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.FieldValue>>(key, value);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.RoleMappingRuleDescriptor Field(Elastic.Clients.Elasticsearch.Field key)
	{
		Instance.Field = new System.Collections.Generic.KeyValuePair<Elastic.Clients.Elasticsearch.Field, System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.FieldValue>>(key, Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfFieldValue.Build(null));
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.RoleMappingRuleDescriptor Field<T>(System.Linq.Expressions.Expression<System.Func<T, object?>> key)
	{
		Instance.Field = new System.Collections.Generic.KeyValuePair<Elastic.Clients.Elasticsearch.Field, System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.FieldValue>>(key, Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfFieldValue.Build(null));
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.RoleMappingRuleDescriptor Field(Elastic.Clients.Elasticsearch.Field key, System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfFieldValue>? action)
	{
		Instance.Field = new System.Collections.Generic.KeyValuePair<Elastic.Clients.Elasticsearch.Field, System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.FieldValue>>(key, Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfFieldValue.Build(action));
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.RoleMappingRuleDescriptor Field<T>(System.Linq.Expressions.Expression<System.Func<T, object?>> key, System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfFieldValue>? action)
	{
		Instance.Field = new System.Collections.Generic.KeyValuePair<Elastic.Clients.Elasticsearch.Field, System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.FieldValue>>(key, Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfFieldValue.Build(action));
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.RoleMappingRuleDescriptor Field(Elastic.Clients.Elasticsearch.Field key, params Elastic.Clients.Elasticsearch.FieldValue[] values)
	{
		Instance.Field = new System.Collections.Generic.KeyValuePair<Elastic.Clients.Elasticsearch.Field, System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.FieldValue>>(key, [.. values]);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.RoleMappingRuleDescriptor Field<T>(System.Linq.Expressions.Expression<System.Func<T, object?>> key, params Elastic.Clients.Elasticsearch.FieldValue[] values)
	{
		Instance.Field = new System.Collections.Generic.KeyValuePair<Elastic.Clients.Elasticsearch.Field, System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.FieldValue>>(key, [.. values]);
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Security.RoleMappingRule Build(System.Action<Elastic.Clients.Elasticsearch.Security.RoleMappingRuleDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Security.RoleMappingRuleDescriptor(new Elastic.Clients.Elasticsearch.Security.RoleMappingRule(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}