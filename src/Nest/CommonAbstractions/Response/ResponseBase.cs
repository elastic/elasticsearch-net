using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public interface IResponse : IBodyWithApiCallDetails
	{
		[JsonIgnore]
		bool IsValid { get; }

		[JsonIgnore]
		IApiCallDetails ApiCall { get; }

		[JsonIgnore]
		ServerError ServerError { get; }

		[JsonIgnore]
		Exception OriginalException { get; }

		[JsonIgnore]
		string DebugInformation { get; }

	}

	public abstract class ResponseBase : IResponse
	{
		public virtual bool IsValid => (this.ApiCall?.Success ?? false) && (this.ServerError == null);

		IApiCallDetails IBodyWithApiCallDetails.CallDetails { get; set; }

		public virtual IApiCallDetails ApiCall => ((IBodyWithApiCallDetails)this).CallDetails;

		public virtual ServerError ServerError => this.ApiCall?.ServerError;

		public Exception OriginalException => this.ApiCall?.OriginalException;

		public string DebugInformation
		{
			get
			{
				var sb = new StringBuilder();
				sb.Append($"{(!this.IsValid ? "Inv" : "V")}alid NEST response built from a ");
				sb.AppendLine(this.ApiCall?.ToString().ToCamelCase() ?? "null ApiCall which is highly exceptional, please open a bug if you see this");
				if (!this.IsValid) this.DebugIsValid(sb);
				if (this.ApiCall != null) ResponseStatics.DebugInformationBuilder(ApiCall, sb);
				return sb.ToString();
			}
		}
		protected virtual void DebugIsValid(StringBuilder sb) { }

		public override string ToString() =>  $"{(!this.IsValid ? "Inv" : "V")}alid NEST response built from a {this.ApiCall?.ToString().ToCamelCase()}";

	}
}
