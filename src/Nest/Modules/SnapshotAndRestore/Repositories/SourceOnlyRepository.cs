// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Runtime.Serialization;
using Nest.Utf8Json;
namespace Nest
{
	/// <summary>
	/// A source repository enables you to create minimal, source-only snapshots that take up to 50% less space on disk.
	/// Source only snapshots contain stored fields and index metadata. They do not include index or doc values structures
	/// and are not searchable when restored. After restoring a source-only snapshot, you must reindex the data into a new index.
	/// </summary>
	[JsonFormatter(typeof(SourceOnlyRepositoryFormatter))]
	public interface ISourceOnlyRepository : IRepositoryWithSettings
	{
		/// <summary>
		/// The type of snapshot repository to delegate to for storage
		/// </summary>
		[IgnoreDataMember]
		string DelegateType { get; }
	}

	/// <inheritdoc />
	public class SourceOnlyRepository : ISourceOnlyRepository
	{
		private readonly object _delegateSettings;
		private readonly string _delegateType;

		internal SourceOnlyRepository() { }

		internal SourceOnlyRepository(string delegateType, object settings)
		{
			_delegateType = delegateType;
			_delegateSettings = settings;
		}

		public SourceOnlyRepository(IRepositoryWithSettings repositoryToDelegateTo)
		{
			if (repositoryToDelegateTo == null) throw new ArgumentNullException(nameof(repositoryToDelegateTo));

			_delegateType = repositoryToDelegateTo.Type;
			_delegateSettings = repositoryToDelegateTo.DelegateSettings;
		}

		object IRepositoryWithSettings.DelegateSettings => _delegateSettings;
		string ISourceOnlyRepository.DelegateType => _delegateType;
		string ISnapshotRepository.Type { get; } = "source";
	}

	/// <inheritdoc cref="ISourceOnlyRepository" />
	public class SourceOnlyRepositoryDescriptor
		: DescriptorBase<SourceOnlyRepositoryDescriptor, ISourceOnlyRepository>, ISourceOnlyRepository
	{
		private object _delegateSettings;
		private string _delegateType;

		object IRepositoryWithSettings.DelegateSettings => _delegateSettings;
		string ISourceOnlyRepository.DelegateType => _delegateType;
		string ISnapshotRepository.Type { get; } = "source";

		private SourceOnlyRepositoryDescriptor DelegateTo<TDescriptor>(Func<TDescriptor, IRepositoryWithSettings> selector)
			where TDescriptor : IRepositoryWithSettings, new() => Custom(selector?.Invoke(new TDescriptor()));

		/// <inheritdoc cref="CreateRepositoryDescriptor.FileSystem" />
		public SourceOnlyRepositoryDescriptor FileSystem(Func<FileSystemRepositoryDescriptor, IFileSystemRepository> selector) =>
			DelegateTo(selector);

		/// <inheritdoc cref="CreateRepositoryDescriptor.ReadOnlyUrl" />
		public SourceOnlyRepositoryDescriptor ReadOnlyUrl(Func<ReadOnlyUrlRepositoryDescriptor, IReadOnlyUrlRepository> selector) =>
			DelegateTo(selector);

		/// <inheritdoc cref="CreateRepositoryDescriptor.Azure" />
		public SourceOnlyRepositoryDescriptor Azure(Func<AzureRepositoryDescriptor, IAzureRepository> selector = null) =>
			DelegateTo(selector);

		/// <inheritdoc cref="CreateRepositoryDescriptor.Hdfs" />
		public SourceOnlyRepositoryDescriptor Hdfs(Func<HdfsRepositoryDescriptor, IHdfsRepository> selector) =>
			DelegateTo(selector);

		/// <inheritdoc cref="CreateRepositoryDescriptor.S3" />
		public SourceOnlyRepositoryDescriptor S3(Func<S3RepositoryDescriptor, IS3Repository> selector) =>
			DelegateTo(selector);

		/// <inheritdoc cref="CreateRepositoryDescriptor.Custom" />
		public SourceOnlyRepositoryDescriptor Custom(IRepositoryWithSettings repository)
		{
			_delegateType = repository?.Type;
			_delegateSettings = repository?.DelegateSettings;
			return this;
		}
	}

	internal class SourceOnlyRepositoryFormatter : IJsonFormatter<ISourceOnlyRepository>
	{
		private static readonly AutomataDictionary Fields = new AutomataDictionary
		{
			{ "type", 0 },
			{ "settings", 1 }
		};

		private static readonly byte[] DelegateType = JsonWriter.GetEncodedPropertyNameWithoutQuotation("delegate_type");

