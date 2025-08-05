using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Finabit_API.Models
{
    public class Owner
    {


        private OwnerType _mOwnerType = new OwnerType();
        public string FirstName { get; set; }
        public OwnerType OwnerType
        {
            get { return _mOwnerType; }
            set { _mOwnerType = value; }
        }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string PersonalNo { get; set; }
        public string BusinessNo { get; set; }
        public DateTime? DOB { get; set; }
        public string Address { get; set; }
        public String Firm { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string VATNo { get; set; }
        public int Place { get; set; }

        public override string ToString()
        {
            return (this.FirstName + " " + this.LastName + " " + this.Firm).Trim();
        }
        
        public string PlaceName { get; set; }
        public int ID { get; set; }
        public int LUB { get;  set; }
        public bool FromQRA { get;  set; }
        public int ErrorID { get;  set; }
        public string ErrorDescription { get;  set; }
    }
}