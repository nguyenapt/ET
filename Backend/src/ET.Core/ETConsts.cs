namespace ET
{
    public class ETConsts
    {
        public const string LocalizationSourceName = "ET";

        public const string ConnectionStringName = "Default";

        public const string SavedTimeSheetStatus = "Editing";

        public const bool MultiTenancyEnabled = true;

        public class ETSettingDefinitions
        {
            public const string EmailSettingGroupName = "SMTP Setting";
            public const string SiteSettingGroupName = "Site Setting";
            public const string LdapGroupName = "Ldap Setting";
            public const string UserManagementGroupName = "User Management Setting";
            public const string EmailTemplateGroupName = "Email Template Setting";
        }

        public class ETEmailSettings
        {
            public const string EnableSmtp = "Abp.Net.Mail.Smtp.Enable";
            public const string AllocatedResourceName = "EmailTemplate.AllocatedResource";
            public const string SowNoteChangedName = "EmailTemplate.ChangedSowNote";
            public const string SubmitTimeSheet = "EmailTemplate.SubmitTimeSheet";
            public const string SubmitLeavePermission = "EmailTemplate.SubmitLeavePermission";
            public const string ApproveLeavePermission = "EmailTemplate.ApproveLeavePermission";
            public const string ApproveTimeSheet = "EmailTemplate.ApproveTimeSheet";
            public const string RejectTimeSheet = "EmailTemplate.RejectTimeSheet";
            public const string RejectLeavePermission = "EmailTemplate.RejectLeavePermission";

            public const string AllocatedResourceBody = "Body";
            public const string AllocatedResourceTitle = "Title";

        }

        public class EmailTemplate
        {
            //Send email to notify a resource about their Allocation
            public class AllocatedResource
            {
                public const string Title = "Your new project allocation";
                public const string Content = "<table class='body' style='background-color: #fff; padding-top: 20px;' border='0' cellspacing='0' cellpadding='0'><tbody><tr><td>&nbsp;</td><td class='container'><div class='content'><table class='main'><tbody><tr><td class='wrapper'><table border='0' cellspacing='0' cellpadding='0'><tbody><tr><td><p>Dear {{FullName}},</p><p>Please be informed you has been allocated to this new work order with following details.</p></td></tr></tbody></table></td></tr></tbody></table></div></td><td>&nbsp;</td></tr></tbody></table><table class='body' style='background-color: #fff; padding-bottom: 15px;' border='0' cellspacing='0' cellpadding='0'><tbody><tr><td>&nbsp;</td><td class='container'><div class='content'><table class='main'><tbody><tr><td class='wrapper'><table border='0' cellspacing='0' cellpadding='0'><tbody><tr><td style='text-align: center; padding-bottom: 10px; font-size:14px;padding-left: 10px; padding-right: 10px;'><strong>Project Name</strong></td><td style='text-align: center; padding-bottom: 10px; font-size: 14px;padding-left: 10px; padding-right: 10px;'><strong>PM Name</strong></td><td style='text-align: center; padding-bottom: 10px; font-size: 14px; padding-left: 10px; padding-right: 10px;'><strong>Bill Type</strong></td><td style='text-align: center; padding-bottom: 10px; font-size: 14px; padding-left: 10px; padding-right: 10px;'><strong>Role</strong></td><td style='text-align: center; padding-bottom: 10px; font-size: 14px; padding-left: 10px; padding-right: 10px;'><strong>FTE</strong></td><td style='text-align: center; padding-bottom: 10px; font-size: 14px; padding-left: 10px; padding-right: 10px;'><strong>Total Hour</strong></td><td style='text-align: center; padding-bottom: 10px; font-size: 14px; padding-left: 10px; padding-right: 10px;'><strong>Total Hour </br>Per month </strong></td><td style='text-align: center; padding-bottom: 10px; font-size: 14px; padding-left: 10px; padding-right: 10px;'><strong>Start Date </br>dd/mm/yy </strong></td><td style='text-align: center; padding-bottom: 10px; font-size: 14px;'><strong>End Date </br>dd/mm/yy </strong></td></td><td style='text-align: center; padding-bottom: 10px; font-size: 14px;'><strong>Time note</strong></td></tr><tr><td style='border-top: 1px solid #dedede; padding-bottom: 20px;' colspan='12'>&nbsp;</td></tr><tr><td style='text-align: center; padding-bottom: 10px; font-size: 14px; padding-left: 10px; padding-right: 10px;'>{{ProjectName}}</td><td style='text-align: center; padding-bottom: 10px; font-size: 14px; padding-left: 10px; padding-right: 10px;'>{{PMName}}</td><td style='text-align: center; padding-bottom: 10px; font-size: 14px; padding-left: 10px; padding-right: 10px;'>{{BillType}}</td><td style='text-align: center; padding-bottom: 10px; font-size: 14px; padding-left: 10px; padding-right: 10px;'>{{Role}}</td><td style='text-align: center; padding-bottom: 10px; font-size: 14px; padding-left: 10px; padding-right: 10px;'>{{FTE}}</td><td style='text-align: center; padding-bottom: 10px; font-size: 14px; padding-left: 10px; padding-right: 10px;'>{{TotalHour}}</td><td style='text-align: center; padding-bottom: 10px; font-size: 14px; padding-left: 10px; padding-right: 10px;'>{{TotalHourPerMonth}}</td><td style='text-align: center; padding-bottom: 10px; font-size: 14px; padding-left: 10px; padding-right: 10px;'>{{StartDate}}</td><td style='text-align: center; padding-bottom: 10px; font-size: 14px; padding-left: 10px; padding-right: 10px;'>{{EndDate}}</td><td style='text-align: center; padding-bottom: 10px; font-size: 14px; padding-left: 10px; padding-right: 10px;'>{{TimeNote}}</td></tr><tr><td colspan='12'><hr style='border-color:#dedede'></td></tr><tr><td colspan='12'><p>If you have any question, please help to contact your PM {{PMEmail}}.</p><p>Thank you!</p><p>ET Team</p></td></tr></tbody></table></td></tr></tbody></table></div></td><td>&nbsp;</td></tr></tbody></table>";
            }

            //Send email to notify a PMO when their SoW note changed.
            public class ChangedSowNote
            {
                public const string Title = "Your new SoW status note";
                public const string Content = "<table class='body' style='background-color: #fff; padding-top: 20px;' border='0' cellspacing='0' cellpadding='0'><tbody><tr><td>&nbsp;</td><td class='container'><div class='content'><table class='main'><tbody><tr><td class='wrapper'><table border='0' cellspacing='0' cellpadding='0'><tbody><tr><td><p>Dear {{FullName}},</p><p>Please be informed that your SoW Status note has been changed.</p></td></tr></tbody></table></td></tr></tbody></table></div></td><td>&nbsp;</td></tr></tbody></table><br><table class='body' style='background-color: #fff; padding-bottom: 15px;' border='0' cellspacing='0' cellpadding='0'><tbody><tr><td>&nbsp;</td><td class='container'><div class='content'><table class='main'><tbody><tr><td class='wrapper'><table border='0' cellspacing='0' cellpadding='0'><tbody><tr><td style='text-align: center; padding-bottom: 10px; font-size: 14px;padding-left: 10px; padding-right: 10px;'><strong>SoW Name</strong></td><td style='text-align: center; padding-bottom: 10px; font-size: 14px; padding-left: 10px; padding-right: 10px;'><strong>SoW Number</strong></td><td style='text-align: center; padding-bottom: 10px; font-size: 14px; padding-left: 10px; padding-right: 10px;'><strong>SoW Version</strong></td><td style='text-align: center; padding-bottom: 10px; font-size: 14px; padding-left: 10px; padding-right: 10px;'><strong>Status note</strong></td></tr><tr><td style='border-top: 1px solid #dedede; padding-bottom: 20px;' colspan='12'>&nbsp;</td></tr><tr><td style='text-align: center; padding-bottom: 10px; font-size: 14px; padding-left: 10px; padding-right: 10px;'>{{SoWName}}</td><td style='text-align: center; padding-bottom: 10px; font-size: 14px; padding-left: 10px; padding-right: 10px;'>{{SoWNumber}}</td><td style='text-align: center; padding-bottom: 10px; font-size: 14px; padding-left: 10px; padding-right: 10px;'>{{SoWVersion}}</td><td style='text-align: center; padding-bottom: 10px; font-size: 14px; padding-left: 10px; padding-right: 10px;'>{{StatusNote}}</td></tr><tr><td colspan='12'><hr style='border-color:#dedede'></td></tr><tr><td colspan='12'><p>If you have any question, please help to contact your last editor {{LastEditorEmail}}.</p><p>Thank you!</p><p>ET Team</p></td></tr></tbody></table></td></tr></tbody></table></div></td><td>&nbsp;</td></tr></tbody></table>";
            }

            // Send email to notify that an user has submitted his or her time sheet
            public class SubmitTimeSheet
            {
                public const string Title = "ET Notification - Request to Approve Timesheet";
                public const string Content = "<html xmlns='http://www.w3.org/1999/xhtml'> <head> <meta http-equiv='Content-Type' content='text/html; charset=utf-8'/> <title>EfficientTime Notification - Please Approval Timesheet</title> <style type='text/css'> h1, h2, h3, body, p, div, span, ul, li{margin: 0; padding: 0; font-size: 100%;} a {text-decoration:none;} body {color:#000; font-family:Arial, Helvetica, sans-serif; line-height:20px; background:#cacaca;} a:hover {text-decoration:underline;} </style> </head> <body> <center> <table style='border-spacing: 0px; margin: 20px auto; width: 600px;'> <tbody> <tr style='vertical-align: bottom;background-color: #0f7ba5;'> <td style='width:156px;height:50px;margin: 0px;padding: 0px;vertical-align: top;'></td> <td style='background-color:#0f7ba5;float:right;color:#FFF;text-align:right;margin:40px 10px 0px 0px;width:100%;height:40px'><b>Request to Approve Timesheet<br> by {{SubmitterFullName}}</b></td> </tr> <tr> <td colspan='2' style='padding:30px 20px; background:#fff; font-size:12px;'> <h2>Dear {{ApprovalFullName}},</h2> <br/> <p>{{SubmitterFullName}} has requested you to approve his or her timesheet in ET. You can review the timesheet by clicking on the link below:</p> <br /> <div class='link' align='center'><a href='{{ApproveTimeSheetLink}}'>Click here</a></div> <br /> <p>For any questions, feel free to send us an email anytime or contact us via Skype from 9:00AM to 6:00PM, Viet Nam Time (GMT +7), Mondays to Fridays:</p> <br /> <div class='link' align='center'> Email: {{EmailSupport}}<br /> Skype: {{SkypeSupport}} </div> <br /> <br />The ET Team </td> </tr> <tr> <td colspan='2' style='height:35px; line-height:35px; text-align:center; background:#0f7ba5;font-size:12px; color:#fff'><a style='font-size:12px; color:#fff;text-decoration:none;' href='http://facebook.com/Provide it here'>Like on Facebook</a>|<a style='font-size:12px; color:#fff;text-decoration:none;' href='http://efficienttime.com'>Visit our website</a></td> </tr> </tbody> </table> </center> </body></html>";
            }

            // Send email to notify PM that an user has submitted his or her leave
            public class SubmitLeavePermission
            {
                public const string Title = "ET Notification - Request to Approve Leave";
                public const string Content = @"<html xmlns='http://www.w3.org/1999/xhtml'>
<head>
    <meta http-equiv='Content-Type' content='text/html; charset=utf-8' />
    <title>EfficientTime Notification - Request to Approve Leave</title>
    <style type='text/css'>
        h1, h2, h3, body, p, div, span, ul, li {
            margin: 0;
            padding: 0;
            font-size: 100%;
        }

        a {
            text-decoration: none;
        }

        body {
            color: #000;
            font-family: Arial, Helvetica, sans-serif;
            line-height: 20px;
            background: #cacaca;
        }

        a:hover {
            text-decoration: underline;
        }
    </style>
</head>

<body>
    <center>
        <table style='border-spacing: 0px; margin: 20px auto; width: 600px;'>
            <tbody>
                <tr style='vertical-align: bottom; background-color: #0f7ba5;'>
                    <td style='width: 156px; height: 50px; margin: 0px; padding: 0px; vertical-align: top;'>                        
                    </td>
                    <td style='background-color: #0f7ba5; float: right; color: #FFF; text-align: right; margin: 40px 10px 0px 0px; width: 100%; height: 40px'>
                        <b>Request to Approve Leave Application by {{SubmitterFullName}}</b>
                    </td>
                </tr>
                <tr>
                    <td colspan='2' style='padding: 30px 20px; background: #fff; font-size: 12px;'>
                        <h2>Dear {{ApprovalFullName}},</h2>
                        <br />
                        <p>{{SubmitterFullName}} has requested you to approve his or her Leave Application for the date(s): {{LeaveFromTime}} to {{LeaveToTime}} ({{LeaveType}}) in EfficientTime with the following leave reason:</p>
                        <br>
                        <p><strong><center>{{Reason}}</center></strong></p>
                        <br>
                        You can review the leave application by clicking on the link below:</p>
                        <br>
                        <div style='text-align: center; font-weight: bold'><a href='{{ApproveTimeSheetLink}}'>Click here</a></div>
                        <br />
                        <p>For any questions, feel free to send us an email anytime or contact us via Skype from 9:00AM to 6:00PM, Viet Nam Time (GMT +7), Mondays to Fridays:</p>
                        <br />
                        <div style='text-align: center;'>
                            Email: {{EmailSupport}}
				<br />
                            Skype: {{SkypeSupport}}
                        </div>
                        <br />
                        <br />
                        The ET Team
                    </td>
                </tr>
                <tr>
                    <td colspan='2' style='height: 35px; line-height: 35px; text-align: center; background: #0f7ba5; font-size: 12px; color: #fff'>
                        <a style='font-size: 12px; color: #fff; text-decoration: none;' href='http://facebook.com/efficienttime'>Like on Facebook</a> | <a style='font-size: 12px; color: #fff; text-decoration: none;' href='http://efficienttime.com'>Visit our website</a>
                    </td>
                </tr>
            </tbody>
        </table>
    </center>
</body>
</html>
";
            }

            public class ApproveLeavePermission
            {
                public const string Title = "ET Notification - Leave Application Approved";
                public const string Content = @"<html xmlns='http://www.w3.org/1999/xhtml'>
<head>
    <meta http-equiv='Content-Type' content='text/html; charset=utf-8' />
    <title>EfficientTime Notification - Leave Application Approved</title>
    <style>
        h1, h2, h3, body, p, div, span, ul, li {margin: 0; padding: 0; font-size: 100%;}
        a {text-decoration:none;}
        body {color:#000; font-family:Arial, Helvetica, sans-serif; line-height:20px; background:#cacaca;}
   
        a:hover {text-decoration:underline;}
    </style>
</head>

<body>
	<center>
<table style='border-spacing: 0px; margin: 20px auto; width: 600px'>
		<tbody>
		<tr style='vertical-align: bottom;background-color: #0f7ba5;'>
			<td style='height: 50px; margin: 5px 0px 0px 0px; padding: 0px; vertical-align: top;'>			  
			</td>
			<td style='background-color:#0f7ba5;float:right;color:#FFF;text-align:right;margin:40px 0px 0px 0px;width:100%;height:40px;padding-right: 10px'>
				<b>Leave Application Approved</b>
			</td>
		</tr>
		<tr>
			<td colspan='2' style='padding:30px 20px; background:#fff; font-size:12px;'>
				<h2>Dear {{SubmitterFullName}},</h2>
				<br>
                <p>{{ApprovalFullName}} has approved your leave application dated for the date(s): {{LeaveFromTime}} to {{LeaveToTime}} in EfficientTime.  You can review the leave application by clicking on the link below:</p>
		        <br/>
		        <div style='text-align:center; font-weight:bold'> <a href='{{ApproveTimeSheetLink}}'>Click here</a></div>
		        <br/>
                <p>For any questions, feel free to send us an email anytime or contact us via Skype from 9:00AM to 6:00PM, Viet Nam Time (GMT +7), Mondays to Fridays:</p>
				<br/>
				<div style='text-align:center; font-weight:bold'>Email: {{EmailSupport}}
				<br/> 	Skype: {{SkypeSupport}} </div>
				<br/> <br/>
				The ET Team
			</td>
		</tr>
		<tr>
			<td colspan='2' style='height:35px; line-height:35px; text-align:center; background:#0f7ba5;font-size:12px; color:#fff'>
				<a style='font-size:12px; color:#fff;text-decoration:none;' href='http://facebook.com/efficienttime'>Like on Facebook</a> | <a style='font-size:12px; color:#fff;text-decoration:none;' href='http://efficienttime.com'>Visit our website</a>
			</td>
		</tr>
	</tbody>
</table>
</center>
</body>
</html>";
            }

            public class RejectLeavePermission
            {
                public const string Title = "ET Notification - Leave Application Rejected";
                public const string Content = @"<html xmlns='http://www.w3.org/1999/xhtml'>
<head>
    <meta http-equiv='Content-Type' content='text/html; charset=utf-8' />
    <title>EfficientTime Notification - Leave Application Rejected</title>
    <style type='text/css'>
        h1, h2, h3, body, p, div, span, ul, li {margin: 0; padding: 0; font-size: 100%;}
        a {text-decoration:none;}
        body {color:#000; font-family:Arial, Helvetica, sans-serif; line-height:20px; background:#cacaca;}
       
        a:hover {text-decoration:underline;}
         img {
        border:none;        
        }
    </style>
</head>

<body>
	<center>
<table style='
    border-spacing: 0px;
    margin: 20px auto;
    width: 600px;
'>
		<tbody>
		<tr style='vertical-align: bottom;background-color: #0f7ba5;'>
			<td style='width:156px;height:50px;margin: 0px;padding: 0px;vertical-align: top;'>			
			</td>
			<td style='background-color:#0f7ba5;float:right;color:#FFF;text-align:right;margin:40px 10px 0px 0px;width:100%;height:40px'>
				<b>Leave Application Rejected</b>
			</td>
		</tr>
		<tr>
			<td colspan='2' style='padding:30px 20px; background:#fff; font-size:12px;'>
				<h2>Dear {{SubmitterFullName}},</h2>
				<br/>
                <p>{{ApprovalFullName}} has rejected your leave application dated for the date(s): {{LeaveFromTime}} to {{LeaveToTime}} in EfficientTime. You can review the leave application by clicking on the link below:</p>
		        <br/>
		        <div class='link' align='center'><a href='{{ApproveTimeSheetLink}}'>Click here</a></div>
		        <br/>
		        <p>Here are your manager's comments: </p>
		        <br/>
		        <p align='center'>{{RejectReason}}</p>
		        <br/>
                <p>For any questions, feel free to send us an email anytime or contact us via Skype from 9:00AM to 6:00PM, Viet Nam Time (GMT +7), Mondays to Fridays:</p>
				<br/>
				<div class='link' align='center'> 	Email: {{EmailSupport}}
				<br/> 	Skype: {{SkypeSupport}} </div>
				<br/> <br/>
				The ET Team
			</td>
		</tr>
		<tr>
			<td colspan='2' style='text-align:center; height: 35px; vertical-align: middle; background:#0f7ba5;font-size:12px; color:#fff'>
				<a style='font-size:12px; color:#fff;text-decoration:none;' href='http://facebook.com/Provide it here'>Like on Facebook</a> | <a style='font-size:12px; color:#fff;text-decoration:none;' href='http://Provide it here.com'>Visit our website</a>
			</td>
		</tr>
	</tbody>
</table>
</center>
</body>
</html>";
            }

            // Send email to notify that an user has submitted his or her time sheet
            public class ApproveTimeSheet
            {
                public const string Title = "EfficientTime Notification - Timesheet Approved";
                public const string Content = "<html xmlns='http://www.w3.org/1999/xhtml'> <head> <meta http-equiv='Content-Type' content='text/html; charset=utf-8' /> <title>EfficientTime Notification - Please Account Changed</title> <style type='text/css'> h1, h2, h3, body, p, div, span, ul, li {margin: 0; padding: 0; font-size: 100%;} a {text-decoration:none;} body {color:#000; font-family:Arial, Helvetica, sans-serif; line-height:20px; background:#cacaca;} a:hover {text-decoration:underline;} </style> </head> <body> <center> <table style=' border-spacing: 0px; margin: 20px auto; width: 600px;'> <tbody> <tr style='vertical-align: bottom;background-color: #0f7ba5;'> <td style='width:156px;height:50px;margin: 0px;padding: 0px;vertical-align: top;'></td> <td style='background-color:#0f7ba5;float:right;color:#FFF;text-align:right;margin:40px 10px 0px 0px;width:100%;height:40px'><b>Attention</b></td> </tr> <tr> <td colspan='2' style='padding:30px 20px; background:#fff; font-size:12px;'> <h2>Dear {{SubmitterFullName}},</h2> <br/> <p>{{ApprovalFullName}} approved the timesheet you submitted</p> <br /> <p>Please review your timesheet by clicking <a href='{{TimeSheetLink}}'>here</a></p> <br /> <p>For any questions, feel free to send us an email anytime or contact us via Skype from 9:00AM to 6:00PM, Viet Nam Time (GMT +7), Mondays to Fridays:</p> <br /> <div class='link' style='text-align: center'> Email: {{EmailSupport}}<br /> Skype: {{SkypeSupport}} </div> <br /> <br />The ET Team </td> </tr> <tr> <td colspan='2' style='height:35px; line-height:35px; text-align:center; background:#0f7ba5;font-size:12px; color:#fff'><a style='font-size:12px; color:#fff;text-decoration:none;' href='http://facebook.com/Provide it here'>Like on Facebook</a> | <a style='font-size:12px; color:#fff;text-decoration:none;' href='http://Provide it here.com'>Visit our website</a></td> </tr> </tbody> </table> </center> </body></html>";
            }

            public class RejectTimeSheet
            {
                public const string Title = "ET Notification - Timesheet rejected";
                public const string Content = "<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'><html xmlns='http://www.w3.org/1999/xhtml'><head> <meta http-equiv='Content-Type' content='text/html; charset=utf-8' /> <title>EfficientTime Notification - Timesheet Rejected</title> <style type='text/css'> h1, h2, h3, body, p, div, span, ul, li {margin: 0; padding: 0; font-size: 100%;} a {text-decoration:none;} body {color:#000; font-family:Arial, Helvetica, sans-serif; line-height:20px; background:#cacaca;} a:hover {text-decoration:underline;} </style></head><body><center><table style=' border-spacing: 0px; margin: 20px auto; width: 600px;'><tbody><tr style='vertical-align: bottom;background-color: #0f7ba5;'><td style='width:156px;height:50px;margin: 0px;padding: 0px;vertical-align: top;'></td><td style='background-color:#0f7ba5;float:right;color:#FFF;text-align:right;margin:40px 10px 0px 0px;width:100%;height:40px'><b>Timesheet Rejected</b></td></tr><tr><td colspan='2' style='padding:30px 20px; background:#fff; font-size:12px;'><h2>Dear {{SubmitterFullName}},</h2><br/> <p>{{ApprovalFullName}} has rejected your timesheet in EfficientTime. You can review the timesheet by clicking on the link below:</p> <br /> <div class='link' align='center'><a href='{{TimeSheetLink}}'>Click here</a></div> <br /> <p>Here are your manager's comments: </p> <br /> <p align='center'>{{Comment}}</p> <br /> <p>For any questions, feel free to send us an email anytime or contact us via Skype from 9:00AM to 6:00PM, Viet Nam Time (GMT +7), Mondays to Fridays:</p><br /><div class='link' align='center'> Email: {{EmailSupport}}<br /> Skype: {{SkypeSupport}} </div><br /> <br />The ET Team</td></tr><tr><td colspan='2' style='height:35px; line-height:35px; text-align:center; background:#0f7ba5;font-size:12px; color:#fff'><a style='font-size:12px; color:#fff;text-decoration:none;' href='http://facebook.com/Provide it here'>Like on Facebook</a> | <a style='font-size:12px; color:#fff;text-decoration:none;' href='http://Provide it here.com'>Visit our website</a></td></tr></tbody></table></center></body></html>";
            }
        }
    }
}
