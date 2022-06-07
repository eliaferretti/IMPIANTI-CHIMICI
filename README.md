# IMPIANTI-CHIMICI
Libreria di funzioni utili per il corso di "Impianti Chimici" del III anno di studi della laurea triennale in ingegneria chimica

Si presenta una lista delle funzioni presenti nel componente aggiuntivo con una descrizione sintetica dei termini di output e di input
 
1)	xi:
Output: restituisce il valore della frazione molare
Input: vettore delle composizioni come frazioni massive (wi), vettore dei pesi molecolari (PM), numero dei componenti (NC), specie di cui si desidera calcolare la frazione molare in output (specie).

2)	wi:
Output: restituisce il valore della frazione massica
Input: vettore delle composizioni (xi), vettore dei pesi molecolari (PM), numero dei componenti (NC), specie di cui si desidera calcolare la frazione massiva in output (specie).

3)	Antoine
Output: restituisce la tensione di vapore nell’unità di misura desiderata di un composto noti i parametri dell’equazione e la temperatura.
Input: parametri dell’equazione (A) (B) (C), temperatura [K] (T_inK), base del logaritmo nella relazione (baseLog), unità di misura della pressione restituita dalla relazione, disponibili: [Pa],[bar],[mmHg],[atm] (unitaPressioneFormula), unità di misura della temperatura nella relazione, disponibili: [K],[°C] (unitaTemperaturaFormula), unità di misura desiderata per la pressione, disponibili: [Pa],[bar],[mmHg],[atm] (unitaPressioneDesiderata)

4)	Rachford_Rice
Output: restituisce il valore dell’equazione di Rachford-Rice che può poi essere azzerata nelle incognite beta (rapporto di vaporizzazione) o nella temperatura.
Input: vettore delle composizioni entranti (z), vettore delle tensioni di vapore (p0), pressione (p), rapporto di vaporizzazione (beta), numero di componenti (NC)

5)	Flash_pRR
Output: restituisce il valore dell’equazione di Rachford-Rice che può poi essere azzerata nell’incognita T Input: vettore delle composizioni entranti (z), vettore delle tensioni di vapore (p0), pressione (p), rapporto di recupero (RR), specie a cui è riferito tale rapporto (specie), fase del rapporto di recupero “V” o “L” (specie), numero dei componenti (NC)

6)	N_stadi
Output: restituisce il numero di stadi ideali (as Integer) di una colonna standard per la distillazione binaria usando il metodo di McCabe-Thiele.
Input: fattore entalpico (q), frazione molare del componente più volatile in testa (xD), frazione molare del componente più volatile in coda (xB), frazione molare del componente più volatile in alimentazione (zF), volatilità relativa media in colonna (alfa), fattore moltiplicativo per determinare R da Rmin (k_rappRecupero)

7)	N_stadiSup_McCabeThiele
Output: restituisce il numero di stadi superiori ideali (as Double) di una colonna standard per la distillazione binaria usando il metodo di McCabe-Thiele (equazione di Riccati)..
Input: frazione molare del componente più volatile in testa (xD), frazione molare del componente più volatile in alimentazione (zF), rapporto di Recupero (R), volatilità relativa media in colonna (alfa).
 
8)	N_stadiInf_McCabeThiele
Output: restituisce il numero di stadi inferiori ideali (as Double) di una colonna standard per la distillazione binaria usando il metodo di McCabe-Thiele (equazione di Riccati).
Input: frazione molare del componente più volatile in testa (xD), frazione molare del componente più volatile in coda (xB), frazione molare del componente più volatile in alimentazione (zF), frazione molare del componente ottenuto all’ultimo stadio del tronco superiore nella fase liquida (zF_meno)***, rapporto di recupero (R), volatilità relativa media in colonna (alfa), fattore entalpico (q).

9)	N_stadiInf
Output: restituisce il numero di stadi inferiori ideali (as Integer) di una colonna standard per la distillazione binaria usando il metodo grafico di McCabe-Thiele.
Input: fattore entalpico (q), frazione molare del componente più volatile in testa (xD), frazione molare del componente più volatile in coda (xB), frazione molare del componente più volatile in alimentazione (zF), volatilità relativa media in colonna (alfa), fattore moltiplicativo per determinare R da Rmin (k_rappRecupero)

10)	N_stadiSup
Output: restituisce il numero di stadi superiori ideali (as Integer) di una colonna standard per la distillazione binaria usando il metodo grafico di McCabe-Thiele.
Input: fattore entalpico (q), frazione molare del componente più volatile in testa (xD), frazione molare del componente più volatile in coda (xB), frazione molare del componente più volatile in alimentazione (zF), volatilità relativa media in colonna (alfa), fattore moltiplicativo per determinare R da Rmin (k_rappRecupero)

11)	Rmin
Output: restituisce il rapporto di riflusso minimo di una colonna standard.
Input: fattore entalpico (q), frazione molare del componente più volatile in testa (xD), frazione molare del componente più volatile in alimentazione (zF), volatilità relativa media in colonna (alfa).

12)	Nmin:
Output: restituisce il numero di stadi minimo con la correlazione di Fenske.
Input: frazione molare del componente più volatile in testa (xD), frazione molare del componente più volatile in coda (xB), volatilità relativa media (alfa).

13)	N_stadi_Eduljee
Output: restituisce il numero di stadi di equilibrio di una colonna standard con la correlazione di Eduljee
Input: rapporto di riflusso (R), rapporto di riflusso minimo (Rmin), numero di stadi minimo (Nmin).

