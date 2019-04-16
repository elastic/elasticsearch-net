using System;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{

	[JsonFormatter(typeof(SourceOnlyRepositoryFormatter))]
	public interface ISourceOnlyRepository : IRepositoryWithSettings
	{
		[IgnoreDataMember]
		string DelegateType { get; }
	}

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

		/// <inheritdoc cref="CreateRepositoryDescriptor.ReadOnlyUrl" />
		public SourceOnlyRepositoryDescriptor Azure(Func<AzureRepositoryDescriptor, IAzureRepository> selector = null) =>
			DelegateTo(selector);

		/// <inheritdoc cref="CreateRepositoryDescriptor.ReadOnlyUrl" />
		public SourceOnlyRepositoryDescriptor Hdfs(Func<HdfsRepositoryDescriptor, IHdfsRepository> selector) =>
			DelegateTo(selector);

		/// <inheritdoc cref="CreateRepositoryDescriptor.ReadOnlyUrl" />
		public SourceOnlyRepositoryDescriptor S3(Func<S3RepositoryDescriptor, IS3Repository> selector) =>
			DelegateTo(selector);

		/// <inheritdoc cref="CreateRepositoryDescriptor.ReadOnlyUrl" />
		public SourceOnlyRepositoryDescriptor Custom(IRepositoryWithSettings repository)
		{
			_delegateType = repository?.Type;
			_delegateSettings = repository?.DelegateSettings;
			return this;
		}
	}

	internal class SourceOnlyRepositoryFormatter : IJsonFormatter<ISourceOnlyRepository>
	{
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
				for (var i = 1; i < buffer.Count - 1; i++)
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

		public ISourceOnlyRepository Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.GetCurrentJsonToken() != JsonToken.BeginObject)
			{
				reader.ReadNextBlock();
				return null;
			}

			//TODO read delegate type and settings

			return new SourceOnlyRepository("fs", null);
		}

	}
}
