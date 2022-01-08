using Luval.DataStore.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.DataStore.Entities
{
    /// <inheritdoc/>
    public class AuditedEntity<TKey> : IAuditedEntity<TKey>
    {
        /// <inheritdoc/>
        [PrimaryKey]
        public virtual TKey Id { get; set; }
        /// <inheritdoc/>
        public DateTime UtcCreatedOn { get; set; }
        /// <inheritdoc/>
        public string CreatedByUserId { get; set; }
        /// <inheritdoc/>
        public DateTime UtcUpdatedOn { get; set; }
        /// <inheritdoc/>
        public string UpdatedByUserId { get; set; }
    }

    /// <inheritdoc/>
    public class StringAuditedEntity : AuditedEntity<string> { }
    /// <inheritdoc/>
    public class NumericAuditedEntity : AuditedEntity<long> { }
    /// <inheritdoc/>
    public class IdentityAuditedEntity : AuditedEntity<int>
    {
        /// <inheritdoc/>
        [PrimaryKey, IdentityColumn]
        public override int Id { get; set; }
    }

    /// <inheritdoc/>
    public class LongIdentityAuditedEntity : AuditedEntity<long>
    {
        /// <inheritdoc/>
        [PrimaryKey, IdentityColumn]
        public override long Id { get; set; }
    }
}
