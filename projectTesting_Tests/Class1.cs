using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using projectTesting;
using Notely_OOD_Project;


namespace projectTesting_Tests
{
    public class Class1
    {   // simple demo of unit testing a method from project// 
        [Test]

        public void GetImageLocation1()
        {
            SUT myClass = new SUT();
            string actualResult = myClass.GetImageLocation(Note.Priority.Relaxed);
            Assert.That(actualResult, Is.EqualTo("images/relaxedW.png"));
        }

        [TestCase(Note.Priority.Urgent, ExpectedResult = "images/urgentW.png")]
        [TestCase(Note.Priority.Important, ExpectedResult = "images/importantW.png")]
        [TestCase(Note.Priority.Critical, ExpectedResult = "images/criticalW.png")]

        public string BoundaryTests(Note.Priority holder)
        {
            SUT myClass = new SUT();
            string actualResult = myClass.GetImageLocation(holder);
            return actualResult;
        }
    }
}
