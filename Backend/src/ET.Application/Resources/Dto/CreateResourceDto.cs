using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ET.Entities;

namespace ET.Resources.Dto
{
    [AutoMapTo(typeof(Resource))]
    public class CreateResourceDto
    {
        public string UserName { get; set; }
        public string EmployeeCode { get; set; }
        public string Country { get; set; }
        public string DepartmentName { get; set; }       
        public string JobTitle { get; set; }
        public string JobTitleLevel { get; set; }
    }

}
