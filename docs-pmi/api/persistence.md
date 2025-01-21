---
icon: floppy-disk-pen
---

# Persistence

For simplicity sake, the Persistence model, or the Read Store, chosen for the project was In-memory store, provided by EventFlow library. As such, other that registering required services, no State awareness and tracking needed to be implemented. As a topic for improvement would be to introduce a persistent storage, such as MSSQL or a NoSQL database.
