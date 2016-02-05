﻿<%@ Control Language="C#" AutoEventWireup="false" EnableViewState="false" %>
<%@ Assembly Name="ASC.Web.CRM" %>
<%@ Assembly Name="ASC.Web.Core" %>
<%@ Import Namespace="ASC.Web.CRM.Resources" %>

<%--Cases List--%>
<script id="casesListBaseTmpl" type="text/x-jquery-tmpl">
    <div id="caseFilterContainer">
        <div id="casesAdvansedFilter"></div>
    </div>

    <div id="caseList" style="display:none; min-height: 400px;">
        <ul id="casesHeaderMenu" class="clearFix contentMenu contentMenuDisplayAll">
            <li class="menuAction menuActionSelectAll menuActionSelectLonely">
                <div class="menuActionSelect">
                    <input type="checkbox" id="mainSelectAllCases" title="<%=CRMCommonResource.SelectAll%>" onclick="ASC.CRM.ListCasesView.selectAll(this);" />
                </div>
            </li>
            <li class="menuAction menuActionAddTag" title="<%= CRMCommonResource.AddNewTag %>">
                <span><%=CRMCommonResource.AddNewTag%></span>
                <div class="down_arrow"></div>
            </li>
            <li class="menuAction menuActionPermissions" title="<%= CRMCommonResource.SetPermissions %>">
                <span><%=CRMCommonResource.SetPermissions%></span>
            </li>
            <li class="menuAction menuActionDelete" title="<%= CRMCommonResource.Delete %>">
                <span><%= CRMCommonResource.Delete%></span>
            </li>
            <li class="menu-action-simple-pagenav">
            </li>
            <li class="menu-action-checked-count">
                <span></span>
                <a class="linkDescribe baseLinkAction" style="margin-left:10px;" onclick="ASC.CRM.ListCasesView.deselectAll();">
                    <%= CRMCommonResource.DeselectAll%>
                </a>
            </li>
            <li class="menu-action-on-top">
                <a class="on-top-link" onclick="javascript:window.scrollTo(0, 0);">
                    <%= CRMCommonResource.OnTop%>
                </a>
            </li>
        </ul>
        <div class="header-menu-spacer">&nbsp;</div>


        <table id="caseTable" class="tableBase" cellpadding="4" cellspacing="0">
            <colgroup>
                <col style="width: 30px;"/>
                <col/>
                <col style="width: 40px;"/>
            </colgroup>
            <tbody>
            </tbody>
        </table>

        <table id="tableForCasesNavigation" class="crm-navigationPanel" cellpadding="4" cellspacing="0" border="0">
            <tbody>
            <tr>
                <td>
                    <div id="divForCasesPager">
                    </div>
                </td>
                <td style="text-align:right;">
                    <span class="gray-text"><%= CRMCasesResource.TotalCases %>:</span>
                    <span class="gray-text" id="totalCasesOnPage"></span>

                    <span class="gray-text"><%= CRMCommonResource.ShowOnPage %>:&nbsp;</span>
                    <select class="top-align">
                        <option value="25">25</option>
                        <option value="50">50</option>
                        <option value="75">75</option>
                        <option value="100">100</option>
                    </select>
                </td>
            </tr>
            </tbody>
        </table>
    </div>

    <div id="addTagCasesDialog" class="studio-action-panel group-actions addTagDialog">
        <ul class="dropdown-content mobile-overflow"></ul>
        <div class="h_line">&nbsp;</div>
        <div style="margin-bottom:5px;"><%= CRMCommonResource.CreateNewTag%>:</div>
        <input type="text" maxlength="50" class="textEdit" />
        <a onclick="ASC.CRM.ListCasesView.addNewTag();" class="button blue" id="addThisTag">
            <%= CRMCommonResource.OK%>
        </a>
    </div>

    <div id="permissionsCasesPanelInnerHtml" class="display-none">
        {{if IsCRMAdmin !== true}}
        <div style="margin-top:10px">
            <b><%= CRMCasesResource.CasesAccessRightsLimit %></b>
        </div>
        {{/if}}
    </div>

    <div id="caseActionMenu" class="studio-action-panel">
        <ul class="dropdown-content">
            <li><a class="showProfileLink dropdown-item"><%= CRMCasesResource.ShowCaseProfile%></a></li>
            <li><a class="showProfileLinkNewTab dropdown-item"><%= CRMCasesResource.ShowCaseProfileNewTab%></a></li>
            <li><a class="setPermissionsLink dropdown-item"><%= CRMCommonResource.SetPermissions%></a></li>
            <li><a class="editCaseLink dropdown-item"><%= CRMCasesResource.EditCase %></a></li>
            <li><a class="deleteCaseLink dropdown-item"><%= CRMCasesResource.DeleteThisCase %></a></li>
        </ul>
    </div>

</script>

<script id="caseListTmpl" type="text/x-jquery-tmpl">
    <tbody>
        {{tmpl(cases) "caseTmpl"}}
    </tbody>
</script>

<script id="caseTmpl" type="text/x-jquery-tmpl">
    <tr id="caseItem_${id}" class="with-entity-menu">
        <td class="borderBase" style="padding: 0 0 0 6px;">
            <input type="checkbox" id="checkCase_${id}" onclick="ASC.CRM.ListCasesView.selectItem(this);"
                 {{if isChecked == true}}checked="checked"{{/if}} />
            <div id="loaderImg_${id}" class="loader-middle baseList_loaderImg"></div>
        </td>

        <td class="borderBase">
            {{if isPrivate == true}}
                <label class="crm-private-lock"></label>
            {{/if}}
            <a class="linkHeaderMedium{{if isClosed == true}} gray-text{{/if}}" href="?id=${id}">
                ${title}
            </a>
        </td>
        <td class="borderBase" style="padding:5px;">
            <div id="caseMenu_${id}" class="entity-menu" title="<%= CRMCommonResource.Actions %>"></div>
        </td>
    </tr>
</script>