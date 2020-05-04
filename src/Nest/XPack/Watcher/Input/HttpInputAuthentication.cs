// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

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
