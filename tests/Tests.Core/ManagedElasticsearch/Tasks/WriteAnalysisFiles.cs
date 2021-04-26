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
using System.IO;
using Elastic.Elasticsearch.Ephemeral;
using Elastic.Elasticsearch.Ephemeral.Tasks;
using Elastic.Elasticsearch.Managed.ConsoleWriters;
using Tests.Core.ManagedElasticsearch.Clusters;

namespace Tests.Core.ManagedElasticsearch.Tasks
{
	public class WriteAnalysisFiles : ClusterComposeTask
	{
		public override void Run(IEphemeralCluster<EphemeralClusterConfiguration> cluster)
		{
			if (!(cluster.ClusterConfiguration is ClientTestClusterConfiguration config)) return;

			var analysisPath = config.AnalysisFolder;
			if (!Directory.Exists(analysisPath)) Directory.CreateDirectory(analysisPath);
			cluster.Writer.WriteDiagnostic($"{{{nameof(WriteAnalysisFiles)}}} writing analysis files to watcher config [{analysisPath}]");
			SetupHunspellFiles(cluster.FileSystem.ConfigPath);
			SetupIcuFiles(cluster.FileSystem.ConfigPath);
			SetupCompoundTokenFilterFopFile(analysisPath);
			SetupCustomStemming(analysisPath);
			SetupStopwordsFile(analysisPath);
		}

		private static void SetupStopwordsFile(string analysisPath) =>
			WriteFileIfNotExist(Path.Combine(analysisPath, "stopwords") + ".txt", "");

		private static void SetupCustomStemming(string analysisPath) =>
			WriteFileIfNotExist(Path.Combine(analysisPath, "custom_stems") + ".txt", "");

		private static void SetupCompoundTokenFilterFopFile(string analysisPath) =>
			WriteFileIfNotExist(Path.Combine(analysisPath, "fop") + ".xml", "<languages-info />");

		private static void SetupHunspellFiles(string configPath)
		{
			var hunspellFolder = Path.Combine(configPath, "hunspell", "en_US");
			var hunspellPrefix = Path.Combine(hunspellFolder, "en_US");
			if (File.Exists(hunspellPrefix + ".dic")) return;

			Directory.CreateDirectory(hunspellFolder);
			File.WriteAllText(hunspellPrefix + ".dic", $"1{Environment.NewLine}abcdegf");
			File.WriteAllText(hunspellPrefix + ".aff", $"SET UTF8{Environment.NewLine}SFX P Y 1{Environment.NewLine}SFX P 0 s");
		}

		private static void SetupIcuFiles(string configPath)
		{
			var icuFolder = Path.Combine(configPath, "icu-files");
			if (!Directory.Exists(icuFolder)) Directory.CreateDirectory(icuFolder);

			var icuFile = Path.Combine(icuFolder, "KeywordTokenizer") + ".rbbi";
			if (!File.Exists(icuFile)) File.WriteAllText(icuFile, ".+ {200};");
		}
	}
}
