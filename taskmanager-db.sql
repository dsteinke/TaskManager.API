--
-- PostgreSQL database dump
--

-- Dumped from database version 17.2
-- Dumped by pg_dump version 17.2

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET transaction_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: Task; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Task" (
    id character(36) NOT NULL,
    userid character(36),
    title character varying(100) NOT NULL,
    description character varying(500),
    duedate timestamp without time zone NOT NULL,
    createdat timestamp without time zone DEFAULT CURRENT_TIMESTAMP NOT NULL,
    updatedat timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
    priority integer,
    iscompleted boolean DEFAULT false NOT NULL
);


ALTER TABLE public."Task" OWNER TO postgres;

--
-- Name: User; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."User" (
    id character(36) NOT NULL,
    email character varying(320) NOT NULL,
    username character varying(50) NOT NULL,
    passwordhash character varying(255) NOT NULL,
    createdat timestamp without time zone DEFAULT CURRENT_TIMESTAMP NOT NULL
);


ALTER TABLE public."User" OWNER TO postgres;

--
-- Data for Name: Task; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Task" (id, userid, title, description, duedate, createdat, updatedat, priority, iscompleted) FROM stdin;
\.


--
-- Data for Name: User; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."User" (id, email, username, passwordhash, createdat) FROM stdin;
538c84c3-1ce3-406d-bc09-51bc493c4c5b	stefan@example.com	Stefan	$2a$11$KXU1jIrD0bFkkshOoBRrLOOsiIUp5Uuhb81YLKK97OuSgKupQEJHm	2025-05-03 23:07:24.699371
\.


--
-- Name: Task Task_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Task"
    ADD CONSTRAINT "Task_pkey" PRIMARY KEY (id);


--
-- Name: User User_email_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."User"
    ADD CONSTRAINT "User_email_key" UNIQUE (email);


--
-- Name: User User_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."User"
    ADD CONSTRAINT "User_pkey" PRIMARY KEY (id);


--
-- Name: Task Task_userid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Task"
    ADD CONSTRAINT "Task_userid_fkey" FOREIGN KEY (userid) REFERENCES public."User"(id) ON DELETE CASCADE;


--
-- PostgreSQL database dump complete
--

