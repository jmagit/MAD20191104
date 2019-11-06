using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain {
    internal class CustomerMetaData {
        public int CustomerID { get; set; }

        public bool NameStyle { get; set; }

        [StringLength(8)]
        [Display(Name ="Tratamiento")]
        public string Title { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Nombre")]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string MiddleName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Apellidos")]
        public string LastName { get; set; }

        [StringLength(10)]
        public string Suffix { get; set; }

        [StringLength(128)]
        public string CompanyName { get; set; }

        [StringLength(256)]
        public string SalesPerson { get; set; }

        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [StringLength(25)]
        public string Phone { get; set; }

        [Required]
        [StringLength(128)]
        public string PasswordHash { get; set; }

        [Required]
        [StringLength(10)]
        public string PasswordSalt { get; set; }

        public Guid rowguid { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
    [MetadataType(typeof(CustomerMetaData))]
    partial class Customer : IValidatableObject {

        public void darBaja() {
            // this.
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) {
            var rslt = new List<ValidationResult>();
            if(FirstName != FirstName.ToUpper()) {
                rslt.Add(new ValidationResult("Tiene que estar en mayúsculas", new[] { nameof(FirstName) }));
            }
            return rslt;
        }
        public bool IsValid {
            get {
                var rslt = new List<ValidationResult>();
                return Validator.TryValidateObject(this, new ValidationContext(this), rslt);
            }
        }
    }
}
