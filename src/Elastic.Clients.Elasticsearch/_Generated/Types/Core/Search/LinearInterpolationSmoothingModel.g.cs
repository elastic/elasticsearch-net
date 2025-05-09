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

namespace Elastic.Clients.Elasticsearch.Core.Search;

internal sealed partial class LinearInterpolationSmoothingModelConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Core.Search.LinearInterpolationSmoothingModel>
{
	private static readonly System.Text.Json.JsonEncodedText PropBigramLambda = System.Text.Json.JsonEncodedText.Encode("bigram_lambda");
	private static readonly System.Text.Json.JsonEncodedText PropTrigramLambda = System.Text.Json.JsonEncodedText.Encode("trigram_lambda");
	private static readonly System.Text.Json.JsonEncodedText PropUnigramLambda = System.Text.Json.JsonEncodedText.Encode("unigram_lambda");

	public override Elastic.Clients.Elasticsearch.Core.Search.LinearInterpolationSmoothingModel Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<double> propBigramLambda = default;
		LocalJsonValue<double> propTrigramLambda = default;
		LocalJsonValue<double> propUnigramLambda = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propBigramLambda.TryReadProperty(ref reader, options, PropBigramLambda, null))
			{
				continue;
			}

			if (propTrigramLambda.TryReadProperty(ref reader, options, PropTrigramLambda, null))
			{
				continue;
			}

			if (propUnigramLambda.TryReadProperty(ref reader, options, PropUnigramLambda, null))
			{
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
		return new Elastic.Clients.Elasticsearch.Core.Search.LinearInterpolationSmoothingModel(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			BigramLambda = propBigramLambda.Value,
			TrigramLambda = propTrigramLambda.Value,
			UnigramLambda = propUnigramLambda.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Core.Search.LinearInterpolationSmoothingModel value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropBigramLambda, value.BigramLambda, null, null);
		writer.WriteProperty(options, PropTrigramLambda, value.TrigramLambda, null, null);
		writer.WriteProperty(options, PropUnigramLambda, value.UnigramLambda, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Core.Search.LinearInterpolationSmoothingModelConverter))]
public sealed partial class LinearInterpolationSmoothingModel
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public LinearInterpolationSmoothingModel(double bigramLambda, double trigramLambda, double unigramLambda)
	{
		BigramLambda = bigramLambda;
		TrigramLambda = trigramLambda;
		UnigramLambda = unigramLambda;
	}
#if NET7_0_OR_GREATER
	public LinearInterpolationSmoothingModel()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public LinearInterpolationSmoothingModel()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal LinearInterpolationSmoothingModel(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
	required
#endif
	double BigramLambda { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	double TrigramLambda { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	double UnigramLambda { get; set; }
}

public readonly partial struct LinearInterpolationSmoothingModelDescriptor
{
	internal Elastic.Clients.Elasticsearch.Core.Search.LinearInterpolationSmoothingModel Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public LinearInterpolationSmoothingModelDescriptor(Elastic.Clients.Elasticsearch.Core.Search.LinearInterpolationSmoothingModel instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public LinearInterpolationSmoothingModelDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Core.Search.LinearInterpolationSmoothingModel(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Core.Search.LinearInterpolationSmoothingModelDescriptor(Elastic.Clients.Elasticsearch.Core.Search.LinearInterpolationSmoothingModel instance) => new Elastic.Clients.Elasticsearch.Core.Search.LinearInterpolationSmoothingModelDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Core.Search.LinearInterpolationSmoothingModel(Elastic.Clients.Elasticsearch.Core.Search.LinearInterpolationSmoothingModelDescriptor descriptor) => descriptor.Instance;

	public Elastic.Clients.Elasticsearch.Core.Search.LinearInterpolationSmoothingModelDescriptor BigramLambda(double value)
	{
		Instance.BigramLambda = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Core.Search.LinearInterpolationSmoothingModelDescriptor TrigramLambda(double value)
	{
		Instance.TrigramLambda = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Core.Search.LinearInterpolationSmoothingModelDescriptor UnigramLambda(double value)
	{
		Instance.UnigramLambda = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Core.Search.LinearInterpolationSmoothingModel Build(System.Action<Elastic.Clients.Elasticsearch.Core.Search.LinearInterpolationSmoothingModelDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Core.Search.LinearInterpolationSmoothingModelDescriptor(new Elastic.Clients.Elasticsearch.Core.Search.LinearInterpolationSmoothingModel(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}