using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Elasticsearch.Net;
using static Tests.Framework.Integration.ClientMethod;

namespace Tests.Framework.Integration
{
	public class CallUniqueValues : Dictionary<ClientMethod, string>
	{
		private readonly string _prefix;
		private string UniqueValue => $"{this._prefix}-{ViewName}-{Guid.NewGuid().ToString("N").Substring(0, 8)}";

		private IDictionary<ClientMethod, ConcurrentDictionary<string, object>> ExtendedValues { get; }
			= new Dictionary<ClientMethod, ConcurrentDictionary<string, object>>();

		public ClientMethod CurrentView { get; set; } = Fluent;
		public string ViewName => this.CurrentView.GetStringValue().ToLowerInvariant();

		public ClientMethod[] Views { get; } = { Fluent, FluentAsync, Initializer, InitializerAsync };

		public string Value => this[CurrentView];
		public T ExtendedValue<T>(string key) where T : class => this.ExtendedValues[CurrentView][key] as T;
		public void ExtendedValue<T>(string key, T value) where T : class => this.ExtendedValues[CurrentView][key] = value;
		public T ExtendedValue<T>(string key, Func<T> value) where T : class =>
			this.ExtendedValues[CurrentView].GetOrAdd(key, value) as T;

		public CallUniqueValues(string prefix = "nest")
		{
			this._prefix = prefix;
			this.SetupClientMethod(Fluent);
			this.SetupClientMethod(FluentAsync);
			this.SetupClientMethod(Initializer);
			this.SetupClientMethod(InitializerAsync);
			this.CurrentView = Fluent;
		}

		private void SetupClientMethod(ClientMethod method)
		{
			this.CurrentView = method;
			this.Add(method, this.UniqueValue);
			this.ExtendedValues.Add(method, new ConcurrentDictionary<string, object>());
		}
	}
}
