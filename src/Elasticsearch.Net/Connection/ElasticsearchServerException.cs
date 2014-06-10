using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace Elasticsearch.Net.Connection
{
	internal class OneToOneServerException
	{
		public int status { get; set; }
		public string error { get; set; }
	}
	
	public class ElasticsearchServerError
	{
		public int Status { get; set; }
		public string Error { get; set; }
		public string ExceptionType { get; set; }

		internal static ElasticsearchServerError Create(OneToOneServerException e)
		{
			if (e == null) return null;
			return new ElasticsearchServerError
			{
				Status = e.status,
				Error = e.error
			};
		}
	}

	public class ElasticsearchServerException : Exception
	{
		private static readonly Regex ExceptionSplitter = new Regex(@"^([^\[]*?)\[(.*)\]", RegexOptions.Singleline);
		private static readonly string _couldNotParseServerException = "Could not parse server exception";

		public int Status { get; set; }
		public string ExceptionType { get; set; }
		public ElasticsearchServerException(ElasticsearchServerError error) : base(ParseError(error))
		{
			this.Status = error.Status;
			this.ExceptionType = error.ExceptionType;
		}
		//iffy side effect assignment to exceptionType needed so that we simply return message to the 
		//base constructor.
		private static string ParseError(ElasticsearchServerError error)
		{
			if (error == null) return _couldNotParseServerException;
			if (error.Error.IsNullOrEmpty()) return _couldNotParseServerException;
			var matches = ExceptionSplitter.Match(error.Error);
			if (matches.Groups.Count != 3) return _couldNotParseServerException;

			error.ExceptionType = matches.Groups[1].Value;
			return matches.Groups[2].Value;
		}
	}
}
