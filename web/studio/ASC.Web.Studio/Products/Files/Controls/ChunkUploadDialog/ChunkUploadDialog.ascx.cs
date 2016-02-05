/*
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
using System.Text;
using System.Web;
using System.Web.UI;
using ASC.Web.Files.Classes;
using ASC.Web.Studio.Core;

namespace ASC.Web.Files.Controls
{
    public partial class ChunkUploadDialog : UserControl
    {
        public static string Location
        {
            get { return PathProvider.GetFileControlPath("ChunkUploadDialog/ChunkUploadDialog.ascx"); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.RegisterBodyScripts("~/js/uploader/jquery.fileupload.js");
            InitScripts();
        }

        protected void InitScripts()
        {
            var apiServer = new ASC.Api.ApiServer();
            var apiUrl = String.Format("{0}settings/quota.json", SetupInfo.WebApiBaseUrl);
            var response = apiServer.GetApiResponse(apiUrl, "GET");
            if (response == null) return;

            var responseApi = Encoding.UTF8.GetString(Convert.FromBase64String(response));

            var inlineScript = new StringBuilder();
            inlineScript.AppendFormat("ASC.Files.ChunkUploads.tenantQuota = {0}.response;ASC.Files.ChunkUploads.changeQuotaText();", responseApi);
            Page.RegisterInlineScript(inlineScript.ToString());
        }
    }
}