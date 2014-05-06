using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace Elasticsearch.Net.Connection
{
	public class ElasticsearchServerError
	{
		public int Status { get; set; }
		public string Error { get; set; }
	
	}

	public class ElasticsearchServerException : Exception
	{
		private static Regex ExceptionSplitter = new Regex(@"^([^\[]*?)\[(.*)\]", RegexOptions.Singleline);

		public int Status { get; set; }
		public string Error { get; set; }
		public string ExceptionType { get; set; }
		public ElasticsearchServerException(ElasticsearchServerError error)
		{
			if (error == null) return;
			this.Status = error.Status;
			if (error.Error.IsNullOrEmpty()) return;
			var matches = ExceptionSplitter.Match(error.Error);
			if (matches.Groups.Count == 3)
			{
				this.Error = matches.Groups[2].Value;
				this.ExceptionType = matches.Groups[1].Value;
			}
			else this.Error = error.Error;
		}
	}
}