14)	N_stadi_Molokanov
Output: restituisce il numero di stadi di equilibrio di una colonna standard con la correlazione di Molokanov
Input: rapporto di riflusso (R), rapporto di riflusso minimo (Rmin), numero di stadi minimo (Nmin).

15)	xB_batchMultistadio
Output: restituisce la frazione molare del componente più volatile nella storta
Input: frazione molare del componente più volatile nel distillato (xD), rapporto di recupero (R), numero di stadi della colonna, storta inclusa (N), volatilità relativa media (alfa).


16)	kremser_N_assorbimento
Output: restituisce il numero di stadi di equilibrio necessari per l’assorbimento di una specie nell’ipotesi
di cascate lineari (problema di progetto)
Input: fattore di estrazione (E), rapporto molare del componente migrante nella frase pesante in ingresso (XN1), frazione molare del componente migrante nella fase leggera in ingresso (Y0), rapporto molare del componente migrante nella fase leggera in uscita, ovvero la specifica di purezza (YN), costante di ripartizione (k).

17)	kremser_YN_assorbimento
Output: restituisce il rapporto molare del componente migrante nella corrente leggera in uscita nell’assorbimento nell’ipotesi di cascate lineari (problema di verifica)
Input: fattore di estrazione (E), numero di stadi (N), rapporto molare del componente migrante nella frase pesante in ingresso (XN1), rapporto molare del componente migrante nella fase leggera in ingresso (Y0), costante di ripartizione (k).

18)	kremser_N_nonLin_assorbimento
Output: restituisce il numero di stadi di equilibrio necessari per l’assorbimento di una specie SENZA
l’ipotesi di cascate lineari (problema di progetto).
Input: costante di ripartizione (k), rapporto molare del componente migrante nella fase leggera in uscita, ovvero la specifica di purezza (YN), rapporto molare del componente migrante nella fase leggera in ingresso (Y0), rapporto molare del componente migrante nella frase pesante in ingresso (XN1), il rapporto tra la portata di solvente liquido e la portata di solvente gassoso L/G (LG).

19)	LGmin_assorbimento
Output: restituisce il valore di L/G minimo per colonne di assorbimento (senza ipotesi di cascate lineari) Input: costante di ripartizione (k), rapporto molare del componente migrante nella frase pesante in ingresso (XN1), rapporto molare del componente migrante nella fase leggera in uscita, ovvero la specifica di purezza (YN), rapporto molare del componente migrante nella fase leggera in ingresso (Y0).

20)	kremser_N_stripping
Output: restituisce il numero di stadi di equilibrio necessari per lo stripping di una corrente nell’ipotesi
di cascate lineari (problema di progetto)
Input: fattore di assorbimento (A), rapporto molare del componente migrante nella frase pesante in ingresso (XN1), rapporto molare del componente migrante nella fase leggera in ingresso (Y0), rapporto molare del componente migrante nella fase pesante in uscita, ovvero la specifica di purezza (X1), costante di ripartizione (k).

21)	kremser_X1_stripping
Output: restituisce il rapporto molare del componente migrante nella corrente pesante in uscita nello stripping nell’ipotesi di cascate lineari (problema di verifica)
Input: fattore di assorbimento (A), numero di stadi (N), rapporto molare del componente migrante nella frase pesante in ingresso (XN1), rapporto molare del componente migrante nella fase leggera in ingresso (Y0), costante di ripartizione (k).


22)	kremser_N_nonLin_stripping
Output: restituisce il numero di stadi di equilibrio necessari per lo stripping di una corrente SENZA
l’ipotesi di cascate lineari (problema di progetto).
Input: costante di ripartizione (k), rapporto molare del componente migrante nella fase pesante in uscita, ovvero la specifica di purezza (X1), rapporto molare del componente migrante nella fase leggera in ingresso (Y0), rapporto molare del componente migrante nella frase pesante in ingresso (XN1), il rapporto tra la portata di solvente liquido e la portata di solvente gassoso G/L (GL).

23)	GLmin_stripping
Output: restituisce il valore di G/L minimo per colonne di stripping (senza ipotesi di cascate lineari) Input: costante di ripartizione (k), rapporto molare del componente migrante nella frase pesante in ingresso (XN1), rapporto molare del componente migrante nella fase pesante in uscita, ovvero la specifica di purezza (X1), rapporto molare del componente migrante nella fase leggera in ingresso (Y0).

24)	NUT_1
Output: restituisce il numero di unità di trasferimento NUT.
Input: fattore di stripping (S), costante di ripartizione (k), rapporto molare del componente migrante in fase gas in ingresso (y1), rapporto molare del componente migrante in fase gas in uscita, ovvero la specifica (y2), rapporto molare del componente migrante nella corrente liquida in ingresso (x2)

25)	NUT_2
Output: restituisce il numero di unità di trasferimento NUT.
Input: il rapporto G/L (GL), costante di ripartizione (k), rapporto molare del componente migrante in fase gas in ingresso (y1), rapporto molare del componente migrante in fase gas in uscita, ovvero la specifica (y2), rapporto molare del componente migrante nella corrente liquida in ingresso (x2).

***zF_meno può essere determinata imponendo N_stadiSup_McCabeThiele uguale al numero di stadi superiori ottenuto, arrotondato all’intero successivo. L’equazione va azzerata nell’incognita zF (come primo tentativo si suggerisce zF della miscela entrante). Il risultato ottenuto per zF coincide con zF_meno.


Per qualsiasi problema o bug si contatti lo sviluppatore all'indirizzo: eliaferretti@outlook.it
