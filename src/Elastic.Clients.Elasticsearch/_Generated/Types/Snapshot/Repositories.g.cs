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

using Elastic.Clients.Elasticsearch.Core;
using Elastic.Clients.Elasticsearch.Fluent;
using Elastic.Clients.Elasticsearch.Serialization;
using Elastic.Transport;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Snapshot;

public partial class Repositories : IsADictionary<string, IRepository>
{
	public Repositories()
	{
	}

	public Repositories(IDictionary<string, IRepository> container) : base(container)
	{
	}

	public void Add(string name, IRepository repository) => BackingDictionary.Add(Sanitize(name), repository);
	public bool TryGetRepository(string name, [NotNullWhen(returnValue: true)] out IRepository repository) => BackingDictionary.TryGetValue(Sanitize(name), out repository);

	public bool TryGetRepository<T>(string name, [NotNullWhen(returnValue: true)] out T? repository) where T : class, IRepository
	{
		if (BackingDictionary.TryGetValue(Sanitize(name), out var matchedValue) && matchedValue is T finalValue)
		{
			repository = finalValue;
			return true;
		}

		repository = null;
		return false;
	}
}

public sealed partial class RepositoriesDescriptor : IsADictionaryDescriptor<RepositoriesDescriptor, Repositories, string, IRepository>
{
	public RepositoriesDescriptor() : base(new Repositories())
	{
	}

	public RepositoriesDescriptor(Repositories repositories) : base(repositories ?? new Repositories())
	{
	}

	public RepositoriesDescriptor Azure(string repositoryName) => AssignVariant<Elastic.Clients.Elasticsearch.Snapshot.AzureRepositoryDescriptor, AzureRepository>(repositoryName, null);
	public RepositoriesDescriptor Azure(string repositoryName, Action<Elastic.Clients.Elasticsearch.Snapshot.AzureRepositoryDescriptor> configure) => AssignVariant<Elastic.Clients.Elasticsearch.Snapshot.AzureRepositoryDescriptor, AzureRepository>(repositoryName, configure);
	public RepositoriesDescriptor Azure(string repositoryName, AzureRepository azureRepository) => AssignVariant(repositoryName, azureRepository);
	public RepositoriesDescriptor Gcs(string repositoryName) => AssignVariant<Elastic.Clients.Elasticsearch.Snapshot.GcsRepositoryDescriptor, GcsRepository>(repositoryName, null);
	public RepositoriesDescriptor Gcs(string repositoryName, Action<Elastic.Clients.Elasticsearch.Snapshot.GcsRepositoryDescriptor> configure) => AssignVariant<Elastic.Clients.Elasticsearch.Snapshot.GcsRepositoryDescriptor, GcsRepository>(repositoryName, configure);
	public RepositoriesDescriptor Gcs(string repositoryName, GcsRepository gcsRepository) => AssignVariant(repositoryName, gcsRepository);
	public RepositoriesDescriptor ReadOnlyUrl(string repositoryName) => AssignVariant<Elastic.Clients.Elasticsearch.Snapshot.ReadOnlyUrlRepositoryDescriptor, ReadOnlyUrlRepository>(repositoryName, null);
	public RepositoriesDescriptor ReadOnlyUrl(string repositoryName, Action<Elastic.Clients.Elasticsearch.Snapshot.ReadOnlyUrlRepositoryDescriptor> configure) => AssignVariant<Elastic.Clients.Elasticsearch.Snapshot.ReadOnlyUrlRepositoryDescriptor, ReadOnlyUrlRepository>(repositoryName, configure);
	public RepositoriesDescriptor ReadOnlyUrl(string repositoryName, ReadOnlyUrlRepository readOnlyUrlRepository) => AssignVariant(repositoryName, readOnlyUrlRepository);
	public RepositoriesDescriptor S3(string repositoryName) => AssignVariant<Elastic.Clients.Elasticsearch.Snapshot.S3RepositoryDescriptor, S3Repository>(repositoryName, null);
	public RepositoriesDescriptor S3(string repositoryName, Action<Elastic.Clients.Elasticsearch.Snapshot.S3RepositoryDescriptor> configure) => AssignVariant<Elastic.Clients.Elasticsearch.Snapshot.S3RepositoryDescriptor, S3Repository>(repositoryName, configure);
	public RepositoriesDescriptor S3(string repositoryName, S3Repository s3Repository) => AssignVariant(repositoryName, s3Repository);
	public RepositoriesDescriptor SharedFileSystem(string repositoryName) => AssignVariant<Elastic.Clients.Elasticsearch.Snapshot.SharedFileSystemRepositoryDescriptor, SharedFileSystemRepository>(repositoryName, null);
	public RepositoriesDescriptor SharedFileSystem(string repositoryName, Action<Elastic.Clients.Elasticsearch.Snapshot.SharedFileSystemRepositoryDescriptor> configure) => AssignVariant<Elastic.Clients.Elasticsearch.Snapshot.SharedFileSystemRepositoryDescriptor, SharedFileSystemRepository>(repositoryName, configure);
	public RepositoriesDescriptor SharedFileSystem(string repositoryName, SharedFileSystemRepository sharedFileSystemRepository) => AssignVariant(repositoryName, sharedFileSystemRepository);
	public RepositoriesDescriptor SourceOnly(string repositoryName) => AssignVariant<Elastic.Clients.Elasticsearch.Snapshot.SourceOnlyRepositoryDescriptor, SourceOnlyRepository>(repositoryName, null);
	public RepositoriesDescriptor SourceOnly(string repositoryName, Action<Elastic.Clients.Elasticsearch.Snapshot.SourceOnlyRepositoryDescriptor> configure) => AssignVariant<Elastic.Clients.Elasticsearch.Snapshot.SourceOnlyRepositoryDescriptor, SourceOnlyRepository>(repositoryName, configure);
	public RepositoriesDescriptor SourceOnly(string repositoryName, SourceOnlyRepository sourceOnlyRepository) => AssignVariant(repositoryName, sourceOnlyRepository);
}

