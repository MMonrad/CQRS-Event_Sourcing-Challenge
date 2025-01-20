---
icon: display-chart-up-circle-dollar
---

# Transaction

**Transaction Entity**

* **Purpose:** Represents a specific transaction within an account (e.g., deposit, withdrawal).
* **Attributes:**
  * `TransactionId`: A unique identifier for the transaction, represented as `Identity<TransactionId>`.
  * `AccountId`: Links the transaction to the [`Account`](accountaggregate.md) aggregate.
  * `TransactionType`: An enumeration (`enum<TransactionType>`) that defines the type of transaction (Deposit, Credit).
  * `Timestamp`: Records the date and time of the transaction.
* **Relationship with Account:**
  * The [`Account`](accountaggregate.md) aggregate maintains a collection of `Transaction` entities, adhering to the _0.._ cardinality, indicating multiple transactions can exist for an account.
* **TransactionType (Enum)**
  * Enumerates possible transaction types:
    * `Deposit`
    * `Credit`
* **Transaction Specification**
  * **Purpose:** Implements a specification pattern to enforce domain constraints.
  * **Rule:** The `TransactionSpecification` ensures that any transaction has a positive amount (`Amount > 0`).
