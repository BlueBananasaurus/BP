using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Monogame_GL
{
    public class CustomSearcher
    {
        public static List<string> GetDirectories(string path, string searchPattern)
        {
            try
            {
                return Directory.GetDirectories(path, searchPattern).ToList();
            }
            catch (Exception)
            {
                return new List<string>();
            }
        }

        public static List<string> GetFiles(string path, string searchPattern)
        {
            try
            {
                return Directory.GetFiles(path, searchPattern).ToList();
            }
            catch (Exception)
            {
                return new List<string>();
            }
        }
    }
}