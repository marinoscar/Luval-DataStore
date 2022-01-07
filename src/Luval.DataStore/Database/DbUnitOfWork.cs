using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.DataStore.Database
{
    public class DbUnitOfWork<TEntity> : UnitOfWork<TEntity> where TEntity : class
    {
        public DbUnitOfWork()
        {

        }
    }
}
