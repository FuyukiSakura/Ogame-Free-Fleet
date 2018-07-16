using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FreeFleet.Core
{
    public class Logger
    {
        public static ObservableCollection<string> Logs { get; } = new ObservableCollection<string>();

        /// <summary>
        /// Log an entry
        /// </summary>
        /// <param name="msg"></param>
        public static void Log(string msg)
        {
            Logs.Add(DateTime.Now.ToLongTimeString() + ": " + msg);
        }
    }
}
