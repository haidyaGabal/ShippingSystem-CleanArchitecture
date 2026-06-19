using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DAL.Exceptions;
using Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace DAL.Repositories
{
        public class Repository<T> : IRepository<T> where T : BaseEntity
        {
            private readonly ShippingContext _context;
            private readonly DbSet<T> _dbSet;
            private readonly ILogger<Repository<T>> _logger;
        private IDbContextTransaction? tx;
        private ILogger<Repository<T>> logger;

        public Repository(ShippingContext context, ILogger<Repository<T>> logger)
            {
                _context = context;
                _dbSet = _context.Set<T>();
                _logger = logger;
            }

        public Repository(IDbContextTransaction? tx, ILogger<Repository<T>> logger)
        {
            this.tx = tx;
            this.logger = logger;
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

        public  bool Add(T entity)
        {
            try
            {
                if (entity.Id == Guid.Empty)
                    entity.Id = Guid.NewGuid();

                entity.CreatedDate = DateTime.Now;
                entity.CurrentState = 1;
               // entity.CreatedBy = userId;

                _dbSet.Add(entity);
               _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex, "Error adding entity", _logger);
            }
        }


        public bool Add(T entity,out Guid id)
        {
            try
            {
                if (entity.Id == Guid.Empty)
                    entity.Id = Guid.NewGuid();

                entity.CreatedDate = DateTime.UtcNow;
                entity.CurrentState = 1;
                // entity.CreatedBy = userId;

                _dbSet.Add(entity);
                 _context.SaveChanges();
                id= entity.Id;
                return true;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex, "Error adding entity", _logger);
            }
        }


        public bool Update(T entity)
        {
            try
            {
                // MUST be tracked
                var existing =  _dbSet.FirstOrDefault(x => x.Id == entity.Id);
                if (existing == null)
                    return false;

                // Preserve audit fields
                entity.CurrentState = existing.CurrentState;
                entity.CreatedDate = existing.CreatedDate;
                entity.CreatedBy = existing.CreatedBy;
                entity.UpdatedDate = DateTime.UtcNow;

                // Copy values
                _context.Entry(existing).CurrentValues.SetValues(entity);

                 _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex, $"Error updating entity with Id={entity.Id}", _logger);
            }
        }



        public bool Delete(Guid id)
            {
                try
                {
                // var entity = _dbSet.Find(id);
                //or
                var entity =  _dbSet.FirstOrDefault(x => x.Id == id);
                if (entity == null)
                        return false;

               
                _dbSet.Remove(entity);
                _context.SaveChanges();

                return true;
                }
                catch (Exception ex)
                {
                    throw new DataAccessException(ex, "", _logger);
                }
        }

            public bool ChangeStatus(Guid id, Guid userId, int status = 1)
            {
                try
                {
                // var entity = _dbSet.Find(id);
                //or
                var entity = _dbSet.FirstOrDefault(x => x.Id == id); 
                if (entity == null)
                        return false;

                     entity.CurrentState = status; // only if entity has Status

                   _context.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    throw new DataAccessException(ex, "", _logger);
                }
        }


        public T FirstOrDefault(Expression<Func<T, bool>> filter)
        {
            try
            {
                return _dbSet.Where(filter).AsNoTracking().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex, "", _logger);
            }
        }

        public async Task<List<T>> GetList(Expression<Func<T, bool>> filter)
        {
            try
            {
                return _dbSet.Where(filter).AsNoTracking().ToList();
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex, "", _logger);
            }
        }
        public async Task<List<T>> GetList(
    Expression<Func<T, bool>> filter,
    params Expression<Func<T, object>>[] includes)
        {
            try
            {
                IQueryable<T> query = _dbSet.AsNoTracking();

                if (includes != null)
                {
                    foreach (var include in includes)
                    {
                        query = query.Include(include);
                    }
                }

                return await query.Where(filter).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex, "", _logger);
            }
        }
       


    }
    }


