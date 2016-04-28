using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Tests.Framework.Integration
{
	public class ElasticsearchConsoleOut : ConsoleOut
	{
		/*
[2015-05-26 20:05:07,681][INFO ][node                     ] [Nick Fury] version[1.5.2], pid[7704], build[62ff986/2015-04-27T09:21:06Z]
[2015-05-26 20:05:07,681][INFO ][node                     ] [Nick Fury] initializing ...
[2015-05-26 20:05:07,681][INFO ][plugins                  ] [Nick Fury] loaded [], sites []
[2015-05-26 20:05:10,790][INFO ][node                     ] [Nick Fury] initialized
[2015-05-26 20:05:10,821][INFO ][node                     ] [Nick Fury] starting ...
[2015-05-26 20:05:11,041][INFO ][transport                ] [Nick Fury] bound_address {inet[/0:0:0:0:0:0:0:0:9300]}, publish_address {inet[/192.168.194.146:9300]}
[2015-05-26 20:05:11,056][INFO ][discovery                ] [Nick Fury] elasticsearch-martijnl/yuiyXva3Si6sQE5tY_9CHg
[2015-05-26 20:05:14,103][INFO ][cluster.service          ] [Nick Fury] new_master [Nick Fury][yuiyXva3Si6sQE5tY_9CHg][WIN-DK60SLEMH8C][inet[/192.168.194.146:9300]], reason: zen-disco-join (elected_as_master)
[2015-05-26 20:05:14,134][INFO ][gateway                  ] [Nick Fury] recovered [0] indices into cluster_state
[2015-05-26 20:05:14,150][INFO ][http                     ] [Nick Fury] bound_address {inet[/0:0:0:0:0:0:0:0:9200]}, publish_address {inet[/192.168.194.146:9200]}
[2015-05-26 20:05:14,150][INFO ][node                     ] [Nick Fury] started
*/

		public DateTime Date { get; }
		public string Level { get; }
		public string Section { get; }
		public string Node { get; }
		public string Message { get; }


		private static readonly Regex ConsoleLineParser =
			new Regex(@"\[(?<date>.*?)\]\[(?<level>.*?)\]\[(?<section>.*?)\] \[(?<node>.*?)\] (?<message>.+)");

		public ElasticsearchConsoleOut(bool error, string consoleLine) : base(error, consoleLine)
		{
			Console.WriteLine(consoleLine);
			if (string.IsNullOrEmpty(consoleLine)) return;
			var match = ConsoleLineParser.Match(consoleLine);
			if (!match.Success) return;
			var dateString = match.Groups["date"].Value.Trim();
			Date = DateTime.ParseExact(dateString, "yyyy-MM-dd HH:mm:ss,fff", CultureInfo.CurrentCulture);
			Level = match.Groups["level"].Value.Trim();
			Section = match.Groups["section"].Value.Trim().Replace("org.elasticsearch.", "");
			Node = match.Groups["node"].Value.Trim();
			Message = match.Groups["message"].Value.Trim();
		}

		private static readonly Regex InfoParser =
			new Regex(@"version\[(?<version>.*)\], pid\[(?<pid>.*)\], build\[(?<build>.+)\]");

		public bool TryParseNodeInfo(out string version, out int? pid)
		{
			version = null;
			pid = null;
			if (this.Section != "node") return false;

			var match = InfoParser.Match(this.Message);
			if (!match.Success) return false;

			version = match.Groups["version"].Value.Trim();
			pid = int.Parse(match.Groups["pid"].Value.Trim());
			return true;
		}

		public bool TryGetStartedConfirmation()
		{
			if (this.Section != "node") return false;
			return this.Message == "started";
		}

		private static readonly Regex PortParser =
			new Regex(@"bound_address(es)? {.+\:(?<port>\d+)}");

		public bool TryGetPortNumber(out int port)
		{
			port = 0;
			if (this.Section != "http") return false;

			var match = PortParser.Match(this.Message);
			if (!match.Success) return false;

			var portString = match.Groups["port"].Value.Trim();
			port = int.Parse(portString);
			return true;
		}
	}
}
