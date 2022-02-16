using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreFrontApplication.DATA.EF
{
    public class MovieMetadata
    {
        [StringLength(100, ErrorMessage = "* The value must be 100 characters or less")]
        [Required]
        [Display(Name = "Movie")]
        [UIHint("MultilineText")]
        public string MovieTitle { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "* Value must be a valid number, 0 or larger.")]
        [DisplayFormat(DataFormatString = "{0:c}", NullDisplayText = "[-N/A-]")]
        public decimal? Price { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "* Value must be a valid number greater than 0")]
        [Display(Name = "Units Sold")]
        [DisplayFormat(NullDisplayText = "[-N/A-]")]
        public Nullable<int> UnitsSold { get; set; }

        [Display(Name = "Realeased")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true, NullDisplayText = "[-N/A-]")]
        public System.DateTime ReleaseDate { get; set; }

        [Required(ErrorMessage = "*")]
        public int RatingID { get; set; }

        [Required(ErrorMessage = "*")]
        public int GenreID { get; set; }

        [Required(ErrorMessage = "*")]
        public int MovieStatusID { get; set; }

        [Display(Name = "Image")]
        public string MovieImage { get; set; }

        [UIHint("MultilineText")]
        [DisplayFormat(NullDisplayText = "[-N/A-]")]
        public string Description { get; set; }
    }

    [MetadataType(typeof(MovieMetadata))]
    public partial class Movie
    { }

    public class GenreMetadata
    {
        //public int GenreID { get; set; }

        [Display(Name = "Genre")]
        [Required(ErrorMessage = "*")]
        [StringLength(25, ErrorMessage = "* Value must be 25 characters or less.")]
        public string GenreName { get; set; }
    }

    [MetadataType(typeof(GenreMetadata))]
    public partial class Genre { }

    public class RatingMetadata
    {
        //public int RatingID { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Rating")]
        public string Rating1 { get; set; }
    }

    [MetadataType(typeof(RatingMetadata))]
    public partial class Rating { }

    public class MovieStatuMetadata
    {
        //public int MovieStatusID { get; set; }
        [Display(Name = "Status")]
        [Required(ErrorMessage = "*")]
        [StringLength(50, ErrorMessage = "* Value must be 50 characters or less.")]
        public string StatusName { get; set; }
    }

    [MetadataType(typeof(MovieStatuMetadata))]
    public partial class MovieStatu { }

    public class DirectorMetadata
    {
        //public int DirectorID { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "First Name")]
        [StringLength(50, ErrorMessage = "* Value must be 50 characters or less.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Last Name")]
        [StringLength(15, ErrorMessage = "* Value must be 50 characters or less.")]
        public string LastName { get; set; }
    }

    [MetadataType(typeof(DirectorMetadata))]
    public partial class Director
    {
        //Custom props - defined in the BUDDY (partial) class with the same name as the Model
        [Display(Name = "Full Name")]
        public string FullName
        {
            get { return $"{FirstName} {LastName}"; }
        }
    }
}
