using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Net;


namespace Nest
{
	public class NestElasticsearchResponse : ElasticsearchResponse
	{
		
		public ElasticInferrer Infer { get; private set; }

		//TODO probably nicer if we make this factory ConnectionStatus.Error() and ConnectionStatus.Valid()
		//and make these constructors private.
		private NestElasticsearchResponse(IConnectionSettingsValues settings) : base(settings)
		{
			this.Infer = new ElasticInferrer(settings);
		}
		
		public NestElasticsearchResponse(IConnectionSettingsValues settings, Exception e) : base(settings, e) { }
		public NestElasticsearchResponse(IConnectionSettingsValues settings, string result) : base(settings, result) { }
		public NestElasticsearchResponse(IConnectionSettingsValues settings, byte[] result) : base(settings, result) { }
		/// <summary>
		/// Returns a response of type R based on the connection status by trying parsing status.Result into R
		/// </summary>
		/// <returns></returns>
		public override T Deserialize<T>(bool allow404 = false)
		{
			var s = this.Serializer as NestSerializer;
			if (typeof(BaseResponse).IsAssignableFrom(typeof(T)))
			  return s.Deserialize<T>(this, notFoundIsValidResponse: allow404);
			return s.Deserialize<T>(this.Result, notFoundIsValidResponse: allow404);
		}

		internal static NestElasticsearchResponse CreateFrom(ElasticsearchResponse response, IConnectionSettingsValues settings)
		{
			return new NestElasticsearchResponse(settings, response.ResultBytes);
		}

	}
}
