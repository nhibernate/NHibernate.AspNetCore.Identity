-- SEQUENCE: public.snow_flake_id_seq

-- DROP SEQUENCE public.snow_flake_id_seq;

CREATE SEQUENCE public.snow_flake_id_seq;

ALTER SEQUENCE public.snow_flake_id_seq
    OWNER TO postgres;

-- FUNCTION: public.snow_flake_id()

-- DROP FUNCTION public.snow_flake_id();

CREATE OR REPLACE FUNCTION public.snow_flake_id(
	)
    RETURNS bigint
    LANGUAGE 'sql'
    COST 100
    VOLATILE
AS $BODY$

SELECT (EXTRACT(EPOCH FROM CURRENT_TIMESTAMP) * 1000)::bigint * 1000000
  + 2 * 10000
  + nextval('public.snow_flake_id_seq') % 1000
  as snow_flake_id

$BODY$;

ALTER FUNCTION public.snow_flake_id()
    OWNER TO postgres;

COMMENT ON FUNCTION public.snow_flake_id()
    IS 'snow flake id ';

-- Table: public.aspnet_roles

-- DROP TABLE public.aspnet_roles;

CREATE TABLE public.aspnet_roles
(
    id character varying(32) COLLATE pg_catalog."default" NOT NULL DEFAULT (snow_flake_id())::character varying,
    name character varying(64) COLLATE pg_catalog."default" NOT NULL,
    normalized_name character varying(64) COLLATE pg_catalog."default" NOT NULL,
    concurrency_stamp character(36) COLLATE pg_catalog."default",
    CONSTRAINT pk_aspnet_roles PRIMARY KEY (id),
    CONSTRAINT u_aspnet_roles_name UNIQUE (name)
,
    CONSTRAINT u_aspnet_roles_normalized_name UNIQUE (normalized_name)

)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE public.aspnet_roles
    OWNER to postgres;

-- Index: ix_aspnet_roles_name

-- DROP INDEX public.ix_aspnet_roles_name;

CREATE INDEX ix_aspnet_roles_name
    ON public.aspnet_roles USING btree
    (normalized_name COLLATE pg_catalog."default")
    TABLESPACE pg_default;

-- Table: public.aspnet_role_claims

-- DROP TABLE public.aspnet_role_claims;

