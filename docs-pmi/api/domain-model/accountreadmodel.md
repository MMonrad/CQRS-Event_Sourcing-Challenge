---
icon: glasses-round
---

# AccountReadModel

**Key Components**

1. **Properties**
   * `AccountId`: Represents the unique identifier of the account.
   * `Balance`: Stores the current balance of the account.
   * `Transactions`: A collection of [`Transaction`](transaction.md) objects representing the history of account transactions.
2. **Event Handlers** The `AccountReadModel` subscribes to the following domain events emitted by the [`AccountAggregate`](accountaggregate.md):
   * [**`AccountCreatedEvent`**](../events/accountcreated.md)
     * Initializes the `AccountReadModel` with a new account's `AccountId`, sets the initial balance to 0, and creates an empty transaction list.
   * [**`MoneyDepositedEvent`**](../events/moneydeposited.md)
     * Adds a `Transaction` object to the `Transactions` list, reflecting the deposited amount.
     * Updates the `Balance` by adding the deposited amount.
   * [**`MoneyWithdrawnEvent`**](../events/moneywithdrawn.md)
     * Adds a `Transaction` object to the `Transactions` list, reflecting the withdrawn amount.
     * Updates the `Balance` by subtracting the withdrawn amount.
