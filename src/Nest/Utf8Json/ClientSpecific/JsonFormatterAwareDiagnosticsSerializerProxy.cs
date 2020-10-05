// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Transport;

namespace Nest.Utf8Json
{
	/// <summary>
	/// A specialized serializer proxy that is <see cref="IJsonFormatter"/> aware.
	/// We wrap serializer so we can emit diagnostics however <see cref="IInternalSerializer"/> defines
	/// <see cref="TryGetJsonFormatter"/> which allows us to reuse the formatter IF the serializer being used is the default one.
	/// </summary>
	internal class JsonFormatterAwareDiagnosticsSerializerProxy : DiagnosticsSerializerProxy, IInternalSerializer
	{
		private readonly bool _wrapsUtf8JsonSerializer;
		private readonly IJsonFormatterResolver _formatterResolver;

		public JsonFormatterAwareDiagnosticsSerializerProxy(ITransportSerializer serializer, string purpose = "request/response")
			: base(serializer, purpose)
		{
			if (serializer is IInternalSerializer s && s.TryGetJsonFormatter(out var formatterResolver))
			{
				_formatterResolver = formatterResolver;
				_wrapsUtf8JsonSerializer = true;
			}
			else
			{
				_formatterResolver = null;
				_wrapsUtf8JsonSerializer = false;
			}
		}

		public bool TryGetJsonFormatter(out IJsonFormatterResolver formatterResolver)
		{
			formatterResolver = _formatterResolver;
			return _wrapsUtf8JsonSerializer;
		}
	}
}
