﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TariffUsage.ascx.cs" Inherits="ASC.Web.Studio.UserControls.Management.TariffUsage" %>
<%@ Import Namespace="System.Globalization" %>
<%@ Import Namespace="ASC.Core" %>
<%@ Import Namespace="ASC.Core.Users" %>
<%@ Import Namespace="ASC.Web.Studio.Core" %>
<%@ Import Namespace="ASC.Web.Studio.Utility" %>
<%@ Import Namespace="Resources" %>

<%@ Register TagPrefix="sc" Namespace="ASC.Web.Studio.Controls.Common" Assembly="ASC.Web.Studio" %>

<% if ((int)RateRuble != 0)
   { %>
<style>
    @font-face {
        font-family: rub;
        src: url('<%= VirtualPathUtility.ToAbsolute("~/UserControls/Management/TariffSettings/css/rub.eot") %>');
        src:
           url("<%= VirtualPathUtility.ToAbsolute("~/UserControls/Management/TariffSettings/css/rub.eot") %>") format("embedded-opentype"),
           url("<%= VirtualPathUtility.ToAbsolute("~/UserControls/Management/TariffSettings/css/rub.woff") %>") format("woff"),
           url("<%= VirtualPathUtility.ToAbsolute("~/UserControls/Management/TariffSettings/css/rub.ttf") %>") format("truetype"),
           url("<%= VirtualPathUtility.ToAbsolute("~/UserControls/Management/TariffSettings/css/rub.svg") %>") format("svg");
    }
    .tariff-price-cur {
        font-family: rub, calibri;
    }
</style>
<% } %>

<% if (Partner != null)
   { %>
<div class="partner-is-label">
    <% if (Partner.DisplayType == PartnerDisplayType.LogoOnly && !string.IsNullOrEmpty(Partner.LogoUrl))
       { %>
    <%: Resource.PartnerIs %>
    <img src="<%= Partner.LogoUrl %>" align="middle" />
    <% } %>

    <% if (Partner.DisplayType == PartnerDisplayType.DisplayNameOnly && !string.IsNullOrEmpty(Partner.DisplayName))
       { %>
    <%: Resource.PartnerIs %>
    <%: Partner.DisplayName %>
    <% } %>

    <% if (Partner.DisplayType == PartnerDisplayType.All && (!string.IsNullOrEmpty(Partner.LogoUrl) || !string.IsNullOrEmpty(Partner.DisplayName)))
       { %>
    <%: Resource.PartnerIs %>
    <% if (!string.IsNullOrEmpty(Partner.LogoUrl))
       { %>
    <img src="<%= Partner.LogoUrl %>" align="middle" />
    <% } %>
    <% if (!string.IsNullOrEmpty(Partner.DisplayName))
       { %>
    <%: Partner.DisplayName %>
    <% } %>
    <% } %>
</div>
<% } %>

<div class="current-tariff-desc">
    <%= TariffDescription() %>
    <br />
    <br />
    <%= String.Format(Resource.TariffStatistics, "<a class=\"link-black-14 bold\" href=\"" + CommonLinkUtility.GetEmployees() + "\">" + UsersCount + "</a>" + (!CurrentQuota.NonProfit ? "/" + CurrentQuota.ActiveUsers : string.Empty)) %>
    <br />
    <%= CoreContext.Configuration.Standalone
            ? ""
            : String.Format(Resource.TariffStatisticsStorage, "<a class=\"link-black-14 bold\" href=\"" + CommonLinkUtility.GetAdministration(ManagementType.Statistic) + "\">" + FileSizeComment.FilesSizeToString(UsedSize) + "</a>" + "/" + FileSizeComment.FilesSizeToString(CurrentQuota.MaxTotalSize)) %>
    <% if (SmsEnable)
       { %>
    <br />
    <asp:PlaceHolder runat="server" ID="SmsBuyHolder"></asp:PlaceHolder>
    <% } %>

    <% if (VoipEnable)
       { %>
    <br />
    <asp:PlaceHolder runat="server" ID="VoipBuyHolder"></asp:PlaceHolder>
    <% } %>
</div>

