using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolarCycle {
    public class AdvancedMetrics {

        /// <summary>
        /// Checking the FTP
        /// </summary>
        /// <param name="pow">Power Value</param>
        /// <returns>FTP</returns>
        public double FThresholdPower(double pow) {
            double ftp, p;
            p = pow;

            ftp = 0.95 * pow;
            return ftp;
        }
        
        /// <summary>
        /// Checking the IF
        /// </summary>
        /// <param name="np">Normalized Power Value</param>
        /// <param name="ftp">FTP Value</param>
        /// <returns>Intensity Factor</returns>
        public double IF(double np, double ftp) {
            double f, n, IntensityF;
            f = ftp;
            n = np;
            IntensityF = n / f;
            return IntensityF;
        }
        /// <summary>
        /// Checking the TSS
        /// </summary>
        /// <param name="duration">Duration Value</param>
        /// <param name="normalizedPower">Normalized Power Value</param>
        /// <param name="intensityFactor">Intensity Factor Value</param>
        /// <param name="ftp">FTP Value</param>
        /// <returns>TSS Value</returns>
        public double TSS(double duration, double normalizedPower, double intensityFactor, double ftp) {
            double dur, np, inf, ft, trainingStressScore;
            dur = duration;
            np = normalizedPower;
            inf = intensityFactor;
            ft = ftp;

            trainingStressScore = (dur * np * inf) / (ft * 36);
            return trainingStressScore;
        }
        /// <summary>
        /// Checking the File Extension
        /// </summary>
        /// <param name="filename">FileName</param>
        /// <returns>Boolean Vaue of the state</returns>
        public string TestFile(string filename) {
            string filePath;
            filePath = filename;

            if (Path.GetExtension(filePath) == ".hrm")
                return "pass";
            else
                return "fail";
        }


    }
}