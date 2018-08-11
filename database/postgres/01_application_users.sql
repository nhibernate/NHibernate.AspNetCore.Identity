-- Table: public.application_users

-- DROP TABLE public.application_users;

CREATE TABLE public.application_users
(
    id character varying(32) COLLATE pg_catalog."default" NOT NULL,
    custom_property character varying(256) COLLATE pg_catalog."default",
    CONSTRAINT application_users_pkey PRIMARY KEY (id)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE public.application_users
    OWNER to postgres;
