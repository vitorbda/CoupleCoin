﻿using System.ComponentModel.DataAnnotations;

namespace CoupleCoinApi.Models
{
    public class Couple
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public User? User1 { get; set; }
        [Required]
        public User? User2 { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}
