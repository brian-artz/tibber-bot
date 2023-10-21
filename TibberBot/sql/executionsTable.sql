/*
The executions PostgresSQL table

- id is autoincremented on insert
- timestamp defaults to the current_timestamp if not specified
- commands denotes the number of commands executed by the robot while cleaning for one particular job
- result denotes the number of unique positions cleaned in the workspace/grid
- duration is a time interval

*/
CREATE TABLE public.executions
(
    id bigserial NOT NULL,
    "timestamp" timestamp with time zone NOT NULL DEFAULT current_timestamp,
    commands integer NOT NULL,
    result integer NOT NULL,
    duration interval NOT NULL,
    PRIMARY KEY (id)
);

ALTER TABLE IF EXISTS public.executions
    OWNER to postgres;