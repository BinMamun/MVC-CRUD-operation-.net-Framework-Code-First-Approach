using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Project_Exam.Models
{
    public enum Gender { Male =1, Female};
    public class Breed
    {
        public Breed()
        {
            this.Cats = new List<Cat>();
        }
        public int BreedId { get; set; }

        [Required, StringLength(50), Display(Name ="Breed")]
        public string BreedName { get; set; }

        [Required, Display(Name ="Averaeg Age")]
        public int AverageAge { get; set; }

        [Required, StringLength(300)]
        public string Description { get; set; }

        public virtual ICollection<Cat> Cats { get; set; }
       
    }

    public class Cat
    {
        public int CatId { get; set; }
   
        [Required, StringLength(50), Display(Name ="Cat Name")]
        public string CatName { get; set; }

        [Required, Column(TypeName ="date")
            , Display(Name ="Date of Birth")
            , DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}", ApplyFormatInEditMode =true)]
        public DateTime Dob { get; set; }

        [Required,EnumDataType(typeof(Gender))]
        public Gender Gender{ get; set; }            

        
        public bool Available { get; set; }
       

        [Required, StringLength(150)]
        public string Picture { get; set; }

        [Required, ForeignKey("Breed")]
        public int BreedId { get; set; }
        public virtual Breed Breed { get; set; }
    }

    public class CatsDbContext : DbContext
    {
        public CatsDbContext()
        {
            Database.SetInitializer(new DbSeeder());
        }
        public DbSet<Breed> Breeds { get; set; } 
        public DbSet<Cat> Cats{ get; set; } 
    }

    public class DbSeeder : DropCreateDatabaseIfModelChanges<CatsDbContext>
    {
        protected override void Seed(CatsDbContext context)
        {
            Breed b = new Breed { BreedName = "Parsian Cat", AverageAge = 15, Description = "The Persian cat is a long-haired breed of cat characterized by its round face and short muzzle." };
            b.Cats.Add(new Cat { CatName = "Jack", Gender = Gender.Male, Available = true, Dob = new DateTime(2020, 02, 28), Picture = "1.jpg" });
            context.Breeds.Add(b);
            context.SaveChanges();
        }
    }



}