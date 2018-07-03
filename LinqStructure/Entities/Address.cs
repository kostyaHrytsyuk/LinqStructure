﻿namespace LinqStructure.Entities
{
    public class Address
    {
        public string Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Zip { get; set; }
        public int UserId { get; set; }
    }

}
