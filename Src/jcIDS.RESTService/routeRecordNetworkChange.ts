var Router = require("restify-router").Router;

var router = new Router();

import RedisFactoryClient from "./dbFactory";

function getListing(request: any, response: any, next: any);

function getListing(request, response, next) {
    return RedisFactoryClient("PageStats", response);
};

router.get("/node/PageStats", getListing);

export default router;