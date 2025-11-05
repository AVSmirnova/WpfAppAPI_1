using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppAPI_1.Model.Dto
{
    public class DeliveryResponseDto
    {
        public int Id { get; set; }
        public DateTime OpeningDateTime { get; set; }
        public DateTime? ClosingDateTime { get; set; }
        public string Street { get; set; }
        public int House { get; set; }
        public int? Building { get; set; }
        public int? Apartment { get; set; }
        public List<ProductDeliveryResponseDto> ProductDeliveries { get; set; }
        public StatusResponseDto Status { get; set; }
        public UserResponseDto Client { get; set; }
        public UserResponseDto Courier { get; set; }
    }
}
