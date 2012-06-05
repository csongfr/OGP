using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.ComponentModel.Composition;


namespace Utils.IO
{
   
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class FileHelper 
    {

        public static void Open(string path)
        {
            if (File.Exists(path))
            {
                File.Open(path, FileMode.Open);
            }
            else
            {
                throw new Exception("Erreur");
                    
            }
        }  
    }
}
