using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.DataStore.Entities
{
    /// <summary>
    /// Provides an implementation for an entity that has properties to audit when and who updated a record
    /// </summary>
    public interface IEntityUpdatedAudit
    {
        /// <summary>
        /// Gets or sets the Utc timestamp of when the record was updated
        /// </summary>
        public DateTime UtcUpdatedOn { get; set; }
        /// <summary>
        /// Gets or sets the Id of the user that updated the record
        /// </summary>
        public string UpdatedByUserId { get; set; }
    }
}
