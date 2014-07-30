
0.2.1 / 2012-07-03
==================

  * Bumped ws
  * Emit headers event for drafts protocol
  * Added `data` event to `Socket`
  * Improves `clientTracking` memory efficiency [ruxkor]

0.2.0 / 2012-06-19
==================

  * Check if socket is writable before finishing handshake
  * Implement readyState and async connection opening
  * Check for readyState before writing
  * Fix wrong interface for hybi protocol
  * Remove logger, use debug for logging
  * Disable client tracking in `ws` (hybi)
  * client tracking -> clientTracking
  * Use `ws` server and parsers for hybi protocols
  * Handle socket "timeout" event
  * Add missing Sec-WebSocket-Protocol header to hybi protocols

0.1.6 / 2012-02-27
==================

  * Fix hybi-16 on node 0.4

0.1.5 / 2012-02-10
==================

  * Bumped ws

0.1.4 / 2012-01-04
==================

  * `close` event fires in a different tick after calling Socket#close

0.1.3 / 2012-01-02
==================

  * Bumped ws
  * Cleaned up tests
  * Cleaned up docs

0.1.2 / 2011-12-12
==================

  * Updated ws
  * Added 0.4 compatibility.

0.1.1 / 2011-11-27
==================

  * Added echo example.
  * Fixed/improved parser benchmark.
  * Updated to easy-websocket 0.2, and pulled in hybi parser from there.
  * Documentation for client-side usage (fixes GH-7).
  * Fix typo in 'detroy' -> 'destroy'
  * Added travis CI
  * Fixed example.

0.1.0 / 2011-11-14
==================

  * Speed improvemements.
    * Faster socket initialization.
    * Faster parser in the order of 17%-40%
  * Added comprehensive tests.
  * Added benchmarks.
