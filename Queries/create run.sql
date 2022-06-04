CREATE TABLE public.run
(
  id integer NOT NULL,
  durationms numeric(10,4),
  CONSTRAINT run_pkey PRIMARY KEY (id),
  CONSTRAINT problemid FOREIGN KEY (id)
      REFERENCES public.problem (id) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE NO ACTION
)