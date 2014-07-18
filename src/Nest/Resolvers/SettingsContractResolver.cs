using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest.Resolvers
{
	class SettingsContractResolver : IContractResolver
	{
		/// <summary>
		/// ConnectionSettings can be requested by JsonConverter's.
		/// </summary>
		public IConnectionSettingsValues ConnectionSettings { get; private set; }

		public ElasticInferrer Infer { get; private set; }

		private IContractResolver _wrapped;

		/// <summary>
		/// Signals to custom converter that it can get serialization state from one of the converters
		/// Ugly but massive performance gain
		/// </summary>
		internal JsonConverterPiggyBackState PiggyBackState { get; set; }

		public SettingsContractResolver(IContractResolver wrapped, IConnectionSettingsValues connectionSettings)
		{
			this.ConnectionSettings = connectionSettings;
			this.Infer = new ElasticInferrer(this.ConnectionSettings);
			this._wrapped = wrapped ?? new DefaultContractResolver();
		}

		public JsonContract ResolveContract(Type type)
		{
			return this._wrapped.ResolveContract(type);
		}
	}
}
