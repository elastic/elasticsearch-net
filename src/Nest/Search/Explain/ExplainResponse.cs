using System.Security.Cryptography.X509Certificates;
using Nest.Domain;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	public interface IExplainResponse<T> : IResponse
		where T : class
	{
		bool Matched { get; }
		ExplanationDetail Explanation { get; }
		ExplainGet<T> Get { get; }

		T Source { get; }
		FieldSelection<T> Fields { get; }
	}

	[JsonObject]
	public class ExplainResponse<T> : BaseResponse, IExplainResponse<T>
		where T : class
	{
		public ExplainResponse()
		{
			this.IsValid = true;
		}

		[JsonProperty(PropertyName = "matched")]
		public bool Matched { get; internal set; }

		[JsonProperty(PropertyName = "explanation")]
		public ExplanationDetail Explanation { get; internal set;}

		[JsonProperty(PropertyName = "get")]
		public ExplainGet<T> Get { get; internal set;}

		public T Source
		{
			get
			{
				if (this.Get == null) return null;
				return this.Get.Source;
			}
		}

		public FieldSelection<T> Fields
		{
			get
			{
				if (this.Get == null) return null;
				return new FieldSelection<T>(this.Settings, this.Get._fields);
			}
		}
	}
}