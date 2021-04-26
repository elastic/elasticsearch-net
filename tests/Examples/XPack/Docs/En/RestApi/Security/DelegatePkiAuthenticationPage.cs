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

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.XPack.Docs.En.RestApi.Security
{
	public class DelegatePkiAuthenticationPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/rest-api/security/delegate-pki-authentication.asciidoc:77")]
		public void Line77()
		{
			// tag::964f522091306634794ff544c867f002[]
			var response0 = new SearchResponse<object>();
			// end::964f522091306634794ff544c867f002[]

			response0.MatchesExample(@"POST /_security/delegate_pki
			{
			  ""x509_certificate_chain"": [""MIIDbTCCAlWgAwIBAgIJAIxTS7Qdho9jMA0GCSqGSIb3DQEBCwUAMFMxKzApBgNVBAMTIkVsYXN0aWNzZWFyY2ggVGVzdCBJbnRlcm1lZGlhdGUgQ0ExFjAUBgNVBAsTDUVsYXN0aWNzZWFyY2gxDDAKBgNVBAoTA29yZzAeFw0xOTA3MTkxMzMzNDFaFw0yMzA3MTgxMzMzNDFaMEoxIjAgBgNVBAMTGUVsYXN0aWNzZWFyY2ggVGVzdCBDbGllbnQxFjAUBgNVBAsTDUVsYXN0aWNzZWFyY2gxDDAKBgNVBAoTA29yZzCCASIwDQYJKoZIhvcNAQEBBQADggEPADCCAQoCggEBANHgMX2aX8t0nj4sGLNuKISmmXIYCj9RwRqS7L03l9Nng7kOKnhHu/nXDt7zMRJyHj+q6FAt5khlavYSVCQyrDybRuA5z31gOdqXerrjs2OXS5HSHNvoDAnHFsaYX/5geMewVTtc/vqpd7Ph/QtaKfmG2FK0JNQo0k24tcgCIcyMtBh6BA70yGBM0OT8GdOgd/d/mA7mRhaxIUMNYQzRYRsp4hMnnWoOTkR5Q8KSO3MKw9dPSpPe8EnwtJE10S3s5aXmgytru/xQqrFycPBNj4KbKVmqMP0G60CzXik5pr2LNvOFz3Qb6sYJtqeZF+JKgGWdaTC89m63+TEnUHqk0lcCAwEAAaNNMEswCQYDVR0TBAIwADAdBgNVHQ4EFgQU/+aAD6Q4mFq1vpHorC25/OY5zjcwHwYDVR0jBBgwFoAU8siFCiMiYZZm/95qFC75AG/LRE0wDQYJKoZIhvcNAQELBQADggEBAIRpCgDLpvXcgDHUk10uhxev21mlIbU+VP46ANnCuj0UELhTrdTuWvO1PAI4z+WbDUxryQfOOXO9R6D0dE5yR56L/J7d+KayW34zU7yRDZM7+rXpocdQ1Ex8mjP9HJ/Bf56YZTBQJpXeDrKow4FvtkI3bcIMkqmbG16LHQXeG3RS4ds4S4wCnE2nA6vIn9y+4R999q6y1VSBORrYULcDWxS54plHLEdiMr1vVallg82AGobS9GMcTL2U4Nx5IYZG7sbTk3LrDxVpVg/S2wLofEdOEwqCeHug/iOihNLJBabEW6z4TDLJAVW5KCY1DfhkYlBfHn7vxKkfKoCUK/yLWWI=""] <1>
			}");
		}
	}
}
