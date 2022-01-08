using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.DataStore.Entities
{
    public interface IEntityCreatedAudit
    {
        /// <summary>
        /// Gets or sets the Utc timestamp of when the record was created
        /// </summary>
        public DateTime UtcCreatedOn { get; set; }
        /// <summary>
        /// Gets or sets the Id of the user that created the record
        /// </summary>
        public string CreatedByUserId { get; set; }
    }
}
