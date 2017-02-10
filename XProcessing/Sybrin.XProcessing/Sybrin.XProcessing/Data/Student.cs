using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sybrin.XProcessing.Data {
    public class Student {
        public string Name { get; set; }

        public string Surname { get; set; }

        public override string ToString() {
            return string.Format("Student: '{0}'", Name);
        }
    }

    public class Students : List<Student> {
        public override string ToString() {
            return string.Format("{0} student(s)", this.Count);
        }
    }
}