<div class="tariff-header clearFix">
    <div class="tariff-user-panel">
        <div class="header-base bold"><%= UserControlsCommonResource.TariffUserChoose %></div>

        <div class="tariff-user-question"><%= UserControlsCommonResource.TariffUserChooseQuestion %></div>

        <div class="tariff-slider-container" data-default="<%= QuotaForDisplay.ActiveUsers %>" data-min="<%= MinActiveUser %>">
            <div id="pricingPlanSlider"></div>
        </div>

        <div class="tariff-user-descr">
            <% for (var i = 0; i < QuotasYear.Count; i++)
               { %>
            <div class="tariff-user-descr-item <%= QuotaForDisplay.ActiveUsers == QuotasYear[i].ActiveUsers ? "tariff-user-descr-item-selected" : string.Empty %>"
                data-users="<%= QuotasYear[i].ActiveUsers %>">
                <% var prevUserCount = TenantExtra.GetPrevUsersCount(QuotasYear[i]); %>
                <%= string.Format(UserControlsCommonResource.TariffUserDescr,
                              "<b>",
                              "</b>",
                              prevUserCount,
                              QuotasYear[i].ActiveUsers,
                              FileSizeComment.FilesSizeToString(QuotasYear[i].MaxTotalSize),
                              "<br />",
                              prevUserCount > 1 ? prevUserCount - 1 : 1,
                              SetStar(UserControlsCommonResource.ActiveUserDescr)) %>
            </div>
            <% } %>

            <div class="tariff-user-warn-min">
                <%= UserControlsCommonResource.TariffUserWarnDescr %>
                <span class="tariff-user-warn-link baseLinkAction"><%= UserControlsCommonResource.TariffUserWarnRead %></span>
            </div>

            <div class="tariff-user-warn-max">
                <%= string.Format(UserControlsCommonResource.TariffUserRequestDescr, QuotasYear.Last().ActiveUsers) %>
            </div>
        </div>
    </div>
</div>

