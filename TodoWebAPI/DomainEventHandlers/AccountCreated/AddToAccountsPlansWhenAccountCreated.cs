using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Todo.Domain;
using Todo.Domain.DomainEvents;
using Todo.Domain.Repositories;

namespace Todo.DomainEventHandlers
{
    public class AddToAccountsPlansWhenAccountCreated : INotificationHandler<AccountCreated>
    {
        private readonly IAccountPlanRepository _accountPlanRepository;

        public AddToAccountsPlansWhenAccountCreated(IAccountPlanRepository accountPlanRepository)
        {
            _accountPlanRepository = accountPlanRepository;
        }
        public async Task Handle(AccountCreated notification, CancellationToken cancellationToken)
        {
            var accountPlan = new AccountPlan()
            {
                AccountId = notification.AccountId,
                PlanId = notification.PlanId
            };

            await _accountPlanRepository.AddAccountPlanAsync(accountPlan);
            await _accountPlanRepository.SaveChangesAsync();
        }
    }
}