CREATE TABLE public.aspnet_role_claims
(
    id integer NOT NULL DEFAULT nextval('snow_flake_id_seq'::regclass),
    role_id character(32) COLLATE pg_catalog."default" NOT NULL,
    claim_type character varying(1024) COLLATE pg_catalog."default" NOT NULL,
    claim_value character varying(1024) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT pk_aspnet_role_claims PRIMARY KEY (id),
    CONSTRAINT fk_aspnet_role_claims_role_id FOREIGN KEY (role_id)
        REFERENCES public.aspnet_roles (id) MATCH SIMPLE
        ON UPDATE CASCADE
        ON DELETE CASCADE
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE public.aspnet_role_claims
    OWNER to postgres;
COMMENT ON TABLE public.aspnet_role_claims
    IS 'aspnet role claims table';

-- Index: ix_aspnet_role_claims_role_id

-- DROP INDEX public.ix_aspnet_role_claims_role_id;

CREATE INDEX ix_aspnet_role_claims_role_id
    ON public.aspnet_role_claims USING btree
    (role_id COLLATE pg_catalog."default")
    TABLESPACE pg_default;

-- Table: public.aspnet_users

-- DROP TABLE public.aspnet_users;

CREATE TABLE public.aspnet_users
(
    id character varying(32) COLLATE pg_catalog."default" NOT NULL DEFAULT (snow_flake_id())::character varying,
    user_name character varying(64) COLLATE pg_catalog."default" NOT NULL,
    normalized_user_name character varying(64) COLLATE pg_catalog."default" NOT NULL,
    email character varying(256) COLLATE pg_catalog."default" NOT NULL,
    normalized_email character varying(256) COLLATE pg_catalog."default" NOT NULL,
    email_confirmed boolean NOT NULL,
    phone_number character varying(32) COLLATE pg_catalog."default",
    phone_number_confirmed boolean NOT NULL,
    lockout_enabled boolean NOT NULL,
    lockout_end_unix_time_milliseconds bigint,
    password_hash character varying(256) COLLATE pg_catalog."default",
    access_failed_count integer NOT NULL,
    security_stamp character varying(256) COLLATE pg_catalog."default",
    two_factor_enabled boolean NOT NULL,
    concurrency_stamp character(36) COLLATE pg_catalog."default",
    CONSTRAINT pk_aspnet_users PRIMARY KEY (id),
    CONSTRAINT u_aspnet_users_normalized_user_name UNIQUE (normalized_user_name)
,
    CONSTRAINT u_aspnet_users_username UNIQUE (user_name)

)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE public.aspnet_users
    OWNER to postgres;
COMMENT ON TABLE public.aspnet_users
    IS 'aspnet users table.';

-- Index: ix_aspnet_users_email

-- DROP INDEX public.ix_aspnet_users_email;

CREATE INDEX ix_aspnet_users_email
    ON public.aspnet_users USING btree
    (normalized_email COLLATE pg_catalog."default")
    TABLESPACE pg_default;

-- Index: ix_aspnet_users_user_name

-- DROP INDEX public.ix_aspnet_users_user_name;

CREATE INDEX ix_aspnet_users_user_name
    ON public.aspnet_users USING btree
    (normalized_user_name COLLATE pg_catalog."default")
    TABLESPACE pg_default;

-- Table: public.aspnet_user_claims

-- DROP TABLE public.aspnet_user_claims;

CREATE TABLE public.aspnet_user_claims
(
    id integer NOT NULL DEFAULT nextval('snow_flake_id_seq'::regclass),
    user_id character(32) COLLATE pg_catalog."default" NOT NULL,
    claim_type character varying(1024) COLLATE pg_catalog."default" NOT NULL,
    claim_value character varying(1024) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT pk_aspnet_user_claims PRIMARY KEY (id),
    CONSTRAINT fk_aspnet_user_claims_user_id FOREIGN KEY (user_id)
        REFERENCES public.aspnet_users (id) MATCH SIMPLE
        ON UPDATE CASCADE
        ON DELETE CASCADE
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE public.aspnet_user_claims
    OWNER to postgres;
COMMENT ON TABLE public.aspnet_user_claims
    IS 'aspnet user claims table';

-- Index: ix_aspnet_user_claims_user_id

-- DROP INDEX public.ix_aspnet_user_claims_user_id;

CREATE INDEX ix_aspnet_user_claims_user_id
    ON public.aspnet_user_claims USING btree
    (user_id COLLATE pg_catalog."default")
    TABLESPACE pg_default;

-- Table: public.aspnet_user_logins

-- DROP TABLE public.aspnet_user_logins;

CREATE TABLE public.aspnet_user_logins
(
    login_provider character varying(32) COLLATE pg_catalog."default" NOT NULL,
    provider_key character varying(1024) COLLATE pg_catalog."default" NOT NULL,
    provider_display_name character varying(32) COLLATE pg_catalog."default" NOT NULL,
    user_id character(32) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT pk_aspnet_user_logins PRIMARY KEY (login_provider, provider_key),
    CONSTRAINT fk_aspnet_user_logins_user_id FOREIGN KEY (user_id)
        REFERENCES public.aspnet_users (id) MATCH SIMPLE
        ON UPDATE CASCADE
        ON DELETE CASCADE
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE public.aspnet_user_logins
    OWNER to postgres;
COMMENT ON TABLE public.aspnet_user_logins
    IS 'aspnet user logins table.';

-- Index: ix_aspnet_user_logins_user_id

-- DROP INDEX public.ix_aspnet_user_logins_user_id;

CREATE INDEX ix_aspnet_user_logins_user_id
    ON public.aspnet_user_logins USING btree
    (user_id COLLATE pg_catalog."default")
    TABLESPACE pg_default;

-- Table: public.aspnet_user_tokens

-- DROP TABLE public.aspnet_user_tokens;

CREATE TABLE public.aspnet_user_tokens
(
    user_id character(32) COLLATE pg_catalog."default" NOT NULL,
    login_provider character varying(32) COLLATE pg_catalog."default" NOT NULL,
    name character varying(32) COLLATE pg_catalog."default" NOT NULL,
    value character varying(256) COLLATE pg_catalog."default",
    CONSTRAINT pk_aspnet_user_tokens PRIMARY KEY (user_id, login_provider, name),
    CONSTRAINT fk_aspnet_user_tokens_user_id FOREIGN KEY (user_id)
        REFERENCES public.aspnet_users (id) MATCH SIMPLE
        ON UPDATE CASCADE
        ON DELETE CASCADE
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE public.aspnet_user_tokens
    OWNER to postgres;
COMMENT ON TABLE public.aspnet_user_tokens
    IS 'aspnet user tokens table.';

-- Index: ix_aspnet_user_tokens_user_id

-- DROP INDEX public.ix_aspnet_user_tokens_user_id;

CREATE INDEX ix_aspnet_user_tokens_user_id
    ON public.aspnet_user_tokens USING btree
    (user_id COLLATE pg_catalog."default")
    TABLESPACE pg_default;

-- Table: public.aspnet_user_roles

-- DROP TABLE public.aspnet_user_roles;

CREATE TABLE public.aspnet_user_roles
(
    user_id character(32) COLLATE pg_catalog."default" NOT NULL,
    role_id character(32) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT pk_aspnet_user_roles PRIMARY KEY (user_id, role_id),
    CONSTRAINT fk_aspnet_user_roles_role_id FOREIGN KEY (role_id)
        REFERENCES public.aspnet_roles (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE CASCADE,
    CONSTRAINT fk_aspnet_user_roles_user_id FOREIGN KEY (user_id)
        REFERENCES public.aspnet_users (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE CASCADE
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE public.aspnet_user_roles
    OWNER to postgres;
COMMENT ON TABLE public.aspnet_user_roles
    IS 'aspnet user roles relation table.';

-- Index: ix_aspnet_user_roles_role_id

-- DROP INDEX public.ix_aspnet_user_roles_role_id;

CREATE INDEX ix_aspnet_user_roles_role_id
    ON public.aspnet_user_roles USING btree
    (role_id COLLATE pg_catalog."default")
    TABLESPACE pg_default;

-- Index: ix_aspnet_user_roles_user_id

-- DROP INDEX public.ix_aspnet_user_roles_user_id;

CREATE INDEX ix_aspnet_user_roles_user_id
    ON public.aspnet_user_roles USING btree
    (user_id COLLATE pg_catalog."default")
    TABLESPACE pg_default;
