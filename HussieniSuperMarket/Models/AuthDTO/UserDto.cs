﻿namespace HussieniSuperMarket.Models.AuthDTO
{
    public class UserDto
    {
        public string ID { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }

        public List<string> Roles { get; set; }


    }
}
