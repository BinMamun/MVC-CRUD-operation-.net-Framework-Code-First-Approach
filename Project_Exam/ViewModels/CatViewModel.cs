using Project_Exam.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project_Exam.ViewModels
{
    
    public class CatCreateViewModel
    {
        public int CatId { get; set; }

        [Required, StringLength(50), Display(Name = "Cat Name")]
        public string CatName { get; set; }

        [Required , Display(Name = "Date of Birth"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Dob { get; set; }

        [Required, EnumDataType(typeof(Gender))]
        public Gender Gender { get; set; }

        public bool Available { get; set; }

        [Required]
        public HttpPostedFileBase Picture { get; set; }

        [Required, ForeignKey("Breed")]
        public int BreedId { get; set; }

    }

    public class CatEditViewModel
    {
        public int CatId { get; set; }

        [Required, StringLength(50), Display(Name = "Cat Name")]
        public string CatName { get; set; }

        [Required, Display(Name = "Date of Birth"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Dob { get; set; }

        [Required, EnumDataType(typeof(Gender))]
        public Gender Gender { get; set; }

        public bool Available { get; set; }
        
        public HttpPostedFileBase Picture { get; set; }

        [Required, ForeignKey("Breed")]
        public int BreedId { get; set; }
    }
}