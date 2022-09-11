using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Validation
{
    public class ExcelValidation
    {
        public ServiceDto<bool> CheckFileisValidatedExcel(IFormFile file)
        {
            using (XLWorkbook workBook = new XLWorkbook(file.OpenReadStream()))
            {
                IXLWorksheet workSheet = workBook.Worksheet(1);
                foreach (IXLRow row in workSheet.Rows())
                {
                    foreach (IXLCell cell in row.Cells())
                    {
                        string _value = cell.Value.ToString();
                        //dt.Columns.Add(cell.Value.ToString());
                        if (_value.IndexOf("@") != -1
                        || _value.IndexOf("+") != -1
                        || _value.IndexOf("-") != -1
                        || _value.IndexOf("=") != -1
                        )
                        {
                            return new ServiceDto<bool>
                            {
                                Data = false,
                                Status = 0,
                                Message = "Abnormal Char in excel file"
                            };
                        }
                    }
                }

            }
            return null;
        }
    }
}
