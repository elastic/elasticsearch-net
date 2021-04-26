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

namespace Tests.Configuration
{
	public class EnvironmentConfiguration : TestConfigurationBase
	{
		public EnvironmentConfiguration(YamlConfiguration yamlConfiguration)
		{
			Mode = Environment.GetEnvironmentVariable("NEST_INTEGRATION_TEST") != null ? TestMode.Integration : TestMode.Unit;

			ClusterFilter = Environment.GetEnvironmentVariable("NEST_INTEGRATION_CLUSTER");
			TestFilter = Environment.GetEnvironmentVariable("NEST_TEST_FILTER");

			var version = Environment.GetEnvironmentVariable("NEST_INTEGRATION_VERSION");
			ElasticsearchVersion = string.IsNullOrWhiteSpace(version) ? yamlConfiguration.ElasticsearchVersion : version;
			if (ElasticsearchVersion == null)
				throw new Exception("Elasticsearch Version could not be determined from env var NEST_INTEGRATION_VERSION nor the test yaml configuration");

			var externalSeed = TryGetEnv("NEST_TEST_SEED", out var seed)
				? int.Parse(seed)
				: yamlConfiguration.SeedProvidedExternally
					? yamlConfiguration.Seed
					: (int?)null;
			SetExternalSeed(externalSeed, out var randomizer);

			TestOnlyOne = RandomBoolConfig("TEST_ONLY_ONE", randomizer, false);
			Random = new RandomConfiguration
			{
				SourceSerializer = RandomBoolConfig("SOURCESERIALIZER", randomizer),
				TypedKeys = RandomBoolConfig("TYPEDKEYS", randomizer),
				HttpCompression = RandomBoolConfig("HTTPCOMPRESSION", randomizer),
				ApiVersioning = ElasticsearchVersion.InRange("<7.12.0") ? false : RandomBoolConfig("APIVERSIONING", randomizer),
			};
		}

		private static bool RandomBoolConfig(string key, Random randomizer, bool? @default = null)
		{
			if (TryGetEnv("NEST_RANDOM_" + key, out var source) && bool.TryParse(source, out var b))
				return b;

			return @default ?? randomizer.NextDouble() >= 0.5;
		}

		private static bool TryGetEnv(string key, out string value)
		{
			value = Environment.GetEnvironmentVariable(key);
			return !string.IsNullOrWhiteSpace(value);
		}
	}
}