		public void Serialize(ref JsonWriter writer, ISourceOnlyRepository value, IJsonFormatterResolver formatterResolver)
		{
			if (value.DelegateType.IsNullOrEmpty())
			{
				writer.WriteNull();
				return;
			}
			writer.WriteBeginObject();
			writer.WritePropertyName("type");
			writer.WriteString("source");
			if (value.DelegateSettings != null)
			{
				writer.WriteValueSeparator();
				writer.WritePropertyName("settings");
				writer.WriteBeginObject();
				writer.WritePropertyName("delegate_type");
				writer.WriteString(value.DelegateType);
				writer.WriteValueSeparator();

				var innerWriter = new JsonWriter();
				switch (value.DelegateType)
				{
					case "s3":
						Serialize<IS3RepositorySettings>(ref innerWriter, value.DelegateSettings, formatterResolver);
						break;
					case "azure":
						Serialize<IAzureRepositorySettings>(ref innerWriter, value.DelegateSettings, formatterResolver);
						break;
					case "url":
						Serialize<IReadOnlyUrlRepositorySettings>(ref innerWriter, value.DelegateSettings, formatterResolver);
						break;
					case "hdfs":
						Serialize<IHdfsRepositorySettings>(ref innerWriter, value.DelegateSettings, formatterResolver);
						break;
					case "fs":
						Serialize<IFileSystemRepositorySettings>(ref innerWriter, value.DelegateSettings, formatterResolver);
						break;
					default:
						Serialize<IRepositorySettings>(ref innerWriter, value.DelegateSettings, formatterResolver);
						break;
				}

				var buffer = innerWriter.GetBuffer();
				// get all the written bytes between the opening and closing {}
				for (var i = 1; buffer.Array != null && i < buffer.Count - 1; i++)
					writer.WriteRawUnsafe(buffer.Array[i]);

				writer.WriteEndObject();
			}
			writer.WriteEndObject();
		}

		private static void Serialize<TRepositorySettings>(ref JsonWriter writer, object value, IJsonFormatterResolver formatterResolver)
			where TRepositorySettings : class, IRepositorySettings
		{
			var formatter = formatterResolver.GetFormatter<TRepositorySettings>();
			formatter.Serialize(ref writer, value as TRepositorySettings, formatterResolver);
		}

		private static TRepositorySettings Deserialize<TRepositorySettings>(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
			where TRepositorySettings : class, IRepositorySettings
		{
			var formatter = formatterResolver.GetFormatter<TRepositorySettings>();
			return formatter.Deserialize(ref reader, formatterResolver);
		}

		public ISourceOnlyRepository Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.GetCurrentJsonToken() != JsonToken.BeginObject)
			{
				reader.ReadNextBlock();
				return null;
			}

			var count = 0;
			ArraySegment<byte> settings = default;

			while (reader.ReadIsInObject(ref count))
			{
				var propertyName = reader.ReadPropertyNameSegmentRaw();
				if (Fields.TryGetValue(propertyName, out var value))
				{
					switch (value)
					{
						case 0:
							reader.ReadNext();
							break;
						case 1:
							settings = reader.ReadNextBlockSegment();
							break;
					}

				}
				else
					reader.ReadNextBlock();
			}

			if (settings == default)
				return null;

			var segmentReader = new JsonReader(settings.Array, settings.Offset);
			string delegateType = null;
			object delegateSettings = null;

			// reset count to zero to so that ReadIsInObject skips opening brace
			count = 0;
			while (segmentReader.ReadIsInObject(ref count))
			{
				var propertyName = segmentReader.ReadPropertyNameSegmentRaw();
				if (propertyName.EqualsBytes(DelegateType))
				{
					delegateType = segmentReader.ReadString();
					break;
				}

				segmentReader.ReadNextBlock();
			}

			// reset the offset
			segmentReader.ResetOffset();

			switch (delegateType)
			{
				case "s3":
					delegateSettings = Deserialize<S3RepositorySettings>(ref segmentReader, formatterResolver);
					break;
				case "azure":
					delegateSettings = Deserialize<AzureRepositorySettings>(ref segmentReader, formatterResolver);
					break;
				case "url":
					delegateSettings = Deserialize<ReadOnlyUrlRepositorySettings>(ref segmentReader, formatterResolver);
					break;
				case "hdfs":
					delegateSettings = Deserialize<HdfsRepositorySettings>(ref segmentReader, formatterResolver);
					break;
				case "fs":
					delegateSettings = Deserialize<FileSystemRepositorySettings>(ref segmentReader, formatterResolver);
					break;
			}

			return new SourceOnlyRepository(delegateType, delegateSettings);
		}
	}
}
