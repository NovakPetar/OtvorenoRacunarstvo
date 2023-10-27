-- napravi tablicu za autoceste
CREATE TABLE Autoceste (
    Id SERIAL PRIMARY KEY,
    Duljina FLOAT NOT NULL,
    Oznaka VARCHAR(255) NOT NULL,
    NeformalniNaziv VARCHAR(255),
    Dionica VARCHAR(255) NOT NULL
);

-- napravi tablicu za naplatne postaje
CREATE TABLE NaplatnaPostaja (
    AutocestaId INT REFERENCES Autoceste(Id),
    Id SERIAL PRIMARY KEY,
    Naziv VARCHAR(255),
    GeoVisina FLOAT,
    GeoSirina FLOAT,
    ImaEnc BOOLEAN,
    Kontakt TEXT
);


-- unesi autoceste
INSERT INTO Autoceste (Duljina, Oznaka, NeformalniNaziv, Dionica)
VALUES
    (480.15, 'A1', 'Autocesta kralja Tomislava, Dalmatina', 'Zagreb (Lučko) – Bosiljevo – Žuta Lokva – Split – Ploče – Dubrovnik'),
    (60.00, 'A2', 'Zagorska autocesta', 'Zagreb (Jankomir) – Macelj'),
    (306.40, 'A3', 'Posavska autocesta', 'Bregana – Zagreb – Okučani – Sredanci – Lipovac'),
    (96.90, 'A4', 'Varaždinska autocesta', 'Zagreb (Ivanja Reka) – Sveta Helena – Goričan'),
    (58.70, 'A5', 'Slavonska autocesta Slavonika', 'Branjin Vrh – Osijek – Sredanci – Svilaj'),
    (81.25, 'A6', 'Primorsko-goranska autocesta Goranka', 'Bosiljevo – Rijeka (Orehovica)'),
    (28.00, 'A7', 'Kvarnerska autocesta Primorka', 'Rupa – Rijeka – Žuta Lokva'),
    (64.20, 'A8', 'Istarski ipsilon', 'Kanfanar – Rijeka (Matulji)'),
    (76.80, 'A9', 'Istarski ipsilon', 'Kaštel – Kanfanar – Pula'),
    (8.50, 'A10', 'Neretvanska autocesta', 'Granični prijelaz Nova Sela – čvor Ploče'),
    (32.70, 'A11', 'Sisačka autocesta', 'Zagreb (Jakuševec) – Sisak');
	
	
--unesi naplatne postaje
insert into NaplatnePostaje (Naziv, GeoSirina, GeoDuzina, ImaEnc, Kontakt, AutocestaId)
values
	('Dugopolje', 43.5960856119359, 16.571457040517554, true, 'prodaja.dugopolje@hac.hr'),
	('Gospić', 44.661611882730945, 15.389265265192948, true, NULL, 1),
	('Vrbovsko', 45.36248340919582, 15.08654407697323, false, NULL, 6),
	('Višnjan', 45.27375392313616, 13.708791723415816, false, NULL, 9),
	('Bregana', 45.839274314127735, 15.704587254134351, true, 'prodaja.bregana@hac.hr', 3),
	('Novi Marof', 46.17427821419313, 16.362853689535108, false, NULL, 4),
	('Čakovec', 46.35015150044522, 16.52968862699292, false, NULL, 4),
	('Ivanić Grad', 45.690781205256606, 16.390836571163966, false, NULL, 3),
	('Kutina', 45.46240759939316, 16.76744572698379, false, NULL, 3),
	('Čakovec', 45.16857439720645, 17.94509031534414, true, 'prodaja.slavonskibrod@hac.hr', 4), 
	('Krapina', 46.133674792586774, 15.883336373016206, false, NULL, 2),
	('Đakovo', 45.324518877965716, 18.38468862526951, true, NULL, 5),
	('Rupa', 45.45642338334027, 14.268873454112756, false, NULL, 7),
	('Cerovlje', 45.27426877256247, 14.016986382938295, false, NULL, 8),
	('Čarapine', 43.11951173459808, 17.547682367476867, true, NULL, 10),
	('Mraclin', 45.664384544576855, 16.075741311796136, false, NULL, 11);
	
-- json export
COPY (
	select json_agg(row_to_json(t))
	from (
	SELECT  a.Oznaka AS "Oznaka",
	  a.NeformalniNaziv AS "NeformalniNaziv",
	  a.Duljina AS "Duljina",
	  json_agg(jsonb_build_object(
		'Naziv', n.Naziv,
		'GeoDuzina', n.GeoDuzina,
		'GeoSirina', n.GeoSirina,
		'ImaEnc', n.ImaEnc,
		'Kontakt', n.Kontakt
	  )) AS "NaplatnePostaje"
	FROM Autoceste a
	LEFT JOIN NaplatnePostaje n ON a.Id = n.AutocestaId
	GROUP BY a.Oznaka, a.NeformalniNaziv, a.Duljina 
	) as t
) TO 'C:/Temp/autoceste.json';


--csv export
COPY (
SELECT
    a.Oznaka,
    a.NeformalniNaziv,
    a.Duljina,
    n.Naziv,
    n.GeoDuzina,
    n.GeoSirina,
    n.ImaEnc,
    n.Kontakt
FROM Autoceste a
INNER JOIN NaplatnePostaje n ON a.Id = n.AutocestaId
) TO 'C:/Temp/autoceste.csv' WITH CSV HEADER;

	
