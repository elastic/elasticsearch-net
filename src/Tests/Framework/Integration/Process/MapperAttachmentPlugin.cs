using System.Collections.Generic;

namespace Tests.Framework.Integration
{
	public class MapperAttachmentPlugin
	{
		private static readonly Dictionary<string, string> Versions = new Dictionary<string, string>
		{
			{"2.1.2", "elasticsearch/elasticsearch-mapper-attachments/3.1.2"},
			{"2.1.1", "elasticsearch/elasticsearch-mapper-attachments/3.1.1"},
			{"2.1.0", "elasticsearch/elasticsearch-mapper-attachments/3.1.0"},
			{"2.0.2", "elasticsearch/elasticsearch-mapper-attachments/3.0.4"},
			{"2.0.1", "elasticsearch/elasticsearch-mapper-attachments/3.0.3"},
			{"2.0.0", "elasticsearch/elasticsearch-mapper-attachments/3.0.2"},
			{"2.0.0-beta2", "elasticsearch/elasticsearch-mapper-attachments/3.0.1"},
			{"2.0.0-beta1", "elasticsearch/elasticsearch-mapper-attachments/3.0.0"},
			{"1.7", "elasticsearch/elasticsearch-mapper-attachments/2.7.1"},
			{"1.6", "elasticsearch/elasticsearch-mapper-attachments/2.6.0"},
			{"1.5", "elasticsearch/elasticsearch-mapper-attachments/2.5.0"},
			{"1.4", "elasticsearch/elasticsearch-mapper-attachments/2.4.3"},
			{"1.3", "elasticsearch/elasticsearch-mapper-attachments/2.3.2"},
			{"1.2", "elasticsearch/elasticsearch-mapper-attachments/2.2.1"},
			{"1.1", "elasticsearch/elasticsearch-mapper-attachments/2.0.0"},
			{"1.0", "elasticsearch/elasticsearch-mapper-attachments/2.0.0"},
			{"0.90", "elasticsearch/elasticsearch-mapper-attachments/1.9.0"}
		};

		public static string GetVersion(string elasticsearchVersion)
		{
			string attachmentVersion;
			if (!Versions.TryGetValue(elasticsearchVersion, out attachmentVersion))
			{
				// assume latest version in elasticsearch repository
				attachmentVersion = "mapper-attachments";
			}

			return attachmentVersion;
		}
	}
}