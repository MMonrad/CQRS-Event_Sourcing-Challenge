using EventFlow.Aggregates;
using EventFlow.Sagas;
using EventFlow.Sagas.AggregateSagas;
using PMI.Domain.AccountModel;
using PMI.Domain.Commands;
using PMI.Domain.Events;
using PMI.Domain.TransactionModel;

namespace PMI.Domain.Sagas;

public class TransferSaga : AggregateSaga<TransferSaga, TransferSagaId, TransferSagaLocator>,
    ISagaIsStartedBy<AccountAggregate, AccountId, TransferInitiatedEvent>,
    ISagaHandles<AccountAggregate, AccountId, MoneyWithdrawnEvent>
{
    public TransferSaga(TransferSagaId id) : base(id)
    {
    }

    public AccountId? SourceAccountId { get; set; }
    public AccountId? TargetAccountId { get; set; }
    public TransactionId? TransactionId { get; set; }
    public decimal Amount { get; set; }

    public Task HandleAsync(IDomainEvent<AccountAggregate, AccountId, MoneyWithdrawnEvent> domainEvent,
        ISagaContext sagaContext, CancellationToken cancellationToken)
    {
        Publish(new DepositMoneyCommand(TargetAccountId!, TransactionId!, domainEvent.Timestamp, Amount));
        Complete();
        return Task.CompletedTask;
    }


    public Task HandleAsync(IDomainEvent<AccountAggregate, AccountId, TransferInitiatedEvent> domainEvent,
        ISagaContext sagaContext, CancellationToken cancellationToken)
    {
        var transferEvent = domainEvent.AggregateEvent;
        Emit(new TransferSagaStartedEvent(transferEvent.SourceAccountId, transferEvent.TargetAccountId,
            transferEvent.TransactionId, transferEvent.Amount));
        Publish(new WithdrawMoneyCommand(transferEvent.SourceAccountId, transferEvent.TransactionId,
            transferEvent.Timestamp, transferEvent.Amount, Id.Value));
        return Task.CompletedTask;
    }


    public void Apply(TransferSagaStartedEvent e)
    {
        SourceAccountId = e.SourceAccountId;
        TargetAccountId = e.TargetAccountId;
        TransactionId = e.TransactionId;
        Amount = e.Amount;
    }
}