internal sealed partial class RepositoryInterfaceConverter : JsonConverter<IRepository>
{
	public override IRepository Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var copiedReader = reader;
		string? type = null;
		using var jsonDoc = JsonDocument.ParseValue(ref copiedReader);
		if (jsonDoc is not null && jsonDoc.RootElement.TryGetProperty("type", out var readType) && readType.ValueKind == JsonValueKind.String)
		{
			type = readType.ToString();
		}

		switch (type)
		{
			case "azure":
				return JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Snapshot.AzureRepository>(ref reader, options);
			case "gcs":
				return JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Snapshot.GcsRepository>(ref reader, options);
			case "url":
				return JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Snapshot.ReadOnlyUrlRepository>(ref reader, options);
			case "s3":
				return JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Snapshot.S3Repository>(ref reader, options);
			case "fs":
				return JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Snapshot.SharedFileSystemRepository>(ref reader, options);
			case "source":
				return JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Snapshot.SourceOnlyRepository>(ref reader, options);
			default:
				ThrowHelper.ThrowUnknownTaggedUnionVariantJsonException(type, typeof(IRepository));
				return null;
		}
	}

	public override void Write(Utf8JsonWriter writer, IRepository value, JsonSerializerOptions options)
	{
		if (value is null)
		{
			writer.WriteNullValue();
			return;
		}

		switch (value.Type)
		{
			case "azure":
				JsonSerializer.Serialize(writer, value, typeof(Elastic.Clients.Elasticsearch.Snapshot.AzureRepository), options);
				return;
			case "gcs":
				JsonSerializer.Serialize(writer, value, typeof(Elastic.Clients.Elasticsearch.Snapshot.GcsRepository), options);
				return;
			case "url":
				JsonSerializer.Serialize(writer, value, typeof(Elastic.Clients.Elasticsearch.Snapshot.ReadOnlyUrlRepository), options);
				return;
			case "s3":
				JsonSerializer.Serialize(writer, value, typeof(Elastic.Clients.Elasticsearch.Snapshot.S3Repository), options);
				return;
			case "fs":
				JsonSerializer.Serialize(writer, value, typeof(Elastic.Clients.Elasticsearch.Snapshot.SharedFileSystemRepository), options);
				return;
			case "source":
				JsonSerializer.Serialize(writer, value, typeof(Elastic.Clients.Elasticsearch.Snapshot.SourceOnlyRepository), options);
				return;
			default:
				var type = value.GetType();
				JsonSerializer.Serialize(writer, value, type, options);
				return;
		}
	}
}

[JsonConverter(typeof(RepositoryInterfaceConverter))]
public partial interface IRepository
{
	public string? Type { get; }
}