# AI e LLM — Guida Base

## 1. Cosa sono AI e LLM?

**AI (Intelligenza Artificiale)** = computer che sembra pensare. Come un
cervello elettronico che capisce domande, ragiona e risponde.

**LLM (Large Language Model)** = un tipo speciale di AI che parla il nostro
linguaggio. Ha letto tantissimi libri, articoli e siti web, e ha imparato a
scrivere come farebbe una persona.

In parole povere: l'LLM è un **collega bravissimo con le parole** che capisce
cosa gli chiedi e ti aiuta a fare cose.

### Come funziona (senza tecnicismi)

L'LLM funziona come un **auto-completamento super intelligente**. Non cerca
risposte in un database: **le scrive parola per parola**, basandosi su cosa
viene di solito dopo cosa. Come quando il cellulare ti suggerisce la prossima
parola, ma molto più avanzato.

Ci sono 4 cose da sapere:

**1. Predice la parola dopo** — Non "sa" le risposte, le costruisce un pezzo
alla volta. Per questo a volte scrive cose sicure ma sbagliate: sembra
plausibile, ma non è vero.

**2. Sa quello che ha letto** — Ha imparato da tantissimi libri e pagine web,
ma la sua conoscenza si ferma alla data in cui è stato addestrato. Non sa cose
recentissime o molto di nicchia.

**3. Ha una memoria limitata** — In una conversazione, ricorda solo quello che
gli hai detto in quella finestra. Se chiudi e riapri, ricomincia da zero. Se
gli dai troppi documenti, si dimentica i primi.

**4. Segue le istruzioni... più o meno** — Più sei specifico e chiaro, meglio
ti segue. Istruzioni vaghe o troppo astratte portano a risultati vaghi.

**Punto chiave:** la forza e il difetto vengono dalla stessa cosa. Scrive bene
perché è un "completatore di pattern". A volte sbaglia per lo stesso motivo.

---

## 2. Prima conversazione: come parlare con un LLM

Quando inizi una conversazione, devi dargli 3 cose:

1. **Imposta la scena** — chi è lui, chi sei tu, di cosa parlerete.
   Esempio: *"Sei un assistente che aiuta a scrivere email."*

2. **Definisci il compito** — cosa deve fare esattamente.
   Esempio: *"Scrivi una email per ringraziare un cliente."*

3. **Specifica le regole** — come deve comportarsi.
   Esempio: *"Usa un tono professionale ma gentile. Massimo 5 righe."*

Più sei chiaro all'inizio, migliore sarà la risposta.

---

## 3. Punti di attenzione

L'LLM non è perfetto. Ecco cosa tenere d'occhio:

- **Allucinazioni** — a volte inventa cose che sembrano vere (nomi, date,
  citazioni). Controlla sempre i fatti importanti.
- **Troppo d'accordo** — tende a darti ragione anche se sbagli. Se gli chiedi
  *"Questa idea è buona, vero?"* probabilmente dirà di sì.
- **Memoria corta** — in conversazioni lunghe, si scorda l'inizio. Ripeti le
  cose importanti ogni tanto.
- **Matematica** — non è bravo coi numeri puri. Per calcoli precisi, usa una
  calcolatrice.

**Regola d'oro:** l'LLM è un ottimo **bozzettista**, ma tu sei il **responsabile**
del risultato finale. Controlla sempre prima di usare.

---

## 4. Aggiungere contesto con documenti

Puoi dare all'LLM dei documenti da leggere prima che risponda.

- Carica un PDF, un Word, un file di testo
- Lui lo legge e lo usa per risponderti
- Utile per: manuali, regolamenti, documenti tecnici, email ricevute

Esempio: carichi un PDF di 100 pagine e chiedi *"Quali sono i punti principali
del capitolo 3?"* — lui li trova subito.

---

## 5. I modelli: Haiku, Sonnet, Opus

Sono tre versioni dell'LLM, come taglie di magliette:

| Modello | Carattere | Quando usarlo |
|---|---|---|
| **Haiku** | Veloce, leggero, economico | Risposte rapide, cose semplici |
| **Sonnet** | Bilanciato, bravo in tutto | Lavoro di tutti i giorni |
| **Opus** | Il più potente, ragiona meglio | Compiti complessi, analisi profonde |

- **Haiku** = velocità
- **Sonnet** = equilibrio
- **Opus** = potenza

Per la maggior parte delle cose, Sonnet va benissimo.

---

## 6. Claude Projects

I **Projects** sono come cartelle di lavoro dove tieni:

- Le tue conversazioni con l'LLM
- I documenti di riferimento
- Le istruzioni personalizzate (regole da seguire sempre)

Ogni progetto può avere un argomento diverso:
- Progetto "Marketing" → tono pubblicitario, documenti di brand
- Progetto "Codice" → linguaggio specifico, regole di programmazione

Non devi ripetere le istruzioni ogni volta: le imposti una volta e valgono per
tutta la vita del progetto.

---

## 7. Skills (abilità)

Le **Skills** sono superpoteri che dai all'LLM.

Invece di dirgli ogni volta *"fai così e così"*, crei una Skill che contiene
tutte le istruzioni e la riutilizzi quando serve.

Esempi:
- Skill "Correggi bozze" → sa esattamente come vuoi le correzioni
- Skill "Scrivi report" → formato giusto, tono giusto, struttura giusta

Le Skills sono **ricette pronte**: le attivi e l'LLM sa già cosa fare.

---

## 8. Claude Code

**Claude Code** è l'assistente per chi scrive codice (programmatori).

- Lavora direttamente nel terminale (la schermata nera dei comandi)
- Legge e modifica i file del progetto
- Aiuta a scrivere, correggere e migliorare il codice

Non serve saperlo usare subito, ma è utile sapere che esiste.

---

## 9. Claude in Microsoft 365

Puoi usare l'LLM direttamente dentro:

- **Word** → scrivere, riassumere, correggere documenti
- **Outlook** → scrivere email, riassumere lunghe conversazioni
- **Teams** → riassumere chat e riunioni

Funziona come un assistente integrato: selezioni un testo e gli chiedi cosa
fare.

---

## 10. Connettori (es. Canva)

I **connettori** sono ponti tra l'LLM e altri programmi.

Esempio con **Canva** (programma per fare grafiche):
- Chiedi all'LLM: *"Crea una presentazione per il cliente"*
- Lui passa le istruzioni a Canva
- Canva genera la presentazione pronta

Altri connettori possibili: Slack, Google Drive, Salesforce, ecc.

---

## In sintesi

| Cosa | A cosa serve |
|---|---|
| AI / LLM | Un assistente intelligente che capisce il linguaggio |
| Come funziona | Auto-completamento che scrive parola per parola |
| Prima conversazione | Dì chi è, cosa fare, come comportarsi |
| Punti di attenzione | Allucinazioni, memoria corta, troppo d'accordo |
| Documenti | Dagli materiale da leggere per rispondere meglio |
| Haiku / Sonnet / Opus | Veloce / Bilanciato / Potente |
| Projects | Cartelle di lavoro con regole e documenti |
| Skills | Ricette pronte per compiti specifici |
| Claude Code | Assistente per programmatori |
| Microsoft 365 | LLM dentro Word, Outlook, Teams |
| Connettori | Ponti verso Canva e altri programmi |
