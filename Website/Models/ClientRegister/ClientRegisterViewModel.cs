﻿namespace Website.Models.ClientRegister
{
    public class ClientRegisterViewModel
    {
        public string Login { get; set; }
        public string Password { get; set; }  // plain password
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string Address { get; set; }
        public string? City { get; set; }
        public string? PostalCode { get; set; }
        public string Country { get; set; }
    }

}
