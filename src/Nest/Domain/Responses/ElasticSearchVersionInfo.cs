using System;
using Newtonsoft.Json;

namespace Nest
{
    public interface IElasticSearchVersionInfo
    {
        string Number { get; set; }
        DateTime Date { get; set; }
        bool IsSnapShotBuild { get; set; }
    }

    public class ElasticSearchVersionInfo : IElasticSearchVersionInfo
    {
		public string Number { get; set; }
		public DateTime Date { get; set; }
		[JsonProperty(PropertyName = "snapshot_build")]
		public bool IsSnapShotBuild { get; set; }
	}
}
