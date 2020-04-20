--
-- PostgreSQL database dump
--

-- Dumped from database version 12.1
-- Dumped by pg_dump version 12.1

-- Started on 2020-04-20 11:35:58

CREATE DATABASE "Warehouse" WITH TEMPLATE = template0 ENCODING = 'UTF8' LC_COLLATE = 'Polish_Poland.1250' LC_CTYPE = 'Polish_Poland.1250';


ALTER DATABASE "Warehouse" OWNER TO postgres;

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 202 (class 1259 OID 49217)
-- Name: Client_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."Client_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    MAXVALUE 2147483647
    CACHE 1;


ALTER TABLE public."Client_Id_seq" OWNER TO postgres;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 203 (class 1259 OID 49219)
-- Name: Clients; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Clients" (
    "Id" integer DEFAULT nextval('public."Client_Id_seq"'::regclass) NOT NULL
);


ALTER TABLE public."Clients" OWNER TO postgres;

--
-- TOC entry 204 (class 1259 OID 49223)
-- Name: OrderProducts; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."OrderProducts" (
    "Id" integer NOT NULL,
    "ProductId" integer NOT NULL,
    "OrderId" integer NOT NULL,
    "Count" integer NOT NULL
);


ALTER TABLE public."OrderProducts" OWNER TO postgres;

--
-- TOC entry 205 (class 1259 OID 49226)
-- Name: OrderProducts_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."OrderProducts_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."OrderProducts_Id_seq" OWNER TO postgres;

--
-- TOC entry 2873 (class 0 OID 0)
-- Dependencies: 205
-- Name: OrderProducts_Id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."OrderProducts_Id_seq" OWNED BY public."OrderProducts"."Id";


--
-- TOC entry 206 (class 1259 OID 49228)
-- Name: OrderProducts_OrderId_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."OrderProducts_OrderId_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."OrderProducts_OrderId_seq" OWNER TO postgres;

--
-- TOC entry 2874 (class 0 OID 0)
-- Dependencies: 206
-- Name: OrderProducts_OrderId_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."OrderProducts_OrderId_seq" OWNED BY public."OrderProducts"."OrderId";


--
-- TOC entry 207 (class 1259 OID 49230)
-- Name: OrderProducts_ProductId_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."OrderProducts_ProductId_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."OrderProducts_ProductId_seq" OWNER TO postgres;

--
-- TOC entry 2875 (class 0 OID 0)
-- Dependencies: 207
-- Name: OrderProducts_ProductId_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."OrderProducts_ProductId_seq" OWNED BY public."OrderProducts"."ProductId";


--
-- TOC entry 208 (class 1259 OID 49232)
-- Name: Orders; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Orders" (
    "Id" integer NOT NULL,
    "Date" timestamp without time zone,
    "ClientId" integer NOT NULL
);


ALTER TABLE public."Orders" OWNER TO postgres;

--
-- TOC entry 209 (class 1259 OID 49235)
-- Name: Orders_ClientId_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."Orders_ClientId_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."Orders_ClientId_seq" OWNER TO postgres;

--
-- TOC entry 2876 (class 0 OID 0)
-- Dependencies: 209
-- Name: Orders_ClientId_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."Orders_ClientId_seq" OWNED BY public."Orders"."ClientId";


--
-- TOC entry 210 (class 1259 OID 49237)
-- Name: Orders_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."Orders_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."Orders_Id_seq" OWNER TO postgres;

--
-- TOC entry 2877 (class 0 OID 0)
-- Dependencies: 210
-- Name: Orders_Id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."Orders_Id_seq" OWNED BY public."Orders"."Id";


--
-- TOC entry 211 (class 1259 OID 49239)
-- Name: Products; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Products" (
    "Id" integer NOT NULL,
    "Name" character varying NOT NULL,
    "NetValue" money NOT NULL,
    "GrossValue" money NOT NULL
);


ALTER TABLE public."Products" OWNER TO postgres;

--
-- TOC entry 212 (class 1259 OID 49245)
-- Name: Products_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."Products_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."Products_Id_seq" OWNER TO postgres;

--
-- TOC entry 2878 (class 0 OID 0)
-- Dependencies: 212
-- Name: Products_Id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."Products_Id_seq" OWNED BY public."Products"."Id";


--
-- TOC entry 2713 (class 2604 OID 49247)
-- Name: OrderProducts Id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."OrderProducts" ALTER COLUMN "Id" SET DEFAULT nextval('public."OrderProducts_Id_seq"'::regclass);


--
-- TOC entry 2714 (class 2604 OID 49248)
-- Name: OrderProducts ProductId; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."OrderProducts" ALTER COLUMN "ProductId" SET DEFAULT nextval('public."OrderProducts_ProductId_seq"'::regclass);


--
-- TOC entry 2715 (class 2604 OID 49249)
-- Name: OrderProducts OrderId; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."OrderProducts" ALTER COLUMN "OrderId" SET DEFAULT nextval('public."OrderProducts_OrderId_seq"'::regclass);


--
-- TOC entry 2716 (class 2604 OID 49250)
-- Name: Orders Id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Orders" ALTER COLUMN "Id" SET DEFAULT nextval('public."Orders_Id_seq"'::regclass);


--
-- TOC entry 2717 (class 2604 OID 49251)
-- Name: Orders ClientId; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Orders" ALTER COLUMN "ClientId" SET DEFAULT nextval('public."Orders_ClientId_seq"'::regclass);


--
-- TOC entry 2718 (class 2604 OID 49252)
-- Name: Products Id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Products" ALTER COLUMN "Id" SET DEFAULT nextval('public."Products_Id_seq"'::regclass);


--
-- TOC entry 2720 (class 2606 OID 49254)
-- Name: Clients Client_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Clients"
    ADD CONSTRAINT "Client_pkey" PRIMARY KEY ("Id");


--
-- TOC entry 2722 (class 2606 OID 49256)
-- Name: OrderProducts OrderProducts_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."OrderProducts"
    ADD CONSTRAINT "OrderProducts_pkey" PRIMARY KEY ("Id");


--
-- TOC entry 2724 (class 2606 OID 49258)
-- Name: Orders Orders_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Orders"
    ADD CONSTRAINT "Orders_pkey" PRIMARY KEY ("Id");


--
-- TOC entry 2726 (class 2606 OID 49260)
-- Name: Products Products_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Products"
    ADD CONSTRAINT "Products_pkey" PRIMARY KEY ("Id");


--
-- TOC entry 2727 (class 2606 OID 49261)
-- Name: OrderProducts OrderProducts_OrderId_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."OrderProducts"
    ADD CONSTRAINT "OrderProducts_OrderId_fkey" FOREIGN KEY ("OrderId") REFERENCES public."Orders"("Id") ON DELETE CASCADE;


--
-- TOC entry 2728 (class 2606 OID 49266)
-- Name: OrderProducts OrderProducts_ProductId_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."OrderProducts"
    ADD CONSTRAINT "OrderProducts_ProductId_fkey" FOREIGN KEY ("ProductId") REFERENCES public."Products"("Id");


--
-- TOC entry 2729 (class 2606 OID 49271)
-- Name: Orders Orders_ClientId_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Orders"
    ADD CONSTRAINT "Orders_ClientId_fkey" FOREIGN KEY ("ClientId") REFERENCES public."Clients"("Id");


-- Completed on 2020-04-20 11:35:58

--
-- PostgreSQL database dump complete
--

