using System;
using Elasticsearch.Net;
using Nest;
using Tests.Framework.Configuration;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Nodes;

namespace Tests.Framework.ManagedElasticsearch.Tasks.ValidationTasks
{
	public class ValidateLicenseTask : NodeValidationTaskBase
	{
		public override void Validate(IElasticClient client, NodeConfiguration configuration)
		{
			if (!configuration.XPackEnabled) return;

			var license = client.GetLicense();
			if (license.IsValid && license.License.Status == LicenseStatus.Active) return;

			var exceptionMessageStart = "Server has license plugin installed, ";
#if DOTNETCORE
			var licenseFile = Environment.GetEnvironmentVariable("ES_LICENSE_FILE");
#else
			var licenseFile = Environment.GetEnvironmentVariable("ES_LICENSE_FILE", EnvironmentVariableTarget.Machine);
#endif
			if (!string.IsNullOrWhiteSpace(licenseFile))
			{
				var putLicense = client.PostLicense(new PostLicenseRequest
				{
					License = License.LoadFromDisk(licenseFile)
				});
				if (!putLicense.IsValid)
					throw new Exception("Server has invalid license and the ES_LICENSE_FILE failed to register\r\n" + putLicense.DebugInformation);

				license = client.GetLicense();
				if (license.IsValid && license.License.Status == LicenseStatus.Active) return;
				exceptionMessageStart += " but the installed license is invalid and we attempted to register ES_LICENSE_FILE ";
			}

			Exception exception = null;
			if (!license.IsValid)
			{
				exception = license.ApiCall.HttpStatusCode == 404
					? new Exception($"{exceptionMessageStart} but the license was not found! Details: {license.DebugInformation}")
					: new Exception($"{exceptionMessageStart} but a {license.ApiCall.HttpStatusCode} was returned! Details: {license.DebugInformation}");
			}
			else if (license.License == null)
				exception = new Exception($"{exceptionMessageStart}  but the license was deleted!");

			else if (license.License.Status == LicenseStatus.Expired)
				exception = new Exception($"{exceptionMessageStart} but the license has expired!");

			else if (license.License.Status == LicenseStatus.Invalid)
				exception = new Exception($"{exceptionMessageStart} but the license is invalid!");

			if (exception != null)
				throw exception;
		}
	}
}
