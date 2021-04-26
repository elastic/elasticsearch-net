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

using System.Collections.Generic;
using System.IO;
using System.Linq;
using Elastic.Elasticsearch.Ephemeral;
using Elastic.Elasticsearch.Ephemeral.Tasks;
using Elastic.Elasticsearch.Managed.ConsoleWriters;

namespace Tests.Core.ManagedElasticsearch.Tasks
{
	public class EnsureNativeSecurityRealmEnabledInElasticsearchYaml : ClusterComposeTask
	{
		public override void Run(IEphemeralCluster<EphemeralClusterConfiguration> cluster)
		{
			var configFile = Path.Combine(cluster.FileSystem.ConfigPath, "elasticsearch.yml");
			var lines = File.ReadAllLines(configFile).ToList();
			var modifiedLines = new List<string>();
			var modified = false;

			foreach (var line in lines)
			{
				modifiedLines.Add(line);

				if (line.Contains("realms:"))
				{
					var collection = cluster.ClusterConfiguration.Version >= "7.0.0-alpha1"
						? new[]
						{
							"        native.native1:",
							"          order: 2"
						}
						: new[]
						{
							"        native1:",
							"          type: native",
							"          order: 2"
						};

					modifiedLines.AddRange(collection);
					modified = true;
				}
			}

			if (!modified)
			{
				var collection = cluster.ClusterConfiguration.Version >= "7.0.0-alpha1"
					? new[]
					{
						string.Empty,
						"xpack:",
						"  security:",
						"    authc:",
						"      realms:",
						"        native1:",
						"          type: native",
						"          order: 0",
						string.Empty
					}
					: new[]
					{
						string.Empty,
						"xpack:",
						"  security:",
						"    authc:",
						"      realms:",
						"        native.native1:",
						"          order: 0",
						string.Empty
					};

				modifiedLines.AddRange(collection);
			}

			File.WriteAllLines(configFile, modifiedLines);
			cluster.Writer.WriteDiagnostic($"{{{nameof(EnsureNativeSecurityRealmEnabledInElasticsearchYaml)}}} native security realm [{configFile}]");
		}
	}
}
