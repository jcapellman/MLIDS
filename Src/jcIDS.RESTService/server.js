"use strict";
var restify = require("restify");
var settings = require("./config");
var server = restify.createServer();
server.use(restify.queryParser());
server.listen(settings.HTTP_SERVER_PORT);
//# sourceMappingURL=server.js.map