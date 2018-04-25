using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolarCycle {
    public class Data {

        public int speed { get; set; }
        public int heart_rate { get; set; }
        public int cadence { get; set; }
        public int altitude { get; set; }
        public int power { get; set; }
        public int power_balance { get; set; }
        public int air_pressure { get; set; }

        public Data() {
            speed = 0;
            heart_rate = 0;
            cadence = 0;
            altitude = 0;
            power = 0;
            power_balance = 0;
            air_pressure = 0;
        }

    }
}