<table class="tariffs-panel" cols="3" cellspacing="0" cellpadding="0" frame="void">
    <thead>
        <tr valign="middle">
            <td width="30%" valign="bottom">
                <div class="tariffs-header-month tariffs-header">
                    <%= string.Format(UserControlsCommonResource.TariffNameSMonth,
                                      "<div class=\"tariffs-name\">",
                                      "</div>",
                                      MonthIsDisable ? SetStar(Resource.TariffRemarkDisabledMonth) : "") %>
                </div>
            </td>
            <td width="30%" valign="bottom">
                <div class="tariffs-header-year tariffs-header">
                    <div class="tariffs-pop"><%= UserControlsCommonResource.TariffPop %></div>
                    <%= string.Format(UserControlsCommonResource.TariffNameSYear,
                                      "<div class=\"tariffs-name\">",
                                      "</div>",
                                      YearIsDisable ? SetStar(Resource.TariffRemarkDisabledYear) : "") %>
                </div>
            </td>
            <td width="30%" valign="bottom">
                <div class="tariffs-header-year3 tariffs-header">
                    <%= string.Format(UserControlsCommonResource.TariffNameSYear3,
                                      "<div class=\"tariffs-name\">",
                                      "</div>") %>
                </div>
            </td>
        </tr>
    </thead>
    <tbody>
        <% for (var i = 0; i < QuotasYear.Count; i++)
           {
               Tuple<string, string, string> buyAttr;
        %>

        <tr class="tariff-item <%= QuotaForDisplay.ActiveUsers == QuotasYear[i].ActiveUsers ? "tariff-item-selected" : string.Empty %>"
            valign="middle" data-users="<%= QuotasYear[i].ActiveUsers %>" data-storage="<%= FileSizeComment.FilesSizeToString(QuotasYear[i].MaxTotalSize) %>">
            <td>
                <div class="tariffs-body tariffs-body-month">

                    <% var quotaMonth = GetQuotaMonth(QuotasYear[i]);
                       if (quotaMonth != null || !QuotasYear[i].Free)
                       { %>
                    <% if (quotaMonth == null)
                       {
                           var fakePrice = new[] { 10.0m, 20.0m }[i]; %>
                    <div class="tariffs-price-dscr"><%= string.Format(UserControlsCommonResource.TariffPricePer, GetPriceString(5)) %></div>

                    <div class="tariffs-price"><%= GetPriceString(fakePrice) %></div>

                    <div class="tariffs-price-per"><%= UserControlsCommonResource.TariffBasicPrice %></div>

                    <div class="tariffs-lost-descr"><%= UserControlsCommonResource.TariffPerYearOnly %></div>

                    <% }
                       else
                       { %>
                    <div class="tariffs-price-dscr"><%= string.Format(UserControlsCommonResource.TariffPricePer, GetPriceString(5)) %></div>

                    <div class="tariffs-price">
                        <%= quotaMonth.Price == decimal.Zero
                                ? UserControlsCommonResource.TariffFree
                                : GetPriceString(quotaMonth.Price) %>
                    </div>

                    <div class="tariffs-price-per"><%= UserControlsCommonResource.TariffBasicPrice %></div>

                    <div class="tariffs-price-sale">
                        <%= quotaMonth.Price2 != decimal.Zero
                                ? string.Format(UserControlsCommonResource.TariffPriceOffer, GetPriceString(quotaMonth.Price2))
                                : "&nbsp;" %>
                    </div>

                    <div class="tariffs-descr">
                        <%= quotaMonth.Price2 != decimal.Zero
                                ? string.Format(Resource.TariffRemarkSale, GetSaleDate())
                                : "&nbsp;" %>
                    </div>

                    <% buyAttr = GetBuyAttr(quotaMonth); %>
                    <a class="tariffs-buy-action button huge <%= buyAttr.Item1 %>"
                        <%= !string.IsNullOrEmpty(buyAttr.Item2) ? "href=\"" + buyAttr.Item2 + "\"" : string.Empty %>>
                        <%= buyAttr.Item3 %>
                    </a>

                    <% } %>
                    <% } %>
                </div>
            </td>

            <td>
                <div class="tariffs-body tariffs-body-year">
                    <div class="tariffs-price-dscr"><%= string.Format(UserControlsCommonResource.TariffPricePer, GetPriceString(2)) %></div>

                    <div class="tariffs-price">
                        <%= QuotasYear[i].Price == decimal.Zero
                                ? UserControlsCommonResource.TariffFree
                                : GetPriceString(QuotasYear[i].Price) %>
                    </div>

                    <div class="tariffs-price-per"><%= UserControlsCommonResource.TariffLimitedPrice %></div>

                    <div class="tariffs-price-sale">
                        <%= QuotasYear[i].Price2 != decimal.Zero
                                ? string.Format(UserControlsCommonResource.TariffPriceOffer, GetPriceString(QuotasYear[i].Price2))
                                : "&nbsp;" %>
                    </div>

                    <div class="tariffs-descr">
                        <%= QuotasYear[i].Price2 != decimal.Zero
                                ? string.Format(Resource.TariffRemarkSale, GetSaleDate())
                                : "&nbsp;" %>
                    </div>

                    <% buyAttr = GetBuyAttr(QuotasYear[i]); %>
                    <a class="tariffs-buy-action button huge <%= buyAttr.Item1 %>"
                        <%= !string.IsNullOrEmpty(buyAttr.Item2) ? "href=\"" + buyAttr.Item2 + "\"" : string.Empty %>>
                        <%= buyAttr.Item3 %>
                    </a>
                </div>
            </td>

            <td>
                <div class="tariffs-body tariffs-body-year3">
                    <% var quotaYear3 = GetQuotaYear3(QuotasYear[i]);
                       if (quotaYear3 != null)
                       { %>

                    <div class="tariffs-price-dscr"><%= string.Format(UserControlsCommonResource.TariffPricePer, GetPriceString(1)) %></div>

                    <div class="tariffs-price">
                        <%= quotaYear3.Price == decimal.Zero
                                ? UserControlsCommonResource.TariffFree
                                : GetPriceString(quotaYear3.Price) %>
                    </div>

                    <div class="tariffs-price-per"><%= UserControlsCommonResource.TariffLimitedPrice %></div>

                    <div class="tariffs-price-sale">
                        <%= quotaYear3.Price2 != decimal.Zero
                                ? string.Format(UserControlsCommonResource.TariffPriceOffer, GetPriceString(quotaYear3.Price2))
                                : "&nbsp;" %>
                    </div>

                    <div class="tariffs-descr">
                        <%= quotaYear3.Price2 != decimal.Zero
                                ? string.Format(Resource.TariffRemarkSale, GetSaleDate())
                                : "&nbsp;" %>
                    </div>

                    <% buyAttr = GetBuyAttr(quotaYear3); %>
                    <a class="tariffs-buy-action button huge <%= buyAttr.Item1 %>"
                        <%= !string.IsNullOrEmpty(buyAttr.Item2) ? "href=\"" + buyAttr.Item2 + "\"" : string.Empty %>>
                        <%= buyAttr.Item3 %>
                    </a>

                    <% } %>
                </div>
            </td>

        </tr>
        <% } %>
    </tbody>
</table>

<% if (Partner != null && false) //todo:!!!
   { %>
<div class="tariffs-button-block clearFix">
    <asp:PlaceHolder runat="server" ID="PaymentsCodeHolder"></asp:PlaceHolder>
</div>
<% } %>

<% var linkList = new Dictionary<string, string>
       {
           {"fr", "http://onlyo.co/1LlMqkT"},
           {"de", "http://onlyo.co/1LlMrWe"},
           {"en", "http://onlyo.co/1LlMtx4"},
           {"ru", "http://onlyo.co/1LlMmS8"},
           {"it", "http://onlyo.co/1LlMpgM"}
       };
   string tariffLink;
   if (!linkList.TryGetValue(CultureInfo.CurrentUICulture.TwoLetterISOLanguageName, out tariffLink))
   {
       tariffLink = linkList["en"];
   }
%>

