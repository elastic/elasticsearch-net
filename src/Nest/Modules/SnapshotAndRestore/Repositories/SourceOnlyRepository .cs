using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{

	[JsonConverter(typeof(SourceOnlyRepositorySerializer))]
	public interface ISourceOnlyRepository : IRepositoryWithSettings
	{
		[JsonIgnore]
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

	internal class SourceOnlyRepositorySerializer : ReserializeJsonConverter<SourceOnlyRepository, ISourceOnlyRepository>
	{
		protected override void SerializeJson(JsonWriter writer, object value, ISourceOnlyRepository castValue, JsonSerializer serializer)
		{
			if (castValue.DelegateType.IsNullOrEmpty())
			{
				writer.WriteNull();
				return;
			}
			writer.WriteStartObject();
			writer.WriteProperty(serializer, "type", "source");
			if (castValue.DelegateSettings != null)
			{
				writer.WritePropertyName("settings");
				writer.WriteStartObject();
				writer.WriteProperty(serializer, "delegate_type", castValue.DelegateType);
				var properties = castValue.DelegateSettings.GetType().GetCachedObjectProperties();
				foreach (var p in properties)
				{
					if (p.Ignored) continue;

					var vv = p.ValueProvider.GetValue(castValue.DelegateSettings);
					if (vv == null) continue;

					writer.WritePropertyName(p.PropertyName);
					serializer.Serialize(writer, vv);
				}
				writer.WriteEndObject();
			}
			writer.WriteEndObject();
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType != JsonToken.StartObject) return null;

			var o = JObject.Load(reader);
			if (o == null) return null;

			if (!o.TryGetValue("settings", out var token))
				return null;

			if (!(token is JObject settingsObject))
				return null;

			if (!settingsObject.TryGetValue("delegate_type", out var delegateTypeToken))
				return null;

			settingsObject.Remove("delegate_type");

			var settings = settingsObject.ToObject<object>(serializer);
			return new SourceOnlyRepository(delegateTypeToken.Value<string>(), settings);
		}
	}
}
