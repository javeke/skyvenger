using System.IO;
using System;
using UnityEngine;

namespace Manager
{
    public static class FileManager
    {
        public static bool WriteSync(string fileName, string data)
        {
            string filePath = Path.Combine(Application.persistentDataPath, fileName);

            try
            {
                File.WriteAllText(filePath, data);
                return true;
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                return false;
            }
        }

        public static bool ReadSync(string fileName, out string result)
        {
            string filePath = Path.Combine(Application.persistentDataPath, fileName);

            try
            {
                result = File.ReadAllText(filePath);
                return true;
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                result = null;
                return false;
            }
        }

        public static bool CreateFileSync(string fileName)
        {
            string filePath = Path.Combine(Application.persistentDataPath, fileName);

            try
            {
                FileStream f = File.Create(filePath); 
                f.Close();
                return true;
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                return false;
            }
        }

        public static bool FileExistsSync(string fileName) 
        {
            string filePath = Path.Combine(Application.persistentDataPath, fileName);

            try
            {
                return File.Exists(filePath);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                return false;
            }
        }
    }
}