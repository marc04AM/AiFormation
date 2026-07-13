---
tipo: progetto
tags: [atlas, migrazione, cloud, acme]
creato: 2026-06-17
aggiornato: 2026-06-17
fonti: 1
stato: attivo
---

# Progetto Atlas

Migrazione del sistema legacy di gestione ordini di [[acme-spa]] (monolite PHP
on-premise) verso una nuova piattaforma cloud. Eseguito da [[sistec]].

- **Stato:** attivo (kickoff 2026-06-17)
- **Lead:** [[marco-manfrin]]
- **Cliente / sponsor:** [[acme-spa]] — [[davide-conti]]
- **Scadenza target:** fine Q4 2026 · budget approvato

## Approccio
Migrazione **incrementale** tramite [[strangler-fig-pattern]] dietro un API
gateway, un modulo alla volta. Niente big-bang.
Decisione: [[2026-06-17-migrazione-incrementale-strangler-fig]].

## Stack (in definizione)
- **DB target:** Postgres (proposto da [[laura-bianchi]], ok cliente)
- **Cloud:** da decidere — Azure favorito (contratto enterprise Acme)
- **API gateway:** in valutazione — Kong vs Azure APIM (spike di [[laura-bianchi]])

## Team
- [[marco-manfrin]] — project lead ([[sistec]])
- [[laura-bianchi]] — backend ([[sistec]])
- [[davide-conti]] — sponsor cliente ([[acme-spa]])

## Milestone
- 2026-06-24 — documento di architettura (owner: [[marco-manfrin]])

## Rischi aperti
- Schema del DB legacy non documentato.
- Compliance GDPR dipendente dal legal di [[acme-spa]].

## Fonti
- [[2026-06-17-kickoff-progetto-atlas]]
