using System;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using Elasticsearch.Net.Connection.Configuration;

namespace Elasticsearch.Net.Connection
{
    /// <summary>
    /// A HttpConnection that uses the provided <param name="certificatesCollection"></param> on every request
    /// </summary>
    public class SignedHttpConnection : HttpConnection
    {
        private readonly X509CertificateCollection _certificateCollection;

        public SignedHttpConnection(IConnectionConfigurationValues settings
            , X509CertificateCollection certificateCollection) : base(settings)
        {
            _certificateCollection = certificateCollection;
        }

        protected override HttpWebRequest CreateHttpWebRequest(Uri uri, string method, byte[] data, IRequestConfiguration requestSpecificConfig)
        {
            var myReq = base.CreateHttpWebRequest(uri, method, data, requestSpecificConfig);
            myReq.ClientCertificates = _certificateCollection;
            return myReq;
        }
    }
}