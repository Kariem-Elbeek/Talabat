using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;

namespace Talabat.Repository.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TalabatApiMarchDbContext _context;
        private Hashtable _repositories;
        public UnitOfWork(TalabatApiMarchDbContext context)
        {
            _context = context;
        }
        public Task<int> Complete()
            => _context.SaveChangesAsync();

        public void Dispose()
            => _context.Dispose();

        public IGenericReopsitory<TEntity> Reopsitory<TEntity>() where TEntity : BaseEntity
        {
            if (_repositories == null)
                _repositories = new Hashtable();

            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repository = new GenericReopsitory<TEntity>(_context);
                _repositories.Add(type, repository);
            }

            return (GenericReopsitory<TEntity>)_repositories[type];

        }
    }
}
