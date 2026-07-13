---
tipo: fonte
tags: [atlas, kickoff, migrazione, acme]
creato: 2026-06-17
aggiornato: 2026-06-17
fonte-tipo: riunione
fonte-data: 2026-06-17
---

# Fonte — Kickoff Progetto Atlas (2026-06-17)

Riunione di kickoff (call, 50 min) per l'avvio del [[progetto-atlas]].
Sorgente: `raw/2026-06-17-kickoff-progetto-atlas.md`.

**Partecipanti:** [[marco-manfrin]] ([[sistec]], lead), [[laura-bianchi]]
([[sistec]], backend), [[davide-conti]] ([[acme-spa]], sponsor).

## TL;DR
- [[acme-spa]] commissiona a [[sistec]] la migrazione del sistema ordini legacy
  (monolite PHP on-premise) verso una piattaforma cloud.
- Approccio deciso: migrazione **incrementale** con [[strangler-fig-pattern]],
  non big-bang → vedi [[2026-06-17-migrazione-incrementale-strangler-fig]].
- DB target proposto: **Postgres** (Acme lo usa già altrove).
- Cloud provider: **non ancora deciso**; Azure favorito per contratto enterprise Acme.
- Scadenza target: fine **Q4 2026**, budget approvato.

## Azioni emerse
- [[marco-manfrin]] → documento di architettura entro 2026-06-24.
- [[laura-bianchi]] → spike strangler fig + API gateway (Kong vs Azure APIM).
- [[davide-conti]] → requisiti di compliance GDPR dal legal Acme.

## Rischi
- DB legacy non documentato → rischio scoperte tardive sullo schema.
- Dipendenza dal team Acme per compliance → possibile collo di bottiglia.
