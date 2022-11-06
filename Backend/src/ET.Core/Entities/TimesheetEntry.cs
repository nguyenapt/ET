namespace ET.Entities
{
    using Abp.Domain.Entities;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("TimesheetEntry")]
    public partial class TimesheetEntry : Entity<Guid>
    {

        public Guid? TaskId { get; set; }

        public Guid? AllocationId { get; set; }

        public Guid? LeavePermissionId { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        [StringLength(20)]
        public string TicketName { get; set; }

        public decimal? Hours { get; set; }

        [Column(TypeName = "date")]
        public DateTime RecordDate { get; set; }

        public DateTime? SubmittedTimestamp { get; set; }

        public DateTime? ApprovedTimestamp { get; set; }

        public bool? isOverTime { get; set; }

        public byte? ApprovalStatus { get; set; }

        public Guid? ApprovalId { get; set; }

        [ForeignKey(nameof(AllocationId))]
        public virtual Allocation Allocation { get; set; }

        [ForeignKey(nameof(LeavePermissionId))]
        public virtual LeavePermission LeavePermission { get; set; }

        [ForeignKey(nameof(ApprovalId))]
        public virtual Resource ApprovalUser { get; set; }

        [ForeignKey(nameof(TaskId))]
        public virtual Task Task { get; set; }
    }
}
