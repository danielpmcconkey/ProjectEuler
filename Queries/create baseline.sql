CREATE TABLE public.baseline
(
  id integer NOT NULL,
  averagedurationms numeric(10,4),
  percentile90durationms numeric(10,4),
  CONSTRAINT baseline_pkey PRIMARY KEY (id),
  CONSTRAINT problemid FOREIGN KEY (id)
      REFERENCES public.problem (id) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE NO ACTION
)