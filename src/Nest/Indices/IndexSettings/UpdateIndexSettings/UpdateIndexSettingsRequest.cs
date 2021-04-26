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

using System;
using Nest.Utf8Json;

namespace Nest
{
	[MapsApi("indices.put_settings.json")]
	[JsonFormatter(typeof(UpdateIndexSettingsRequestFormatter))]
	public partial interface IUpdateIndexSettingsRequest
	{
		IDynamicIndexSettings IndexSettings { get; set; }
	}

	public partial class UpdateIndexSettingsRequest
	{
		public IDynamicIndexSettings IndexSettings { get; set; }
	}

	public partial class UpdateIndexSettingsDescriptor
	{
		IDynamicIndexSettings IUpdateIndexSettingsRequest.IndexSettings { get; set; }

		/// <inheritdoc />
		public UpdateIndexSettingsDescriptor IndexSettings(Func<DynamicIndexSettingsDescriptor, IPromise<IDynamicIndexSettings>> settings) =>
			Assign(settings, (a, v) => a.IndexSettings = v?.Invoke(new DynamicIndexSettingsDescriptor())?.Value);
	}

	internal class UpdateIndexSettingsRequestFormatter : IJsonFormatter<IUpdateIndexSettingsRequest>
	{
		private static readonly DynamicIndexSettingsFormatter DynamicIndexSettingsFormatter =
			new DynamicIndexSettingsFormatter();

		public IUpdateIndexSettingsRequest Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var dynamicSettings = DynamicIndexSettingsFormatter.Deserialize(ref reader, formatterResolver);
			return new UpdateIndexSettingsRequest { IndexSettings = dynamicSettings };
		}

		public void Serialize(ref JsonWriter writer, IUpdateIndexSettingsRequest value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			DynamicIndexSettingsFormatter.Serialize(ref writer, value.IndexSettings, formatterResolver);
		}
	}
}
