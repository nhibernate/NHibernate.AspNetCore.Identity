-- Table: public.application_roles

-- DROP TABLE public.application_roles;

CREATE TABLE public.application_roles
(
    id character varying(32) COLLATE pg_catalog."default" NOT NULL,
    description character varying(256) COLLATE pg_catalog."default",
    CONSTRAINT application_roles_pkey PRIMARY KEY (id)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE public.application_roles
    OWNER to postgres;
COMMENT ON TABLE public.application_roles
    IS 'application roles table.';

COMMENT ON COLUMN public.application_roles.description
    IS 'roles description';
