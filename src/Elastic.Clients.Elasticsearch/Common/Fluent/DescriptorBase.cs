// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;
using Elastic.Clients.Elasticsearch.QueryDsl;

namespace Elastic.Clients.Elasticsearch;

public interface IDescriptor { }

internal interface ISelfSerializable
{
	void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings);
}

internal interface ISelfDeserializable<T>
{
	T Deserialize(ref Utf8JsonReader reader, JsonSerializerOptions options, IElasticsearchClientSettings settings);
}

internal interface IFactoryDeserializable<T>
{
}

internal abstract class UnionFactory<T>
{
	internal abstract T Deserialize(ref Utf8JsonReader reader, JsonSerializerOptions options);
}




public abstract class DescriptorBase<TDescriptor> : IDescriptor, ISelfSerializable
	where TDescriptor : DescriptorBase<TDescriptor>
{
	private readonly TDescriptor _self;

	protected DescriptorBase() => _self = (TDescriptor)this;

	[IgnoreDataMember]
	protected TDescriptor Self => _self;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	protected TDescriptor Assign<TValue>(TValue value, Action<TDescriptor, TValue> assign) => Fluent.Assign(_self, value, assign);

	protected abstract void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings);

	/// <summary>
	/// Hides the <see cref="Equals" /> method.
	/// </summary>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	// ReSharper disable once BaseObjectEqualsIsObjectEquals
	//only used to hide by default
	public override bool Equals(object obj) => base.Equals(obj);

	/// <summary>
	/// Hides the <see cref="GetHashCode" /> method.
	/// </summary>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	// ReSharper disable once BaseObjectGetHashCodeCallInGetHashCode
	//only used to hide by default
	public override int GetHashCode() => base.GetHashCode();

	/// <summary>
	/// Hides the <see cref="ToString" /> method.
	/// </summary>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public override string ToString() => base.ToString();

	void ISelfSerializable.Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings) => Serialize(writer, options, settings);
}

public abstract class QueryDescriptorBase<TDescriptor> : DescriptorBase<TDescriptor>, IQuery
		where TDescriptor : QueryDescriptorBase<TDescriptor>, IQuery
{
	internal string _name;

	///// <inheritdoc cref="IQuery.Conditionless"/>
	//protected abstract bool Conditionless { get; }

	//double? IQuery.Boost { get; set; }

	//bool IQuery.Conditionless => Conditionless;

	//bool IQuery.IsStrict { get; set; }

	//bool IQuery.IsVerbatim { get; set; }

	bool IQuery.IsWritable => true; /*Self.IsVerbatim || !Self.Conditionless;*/

	public TDescriptor Name(string name) => Assign(name, (a, v) => a._name = v);

	///// <inheritdoc cref="IQuery.Boost"/>
	//public TDescriptor Boost(double? boost) => Assign(boost, (a, v) => a.Boost = v);

	///// <inheritdoc cref="IQuery.IsVerbatim"/>
	//public TDescriptor Verbatim(bool verbatim = true) => Assign(verbatim, (a, v) => a.IsVerbatim = v);

	///// <inheritdoc cref="IQuery.IsStrict"/>
	//public TDescriptor Strict(bool strict = true) => Assign(strict, (a, v) => a.IsStrict = v);
}

//internal abstract class QueryDescriptorConverterBase<T> : JsonConverter<T> where T : QueryDescriptorBase<T>
//{
//}

//internal abstract class FieldNameQueryDescriptorConverterBase<T> : JsonConverter<T> where T : FieldNameQueryDescriptorBase<T>
//{
//	public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
//	public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
//	{
//		writer.WriteStartObject();
//		if (value._field is not null)
//		{
//			writer.WritePropertyName(value._field.ToString());
//			WriteInternal(writer, value, options);
//		}
//		writer.WriteEndObject();
//	}

//	internal abstract void WriteInternal(Utf8JsonWriter writer, T value, JsonSerializerOptions options);
//}

public abstract class FieldNameQueryDescriptorBase<TDescriptor, T> : QueryDescriptorBase<TDescriptor>
		where TDescriptor : FieldNameQueryDescriptorBase<TDescriptor, T>
{
	internal Field _field;

	//bool IQuery.IsStrict { get; set; }


	//bool IQuery.IsVerbatim { get; set; }

	public TDescriptor Field(Field field) => Assign(field, (a, v) => a._field = v);

	public TDescriptor Field<TValue>(Expression<Func<TDescriptor, TValue>> objectPath) =>
		Assign(objectPath, (a, v) => a._field = v);
}
