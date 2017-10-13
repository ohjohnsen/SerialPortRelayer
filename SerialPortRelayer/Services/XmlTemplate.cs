using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialPortRelayer.Services {

    [Serializable]
    public class XmlTemplate {

        public string RelayerPortAName { get; set; }
        public string RelayerPortBName { get; set; }
        public string SnifferPortABName { get; set; }
        public string SnifferPortBAName { get; set; }
        public int BaudRate { get; set; }
    }
}
