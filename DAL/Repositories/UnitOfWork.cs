using BL.Contracts;

using DAL;
using DAL.Repositories;
using Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Shipment
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ShippingContext _dbContext;
        private readonly ConcurrentDictionary<Type, object> _repositories = new();

        private readonly ILoggerFactory _loggerFactory;
        IDbContextTransaction? _tx;

        public UnitOfWork(
       ShippingContext dbContext,
       ILoggerFactory loggerFactory)
        {
            _dbContext = dbContext;
            _loggerFactory = loggerFactory;
        }

        /// <summary>
        /// Get or create a repository for the specified entity type
        /// </summary>
        public IRepository<T> Repository<T>() where T : BaseEntity
        {


            return (IRepository<T>)_repositories.GetOrAdd(typeof(T),

                _ => new Repository<T>(
                 _dbContext,
                 _loggerFactory.CreateLogger<Repository<T>>()));

        }
        public async Task BeginTransactionAsync()
                    => _tx = await _dbContext.Database.BeginTransactionAsync();

        public async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
            if (_tx is not null) await _tx.CommitAsync();
        }

        public async Task RollbackAsync()
            => await _tx?.RollbackAsync()!;

        public Task<int> SaveChangesAsync() => _dbContext.SaveChangesAsync();

        public async ValueTask DisposeAsync()
        {
            if (_tx is not null) await _tx.DisposeAsync();
            await _dbContext.DisposeAsync();
        }
    }
}