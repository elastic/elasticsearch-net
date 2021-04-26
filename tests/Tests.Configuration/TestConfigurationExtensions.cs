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
	public static class TestConfigurationExtensions
	{
		public static void DumpConfiguration(this TestConfigurationBase config)
		{
			Console.WriteLine(new string('-', 20));
			Console.WriteLine("Starting tests using config:");
			Console.WriteLine($" - {nameof(config.TestAgainstAlreadyRunningElasticsearch)}: {config.TestAgainstAlreadyRunningElasticsearch}");
			Console.WriteLine($" - {nameof(config.ElasticsearchVersion)}: {config.ElasticsearchVersion}");
			Console.WriteLine($" - {nameof(config.Mode)}: {config.Mode}");
			Console.WriteLine($" - {nameof(config.Seed)}: {config.Seed}");
			Console.WriteLine($" - {nameof(config.ForceReseed)}: {config.ForceReseed}");
			Console.WriteLine($" - {nameof(config.TestOnlyOne)}: {config.TestOnlyOne}");
			if (config.Mode == TestMode.Integration)
			{
				Console.WriteLine($" - {nameof(config.ClusterFilter)}: {config.ClusterFilter}");
				Console.WriteLine($" - {nameof(config.TestFilter)}: {config.TestFilter}");
			}
			Console.WriteLine($" - {nameof(config.RunIntegrationTests)}: {config.RunIntegrationTests}");
			Console.WriteLine($" - {nameof(config.RunUnitTests)}: {config.RunUnitTests}");
			Console.WriteLine($" - Random:");
			Console.WriteLine($" \t- {nameof(config.Random.SourceSerializer)}: {config.Random.SourceSerializer}");
			Console.WriteLine($" \t- {nameof(config.Random.TypedKeys)}: {config.Random.TypedKeys}");
			Console.WriteLine($" \t- {nameof(config.Random.HttpCompression)}: {config.Random.HttpCompression}");
			Console.WriteLine(new string('-', 20));
		}
	}
}
