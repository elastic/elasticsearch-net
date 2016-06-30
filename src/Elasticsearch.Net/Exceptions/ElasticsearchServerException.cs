using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Text.RegularExpressions;

namespace Elasticsearch.Net
{
	[Serializable]
	public class ElasticsearchServerException : Exception
	{
		private static readonly Regex ExceptionSplitter = new Regex(@"^([^\[]*?)\[(.*)\]", RegexOptions.Singleline);
		private static readonly string _couldNotParseServerException = "Could not parse server exception";

		public int Status { get; set; }
		public string ExceptionType { get; set; }
		internal ElasticsearchServerException(int status, string exceptionType)
		{
			this.Status = status;
			this.ExceptionType = exceptionType;
		}
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


		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		protected ElasticsearchServerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.Status = info.GetInt32("Status");
			this.ExceptionType = info.GetString("ExceptionType");
		}

		[SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null) throw new ArgumentNullException(nameof(info));

			info.AddValue("Status", this.Status);
			info.AddValue("ExceptionType", this.ExceptionType);
			base.GetObjectData(info, context);
		}
	}
}
