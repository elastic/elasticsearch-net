/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

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
