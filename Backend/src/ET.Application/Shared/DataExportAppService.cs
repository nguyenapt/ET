using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using Abp.Application.Services;
using ET.Shared.Dto;
using ET.Shared.Extensions;
using ET.SOWRoles;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace ET.Shared
{
    public interface IDataExportAppService
    {
        byte[] GetXlsExportData<THeader, TItem, TFooter>(DataExportInput<THeader, TItem, TFooter> dataExport)
            where THeader : class
            where TItem : class
            where TFooter : class;
    }

    public class DataExportAppService : ApplicationService, IDataExportAppService
    {
        private const string StartCellIndex = "StartCellIndex";

        private readonly ISOWRoleAppService _roleAppService;

        public DataExportAppService(ISOWRoleAppService appService)
        {
            _roleAppService = appService;
        }

        public byte[] GetXlsExportData<THeader, TItem, TFooter>(DataExportInput<THeader, TItem, TFooter> dataExport)
            where THeader : class
            where TItem : class
            where TFooter : class
        {
            var sheetName = "SoW";

            var workbook = new XSSFWorkbook();
            var sheet = workbook.CreateSheet(sheetName);

            var dtRowIndex = 0;

            // Set header
            if (dataExport.Header != null)
            {
                dtRowIndex = SetHeader(workbook, sheet, dataExport.Header);
            }

            // Set table data
            if (dataExport.Items.Any())
            {
                dtRowIndex = SetDataRows(sheet, dataExport.Items, ++dtRowIndex);
            }

            // Footer
            if (dataExport.Footer != null)
            {
                SetFooterRow(workbook, sheet, dataExport.Footer, dtRowIndex);
            }

            using (var ms = new MemoryStream())
            {
                workbook.Write(ms);
                workbook.Close();

                return ms.ToArray();
            }
        }

        private int SetDataRows<TItem>(ISheet sheet, IList<TItem> items, int startRow = 0)
        {
            var properties = typeof(TItem).GetProperties().OrderBy(x => x.GetAttributValue((DisplayAttribute a) => a.Order));

            // set header
            var startGroupRow1 = startRow;
            var groupRow1 = sheet.CreateRow(startGroupRow1);
            var startGroupRow2 = startRow + 1;
            var groupRow2 = sheet.CreateRow(startGroupRow2);

            startRow += 2;
            var headerRow = sheet.CreateRow(startRow);

            var firstItem = items.First();

            var cellIndex = 0;
            if (firstItem is IExcelRowExport dtCellIdx)
            {
                cellIndex = dtCellIdx.StartCellIndex;
            }

            foreach (var propertyInfo in properties)
            {
                if (!propertyInfo.CanRead || propertyInfo.Name == StartCellIndex) continue;

                if (propertyInfo.PropertyType == typeof(IList<IKeyValue>))
                {
                    var kvList = propertyInfo.GetValue(firstItem) as IList<IKeyValue>;
                    
                    var colGroupName1 = propertyInfo.GetAttributValue((DisplayAttribute a) => a.GroupName) ?? string.Empty;
                    var colGroupName2 = propertyInfo.GetAttributValue((DisplayAttribute a) => a.Name) ?? string.Empty;
                    groupRow1.CreateCell(cellIndex).SetCellValue(colGroupName1);

                    if (kvList != null)
                    {
                        groupRow2.CreateCell(cellIndex).SetCellValue(colGroupName2);

                        foreach (var kv in kvList)
                        {
                            headerRow.CreateCell(cellIndex).SetCellValue(kv.Name);
                            cellIndex++;
                        }
                    }
                }
                else
                {
                    var colName = propertyInfo.GetAttributValue((DisplayAttribute a) => a.Name) ?? propertyInfo.Name;
                    headerRow.CreateCell(cellIndex).SetCellValue(colName);
                    cellIndex++;
                }
            }

            // Set data
            startRow++;
            foreach (var item in items)
            {
                var cellIndex2 = 0;
                var dtCellIdx2 = item as IExcelRowExport;
                if (dtCellIdx2 != null)
                {
                    cellIndex2 = dtCellIdx2.StartCellIndex;
                }

                var dtRow = sheet.CreateRow(startRow);
                foreach (var propertyInfo in properties)
                {
                    if (!propertyInfo.CanRead || propertyInfo.Name == StartCellIndex) continue;

                    if (propertyInfo.PropertyType == typeof(IList<IKeyValue>))
                    {
                        var kvList = propertyInfo.GetValue(item) as IList<IKeyValue>;
                        foreach (var kv in kvList)
                        {
                            dtRow.CreateCell(cellIndex2).SetCellValue(kv.ToDisplayValue());
                            cellIndex2++;
                        }
                    }
                    else
                    {
                        dtRow.CreateCell(cellIndex2).SetCellValue(propertyInfo.GetDisplayValue(item));
                        cellIndex2++;
                    }
                }

                // Next row
                startRow++;
            }

            return startRow;
        }

        private int SetHeader<THeader>(IWorkbook workbook, ISheet sheet, THeader headerDto, int startRow = 0)
        {
            var type = headerDto.GetType();
            var properties = type.GetProperties().OrderBy(x => x.GetAttributValue((DisplayAttribute a) => a.Order));
            foreach (var propertyInfo in properties)
            {
                if (!propertyInfo.CanRead || propertyInfo.Name == StartCellIndex) continue;

                var label = propertyInfo.GetAttributValue((DisplayAttribute a) => a.Name);
                var value = propertyInfo.GetValue(headerDto)?.ToString();

                var dataRow = sheet.CreateRow(startRow);

                var cellLabel = dataRow.CreateCell(0);
                SetHeaderStyle(workbook, cellLabel);

                cellLabel.SetCellValue(label);
                dataRow.CreateCell(1).SetCellValue(value);
                startRow++;
            }

            return startRow;
        }

        private int SetFooterRow<TFooter>(IWorkbook workbook, ISheet sheet, TFooter footerDto, int startRow = 0)
        {
            var type = footerDto.GetType();
            var properties = type.GetProperties().OrderBy(x => x.GetAttributValue((DisplayAttribute a) => a.Order)); ;

            var startCellIndex = (footerDto as IExcelRowExport)?.StartCellIndex ?? 0;
            var dataRow = sheet.CreateRow(startRow);

            foreach (var propertyInfo in properties)
            {
                if (!propertyInfo.CanRead || propertyInfo.Name == StartCellIndex) continue;

                var label = propertyInfo.GetAttributValue((DisplayAttribute a) => a.Name);
                
                if (propertyInfo.PropertyType == typeof(IList<IKeyValue>))
                {
                    var kvList = propertyInfo.GetValue(footerDto) as IList<IKeyValue>;
                    foreach (var kv in kvList)
                    {
                        var cell = dataRow.CreateCell(startCellIndex);
                        SetFooterStyle(workbook, cell);

                        cell.SetCellValue(kv.ToDisplayValue());
                        startCellIndex++;
                    }
                }
                else
                {
                    var cell = dataRow.CreateCell(startCellIndex);
                    SetFooterStyle(workbook, cell);

                    cell.SetCellValue(propertyInfo.GetDisplayValue(footerDto));
                    startCellIndex++;
                }
            }

            return startRow;
        }

        private void SetHeaderStyle(IWorkbook workbook, ICell cell)
        {
            //Create a Title row
            var titleFont = workbook.CreateFont();
            titleFont.Boldweight = (short)FontBoldWeight.Bold;
            titleFont.FontHeightInPoints = 11;
            titleFont.Underline = FontUnderlineType.Single;

            var titleStyle = workbook.CreateCellStyle();
            titleStyle.SetFont(titleFont);
        }

        private void SetFooterStyle(IWorkbook workbook, ICell cell)
        {
            //Create a Title row
            var titleFont = workbook.CreateFont();
            titleFont.Boldweight = (short)FontBoldWeight.Bold;
            titleFont.FontHeightInPoints = 11;
            titleFont.Underline = FontUnderlineType.Single;

            var titleStyle = workbook.CreateCellStyle();
            titleStyle.SetFont(titleFont);
        }
    }
}
