using System;
using System.Collections.Generic;
using System.Text;

namespace HealthTracker.Domain.Abstractions
{
    public interface IEntity
    {
        bool IsDeleted { get; }

        DateTimeOffset CreatedAt { get; }

        DateTimeOffset? UpdatedAt { get; }
    }
}
