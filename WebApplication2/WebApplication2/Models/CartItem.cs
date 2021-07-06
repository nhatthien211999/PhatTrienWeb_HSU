using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    [Serializable]
    public class CartItem
    {
        public Product Product {get; set;}
        public int Quatity { get; set; }
    }
}