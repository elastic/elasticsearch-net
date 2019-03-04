using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Elasticsearch.Net;
using static Tests.Framework.Integration.ClientMethod;

namespace Tests.Framework.Integration
{
	/// <summary>
	/// Holds unique values for the the two DSL's and the exposed sync and async methods we expose
	/// <see cref="ClientMethod" />
	/// </summary>
	public class CallUniqueValues : Dictionary<ClientMethod, string>
	{
		private readonly string _prefix;

		public CallUniqueValues(string prefix = "nest")
		{
			_prefix = prefix;
			FixedForAllCallsValue = UniqueValue;
			SetupClientMethod(ClientMethod.Fluent);
			SetupClientMethod(FluentAsync);
			SetupClientMethod(Initializer);
			SetupClientMethod(InitializerAsync);
			CurrentView = ClientMethod.Fluent;
		}

		public ClientMethod CurrentView { get; set; } = ClientMethod.Fluent;
		public string FixedForAllCallsValue { get; }

		public string Value => this[CurrentView];
		public string ViewName => CurrentView.GetStringValue().ToLowerInvariant();

		public ClientMethod[] Views { get; } = { ClientMethod.Fluent, FluentAsync, Initializer, InitializerAsync };

		private IDictionary<ClientMethod, ConcurrentDictionary<string, object>> ExtendedValues { get; }
			= new Dictionary<ClientMethod, ConcurrentDictionary<string, object>>();

		private string UniqueValue => $"{_prefix}-{ViewName}-{Guid.NewGuid().ToString("N").Substring(0, 8)}";

		public T ExtendedValue<T>(string key) where T : class => ExtendedValues[CurrentView][key] as T;

		public void ExtendedValue<T>(string key, T value) where T : class => ExtendedValues[CurrentView][key] = value;

		public T ExtendedValue<T>(string key, Func<T> value) where T : class =>
			ExtendedValues[CurrentView].GetOrAdd(key, value) as T;

		private void SetupClientMethod(ClientMethod method)
		{
			CurrentView = method;
			Add(method, UniqueValue);
			ExtendedValues.Add(method, new ConcurrentDictionary<string, object>());
		}
	}
}
