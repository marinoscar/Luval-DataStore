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
    public interface IAuditedEntity : IEntityCreatedAudit, IEntityUpdatedAudit 
    {
    }

    /// <inheritdoc/>
    public interface IStringAuditedEntity : IAuditedEntity
    {

    }

    /// <inheritdoc/>
    public interface INumericAusitedEntity : IAuditedEntity
    {

    }

    /// <inheritdoc/>
    public interface IIdAuditedEntity<TKey> : IIdEntity<TKey>, IAuditedEntity 
    {
        
    }
}
