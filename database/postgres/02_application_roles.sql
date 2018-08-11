-- Table: public.application_roles

-- DROP TABLE public.application_roles;

CREATE TABLE public.application_roles
(
    id character varying(32) COLLATE pg_catalog."default" NOT NULL,
    custom_property character varying(256) COLLATE pg_catalog."default",
    CONSTRAINT application_roles_pkey PRIMARY KEY (id)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE public.application_roles
    OWNER to postgres;
