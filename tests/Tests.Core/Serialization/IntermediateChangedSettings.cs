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
using Nest;

namespace Tests.Core.Serialization 
{
	public class IntermediateChangedSettings
	{
		private readonly Func<ConnectionSettings, ConnectionSettings> _connectionSettingsModifier;
		private IPropertyMappingProvider _propertyMappingProvider;
		private ConnectionSettings.SourceSerializerFactory _sourceSerializerFactory;

		internal IntermediateChangedSettings(Func<ConnectionSettings, ConnectionSettings> settings) => _connectionSettingsModifier = settings;

		public IntermediateChangedSettings WithSourceSerializer(ConnectionSettings.SourceSerializerFactory factory)
		{
			_sourceSerializerFactory = factory;
			return this;
		}

		public IntermediateChangedSettings WithPropertyMappingProvider(IPropertyMappingProvider propertyMappingProvider)
		{
			_propertyMappingProvider = propertyMappingProvider;
			return this;
		}

		public JsonRoundTripper Expect(object expected, bool preserveNullInExpected = false) =>
			new JsonRoundTripper(expected, _connectionSettingsModifier, _sourceSerializerFactory, _propertyMappingProvider,
				preserveNullInExpected);

		public ObjectRoundTripper<T> Object<T>(T expected) =>
			new ObjectRoundTripper<T>(expected, _connectionSettingsModifier, _sourceSerializerFactory, _propertyMappingProvider);
	}
}