---
tipo: decisione
tags: [atlas, architettura, migrazione]
creato: 2026-06-17
aggiornato: 2026-06-17
fonti: 1
stato: accettata
---

# ADR — Migrazione incrementale con Strangler Fig (Atlas)

- **Progetto:** [[progetto-atlas]]
- **Stato:** accettata (2026-06-17)
- **Decisori:** [[marco-manfrin]], [[laura-bianchi]], [[davide-conti]]

## Contesto
Il sistema ordini di [[acme-spa]] è un monolite PHP on-premise, poco testato e
con deploy manuali. Va migrato in cloud entro Q4 2026 senza interrompere
l'operatività.

## Decisione
Adottare lo [[strangler-fig-pattern]]: avvolgere il legacy dietro un API gateway
e spostare un modulo alla volta verso la nuova piattaforma, fino a dismettere il
monolite. **Niente big-bang.**

## Alternative considerate
- **Big-bang rewrite + cutover unico** — scartata: rischio operativo troppo alto
  su un sistema non documentato.

## Conseguenze
- Serve un API gateway in front del legacy (spike Kong vs Azure APIM in corso).
- Coesistenza legacy/nuovo per tutta la durata → complessità di routing e dati.
- Riduce il rischio di cutover ma allunga la finestra di doppia manutenzione.

## Fonti
- [[2026-06-17-kickoff-progetto-atlas]]
