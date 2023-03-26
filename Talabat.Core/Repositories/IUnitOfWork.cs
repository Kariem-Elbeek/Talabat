using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericReopsitory<TEntity> Reopsitory<TEntity>() where TEntity : BaseEntity;
        Task<int> Complete();
    }
}
