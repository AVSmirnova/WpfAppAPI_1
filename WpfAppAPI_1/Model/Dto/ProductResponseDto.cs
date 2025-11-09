using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppAPI_1.Model.Dto
{
    internal class ProductResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        //public CategoryResponseDto Category { get; set; }
        //public ShopResponseDto Shop { get; set; }
    }
}
