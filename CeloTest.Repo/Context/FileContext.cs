using CeloTest.Domain;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CeloTest.Repo.Context
{
    public class FileContext: IFileContext
    {
        private readonly IConfiguration _config;
        public FileContext(IConfiguration config)
        {
            _config = config;
        }
        public List<User> Read()
        {
            using (StreamReader r = new StreamReader(_config["FileLocation"]))
            {
                string json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<List<User>>(json);
            }
        }

        public void Write(IList<User> users)
        {
            using (StreamWriter file = File.CreateText(_config["FileLocation"]))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, users);
            }
        }
    }

    public interface IFileContext
    {
        List<User> Read();
        void Write(IList<User> users);
    }
}
