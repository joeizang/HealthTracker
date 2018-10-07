using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthTracker.Domain.Events
{
    public class CreateEvent : INotification
    {
        public CreateEvent(string message)
        {
            Message = message;
        }

        public string Message { get; set; }
    }
}
