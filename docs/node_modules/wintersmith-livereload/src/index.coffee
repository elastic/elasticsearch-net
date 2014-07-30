path = require 'path'
LivereloadServer = require 'livereload-server'

module.exports = (env, callback) ->
  env.helpers.livereload = -> ''
  return callback() if env.mode isnt 'preview'

  defaults =
    port: 35729
    clientScript: 'livereload.js'
    liveCSS: true

  options = env.config.livereload or {}
  for key of defaults
    options[key] ?= defaults[key]

  clientScript = new env.plugins.StaticFile
    full: path.resolve __dirname, './../livereload.js'
    relative: options.clientScript
  clientScript.__env = env

  server = new LivereloadServer
    id: 'com.yellowagents.wintersmith'
    name: 'Wintersmith'
    version: '0.1.0'
    port: options.port
    protocols:
      monitoring: 7
      saving: 1

  server.on 'connected', (conneciton) ->
    env.logger.verbose "LiveReload: Client connected (#{ conneciton.id })"

  server.on 'disconnected', (conneciton) ->
    env.logger.verbose "LiveReload: Client disconnected (#{ conneciton.id })"

  server.on 'command', (conneciton, command) ->
    env.logger.verbose "LiveReload: #{ JSON.stringify(command) }"

  server.on 'error', (error) ->
    env.logger.error error.message, error

  env.helpers.livereload = ->
    """<script src="#{ clientScript.url }?port=#{ options.port }" type="text/javascript"></script>"""

  env.locals.livereloadScript ?= env.helpers.livereload()

  env.registerGenerator 'livereload', (contents, callback) ->
    callback null, {livereload: clientScript}

  env.on 'change', (filename) ->
    for id, connection of server.connections
      connection.send
        command: 'reload'
        path: filename or ''
        liveCSS: options.liveCSS

  server.listen (error) ->
    server.wsserver.on 'connection', (socket) ->
      socket.on 'error', (error) ->
        throw error if error.code isnt 'ECONNRESET' # ignore connection resets
    env.logger.info "LiveReload listening on port #{ server.port }" if not error?
    callback error
