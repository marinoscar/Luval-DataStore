using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.DataStore.Entities
{
    /// <summary>
    /// Provides an implementation for an entity that has properties to audit when and who created or updated a record
    /// </summary>
    public interface IAuditedEntity<TKey> : IIdEntity<TKey>, IEntityCreatedAudit, IEntityUpdatedAudit 
    {
    }

    /// <inheritdoc/>
    public interface IStringAuditedEntity : IAuditedEntity<string>
    {

    }

    /// <inheritdoc/>
    public interface INumericAusitedEntity : IAuditedEntity<long>
    {

    }
}
