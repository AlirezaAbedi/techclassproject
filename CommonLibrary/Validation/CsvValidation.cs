using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Http;
using Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Validation
{
    public class CsvValidation
    {
        public ServiceDto<bool> CheckFileisValidatedCSV(IFormFile file)
        {

            var result = new StringBuilder();
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                { 
                    string line = reader.ReadLine();
                    if (line.IndexOf("@") != -1 
                        || line.IndexOf("+") != -1
                        || line.IndexOf("-") != -1
                        || line.IndexOf("=") != -1
                        )
                    {
                        return new ServiceDto<bool>
                        {
                            Data = false,
                            Status = 0,
                            Message = "Abnormal Char in csv file"
                        };
                    }
                }
            }

            return new ServiceDto<bool>
            {
                Data = true
            };

        }

        
    }
}
