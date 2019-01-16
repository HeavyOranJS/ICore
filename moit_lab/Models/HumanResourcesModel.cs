using System;
using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore;

namespace moit_lab.Models
{
    public class HumanResourcesContext: DbContext
    {            
        public HumanResourcesContext(DbContextOptions<HumanResourcesContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Staff> StaffMember { get; set; }
    }

    public class Staff
    {
        public uint Id { get; set; }

        [Required]
        [StringLength(40)]
        public string FamilyName { get; set; }

        [Required]
        [StringLength(40)]
        public string Name { get; set; }

        [Required]
        [StringLength(40)]
        public string Surmane { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        public string Image{ get; set; }
        
    }
}
