using System.Collections.Generic;
using System.IO;
using System.Linq;
using Humanizer;

namespace Benchmarking
{
	public class Results
	{
		public string TesterName { get; set; }

		public double RatePerSecond { get; set; }

		public double ElapsedMillisecond { get; set; }

		public int IndexedDocuments { get; set; }

		public IEnumerable<int> EsTimings { get; set; }

		public Metrics Before { get; set; }

		public Metrics After { get; set; }

		public void Write(TextWriter output)
		{
			output.WriteLine("---{0}\t---------------", TesterName);
			output.WriteLine("  {0:0,0} msg/s {1} ms {2} docs", RatePerSecond, ElapsedMillisecond, IndexedDocuments);
			output.WriteLine("  max es-time:{0} mean es-time:{1}", EsTimings.Max(), EsTimings.GetMedian());
			output.WriteLine("  memory before:{0} thread count before:{1}", Before.MemorySize.Bytes(), Before.ThreadCount);
			output.WriteLine("  memory after:{0} thread count after:{1}", After.MemorySize.Bytes(), After.ThreadCount);
		}
	}
}