CREATE TABLE IF NOT EXISTS users (
    id uuid DEFAULT gen_random_uuid() PRIMARY KEY,
    name character varying(20) NOT NULL
);

CREATE TABLE IF NOT EXISTS leaderboardrecords (
    id uuid DEFAULT gen_random_uuid() PRIMARY KEY,
    user_id uuid NOT NULL REFERENCES users(id),
    playtime time without time zone NOT NULL,
    date timestamp without time zone NOT NULL
);


