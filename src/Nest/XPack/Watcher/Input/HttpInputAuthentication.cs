using System;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// The Authentication mechanism for a request to a HTTP endpoint
	/// </summary>
	[JsonObject]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<HttpInputAuthentication>))]
	public interface IHttpInputAuthentication
	{
		/// <summary>
		/// Basic Authentication credentials
		/// </summary>
		[JsonProperty("basic")]
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
			Assign(a => a.Basic = selector.Invoke(new HttpInputBasicAuthenticationDescriptor()));
	}

	/// <summary>
	/// Basic Authentication credentials
	/// </summary>
	[JsonObject]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<HttpInputBasicAuthentication>))]
	public interface IHttpInputBasicAuthentication
	{
		/// <summary>
		/// Username for Basic Authentication
		/// </summary>
		[JsonProperty("username")]
		string Username { get; set; }

		/// <summary>
		/// Password for Basic Authentication
		/// </summary>
		[JsonProperty("password")]
		string Password { get; set; }
	}

	/// <inheritdoc />
	[JsonObject]
	public class HttpInputBasicAuthentication : IHttpInputBasicAuthentication
	{
		/// <inheritdoc />
		public string Username { get; set; }
		/// <inheritdoc />
		public string Password { get; set; }
	}

	/// <inheritdoc />
	public class HttpInputBasicAuthenticationDescriptor
		: DescriptorBase<HttpInputBasicAuthenticationDescriptor, IHttpInputBasicAuthentication>, IHttpInputBasicAuthentication
	{
		string IHttpInputBasicAuthentication.Username { get; set; }
		string IHttpInputBasicAuthentication.Password { get; set; }

		/// <inheritdoc />
		public HttpInputBasicAuthenticationDescriptor Username(string username) =>
			Assign(a => a.Username = username);

		/// <inheritdoc />
		public HttpInputBasicAuthenticationDescriptor Password(string password) =>
			Assign(a => a.Password = password);
	}
}
