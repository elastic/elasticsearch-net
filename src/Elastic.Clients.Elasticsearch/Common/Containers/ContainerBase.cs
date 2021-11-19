// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json.Serialization;
using Elastic.Clients.Elasticsearch.QueryDsl;

namespace Elastic.Clients.Elasticsearch;

public interface IContainer
{
}

public abstract class ContainerBase : IContainer
{
	internal ContainerVariantBase Variant { get; }

	public ContainerBase(ContainerVariantBase variant) => Variant = variant ?? throw new ArgumentNullException(nameof(variant));
}

public abstract class ContainerAndVariantBase<TVariantContainer> : ContainerVariantBase<TVariantContainer>, IContainer where TVariantContainer : IContainer
{
	internal ContainerVariantBase Variant { get; }

	public ContainerAndVariantBase(ContainerVariantBase variant) => Variant = variant ?? throw new ArgumentNullException(nameof(variant));
}

public abstract class ContainerVariantBase
{
	[JsonIgnore]
	internal abstract string VariantName { get; }
}

public abstract class ContainerVariantBase<TVariantContainer> : ContainerVariantBase where TVariantContainer : IContainer
{
	internal TVariantContainer WrapInContainer() => (TVariantContainer)Activator.CreateInstance(typeof(TVariantContainer), this);
}

public abstract class VariantDescriptorBase<T> : DescriptorBase<T> where T : DescriptorBase<T>
{
	internal string VariantName { get; private set; }

	protected void SetVariantName(string name) => VariantName = name;
}

internal interface IContainerVariant
{
	string VariantName { get; }
}

internal interface IQueryContainerVariant /*: IContainerVariant<TestContainer>*/
{
	string QueryContainerVariantName { get; }

	TestContainer WrapInQueryContainer();
}

internal interface IContainerVariant<TVariantContainer> /*: IContainerVariant*/ where TVariantContainer : IContainer
{
	TVariantContainer WrapInContainer();
}

public class TestVariant : IQueryContainerVariant
{
	string IQueryContainerVariant.QueryContainerVariantName => "TEST";

	TestContainer IQueryContainerVariant.WrapInQueryContainer() => throw new NotImplementedException();

	//internal string VariantName => this.VariantName();

	//public TestContainer WrapInContainer(TestContainer container) => new TestContainer(this);
}

//internal static class IContainerVariantExtensions
//{
//	public static string VariantName(this IContainerVariant @interface) => @interface.VariantName;
//}

public partial class TestContainer : IContainer
{
	private readonly IContainerVariant _variant;

	internal TestContainer(IContainerVariant variant) => _variant = variant;
}


//public class QueryContainerConverter : JsonConverter<QueryContainer>
//{
//	public override QueryContainer? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
//	{
//		reader.Read();

//		if (reader.TokenType != JsonTokenType.PropertyName)
//		{
//			throw new JsonException();
//		}

//		var propertyName = reader.GetString();

//		if (propertyName == "bool")
//		{
//			var boolQuery = JsonSerializer.Deserialize<BoolQuery>(ref reader, options);
//			reader.Read();
//			return new QueryContainer(boolQuery);
//		}

//		return null;
//	}

//	public override void Write(Utf8JsonWriter writer, QueryContainer value, JsonSerializerOptions options)
//	{
//		if (value is null || value.Variant is null)
//		{
//			writer.WriteNullValue();
//			return;
//		}

//		writer.WriteStartObject();

//		writer.WritePropertyName(value.Variant.QueryContainerVariantName);

//		switch (value.Variant)
//		{
//			case BoolQuery boolQuery:
//				JsonSerializer.Serialize(writer, boolQuery, options);
//				break;
//			case BoostingQuery boostingQuery:
//				JsonSerializer.Serialize(writer, boostingQuery, options);
//				break;
//		}

//		writer.WriteEndObject();
//	}
//}
