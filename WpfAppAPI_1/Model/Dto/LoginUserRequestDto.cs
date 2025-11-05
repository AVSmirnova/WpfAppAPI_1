using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppAPI_1.Model.Dto
{
    public class LoginUserRequestDto
    {
        
        public string Login { get; set; }
        
        public string Password { get; set; }
    }
}
