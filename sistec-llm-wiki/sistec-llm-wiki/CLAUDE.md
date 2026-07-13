# Sistec LLM Wiki — Schema

Questo file è lo **schema** del wiki: dice all'agente LLM come è strutturato il
wiki, quali convenzioni seguire e quali workflow eseguire. È la configurazione
che trasforma l'LLM da chatbot generico in **manutentore disciplinato** di una
base di conoscenza su lavoro e progetti.

Dominio: **lavoro / progetti**. Lingua delle pagine: **italiano**.

---

## Ruoli

- **Umano (Dipendente):** procura le fonti, dirige l'analisi, fa domande, decide cosa
  conta. Non scrive le pagine del wiki.
- **LLM (tu):** leggi le fonti, ne estrai l'essenziale, scrivi e mantieni *tutte*
  le pagine del wiki, i collegamenti incrociati, l'indice e il log. Fai il lavoro
  di manutenzione che nessuno vuole fare.

Regola d'oro: **l'umano cura, l'LLM compila e mantiene.**

---

## Architettura: tre livelli

1. **Fonti grezze — `raw/`**
   Documenti sorgente: note di riunioni, transcript, articoli, report, email,
   documenti di progetto, immagini (`raw/assets/`). Sono **immutabili**: le leggi,
   non le modifichi mai. Sono la fonte di verità.

2. **Il wiki — `wiki/`**
   Pagine markdown generate dall'LLM. Le crei, le aggiorni quando arrivano nuove
   fonti, mantieni i cross-reference e la coerenza. L'umano legge, tu scrivi.

3. **Lo schema — questo file (`CLAUDE.md`)**
   Le regole. Si co-evolve nel tempo: quando troviamo una convenzione migliore,
   aggiorniamo questo file.

---

## Convenzioni delle cartelle

```
raw/                  fonti immutabili (mai modificate)
  assets/             immagini e allegati scaricati localmente
wiki/
  overview.md         sintesi globale: stato, tesi/contesto in evoluzione
  progetti/           una pagina per progetto
  decisioni/          ADR — una pagina per decisione significativa
  persone/            individui: stakeholder, colleghi, clienti, fornitori
  organizzazioni/     aziende, team, clienti, fornitori (entità collettive)
  concetti/           tecnologie, processi, termini, glossario, sistemi
  fonti/              un riassunto per ogni fonte acquisita (incl. riunioni)
index.md              catalogo di tutto il wiki (orientato al contenuto)
log.md                registro cronologico append-only (ingest/query/lint)
CLAUDE.md             questo schema
```

Le **fonti** (`fonti/`) sono il riassunto 1:1 di ciò che entra in `raw/`. Tutto
il resto (progetti, persone, ecc.) sono pagine **sintetiche** che aggregano
informazioni da più fonti.

---

## Convenzioni delle pagine

- **Nomi file:** `kebab-case`, descrittivi. Per le fonti, prefisso data:
  `fonti/2026-06-17-kickoff-progetto-atlas.md`.
- **Collegamenti:** usa i wikilink Obsidian `[[nome-pagina]]` (o
  `[[nome-pagina|testo visibile]]`). Collega generosamente: ogni persona,
  progetto, organizzazione o concetto citato va linkato alla sua pagina.
- **Frontmatter YAML** in cima a ogni pagina:

```yaml
---
tipo: progetto        # progetto | decisione | persona | organizzazione | concetto | fonte
tags: [atlas, infra]
creato: 2026-06-17
aggiornato: 2026-06-17
fonti: 1              # quante fonti contribuiscono a questa pagina (pagine sintetiche)
stato: attivo         # opz.: attivo | in-pausa | chiuso | proposta | accettata | superata
---
```

- **Citazioni:** ogni affermazione non ovvia rimanda alla fonte con
  `[[fonti/2026-06-17-...]]`. La conoscenza deve essere tracciabile.
- **Contraddizioni:** quando una nuova fonte contraddice una pagina, NON
  cancellare il vecchio. Segnala con un blocco:
  `> ⚠️ Contraddizione: la fonte X (data) afferma … mentre Y (data) affermava …`
