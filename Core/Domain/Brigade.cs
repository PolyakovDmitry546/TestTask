using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Brigade
    {
        private readonly string code;
        
        public Brigade(string code) {
            this.code = code;
        }
        
        public string Code { 
            get { return code; }
        }
    }
}
