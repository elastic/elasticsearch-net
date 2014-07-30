
/**
 * Module dependencies.
 */

var net = require('net')
  , http = require('http')

/**
 * Version number.
 *
 * @api public
 */

exports.version = '0.2.1';

/**
 * WebSocket protocols impls.
 *
 * @api public
 */

exports.protocols = require('./protocols');

/**
 * Server constructor.
 *
 * @api public
 */

exports.Server = require('./server');

/**
 * Socket constructor.
 *
 * @api public
 */

exports.Socket = require('./socket');

/**
 * Crates an http.Server exclusively used for WS upgrades.
 *
 * @param {Number} port
 * @param {Function} callback
 * @param {Object} options
 * @return {Server} websocket.io server
 * @api public
 */

exports.listen = function (port, fn, options) {
  if ('object' == typeof fn) {
    options = fn;
    fn = null;
  }

  var server = http.createServer(function (req, res) {
    res.writeHead(501);
    res.end('Not Implemented');
  });

  server.listen(port, fn);

  // create ws server
  var ws = exports.attach(server, options);
  ws.httpServer = server;

  return ws;
};

/**
 * Captures upgrade requests for a http.Server.
 *
 * @param {http.Server} server
 * @param {Object} options
 * @return {Server} websocket.io server
 * @api public
 */

exports.attach = function (server, options) {
  var ws = new exports.Server(options);

  server.on('upgrade', function (req, socket, head) {
    ws.handleUpgrade(req, socket, head);
  });

  return ws;
};
