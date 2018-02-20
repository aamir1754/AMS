using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMS.DataTransferObjects.Modules
{
    public class PartsDTo
    {
        public int PartId { get; set; }

        public string PartName { get; set; }

        public List<OptionsDTO> Options { get; set; }
    }
}