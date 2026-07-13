# Note riunione — Kickoff Progetto Atlas

**Data:** 2026-06-17
**Tipo:** Riunione di kickoff (call, 50 min)
**Partecipanti:** Marco Manfrin (Sistec, project lead), Laura Bianchi (Sistec, backend), Davide Conti (Acme S.p.A., sponsor cliente)

## Contesto
Acme S.p.A. ci ha commissionato la migrazione del loro sistema di gestione
ordini legacy verso una nuova piattaforma cloud. Progetto interno battezzato
"Atlas". Budget approvato, scadenza target: fine Q4 2026.

## Punti discussi
- Il sistema legacy gira on-premise su un monolite PHP; pochi test, deploy manuali.
- Davide (Acme) ci ha dato accesso in sola lettura al DB di produzione.
- Si è discusso l'approccio: niente big-bang. Decisione presa di andare con una
  migrazione incrementale tramite "strangler fig pattern" — avvolgere il legacy
  e spostare un modulo alla volta dietro un API gateway.
- Laura ha proposto Postgres come DB target; Davide ok, Acme già usa Postgres altrove.
- Aperto il dubbio se usare AWS o Azure. Acme ha contratto enterprise con Azure,
  quindi probabile Azure ma non ancora deciso formalmente.

## Azioni
- Marco: preparare il documento di architettura entro 2026-06-24.
- Laura: spike sullo strangler fig + API gateway, valutare Kong vs APIM.
- Davide: ottenere accesso ai requisiti di compliance (GDPR) dal legal di Acme.

## Rischi emersi
- Il DB legacy non è documentato; rischio di scoperte tardive sullo schema.
- Dipendenza dal team Acme per la compliance: possibile collo di bottiglia.
