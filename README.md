# Otvoreno računarstvo

Ovaj repozitorij sadži skup otvorenih podataka s informacijama o hrvatskim autocestama i naplatnim postajama na njima.

## O skupu podataka

- **Autor**: Petar Novak
- **Verzija**: 1.0
- **Zadnje ažurirano**: 27.10.2023.
- **Jezik**: Hrvatski
- **Licencija**: Creative Commons Zero (CC0) v1.0 Universal
	- omogućava bilo kome da koristi skup podataka u bilo koje svrhe, bez ikakvih ograničenja
	- skup podataka je u javnom vlasništvu u najvećoj mogućoj mjeri u okviru zakona o autorskim pravima
- **Dostupni formati**: JSON, CSV
	
## Opis atributa
- Autoceste
	- **Oznaka** - string - oznaka autoceste (npr. A1)
	- **NeformalniNaziv** - string *(optional)* - neformalni naziv autoceste (npr. Istarski ipsilon)
	- **Duljina** - float - duljina autoceste u kilometrima
	- **NaplatnePostaje** - array - lista naplatnih postaja koje se nalaze na autocesti (odnosi se samo na JSON format)
- Naplatne postaje
	- **Naziv** - string - naziv naplatne postaje, obično nazvana po najbližem gradu ili općini
	- **GeoDuzina** - float - geografska dužina naplatne postaje iskazana kao decimalni broj, pozitivni brojevi označavaju istok, a negativni zapad
	- **GeoSirina** - float - geografska širina naplatne postaje iskazana kao decimalni broj, pozitivni brojevi označavaju sjever, a negativni jug
	- **ImaEnc** - boolean - TRUE ako je na naplatnoj postaji moguće plaćanje cestarine ENC-uređajem, FALSE inače
	- **Kontakt** - string *(optional)* - e-mail adresa za upite vezane uz naplatnu postaju