using System;
using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// The Authentication mechanism for a request to a HTTP endpoint
	/// </summary>
	[DataContract]
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
			Assign(a => a.Basic = selector.Invoke(new HttpInputBasicAuthenticationDescriptor()));
	}

	/// <summary>
	/// Basic Authentication credentials
	/// </summary>
	[DataContract]
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
			Assign(a => a.Username = username);

		/// <inheritdoc />
		public HttpInputBasicAuthenticationDescriptor Password(string password) =>
			Assign(a => a.Password = password);
	}
}
