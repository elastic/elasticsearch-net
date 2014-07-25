
/**
 * Module dependencies.
 */

var protocols = require('./protocols')
  , EventEmitter = process.EventEmitter
  , url = require('url');

/**
 * Module exports.
 */

module.exports = Server;

/**
 * Constructor. HTTP Server agnostic.
 *
 * @param {Object} options
 * @api public
 */

function Server (options) {
  this.options = {
      path: null
    , clientTracking: true
  };

  for (var i in options) {
    this.options[i] = options[i];
  }

  this.clients = [];
  this.clientsCount = 0;
  this.clientsNull = [];
}

/**
 * Inherits from EventEmitter.
 */

Server.prototype.__proto__ = EventEmitter.prototype;

/**
 * Handles a Request after `upgrade` event.
 *
 * @param {http.Request} request object
 * @param {http.Stream} socket
 * @param {Buffer} data stream head
 * @return {Server} for chaining.
 * @api public
 */

Server.prototype.handleUpgrade = function (req, socket, head) {
  var self = this;

  if (! this.checkRequest(req)) {
    return this;
  }

  // attach the legacy `head` property to request
  req.head = head;

  var client = this.createClient(req);

  client.on('open', function() {
    if (self.options.clientTracking) {
      var i = null;
      if (self.clientsNull.length) {
        i = self.clientsNull.shift();
        self.clients[i] = client;
      }
      else {
        i = self.clients.length;
        self.clients.push(client);
      }
      self.clientsCount++;
      client.on('close', function () {
        self.clients[i] = null;
        self.clientsNull.push(i);
        self.clientsCount--;
      });
    }
    self.emit('connection', client);
  });

  return this;
};

/**
 * Checks whether the request path matches.
 *
 * @return {Bool}
 * @api private
 */

Server.prototype.checkRequest = function (req) {
  if (!(req.method == 'GET' &&
        req.headers['upgrade'] &&
        req.headers.upgrade.toLowerCase() == 'websocket')) {
    // not a valid WebSocket upgrade
    return false;
  }

  if (this.options.path) {
    var u = url.parse(req.url);
    if (u && u.pathname !== this.options.path) return false;
  }

  // no options.path => match all paths
  return true;
};

/**
 * Initializes a client for the request with appropriate protocol.
 *
 * @param {http.Request} request object
 * @api private
 */

Server.prototype.createClient = function (req) {
  var version = req.headers['sec-websocket-version']
    , name = protocols[version] ? version : 'drafts'

  return new protocols[name](req);
};
