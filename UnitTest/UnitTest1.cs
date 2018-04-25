using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PolarCycle;

namespace UnitTest {
    [TestClass]
    public class UnitTest1 {
        [TestMethod]
        public void TestMethod1() {
        }

        [TestMethod]
        public void IF() {
            //Arrange
            double expectedResult1 = 20;
            double np = 120;
            double ftp = 6;
            AdvancedMetrics obj1 = new AdvancedMetrics();
            //commonFunction obj1 = new commonFunction();

            //Act
            double ActualResult1 = obj1.IF(np, ftp);

            //Assert
            Assert.AreEqual(expectedResult1, ActualResult1);
        }

        [TestMethod]
        public void TestFTP() {
            //Arrange
            double expectedResult2 =114;
            double pow = 120;
            AdvancedMetrics obj2 = new AdvancedMetrics();
            //commonFunction obj2 = new commonFunction();

            //Act
            double ActualResult2 = obj2.FThresholdPower(pow);

            //Assert
            Assert.AreEqual(expectedResult2, ActualResult2);
        }

        public void TestTSS() {
            //Arrange
            double expectedResult2 = 25;
            double duration = 100;
            double normalizedPower = 10;
            double intensityFactor = 18;
            double ftp = 20;
            AdvancedMetrics obj2 = new AdvancedMetrics();
            //commonFunction obj2 = new commonFunction();
            //Act
            double ActualResult2 = obj2.TSS(duration, normalizedPower, intensityFactor, ftp);

            //Assert
            Assert.AreEqual(expectedResult2, ActualResult2);
        }

        [TestMethod]
        public void FileCheck() {
            //Arrange
            string expectedResult = "pass";
            string filename = "ASDBExampleCycleComputerData.hrm";

            //commonFunction obj2 = new commonFunction();
            AdvancedMetrics obj2 = new AdvancedMetrics();
            //Act
            string ActualResult2 = obj2.TestFile(filename);

            //Assert
            Assert.AreEqual(expectedResult, ActualResult2);
        }

    }
}
