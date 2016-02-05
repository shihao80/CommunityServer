﻿/*
 *
 * (c) Copyright Ascensio System Limited 2010-2015
 *
 * This program is freeware. You can redistribute it and/or modify it under the terms of the GNU 
 * General Public License (GPL) version 3 as published by the Free Software Foundation (https://www.gnu.org/copyleft/gpl.html). 
 * In accordance with Section 7(a) of the GNU GPL its Section 15 shall be amended to the effect that 
 * Ascensio System SIA expressly excludes the warranty of non-infringement of any third-party rights.
 *
 * THIS PROGRAM IS DISTRIBUTED WITHOUT ANY WARRANTY; WITHOUT EVEN THE IMPLIED WARRANTY OF MERCHANTABILITY OR
 * FITNESS FOR A PARTICULAR PURPOSE. For more details, see GNU GPL at https://www.gnu.org/copyleft/gpl.html
 *
 * You can contact Ascensio System SIA by email at sales@onlyoffice.com
 *
 * The interactive user interfaces in modified source and object code versions of ONLYOFFICE must display 
 * Appropriate Legal Notices, as required under Section 5 of the GNU GPL version 3.
 *
 * Pursuant to Section 7 § 3(b) of the GNU GPL you must retain the original ONLYOFFICE logo which contains 
 * relevant author attributions when distributing the software. If the display of the logo in its graphic 
 * form is not reasonably feasible for technical reasons, you must include the words "Powered by ONLYOFFICE" 
 * in every copy of the program you distribute. 
 * Pursuant to Section 7 § 3(e) we decline to grant you any rights under trademark law for use of our trademarks.
 *
*/


using System;
using System.Web.UI;
using System.Web;
using ASC.Web.Studio.Utility;
using ASC.Web.Studio.Core;
using AjaxPro;
using ASC.Web.Core.Utility.Settings;
using ASC.Core;
using ASC.MessagingSystem;
using Resources;
using ASC.Web.Core.WhiteLabel;

namespace ASC.Web.Studio.UserControls.Management
{
    [AjaxNamespace("GreetingSettingsController")]
    public partial class GreetingSettings : UserControl
    {
        public const string Location = "~/UserControls/Management/GreetingSettings/GreetingSettings.ascx";

        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(GetType());
            Page.RegisterBodyScripts(ResolveUrl("~/usercontrols/management/greetingsettings/js/greetingsettings.js"));

            Page.RegisterStyle("~/usercontrols/management/greetingsettings/css/greetingsettings.less");

            if (GreetingLogoSettings.AvailableControl && !Web.UserControls.WhiteLabel.WhiteLabel.AvailableControl)
            {
                logoSettingsContainer.Controls.Add(LoadControl(GreetingLogoSettings.Location));
            }
        }

        [AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
        public object SaveGreetingSettings(string header)
        {
            try
            {
                SecurityContext.DemandPermissions(SecutiryConstants.EditPortalSettings);

                var currentTenant = CoreContext.TenantManager.GetCurrentTenant();
                currentTenant.Name = header;
                CoreContext.TenantManager.SaveTenant(currentTenant);

                MessageService.Send(HttpContext.Current.Request, MessageAction.GreetingSettingsUpdated);

                return new {Status = 1, Message = Resource.SuccessfullySaveGreetingSettingsMessage};
            }
            catch (Exception e)
            {
                return new {Status = 0, Message = e.Message.HtmlEncode()};
            }
        }

        [AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
        public object RestoreGreetingSettings()
        {
            try
            {
                SecurityContext.DemandPermissions(SecutiryConstants.EditPortalSettings);

                var _tenantInfoSettings = SettingsManager.Instance.LoadSettings<TenantInfoSettings>(TenantProvider.CurrentTenantID);
                _tenantInfoSettings.RestoreDefaultTenantName();
                //SettingsManager.Instance.SaveSettings(_tenantInfoSettings, TenantProvider.CurrentTenantID);

                return new
                    {
                        Status = 1,
                        Message = Resource.SuccessfullySaveGreetingSettingsMessage,
                        CompanyName = CoreContext.TenantManager.GetCurrentTenant().Name
                    };
            }
            catch (Exception e)
            {
                return new {Status = 0, Message = e.Message.HtmlEncode()};
            }
        }
    }
}