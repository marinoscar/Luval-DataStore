using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.DataStore.Entities
{
    public interface IAuditedEntity<TKey> : IIdEntity<TKey>, IEntityCreatedAudit, IEntityUpdatedAudit 
    {
    }

    public interface IStringAuditedEntity : IAuditedEntity<string>
    {

    }

    public interface INumericAusitedEntity : IAuditedEntity<long>
    {

    }
}
