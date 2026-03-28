using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DAL.Exceptions;
using Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DAL.Repositories
{
        public class Repository<T> : IRepository<T> where T : BaseEntity
        {
            private readonly ShippingContext _context;
            private readonly DbSet<T> _dbSet;
            private readonly ILogger<Repository<T>> _logger;

            public Repository(ShippingContext context, ILogger<Repository<T>> logger)
            {
                _context = context;
                _dbSet = _context.Set<T>();
                _logger = logger;
            }

            public List<T> GetAll()
            {
                try
                {

                return _dbSet.Where(data=>data.CurrentState>0).AsNoTracking().ToList();
            }
                catch (Exception ex)
                {
                    throw new DataAccessException(ex,"",_logger);
                }
            }

            public T? GetById(Guid id)
            {
                try
                {
                return _dbSet.AsNoTracking().FirstOrDefault(data => data.Id == id);
            }
                catch (Exception ex)
                {
                    throw new DataAccessException(ex, "", _logger);
                }
        }

        public async Task<bool> Add(T entity)
        {
            try
            {
                if (entity.Id == Guid.Empty)
                    entity.Id = Guid.NewGuid();

                entity.CreatedDate = DateTime.UtcNow;
                entity.CurrentState = 1;
               // entity.CreatedBy = userId;

                _dbSet.Add(entity);
              await  _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex, "Error adding entity", _logger);
            }
        }

        public async Task<bool> Update(T entity)
        {
            try
            {
                // MUST be tracked
                var existing = await _dbSet.FirstOrDefaultAsync(x => x.Id == entity.Id);
                if (existing == null)
                    return false;

                // Preserve audit fields
                entity.CurrentState = existing.CurrentState;
                entity.CreatedDate = existing.CreatedDate;
                entity.CreatedBy = existing.CreatedBy;
                entity.UpdatedDate = DateTime.UtcNow;

                // Copy values
                _context.Entry(existing).CurrentValues.SetValues(entity);

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex, $"Error updating entity with Id={entity.Id}", _logger);
            }
        }



        public async Task<bool> Delete(Guid id)
            {
                try
                {
                // var entity = _dbSet.Find(id);
                //or
                var entity = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
                if (entity == null)
                        return false;

               
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();

                return true;
                }
                catch (Exception ex)
                {
                    throw new DataAccessException(ex, "", _logger);
                }
        }

            public async Task<bool> ChangeStatus(Guid id, int status = 1)
            {
                try
                {
                // var entity = _dbSet.Find(id);
                //or
                var entity = await _dbSet.FirstOrDefaultAsync(x => x.Id == id); 
                if (entity == null)
                        return false;

                     entity.CurrentState = status; // only if entity has Status

                   await _context.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    throw new DataAccessException(ex, "", _logger);
                }
        }

       

        public async Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return await _dbSet.AsNoTracking().FirstOrDefaultAsync(predicate);
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex, "Error in FirstOrDefaultAsync", _logger);
            }
        }

        public async Task<List<T>> GetListAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return await _dbSet
                    .AsNoTracking()
                    .Where(predicate)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex, "Error in GetListAsync", _logger);
            }
        }

    }
    }


