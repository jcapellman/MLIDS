var restify = require("restify");

import settings = require("./config");

var recordNetworkChangeRouter = require("./routeRecordNetworkChange");

var server = restify.createServer();

server.use(restify.queryParser());

recordNetworkChangeRouter.applyRoutes(server);

console.log(`Listing on port ${settings.HTTP_SERVER_PORT}...`);

server.listen(settings.HTTP_SERVER_PORT);