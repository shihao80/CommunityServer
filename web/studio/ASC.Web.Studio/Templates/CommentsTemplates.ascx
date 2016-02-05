﻿<%@ Control Language="C#" AutoEventWireup="false" EnableViewState="false" %>
<%@ Import Namespace="Resources" %>


<script id="template-commentsList" type="text/x-jquery-tmpl">
    {{if showCaption == true}}
        <div id="commentsTitle" class="headerPanel"><%= Resources.Resource.Comments %></div>
    {{/if}}
    
    <div id="noComments"{{if isEmpty !== true}} style="display:none;"{{/if}}><%= UserControlsCommonResource.NoComments %></div>

    <div id="mainCommentsContainer" class="comment-main-container" style="{{if total%2 == 0}}border-bottom:1px solid #ddd; {{/if}}{{if isEmpty === true}}display:none;{{/if}}">
        {{each(i, cmt) comments}}
            {{tmpl({comment: cmt, commentLevel: 1, maxDepthLevel: maxDepthLevel}) 'template-comment'}}
        {{/each}}
    </div>
    <br />

    {{if isShowAddCommentBtn == true}}
        <a id="add_comment_btn"><%= Resources.Resource.AddNewCommentButton %></a>
    {{/if}}
    <div id="commentBox" class="comment-main-actions-container">
        <div id="commentBoxContainer">
            <input type="hidden" id="hdnParentComment" value="" />
            <input type="hidden" id="hdnAction" value="" />
            <input type="hidden" id="hdnCommentID" value="" />
            <input type="hidden" id="hdnObjectID" value="${objectID}" />

            <textarea id="commentEditor${_jsObjName}" name="commentEditor${_jsObjName}"></textarea>

            <div class="middle-button-container">
                <a href="javascript:void(0);" id="btnAddComment" class="button">
                    <%= Resources.Resource.PublishButton %>
                </a>
                <span class="splitter-buttons"></span>
                <a href="javascript:void(0);" id="btnPreview" class="button disable">
                    <%= Resources.Resource.PreviewButton %>
                </a>
                <span class="splitter-buttons"></span>
                <a href="javascript:void(0);" id="btnCancel" class="button gray cancelFckEditorChangesButtonMarker">
                    <%= Resources.Resource.CancelButton %>
                </a>
            </div>
        </div>

        <div id="previewBox" class="comment-preview-container">
            <div class="headerPanel"><%= Resources.Resource.PreviewButton %></div>
            <div id="previewBoxBody"></div>
            <div class="middle-button-container">
                <a href="javascript:void(0);" class="button blue"><%= Resources.Resource.CloseButton %></a>
            </div>
        </div>
    </div>
</script>


<script id="template-comment" type="text/x-jquery-tmpl">
    <div{{if commentLevel != 1 && commentLevel <= maxDepthLevel}} style="margin-left: 35px;"{{/if}} id="container_${comment.commentID}">
        <div class="comments-item-container" id="comment_${comment.commentID}">
            {{if comment.inactive}}
                {{tmpl 'template-comment-inactive'}}
            {{else}}
            <table cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <img class="userMiniPhoto" alt="${comment.userFullName}" src="${comment.userAvatarPath}"/>
                </td>
                <td>
                    <div class="comments-item-data">
                        <div>
                            <a style="margin-left:10px;" class="link bold" href="${comment.userProfileLink}">${comment.userFullName}</a>
                            &nbsp;&nbsp;
                            <span class="text-medium-describe" style="padding-left:5px;">
                                ${comment.timeStampStr}
                            </span>
                        </div>

                        {{if comment.userPost != ""}}
                            <div style="padding-top:2px;padding-left:10px;" class="describe-text">${comment.userPost}</div>
                        {{/if}}

                        <div id="content_${comment.commentID}" style="padding-top:8px;padding-left:10px;" class="longWordsBreak">
                            {{html comment.commentBody.replace(/<\s*script([\s\S]*?)<\s*\/\s*script\s*>/,'')}}
                        </div>

                        {{if comment.attachments != null && comment.attachments.length > 0}}
                            <div id="attacments_${comment.commentID}" style="padding-top:10px;padding-left:15px;">
                            {{each(k, attach) comment.attachments}}
                                {{if k!=0}}, {{/if}}
                                <a class="linkDescribe" href="${attach.FilePath}">${attach.FileName}</a>
                                <input name="attacment_name_${comment.commentID}" type="hidden" value="${attach.FileName}" />
                                <input name="attacment_path_${comment.commentID}" type="hidden" value="${attach.FileName}" />
                            {{/each}}
                            </div>
                        {{/if}}
                    </div>

                    <div class="clearFix comments-item-actions">
                        {{if comment.isResponsePermissions == true}}
                        <div class="float-left">
                            <a class="link dotline gray" id="response_${comment.commentID}" href="javascript:void(0);">
                                <%= Resources.Resource.AnswerButton %>
                            </a>
                        </div>
                        {{/if}}

                       {{if comment.isEditPermissions == true}}
                        <div class="float-right">
                            <a class="link dotline gray" id="edit_${comment.commentID}" href="javascript:void(0);"><%= Resources.Resource.EditButton %></a>

                            <span class="text-medium-describe splitter"> </span>

                            <a class="link dotline gray" id="remove_${comment.commentID}" href="javascript:void(0);"><%= Resources.Resource.DeleteButton %></a>
                        </div>
                        {{/if}}
                    </div>
                </td>
            </tr>
        </table>
        {{/if}}
        </div>

        {{if comment.commentList != null && comment.commentList.length > 0}}
        {{each(ind, citem) comment.commentList}}
            {{tmpl({comment: citem, commentLevel: commentLevel + 1, maxDepthLevel: maxDepthLevel}) 'template-comment'}}
        {{/each}}
        {{/if}}

    </div>
</script>

<script id="template-comment-inactive" type="text/x-jquery-tmpl">
    <div class="comment-inactive"><%= Resources.Resource.CommentWasRemoved %></div>
</script>