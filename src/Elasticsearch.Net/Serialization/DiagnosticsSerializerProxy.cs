using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net.Diagnostics;
using Elasticsearch.Net.Utf8Json;

namespace Elasticsearch.Net
{
	public class SerializerRegistrationInformation
	{
		private readonly string _stringRepresentation;

		public SerializerRegistrationInformation(Type type, string purpose)
		{
			TypeInformation = type;
			Purpose = purpose;
			_stringRepresentation = $"{Purpose}: {TypeInformation.FullName}";
		}


		public Type TypeInformation { get; }

		/// <summary>
		/// A string describing the purpose of the serializer emitting this events.
		/// <para>In `Elastisearch.Net` this will always be "request/response"</para>
		/// <para>Using `Nest` this could also be `source` allowing you to differentiate between the internal and configured source serializer</para>
		/// </summary>
		public string Purpose { get; }

		public override string ToString() => _stringRepresentation;
	}

	//TODO this no longer needs to be IInternalSerializerWithFormatter
	// Previous we checked that the serializer we are wrapping implements the interface and if so set the formatter
	// However its totally fine for formatter to be null. `IJsonFormatter` solves this better.
	/// <summary>
	/// Wraps configured serializer so that we can emit diagnostics per configured serializer.
	/// </summary>
	internal class DiagnosticsSerializerProxy : IElasticsearchSerializer, IInternalSerializerWithFormatter, IInternalSerializer
	{
		private readonly IElasticsearchSerializer _serializer;
		private readonly bool _wrapsUtf8JsonSerializer;
		private readonly SerializerRegistrationInformation _state;
		private readonly IJsonFormatterResolver _formatterResolver;
		private static DiagnosticSource DiagnosticSource { get; } = new DiagnosticListener(DiagnosticSources.Serializer.SourceName);

		public DiagnosticsSerializerProxy(IElasticsearchSerializer serializer, string purpose = "request/response")
		{
			_serializer = serializer;
			_state = new SerializerRegistrationInformation(serializer.GetType(), purpose);
			if (serializer is IInternalSerializerWithFormatter withFormatter)
			{
				_formatterResolver = withFormatter.FormatterResolver;
				_wrapsUtf8JsonSerializer = true;
			}
			else
			{
				_wrapsUtf8JsonSerializer = false;
				_formatterResolver = null;
			}
		}

		public IJsonFormatterResolver FormatterResolver => _formatterResolver;

		public bool TryGetJsonFormatter(out IJsonFormatterResolver formatterResolver)
		{
			formatterResolver = _formatterResolver;
			return _wrapsUtf8JsonSerializer;
		}

		public object Deserialize(Type type, Stream stream)
		{
			using (DiagnosticSource.Diagnose(DiagnosticSources.Serializer.Deserialize, _state))
				return _serializer.Deserialize(type, stream);
		}


		public T Deserialize<T>(Stream stream)
		{
			using (DiagnosticSource.Diagnose(DiagnosticSources.Serializer.Deserialize, _state))
				return _serializer.Deserialize<T>(stream);
		}

		public Task<object> DeserializeAsync(Type type, Stream stream, CancellationToken cancellationToken = default)
		{
			using (DiagnosticSource.Diagnose(DiagnosticSources.Serializer.Deserialize, _state))
				return _serializer.DeserializeAsync(type, stream, cancellationToken);
		}

		public Task<T> DeserializeAsync<T>(Stream stream, CancellationToken cancellationToken = default)
		{
			using (DiagnosticSource.Diagnose(DiagnosticSources.Serializer.Deserialize, _state))
				return _serializer.DeserializeAsync<T>(stream, cancellationToken);
		}

		public void Serialize<T>(T data, Stream stream, SerializationFormatting formatting = SerializationFormatting.None)
		{
			using (DiagnosticSource.Diagnose(DiagnosticSources.Serializer.Serialize, _state))
				_serializer.Serialize<T>(data, stream, formatting);
		}

		public Task SerializeAsync<T>(T data, Stream stream, SerializationFormatting formatting = SerializationFormatting.None,
			CancellationToken cancellationToken = default
		)
		{
			using (DiagnosticSource.Diagnose(DiagnosticSources.Serializer.Serialize, _state))
				return _serializer.SerializeAsync<T>(data, stream, formatting, cancellationToken);
		}

	}
}
