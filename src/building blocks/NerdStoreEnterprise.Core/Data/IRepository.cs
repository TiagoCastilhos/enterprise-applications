using NerdStoreEnterprise.Core.DomainObjects;
using System;

namespace NerdStoreEnterprise.Core.Data
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
    }
}