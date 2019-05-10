using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendCity
{
    public class User
    {
        [Key, Column(Order = 1)]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string IP { get; set; }
        public double Chiffre { get; set; }
        public string Text { get; set; }
        /* public DateTime? Expire { get; set; } */
    }
}