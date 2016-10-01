"use strict";
var Router = require("restify-router").Router;
var router = new Router();
var RedisFactoryClient = require("./dbFactory");
function getListing(request, response, next) {
    return RedisFactoryClient("PageStats", response);
}
;
router.get("/node/PageStats", getListing);
module.exports = router;
//# sourceMappingURL=routeRecordNetworkChange.js.map