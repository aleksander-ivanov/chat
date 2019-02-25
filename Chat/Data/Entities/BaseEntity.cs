using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chat.Data.Entities
{
    /// <summary>
    /// Base class for entities
    /// </summary>
    public abstract class BaseEntity<TKey> where TKey : struct
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public TKey Id { get; set; }

        public string CreatedById { get; set; }

        public DateTime CreatedDate
        {
            get => _createdDate ?? DateTime.Now;
            set => _createdDate = value;
        }

        [NotMapped]
        private DateTime? _createdDate = null;

        public string LastModifiedById { get; set; }

        public DateTime? LastModifiedDate { get; set; }
    }
}
