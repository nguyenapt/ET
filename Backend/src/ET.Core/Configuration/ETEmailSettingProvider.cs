using System.Collections.Generic;
using Abp.Configuration;

namespace ET.Configuration
{
    public class ETEmailSettingProvider : SettingProvider
    {
        public override IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context)
        {
            return new[]
            {
                new SettingDefinition(
                    ETConsts.ETEmailSettings.EnableSmtp,
                    "false"
                ),

                new SettingDefinition(
                    $"{ETConsts.ETEmailSettings.AllocatedResourceName}.{ETConsts.ETEmailSettings.AllocatedResourceTitle}",
                    ETConsts.EmailTemplate.AllocatedResource.Title
                ),

                new SettingDefinition(
                    $"{ETConsts.ETEmailSettings.AllocatedResourceName}.{ETConsts.ETEmailSettings.AllocatedResourceBody}",
                    ETConsts.EmailTemplate.AllocatedResource.Content
                ),

                new SettingDefinition(
                    $"{ETConsts.ETEmailSettings.SowNoteChangedName}.{ETConsts.ETEmailSettings.AllocatedResourceTitle}",
                    ETConsts.EmailTemplate.ChangedSowNote.Title
                ),

                new SettingDefinition(
                    $"{ETConsts.ETEmailSettings.SowNoteChangedName}.{ETConsts.ETEmailSettings.AllocatedResourceBody}",
                    ETConsts.EmailTemplate.ChangedSowNote.Content
                ),

                new SettingDefinition(
                    $"{ETConsts.ETEmailSettings.SubmitTimeSheet}.{ETConsts.ETEmailSettings.AllocatedResourceTitle}",
                ETConsts.EmailTemplate.SubmitTimeSheet.Title
                ),
                
                new SettingDefinition(
                    $"{ETConsts.ETEmailSettings.SubmitTimeSheet}.{ETConsts.ETEmailSettings.AllocatedResourceBody}",
                ETConsts.EmailTemplate.SubmitTimeSheet.Content
                ),

                new SettingDefinition($"{ETConsts.ETEmailSettings.SubmitTimeSheet}.{ETConsts.ETEmailSettings.AllocatedResourceBody}",
                ETConsts.EmailTemplate.SubmitTimeSheet.Content),

                new SettingDefinition($"{ETConsts.ETEmailSettings.SubmitLeavePermission}.{ETConsts.ETEmailSettings.AllocatedResourceTitle}",
                ETConsts.EmailTemplate.SubmitLeavePermission.Title),

                new SettingDefinition($"{ETConsts.ETEmailSettings.SubmitLeavePermission}.{ETConsts.ETEmailSettings.AllocatedResourceBody}",
                ETConsts.EmailTemplate.SubmitLeavePermission.Content),

                new SettingDefinition($"{ETConsts.ETEmailSettings.ApproveLeavePermission}.{ETConsts.ETEmailSettings.AllocatedResourceTitle}",
                ETConsts.EmailTemplate.ApproveLeavePermission.Title),

                new SettingDefinition($"{ETConsts.ETEmailSettings.ApproveLeavePermission}.{ETConsts.ETEmailSettings.AllocatedResourceBody}",
                ETConsts.EmailTemplate.ApproveLeavePermission.Content),

                  new SettingDefinition($"{ETConsts.ETEmailSettings.RejectLeavePermission}.{ETConsts.ETEmailSettings.AllocatedResourceTitle}",
                ETConsts.EmailTemplate.RejectLeavePermission.Title),

                new SettingDefinition($"{ETConsts.ETEmailSettings.RejectLeavePermission}.{ETConsts.ETEmailSettings.AllocatedResourceBody}",
                ETConsts.EmailTemplate.RejectLeavePermission.Content),

                new SettingDefinition(
                    $"{ETConsts.ETEmailSettings.ApproveTimeSheet}.{ETConsts.ETEmailSettings.AllocatedResourceTitle}",
                ETConsts.EmailTemplate.ApproveTimeSheet.Title
                ),

                new SettingDefinition(
                    $"{ETConsts.ETEmailSettings.ApproveTimeSheet}.{ETConsts.ETEmailSettings.AllocatedResourceBody}",
                    ETConsts.EmailTemplate.ApproveTimeSheet.Content
                ),

                new SettingDefinition(
                    $"{ETConsts.ETEmailSettings.RejectTimeSheet}.{ETConsts.ETEmailSettings.AllocatedResourceTitle}",
                    ETConsts.EmailTemplate.RejectTimeSheet.Title
                ),

                new SettingDefinition(
                    $"{ETConsts.ETEmailSettings.RejectTimeSheet}.{ETConsts.ETEmailSettings.AllocatedResourceBody}",
                    ETConsts.EmailTemplate.RejectTimeSheet.Content
                ),
            };
        }
    }
}
