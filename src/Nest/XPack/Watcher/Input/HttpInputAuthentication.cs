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
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	/// <summary>
	/// The Authentication mechanism for a request to a HTTP endpoint
	/// </summary>
	[InterfaceDataContract]
	[ReadAs(typeof(HttpInputAuthentication))]
	public interface IHttpInputAuthentication
	{
		/// <summary>
		/// Basic Authentication credentials
		/// </summary>
		[DataMember(Name ="basic")]
		IHttpInputBasicAuthentication Basic { get; set; }
	}

	/// <inheritdoc />
	public class HttpInputAuthentication : IHttpInputAuthentication
	{
		/// <inheritdoc />
		public IHttpInputBasicAuthentication Basic { get; set; }
	}

	/// <inheritdoc />
	public class HttpInputAuthenticationDescriptor
		: DescriptorBase<HttpInputAuthenticationDescriptor, IHttpInputAuthentication>, IHttpInputAuthentication
	{
		IHttpInputBasicAuthentication IHttpInputAuthentication.Basic { get; set; }

		/// <inheritdoc />
		public HttpInputAuthenticationDescriptor Basic(Func<HttpInputBasicAuthenticationDescriptor, IHttpInputBasicAuthentication> selector) =>
			Assign(selector.Invoke(new HttpInputBasicAuthenticationDescriptor()), (a, v) => a.Basic = v);
	}

	/// <summary>
	/// Basic Authentication credentials
	/// </summary>
	[InterfaceDataContract]
	[ReadAs(typeof(HttpInputBasicAuthentication))]
	public interface IHttpInputBasicAuthentication
	{
		/// <summary>
		/// Password for Basic Authentication
		/// </summary>
		[DataMember(Name ="password")]
		string Password { get; set; }

		/// <summary>
		/// Username for Basic Authentication
		/// </summary>
		[DataMember(Name ="username")]
		string Username { get; set; }
	}

	/// <inheritdoc />
	[DataContract]
	public class HttpInputBasicAuthentication : IHttpInputBasicAuthentication
	{
		/// <inheritdoc />
		public string Password { get; set; }

		/// <inheritdoc />
		public string Username { get; set; }
	}

	/// <inheritdoc />
	public class HttpInputBasicAuthenticationDescriptor
		: DescriptorBase<HttpInputBasicAuthenticationDescriptor, IHttpInputBasicAuthentication>, IHttpInputBasicAuthentication
	{
		string IHttpInputBasicAuthentication.Password { get; set; }
		string IHttpInputBasicAuthentication.Username { get; set; }

		/// <inheritdoc />
		public HttpInputBasicAuthenticationDescriptor Username(string username) =>
			Assign(username, (a, v) => a.Username = v);

		/// <inheritdoc />
		public HttpInputBasicAuthenticationDescriptor Password(string password) =>
			Assign(password, (a, v) => a.Password = v);
	}
}
