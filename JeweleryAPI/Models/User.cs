﻿namespace JewelAPI.Models
{
    public class User
    {
     public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsPrivilegedUser { get; set; }
    }
}
