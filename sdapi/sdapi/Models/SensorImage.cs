using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sdapi.Models
{
    public class SensorImage
    {
        public int SensorImageId { get; set; }
        public String TimeStamp { get; set; }
        public byte[] Data { get; set; }
    }
}