-- Table: public.application_users

-- DROP TABLE public.application_users;

CREATE TABLE public.application_users
(
    id character varying(32) COLLATE pg_catalog."default" NOT NULL,
    create_time timestamp without time zone NOT NULL DEFAULT now(),
    last_login timestamp without time zone,
    login_count integer DEFAULT 0,
    CONSTRAINT pk_application_users PRIMARY KEY (id),
    CONSTRAINT fk_application_users_id_aspnet_users_id FOREIGN KEY (id)
        REFERENCES public.aspnet_users (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE public.application_users
    OWNER to postgres;
COMMENT ON TABLE public.application_users
    IS 'application users table.';
