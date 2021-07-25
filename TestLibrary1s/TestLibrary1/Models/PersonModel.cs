using System;
using System.Collections.Generic;
using System.Text;

namespace TestLibrary1.Models
{
    public class PersonModel
    {
        public int id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailAddress { get; set; }
        public string CellPhoneNumber { get; set; }

        //same as readonly prop
        public string FullName  => $"{FirstName} {LastName}";
        

        public PersonModel(string firstName, string lastName, string emailAddress, string cellPhoneNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
            CellPhoneNumber = cellPhoneNumber;
        }

        public PersonModel()
        {

        }
    }
}
