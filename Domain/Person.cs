using System;
using System.Collections.Generic;

namespace Domain
{
    public class Person
    {
        public Guid Id { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public Gender Gender { get; set; }
        public string Address { get; set; }
    }

    public enum Gender
    {
        Male,
        Female,
        Complicated
    }
}