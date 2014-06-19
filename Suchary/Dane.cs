using System.Collections.Generic;
using System;

namespace Suchary
{
    public static class Dane
    {
        private static List<String> _suchary = new List<String>();
        public static List<String> suchary 
        {
            get
            {
                return _suchary;
            } 
            set
            {
                _suchary = value;
            } 
        }
    }
}