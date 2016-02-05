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
using System.Security.Authentication;
using ASC.Core;
using ASC.Core.Tenants;
using ASC.Core.Users;
using ASC.Data.Storage;
using ASC.Mail.Aggregator.Common.Logging;
using ASC.Mail.Aggregator.Common.Utils;

namespace ASC.Mail.Aggregator.Common.Extension
{
    public static class MailBoxExtensions
    {
        public static bool IsUserTerminated(this MailBox mailbox)
        {
            try
            {
                CoreContext.TenantManager.SetCurrentTenant(mailbox.TenantId);

                var user = CoreContext.UserManager.GetUsers(new Guid(mailbox.UserId));

                return user.Status == EmployeeStatus.Terminated;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IsUserRemoved(this MailBox mailbox)
        {
            try
            {
                CoreContext.TenantManager.SetCurrentTenant(mailbox.TenantId);
                Guid user;
                if (!Guid.TryParse(mailbox.UserId, out user))
                    return true;

                return !CoreContext.UserManager.UserExists(user) || CoreContext.UserManager.IsSystemUser(user);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static UserInfo GetUserInfo(this MailBox mailbox)
        {
            try
            {
                CoreContext.TenantManager.SetCurrentTenant(mailbox.TenantId);
                var userInfo = CoreContext.UserManager.GetUsers(new Guid(mailbox.UserId));

                return userInfo;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static Defines.TariffType GetTenantStatus(this MailBox mailbox, int tenantOverdueDays, ILogger log = null)
        {
            log = log ?? new NullLogger();

            Defines.TariffType type;

            try
            {
                CoreContext.TenantManager.SetCurrentTenant(mailbox.TenantId);

                var tenantInfo = CoreContext.TenantManager.GetCurrentTenant();

                if (tenantInfo.Status == TenantStatus.RemovePending)
                    return Defines.TariffType.LongDead;

                try
                {
                    SecurityContext.AuthenticateMe(tenantInfo.OwnerId);
                }
                catch (InvalidCredentialException)
                {
                    SecurityContext.AuthenticateMe(mailbox.UserId);
                }

                type = ApiHelper.GetTenantTariff(tenantOverdueDays);

            }
            catch (Exception ex)
            {
                log.Error("GetTenantStatus() Exception:\r\n{0}\r\n", ex.ToString());
                type = Defines.TariffType.Active;
            }

            return type;
        }

        public static bool IsTenantQuotaEnded(this MailBox mailbox, long minBalance, ILogger log = null)
        {
            var result = false;
            log = log ?? new NullLogger();

            try
            {
                var quotaController = new TennantQuotaController(mailbox.TenantId);
                var quota = CoreContext.TenantManager.GetTenantQuota(mailbox.TenantId);
                var usedQuota = quotaController.QuotaCurrentGet();
                log.Debug("Tenant = {0}. Tenant quota = {1}Mb ({2}), used quota = {3}Mb ({4})", mailbox.TenantId,
                    MailUtil.BytesToMegabytes(quota.MaxTotalSize), quota.MaxTotalSize, MailUtil.BytesToMegabytes(usedQuota), usedQuota);
                result = quota.MaxTotalSize - usedQuota < minBalance;
            }
            catch (Exception ex)
            {
                log.Error("IsQuotaExhausted with param tenant={0} Exception:\r\n{0}\r\n", mailbox.TenantId, ex.ToString());
            }

            return result;
        }
    }
}
