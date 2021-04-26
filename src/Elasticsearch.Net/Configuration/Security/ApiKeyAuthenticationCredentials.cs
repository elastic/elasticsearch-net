/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System;
using System.Security;
using System.Text;

namespace Elasticsearch.Net
{
	/// <summary>
	/// Credentials for Api Key Authentication
	/// </summary>
	public class ApiKeyAuthenticationCredentials : IDisposable
	{
		//TODO remove this constructor in 8.0
		public ApiKeyAuthenticationCredentials() { }

		public ApiKeyAuthenticationCredentials(string id, SecureString apiKey) => 
			Base64EncodedApiKey = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{id}:{apiKey.CreateString()}")).CreateSecureString();

		public ApiKeyAuthenticationCredentials(string id, string apiKey) => 
			Base64EncodedApiKey = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{id}:{apiKey}")).CreateSecureString();

		public ApiKeyAuthenticationCredentials(string base64EncodedApiKey) => 
			Base64EncodedApiKey = base64EncodedApiKey.CreateSecureString();

		public ApiKeyAuthenticationCredentials(SecureString base64EncodedApiKey) => 
			Base64EncodedApiKey = base64EncodedApiKey;

		/// <summary>
		/// The Base64 encoded api key with which to authenticate
		/// Take the form, id:api_key, which is then base 64 encoded
		/// </summary>
		public SecureString Base64EncodedApiKey { get; }

		public void Dispose() => Base64EncodedApiKey?.Dispose();
	}
}
