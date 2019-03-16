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
				writer.WritePropertyName("settings");
				writer.WriteBeginObject();
				writer.WritePropertyName("delegate_type");
				writer.WriteString(value.DelegateType);

				var settings = value.DelegateSettings as IRepositorySettings;
				var innerWriter = new JsonWriter();
				var formatter = DynamicObjectResolver.ExcludeNullCamelCase.GetFormatter<IRepositorySettings>();
				formatter.Serialize(ref innerWriter, settings, formatterResolver);

				var buffer = innerWriter.GetBuffer();
				// get all the written bytes except the closing }
				for (var i = buffer.Offset; i < buffer.Count - 1; i++)
					writer.WriteRawUnsafe(buffer.Array[i]);

				writer.WriteEndObject();
			}
			writer.WriteEndObject();
		}

		public ISourceOnlyRepository Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.GetCurrentJsonToken() != JsonToken.BeginObject) return null;

			//TODO read delegate type and settings

			return new SourceOnlyRepository("fs", null);
		}

	}
}
