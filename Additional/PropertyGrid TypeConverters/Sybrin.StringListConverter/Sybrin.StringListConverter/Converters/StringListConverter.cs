using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Sybrin.StringListConverter.Converters {
    public class MyConverter : StringConverter {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context) {
            return true;
        }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context) {
            List<string> myList = new List<string>() {
                "Test 1",
                "Test 2",
                "Test 3"
            };

            return new StandardValuesCollection(myList);
        }
    }
}
