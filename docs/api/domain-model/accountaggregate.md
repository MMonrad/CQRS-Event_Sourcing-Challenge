---
icon: group-arrows-rotate
---

# AccountAggregate

*   **Account Aggregate Root**

    * **Purpose:** Represents the primary business entity in the banking domain.
    * **Attributes:**
      * `AccountId`: A unique identifier for the account, represented as an `Identity<AccountId>` type.
      * `Balance`: Tracks the current balance of the account (decimal).
      * `Transactions`: A collection of [`Transaction`](transaction.md) entities associated with this account.
    * **Responsibilities (Methods):**
      * `CreateAccount`: Initializes a new account.
      * `Deposit`: Handles adding funds to the account.
      * `Withdraw`: Handles removing funds from the account.
      * `InitiateTransfer`: Supports initiating a transfer to another account.

    This aggregate ensures that business rules, such as maintaining account integrity and balance updates, are encapsulated within the domain logic.
