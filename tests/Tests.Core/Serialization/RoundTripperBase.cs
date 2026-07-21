// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using Elastic.Clients.Elasticsearch;
using Tests.Core.Client.Settings;
using Tests.Domain.Extensions;

namespace Tests.Core.Serialization
{
	public abstract class RoundTripperBase
	{
		internal RoundTripperBase(
			Func<ElasticsearchClientSettings, ElasticsearchClientSettings> settingsModifier = null,
			ElasticsearchClientSettings.SourceSerializerFactory sourceSerializerFactory = null,
			IPropertyMappingProvider propertyMappingProvider = null,
			bool preserveNullInExpected = false
		)
		{
			PreserveNullInExpected = preserveNullInExpected;
			if (settingsModifier == null && sourceSerializerFactory == null && propertyMappingProvider == null)
				Tester = SerializationTester.Default;
			else
			{
				var settings =
					new AlwaysInMemoryElasticsearchClientSettings(sourceSerializerFactory: sourceSerializerFactory,
							propertyMappingProvider: propertyMappingProvider)
						.ApplyDomainSettings();

				if (settingsModifier != null)
					settings = settingsModifier(settings);
				Tester = new SerializationTester(new ElasticsearchClient(settings));
			}
		}

		protected bool PreserveNullInExpected { get; set; }
		protected SerializationTester Tester { get; }
	}
}
