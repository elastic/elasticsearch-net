// Standalone browser harness for the .NET request-converter WASM module.
//
// It mirrors how the host's demo runs in the browser: parse Dev Console text with the published
// @elastic/request-converter parser (which needs the Elasticsearch schema to resolve the API name
// and URL parameters), then hand the ParsedRequest[] to our locally built WASM module.
//
// Only the library's `parse` submodule is imported. The package's main entry pulls in the
// Handlebars/Prettier-based language exporters, which the esm.sh CDN transform ships broken
// ("h is not a function"); the parser submodule has no such dependency. The host's tiny
// ExternalExporter glue (getSlimRequests + JSON framing) is inlined below instead of imported.

const LIB_VERSION = "9.5.0";
const PARSE_URL = `https://esm.sh/@elastic/request-converter@${LIB_VERSION}/dist/parse.js`;
const SCHEMA_URL =
  "https://raw.githubusercontent.com/elastic/elasticsearch-specification/refs/heads/main/output/schema/schema.json";

const els = {
  bundleUrl: document.getElementById("bundleUrl"),
  callStyle: document.getElementById("callStyle"),
  clientCall: document.getElementById("clientCall"),
  documentType: document.getElementById("documentType"),
  emitUsings: document.getElementById("emitUsings"),
  namespaces: document.getElementById("namespaces"),
  output: document.getElementById("output"),
  source: document.getElementById("source"),
  status: document.getElementById("status"),
  style: document.getElementById("style"),
  syntax: document.getElementById("syntax"),
  typedDocument: document.getElementById("typedDocument"),
};

function setStatus(text, isError = false) {
  els.status.textContent = text;
  els.status.classList.toggle("error", isError);
}

let parseRequests; // from the library's parse submodule
let wasm; // { check, convert } from the WASM AppBundle

// Strip the heavy `request` (schema) property before sending, mirroring the host's getSlimRequests.
function slim(requests) {
  return requests.map(({ request, ...rest }) => rest);
}

async function init() {
  setStatus("Loading @elastic/request-converter parser and Elasticsearch schema...");
  const [parseModule, schema] = await Promise.all([
    import(/* @vite-ignore */ PARSE_URL),
    fetch(SCHEMA_URL).then((r) => {
      if (!r.ok) throw new Error(`schema fetch failed: ${r.status}`);
      return r.json();
    }),
  ]);
  parseRequests = parseModule.parseRequests;
  await parseModule.loadSchema(schema);

  await loadBundle();
}

async function loadBundle() {
  setStatus("Booting .NET WASM runtime (downloads the AppBundle, ~40 MB uncompressed)...");
  const url = new URL(els.bundleUrl.value, document.baseURI).href;
  const { boot } = await import(/* @vite-ignore */ url);
  wasm = await boot();
  setStatus("Ready. Edit the request to convert live.");
  await run();
}

// Wrap the WASM convert the way the host's ExternalExporter does: JSON in, JSON out, raise on error.
function convert(requests, options) {
  const response = JSON.parse(
    wasm.convert(JSON.stringify({ requests: slim(requests), options })),
  );
  if (response.error) {
    throw new Error(response.error);
  }
  return response.return;
}

let timer;
function schedule() {
  clearTimeout(timer);
  timer = setTimeout(run, 200);
}

async function run() {
  if (!wasm || !parseRequests) return;
  const source = els.source.value.trim();
  els.namespaces.textContent = "";
  if (!source) {
    els.output.textContent = "";
    return;
  }
  try {
    const requests = await parseRequests(source);
    const code = convert(requests, {
      type_name_style: els.style.value,
      syntax_mode: els.syntax.value,
      use_strongly_typed_document: els.typedDocument.checked,
      document_type_name: els.documentType.value,
      emit_usings: els.emitUsings.checked,
      client_call_format: els.clientCall.value,
      client_call_style: els.callStyle.value,
    });
    els.output.textContent = code;
    setStatus("Converted.");
  } catch (err) {
    els.output.textContent = "";
    setStatus(String(err.message ?? err), true);
  }
}

els.callStyle.addEventListener("change", run);
els.clientCall.addEventListener("change", run);
els.documentType.addEventListener("input", schedule);
els.emitUsings.addEventListener("change", run);
els.source.addEventListener("input", schedule);
els.style.addEventListener("change", run);
els.syntax.addEventListener("change", run);
els.typedDocument.addEventListener("change", run);
els.bundleUrl.addEventListener("change", () => {
  wasm = undefined;
  loadBundle().catch((err) => setStatus(String(err.message ?? err), true));
});

init().catch((err) => setStatus(String(err.message ?? err), true));
