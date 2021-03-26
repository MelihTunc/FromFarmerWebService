using System;
using System.ComponentModel.DataAnnotations;

namespace FromFarmer.Entities
{
    public class BaseEntity
    {
        [Key]
        public long ID { get; set; }

        public DateTime? CREATE_AT { get; set; }

        public long USER_CREATED_ID { get; set; }

        public long IS_MODIFIED { get; set; }

        public DateTime? MODIFIED_AT { get; set; }

        public long USER_MODIFIED_ID { get; set; }

        public long IS_DELETED { get; set; }
    }
}
