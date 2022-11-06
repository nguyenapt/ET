using ET.Shared.Dto;
using System.ComponentModel.DataAnnotations;

namespace ET.SoW.ExportDtos
{
    public class SoWExportHeader : IExcelRowExport
    {
        [Display(Order = 1, Name = "Client")]
        public string ClientName { get; set; }

        [Display(Name="Division", Order = 2)]
        public string Department { get; set; }

        [Display(Name="Project", Order = 3)]
        public string Project { get; set; }

        [Display(Name = "SoW Name", Order = 4)]
        public string SoWName { get; set; }

        [Display(Name = "Sow Number", Order = 5)]
        public string SowNumber { get; set; }

        [Display(Name = "Version", Order = 6)]
        public string Version { get; set; }

        [Display(Name="Status", Order = 7)]
        public string Status { get; set; }

        [Display(Name="FileUrl", Order = 8)]
        public string FileUrl { get; set; }

        [Display(Name="Start Date", Order = 9)]
        public string StartDate { get; set; }

        [Display(Name="End Date", Order = 10)]
        public string EndDate { get; set; }

        [Display(Name="KAM Name", Order = 11)]
        public string KAMName { get; set; }

        [Display(Name="Client PO Number", Order = 12)]
        public string ClientPONumber { get; set; }

        [Display(Name = "SOW Note", Order = 13)]
        public string SOWNote { get; set; }

        public int StartCellIndex { get; set; }
    }
}
