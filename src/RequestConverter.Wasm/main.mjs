import { dotnet } from './_framework/dotnet.js';

let exportsPromise;

/**
 * Boots the .NET WASM runtime and returns the converter entry points the host's `ExternalExporter`
 * binds to: `{ check, convert }`. Both are synchronous (the runtime is fully created here) and each
 * takes and returns a plain JSON string per the external-exporter protocol. The runtime is created
 * once and the same exports are reused across calls.
 */
export async function boot() {
  exportsPromise ??= createExports();
  return exportsPromise;
}

async function createExports() {
  const { getAssemblyExports, getConfig } = await dotnet
    .withDiagnosticTracing(false)
    .create();

  const config = getConfig();
  if (!config?.mainAssemblyName) {
    throw new Error('Failed to read the .NET WASM runtime configuration.');
  }

  const exports = await getAssemblyExports(config.mainAssemblyName);
  return {
    check: exports.Exporter.Check,
    convert: exports.Exporter.Convert,
  };
}

export default boot;

// Node CLI convenience: `node main.mjs <check|convert> <jsonPayload>` prints the plain-JSON result.
// Guarded to run only when this file is the process entry point, so importing the module (in the
// browser or a Node smoke test) does not trigger it.
if (typeof process !== 'undefined' && Array.isArray(process.argv) && process.argv[1]) {
  const { pathToFileURL } = await import('node:url');
  if (import.meta.url === pathToFileURL(process.argv[1]).href) {
    const fn = process.argv.at(-2);
    const payload = process.argv.at(-1);
    const api = await boot();
    process.stdout.write(api[fn](payload));
    process.exit(0);
  }
}
