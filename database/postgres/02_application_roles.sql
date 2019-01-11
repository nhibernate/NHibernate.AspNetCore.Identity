-- Table: public.application_roles

-- DROP TABLE public.application_roles;

CREATE TABLE public.application_roles
(
    id character varying(32) COLLATE pg_catalog."default" NOT NULL,
    description character varying(256) COLLATE pg_catalog."default",
    CONSTRAINT pk_application_roles PRIMARY KEY (id),
    CONSTRAINT fk_aspnet_roles_id FOREIGN KEY (id)
        REFERENCES public.aspnet_roles (id) MATCH SIMPLE
        ON UPDATE CASCADE
        ON DELETE CASCADE
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
