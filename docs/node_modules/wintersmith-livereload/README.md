wintersmith-livereload
======================

[LiveReload](https://github.com/livereload) plugin for [Wintersmith](https://github.com/jnordberg/wintersmith).

## Installing

```
wintersmith plugin install livereload
```

or using npm

```
npm install [-g] wintersmith-livereload
```

and then add `wintersmith-livereload` to your config.json

```json
{
  "plugins": [
    "wintersmith-livereload"
  ]
}
```

## Usage

Include `livereloadScript` somewhere in your template, it will inject the livereload script if running in preview mode.

example using jade:

```
doctype html
html
  head
    meta(charset='utf-8')
    | !{ livereloadScript }
  body
    h1 Hello world
```

or using nunjucks:
```html
<!DOCTYPE html>
<html>
<head>
  <title>Titlar</title>
  {{ env.helpers.livereload() | safe }}
</head>
<body>
  <h1>Hello world</h1>
</body>
</html>
```

Options (can be omitted, defaults shown here):

```json
{
  "livereload": {
    "port": 35729
    "clientScript": "livereload.js"
    "liveCSS": true
  }
}
```

## Running tests

```
npm install
npm test
```
