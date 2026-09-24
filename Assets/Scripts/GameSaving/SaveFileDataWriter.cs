using System;
using System.IO;
using UnityEngine;

namespace GameSaving
{
    public class SaveFileDataWriter
    {
        private string saveDataDirectoryPath = Application.persistentDataPath;
        
        public bool CheckFileExists(string filename)
        {
            return File.Exists(Path.Combine(saveDataDirectoryPath));
        }
    
        public void DeleteFile(string filename)
        {
            File.Delete(Path.Combine(saveDataDirectoryPath, filename));
        }

        public CharacterSaveData LoadCharacterSaveData(string filename)
        {
            string savePath = Path.Combine(saveDataDirectoryPath, filename);
            CharacterSaveData newData = null;
            
            if (File.Exists(savePath))
            {
                try
                {
                    string dataToLoad = "";
                    using (FileStream fileStream = new FileStream(savePath, FileMode.Open))
                    {
                        using (StreamReader reader = new StreamReader(fileStream))
                        {
                            dataToLoad = reader.ReadToEnd();
                        }
                    }

                    newData = JsonUtility.FromJson<CharacterSaveData>(dataToLoad);
                }
                catch (Exception err)
                {
                    Debug.Log("failed to load: "+ err);
                }
            }

            return newData;
        }
        
        // ReSharper disable Unity.PerformanceAnalysis
        public void CreateNewCharacterSaveFile(CharacterSaveData characterSaveData, string filename)
        {
            Debug.Log(saveDataDirectoryPath);
            string savePath = Path.Combine(saveDataDirectoryPath, filename);

            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(savePath));
                string datastore = JsonUtility.ToJson(characterSaveData);

                using (FileStream fileStream = new FileStream(savePath, FileMode.Create))
                {
                    using (StreamWriter streamWriter = new StreamWriter(fileStream))
                    {
                        streamWriter.Write(datastore);
                    }
                }
            }
            catch (Exception err)
            {
                Debug.Log("saving failed: "+err);
            }
            
        }
    }
}