<a class="see-full-price link-black-12" target="_blank" href="<%= tariffLink %>"><%= UserControlsCommonResource.TariffFullPrice %></a>

<div class="tariff-request-panel clearFix">
    <div class="header-base bold"><%= UserControlsCommonResource.TariffRequestHeader %></div>
    <div class="request-form">
        <% var userInfo = CoreContext.UserManager.GetUsers(SecurityContext.CurrentAccount.ID); %>

        <p class="confirm-block-text gray-text">
            <%= Resource.Name %>
        </p>
        <input type="text" maxlength="64" tabindex="1" class="text-edit-name text-edit" required="required" placeholder="<%= Resource.Name %>" value="<%= userInfo.DisplayUserName() %>">

        <p class="confirm-block-text gray-text">
            <%= Resource.Email %>
        </p>
        <input type="email" maxlength="64" tabindex="2" class="text-edit-email text-edit" required="required" placeholder="<%= Resource.Email %>" value="<%= userInfo.Email %>">

        <p class="confirm-block-text gray-text">
            <%= UserControlsCommonResource.TariffRequestContent %>
        </p>
        <textarea rows="4" tabindex="3" class="text-edit-message text-edit" required="required" placeholder="<%= UserControlsCommonResource.TariffRequestContentHolder %>"></textarea>
    </div>

    <div class="middle-button-container">
        <a class="tariff-request button blue huge" tabindex="4">
            <%= UserControlsCommonResource.TariffRequestBtn %></a>
    </div>
</div>

<% if (Partner == null)
   { %>
<div class="support-block clearFix">
    <div class="support-photo"></div>
    <div class="support-actions">
        <div class="support-title"><%= Resource.SupportBlockTitle %></div>
        <div>
            <span class="support-mail-btn">
                <a class="link dotline" href="mailto:support@onlyoffice.com"><%= Resource.SupportBlockEmailBth %></a>
            </span>
            <span class="support-chat-btn">
                <a class="link dotline" onclick="window.LC_API.open_chat_window()"><%= Resource.SupportBlockChatBtn %></a>
            </span>
        </div>
    </div>
</div>
<% } %>

<div class="tariff-remark">
    <%= GetRemarks() %>
</div>

<%-- Dialog --%>
<div id="tafirrDowngradeDialog" class="display-none">
    <sc:Container ID="downgradeInfoContainer" runat="server">
        <Header>
            <%= Resource.TariffDowngrade %>
        </Header>
        <Body>
            <span>
                <%: Resource.TariffDowngradeErrorTitle %>
            </span>
            <br />
            <br />
            <span>
                <%= String.Format(Resource.TariffDowngradeErrorStatisticsUsers.HtmlEncode(), "<span id=\"downgradeUsers\" class=\"header-base-small\"></span>", "<span class=\"header-base-small\">" + UsersCount + "</span>") %>
            </span>
            <br />
            <span>
                <%= String.Format(Resource.TariffDowngradeErrorStatisticsStorage.HtmlEncode(), "<span id=\"downgradeStorage\" class=\"header-base-small\"></span>", "<span class=\"header-base-small\">" + FileSizeComment.FilesSizeToString(UsedSize) + "</span>")%>
            </span>
            <br />
            <br />
            <span>
                <%: Resource.TarffDowngradeErrorDescription %>
            </span>
            <div class="middle-button-container">
                <a class="button gray middle" onclick="PopupKeyUpActionProvider.CloseDialog(); return false;">
                    <%= Resource.OKButton %>
                </a>
            </div>
        </Body>
    </sc:Container>
</div>

<% if (!HideBuyRecommendation && false)
   { %>
<div id="buyRecommendationDialog" class="display-none">
    <sc:Container runat="server" ID="buyRecommendationContainer">
        <Header><%= Resource.TariffBuyRecommendationTitle%></Header>
        <Body>
            <span><%: Resource.TariffBuyRecommendation %></span>
            <br />
            <br />
            <label>
                <input type="checkbox" id="buyRecommendationDisplay" class="checkbox" />
                <%= Resource.LabelDontShowMessage %>
            </label>
            <div class="middle-button-container">
                <a id="buyRecommendationOk" class="button gray middle">
                    <%= Resource.OKButton %>
                </a>
            </div>
        </Body>
    </sc:Container>
</div>
<% } %>

<% if (Partner == null)
   { %>
<!-- BEGIN livechatinc.com Code -->
<div class="livechat online" style="display: none; margin-top: 24px;">
    <a href="javascript:window.LC_API.open_chat_window();">
        <img style="max-width: 983px;" title="<%= Resource.SupportByChat %>" src="<%= GetChatBannerPath() %>" />
    </a>
</div>
<!-- END livechatinc.com Code -->
<% } %>
