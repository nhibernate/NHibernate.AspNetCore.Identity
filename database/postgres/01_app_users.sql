-- Table: public.app_users

-- DROP TABLE public.app_users;

CREATE TABLE public.app_users
(
    id character varying(32) COLLATE pg_catalog."default" NOT NULL,
    create_time timestamp without time zone NOT NULL DEFAULT now(),
    last_login timestamp without time zone,
    login_count integer DEFAULT 0,
    CONSTRAINT pk_app_users PRIMARY KEY (id),
    CONSTRAINT fk_aspnet_users_id FOREIGN KEY (id)
        REFERENCES public.aspnet_users (id) MATCH SIMPLE
        ON UPDATE CASCADE
        ON DELETE CASCADE
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE public.app_users
    OWNER to postgres;
COMMENT ON TABLE public.app_users
    IS 'application users table.';
