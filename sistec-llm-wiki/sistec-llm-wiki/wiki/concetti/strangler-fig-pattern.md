---
tipo: concetto
tags: [architettura, migrazione, pattern]
creato: 2026-06-17
aggiornato: 2026-06-17
---

# Strangler Fig Pattern

Pattern di migrazione incrementale: si avvolge un sistema legacy dietro un layer
di routing (tipicamente un API gateway) e si sostituisce **un modulo alla volta**
con la nuova implementazione, finché il legacy non è completamente "strangolato"
e può essere dismesso. Evita il rischio di un rewrite big-bang.

## Dove compare da noi
- Approccio scelto per [[progetto-atlas]] →
  [[2026-06-17-migrazione-incrementale-strangler-fig]].

## Correlati
- Richiede un API gateway in front del legacy (in valutazione: Kong vs Azure APIM).