- **Stile:** conciso, fattuale, a punti dove utile. Niente fronzoli. L'umano
  sfoglia in Obsidian, quindi titoli chiari e struttura scannabile.

### Struttura tipica per tipo

- **fonti/**: metadati (tipo fonte, data, partecipanti), TL;DR in 3-5 punti,
  takeaway chiave, entità citate (link), azioni/decisioni emerse.
- **progetti/**: obiettivo, stato, persone coinvolte (link), decisioni (link),
  timeline/milestone, rischi aperti, fonti collegate.
- **decisioni/** (ADR): contesto, decisione, alternative considerate, conseguenze,
  stato (proposta/accettata/superata), progetto e persone collegati.
- **persone/**: ruolo, organizzazione (link), progetti (link), note di contesto.
- **organizzazioni/**: cosa fa, relazione con noi, persone (link), progetti (link).
- **concetti/**: definizione, dove compare nel nostro contesto, link correlati.

---

## Operazioni

### 1. Ingest (acquisizione)

Quando l'umano aggiunge una fonte in `raw/` e chiede di processarla:

1. **Leggi** la fonte per intero (se è markdown con immagini inline, leggi prima
   il testo, poi apri le immagini referenziate in `raw/assets/` separatamente).
2. **Discuti** con l'umano i takeaway chiave prima di scrivere (1-2 frasi +
   eventuali domande). Mantieni l'umano coinvolto.
3. **Scrivi il riassunto** in `wiki/fonti/AAAA-MM-GG-titolo.md`.
4. **Aggiorna le pagine sintetiche** toccate: progetti, persone, organizzazioni,
   decisioni, concetti. Crea le pagine mancanti per ogni entità nuova rilevante.
   Una singola fonte può toccare 5-15 pagine.
5. **Segnala contraddizioni** con il blocco ⚠️ dove la nuova fonte cozza con
   pagine esistenti.
6. **Aggiorna `index.md`** (nuove pagine, contatori).
7. **Aggiorna `overview.md`** se la fonte cambia il quadro generale.
8. **Appendi una riga a `log.md`** (vedi formato sotto).

### 2. Query (interrogazione)

Quando l'umano fa una domanda:

1. Leggi prima `index.md` per trovare le pagine rilevanti, poi entra in quelle.
   (Se in futuro c'è un motore di ricerca, usalo; per ora l'indice basta.)
2. Sintetizza una risposta **con citazioni** ai `[[...]]` delle pagine usate.
3. **Le buone risposte si archiviano.** Se la risposta è una sintesi/comparazione
   di valore, proponi di salvarla come nuova pagina del wiki (es. in `concetti/`
   o una nota dedicata) così le esplorazioni si accumulano. Aggiorna index e log.

### 3. Lint (manutenzione)

Quando l'umano chiede un controllo di salute del wiki, cerca:

- contraddizioni tra pagine;
- affermazioni stantie superate da fonti più recenti;
- pagine orfane (nessun link in entrata);
- concetti/persone citati ma senza pagina propria;
- cross-reference mancanti;
- buchi informativi colmabili con una ricerca web o una nuova fonte.

Produci un report con problemi + azioni proposte. Suggerisci nuove domande da
investigare e nuove fonti da cercare. Registra il lint nel log.

---

## index.md e log.md

- **`index.md`** è orientato al *contenuto*: catalogo di ogni pagina con link e
  riassunto di una riga, organizzato per categoria. Aggiornalo a ogni ingest.
  È la prima cosa che leggi quando rispondi a una query.

- **`log.md`** è *cronologico* e append-only. Ogni voce inizia con un prefisso
  consistente così è parsabile con unix:
  `## [AAAA-MM-GG] tipo | descrizione`
  dove `tipo` ∈ {ingest, query, lint, schema}. Esempio:
  `grep "^## \[" log.md | tail -5` mostra le ultime 5 voci.

---

## Promemoria operativi

- Non riscrivere mai le fonti in `raw/`.
- Collega aggressivamente: un wiki senza link è solo una cartella di file.
- Preferisci aggiornare una pagina esistente piuttosto che crearne una duplicata.
- Ogni affermazione tracciabile a una fonte.
- Questo wiki è un repo git: i commit danno cronologia gratis.
