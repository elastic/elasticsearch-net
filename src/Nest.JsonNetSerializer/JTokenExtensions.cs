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

using System.IO;
using Elastic.Transport;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest.JsonNetSerializer
{
	internal static class JTokenExtensions
	{
		/// <summary>
		/// Writes a <see cref="JToken" /> to a <see cref="MemoryStream" /> using <see cref="ConnectionSettingsAwareSerializerBase.ExpectedEncoding" />
		/// </summary>
		public static MemoryStream ToStream(this JToken token, IMemoryStreamFactory memoryStreamFactory)
		{
			var ms = memoryStreamFactory.Create();
			using (var streamWriter = new StreamWriter(ms, ConnectionSettingsAwareSerializerBase.ExpectedEncoding,
				ConnectionSettingsAwareSerializerBase.DefaultBufferSize, true))
			using (var writer = new JsonTextWriter(streamWriter))
			{
				token.WriteTo(writer);
				writer.Flush();
				ms.Position = 0;
				return ms;
			}
		}
	}
}
