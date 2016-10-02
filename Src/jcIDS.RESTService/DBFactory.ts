var redis = require("redis");
import settings = require("./config");

var redisFactory;

redisFactory = (key, response) => {
    const client = redis.createClient(settings.REDIS_DATABASE_PORT, settings.REDIS_DATABASE_HOSTNAME);

    client.on("error",
        err => {
            console.log("Error " + err);
        });

    client.get(key, (err, reply) => {
        if (reply == null) {
            response.writeHead(200, { 'Content-Type': 'application/json' });

            response.end("");

            return response;
        }

        response.writeHead(200, { 'Content-Type': 'application/json' });

        response.end(reply);

        return response;
    });
};

export = redisFactory;