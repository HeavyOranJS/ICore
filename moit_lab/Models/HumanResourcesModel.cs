using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace moit_lab.Models
{
    public class HumanResourcesContext: DbContext
    {            
        public HumanResourcesContext(DbContextOptions<HumanResourcesContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<HumanResourcesModel> StaffMember { get; set; }
    }

    public class HumanResourcesModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [StringLength(40)]
        public string FamilyName { get; set; }

        [Required]
        [StringLength(40)]
        public string Name { get; set; }

        [Required]
        [StringLength(40)]
        public string Surname { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        public string Image{ get; set; }
        
    }
}
