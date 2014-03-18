using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Elasticsearch.Net;
using Nest.Resolvers;

namespace Nest
{
	public interface IResponse
	{
		bool IsValid { get; }
		IElasticsearchResponse ConnectionStatus { get; }
		ElasticInferrer Infer { get; }
	}

	public class BaseResponse : IResponse
	{
		public BaseResponse()
		{
			this.IsValid = true;
		}
		public virtual bool IsValid { get; internal set; }
		public IElasticsearchResponse ConnectionStatus { get; internal set; }
		public ElasticInferrer _infer;

		public ElasticInferrer Infer
		{
			get
			{
				if (this._infer != null)
					return this._infer;
				if (this.ConnectionStatus == null)
					return null;

				return null; //TODO REVISIT
				//var settings = this.ConnectionStatus.Settings as IConnectionSettingsValues;
				//if (settings == null)
				//	return null;
				//this._infer = new ElasticInferrer(settings);
				//return this._infer;
			}
		}
	}
}
