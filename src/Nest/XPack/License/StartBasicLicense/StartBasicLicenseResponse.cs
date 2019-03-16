using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	public interface IStartBasicLicenseResponse : IAcknowledgedResponse
	{
		[DataMember(Name = "basic_was_started")]
		bool BasicWasStarted { get; }

		[DataMember(Name = "error_message")]
		string ErrorMessage { get; }

		[DataMember(Name = "acknowledge")]
		StartBasicLicenseFeatureAcknowledgements Acknowledge { get; }
	}

	public class StartBasicLicenseResponse : AcknowledgedResponseBase, IStartBasicLicenseResponse
	{
		//TODO: make this the default on base class for 7.0 ?
		public override bool IsValid => base.IsValid && Acknowledged;

		public bool BasicWasStarted { get; internal set; }

		public string ErrorMessage { get; internal set; }

		public StartBasicLicenseFeatureAcknowledgements Acknowledge { get; internal set; }
	}

	// TODO this might need a new formatter
	//[JsonFormatter(typeof(StartBasicLicenseFeatureAcknowledgementsJsonConverter))]
	public class StartBasicLicenseFeatureAcknowledgements : ReadOnlyDictionary<string, string[]>
	{
		internal StartBasicLicenseFeatureAcknowledgements(IDictionary<string, string[]> dictionary)
			: base(dictionary) { }

		[DataMember(Name = "message")]
		public string Message { get; internal set; }

	}
}
