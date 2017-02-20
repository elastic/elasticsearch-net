using System.IO;
using Tests.Framework.Configuration;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Nodes;

namespace Tests.Framework.ManagedElasticsearch.Tasks.InstallationTasks
{
	public class WriteAnalysisFiles : InstallationTaskBase
	{
		public override void Run(NodeConfiguration config, NodeFileSystem fileSystem)
		{
			var analysisPath = fileSystem.AnalysisFolder;
			if (!Directory.Exists(analysisPath)) Directory.CreateDirectory(analysisPath);
			SetupHunspellFiles(fileSystem.ConfigPath);
			SetupIcuFiles(fileSystem.ConfigPath);
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
			File.WriteAllText(hunspellPrefix + ".dic", "1\r\nabcdegf");
			File.WriteAllText(hunspellPrefix + ".aff", "SET UTF8\r\nSFX P Y 1\r\nSFX P 0 s");
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
