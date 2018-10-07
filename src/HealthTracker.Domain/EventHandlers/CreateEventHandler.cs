using HealthTracker.Domain.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HealthTracker.Domain.EventHandlers
{
    public class CreateEventHandler : INotificationHandler<CreateEvent>
    {
        public Task Handle(CreateEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
