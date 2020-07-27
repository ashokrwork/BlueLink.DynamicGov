using Newtonsoft.Json;
using OneHub360.CMS.DAL;
using System.IO;
using System.Web.Hosting;

namespace OneHub360.CMS.Business
{
    public class NumberGenerator
    {
        private static NumberGenerator instance;
        private static NumberGeneratorJson configData;

        private NumberGenerator()
        {
            ReadConfig();
        }

        internal void ReadConfig()
        {
            configData = JsonConvert.DeserializeObject<NumberGeneratorJson>(
                        File.ReadAllText(HostingEnvironment.MapPath(ModuleConstants.NumberGeneratorConfigPath)));
        }

        internal void SaveConfig()
        {
            File.WriteAllText(HostingEnvironment.MapPath(ModuleConstants.NumberGeneratorConfigPath),
                JsonConvert.SerializeObject(configData));
        }

        public static NumberGenerator Generator
        {
            get
            {
                if (instance == null)
                {
                    instance = new NumberGenerator();
                }
                return instance;
            }
        }

        public int MemoOutgoingNumber
        {
            get
            {
                lock (configData)
                {
                    configData.LastMemoOutgoingNumber += 1;
                    SaveConfig();
                    return configData.LastMemoOutgoingNumber;
                }
            }
        }

        public int LetterIncomingNumber
        {
            get
            {
                lock (configData)
                {
                    configData.LastLetterIncomingNumber += 1;
                    SaveConfig();
                    return configData.LastLetterIncomingNumber;
                }
            }
        }

        public int LetterOutgoingNumber
        {
            get
            {
                lock (configData)
                {
                    configData.LastLetterOutgoingNumber += 1;
                    SaveConfig();
                    return configData.LastLetterOutgoingNumber;
                }
            }
        }

        public int MemoIncomingNumber
        {
            get
            {
                lock (configData)
                {
                    configData.LastMemoIncomingNumber += 1;
                    SaveConfig();
                    return configData.LastMemoIncomingNumber;
                }
            }
        }
    }

    public class NumberGeneratorJson
    {
        public int LastMemoOutgoingNumber;
        public int LastMemoIncomingNumber;
        public int LastLetterOutgoingNumber;
        public int LastLetterIncomingNumber;
    }
}
