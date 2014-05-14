using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Elasticsearch.Net;
using Elasticsearch.Net.Connection;
using Nest.Resolvers;

namespace Nest
{
	public interface IResponse : IResponseWithRequestInformation
	{
		bool IsValid { get; }
		IElasticsearchResponse ConnectionStatus { get; }
		ElasticInferrer Infer { get; }

		ElasticsearchServerError GetServerException();
	}
		
	public class BaseResponse : IResponse
	{
		public BaseResponse()
		{
			this.IsValid = true;
		}
		public virtual bool IsValid { get; internal set; }
		
		IElasticsearchResponse IResponseWithRequestInformation.RequestInformation { get; set; }

		public IElasticsearchResponse ConnectionStatus { get { return ((IResponseWithRequestInformation)this).RequestInformation;  } }
		
		public ElasticsearchServerError GetServerException()
		{
			if (this.IsValid || this.ConnectionStatus == null || this.ConnectionStatus.OriginalException == null)
				return null;
			var e = this.ConnectionStatus.OriginalException as ElasticsearchServerException;
			if (e == null)
				return null;
			return new ElasticsearchServerError
			{
				Status = e.Status,
				Error = e.Message,
				ExceptionType = e.ExceptionType
			};
		}

		public ElasticInferrer _infer;
		
		protected IConnectionSettingsValues Settings
		{
			get
			{
				if (this.ConnectionStatus == null)
					return null;

				var settings = this.ConnectionStatus.Settings as IConnectionSettingsValues;
				return settings;
			}
		}


		public ElasticInferrer Infer
		{
			get
			{
				if (this._infer != null)
					return this._infer;

				var settings = this.Settings;
				if (settings == null)
					return null;
				this._infer = new ElasticInferrer(settings);
				return this._infer;
			}
		}

	}
}
