﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace ASC.Api.Mail.Resources {
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [DebuggerNonUserCode()]
    [CompilerGenerated()]
    public class MailApiResource {
        
        private static ResourceManager resourceMan;
        
        private static CultureInfo resourceCulture;
        
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal MailApiResource() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public static ResourceManager ResourceManager {
            get {
                if (ReferenceEquals(resourceMan, null)) {
                    ResourceManager temp = new ResourceManager("ASC.Api.Mail.Resources.MailApiResource", typeof(MailApiResource).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public static CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Some input params are invalid..
        /// </summary>
        public static string AttachmentsBadInputParamsError {
            get {
                return ResourceManager.GetString("AttachmentsBadInputParamsError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Access to document is denied..
        /// </summary>
        public static string AttachmentsDocumentAccessDeniedError {
            get {
                return ResourceManager.GetString("AttachmentsDocumentAccessDeniedError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Document not found or not exists on storage..
        /// </summary>
        public static string AttachmentsDocumentNotFoundError {
            get {
                return ResourceManager.GetString("AttachmentsDocumentNotFoundError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Empty files not supported..
        /// </summary>
        public static string AttachmentsEmptyFileNotSupportedError {
            get {
                return ResourceManager.GetString("AttachmentsEmptyFileNotSupportedError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Message not found..
        /// </summary>
        public static string AttachmentsMessageNotFoundError {
            get {
                return ResourceManager.GetString("AttachmentsMessageNotFoundError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Total size of all attachments exceeds limit..
        /// </summary>
        public static string AttachmentsTotalLimitError {
            get {
                return ResourceManager.GetString("AttachmentsTotalLimitError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Some error has happend. Try again later..
        /// </summary>
        public static string AttachmentsUnknownError {
            get {
                return ResourceManager.GetString("AttachmentsUnknownError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to This message was created automatically by mail delivery software..
        /// </summary>
        public static string DeliveryFailureAutomaticMessage {
            get {
                return ResourceManager.GetString("DeliveryFailureAutomaticMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Change your message.
        /// </summary>
        public static string DeliveryFailureBtn {
            get {
                return ResourceManager.GetString("DeliveryFailureBtn", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to In case the error persists, please read the {url_begin}FAQ section{url_end} to learn more about the problem..
        /// </summary>
        public static string DeliveryFailureFAQInformation {
            get {
                return ResourceManager.GetString("DeliveryFailureFAQInformation", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Message headers follow.
        /// </summary>
        public static string DeliveryFailureHeaders {
            get {
                return ResourceManager.GetString("DeliveryFailureHeaders", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Delivery failed for message with subject &quot;{subject}&quot; from {date}..
        /// </summary>
        public static string DeliveryFailureMessageIdentificator {
            get {
                return ResourceManager.GetString("DeliveryFailureMessageIdentificator", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Response from Smtp-server.
        /// </summary>
        public static string DeliveryFailureReason {
            get {
                return ResourceManager.GetString("DeliveryFailureReason", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Message could not be delivered to recipient(s).
        /// </summary>
        public static string DeliveryFailureRecipients {
            get {
                return ResourceManager.GetString("DeliveryFailureRecipients", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Please, check your message recipients addresses and message format.  If you are sure your message is correct, check all the {account_name} account settings, and, if everything is correct, sign in to the mail service you use and confirm any verification questions, in case there are some. After then try again..
        /// </summary>
        public static string DeliveryFailureRecommendations {
            get {
                return ResourceManager.GetString("DeliveryFailureRecommendations", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Message Delivery Failure.
        /// </summary>
        public static string DeliveryFailureSubject {
            get {
                return ResourceManager.GetString("DeliveryFailureSubject", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Field %1 is empty.
        /// </summary>
        public static string ErrorEmptyField {
            get {
                return ResourceManager.GetString("ErrorEmptyField", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Incorrect e-mail address in %1 field.
        /// </summary>
        public static string ErrorIncorrectEmailAddress {
            get {
                return ResourceManager.GetString("ErrorIncorrectEmailAddress", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Tag %1 already exists.
        /// </summary>
        public static string ErrorTagNameAlreadyExists {
            get {
                return ResourceManager.GetString("ErrorTagNameAlreadyExists", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Tag name can&apos;t be empty.
        /// </summary>
        public static string ErrorTagNameCantBeEmpty {
            get {
                return ResourceManager.GetString("ErrorTagNameCantBeEmpty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Bcc.
        /// </summary>
        public static string FieldNameBcc {
            get {
                return ResourceManager.GetString("FieldNameBcc", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cc.
        /// </summary>
        public static string FieldNameCc {
            get {
                return ResourceManager.GetString("FieldNameCc", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to From.
        /// </summary>
        public static string FieldNameFrom {
            get {
                return ResourceManager.GetString("FieldNameFrom", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to To.
        /// </summary>
        public static string FieldNameTo {
            get {
                return ResourceManager.GetString("FieldNameTo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to imap server connection timeout exceeded..
        /// </summary>
        public static string ImapConnectionTimeoutError {
            get {
                return ResourceManager.GetString("ImapConnectionTimeoutError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to IMAP response:.
        /// </summary>
        public static string ImapResponse {
            get {
                return ResourceManager.GetString("ImapResponse", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to pop3 server connection timeout exceeded..
        /// </summary>
        public static string Pop3ConnectionTimeoutError {
            get {
                return ResourceManager.GetString("Pop3ConnectionTimeoutError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to POP3 response:.
        /// </summary>
        public static string Pop3Response {
            get {
                return ResourceManager.GetString("Pop3Response", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to smtp server connection timeout exceeded..
        /// </summary>
        public static string SmtpConnectionTimeoutError {
            get {
                return ResourceManager.GetString("SmtpConnectionTimeoutError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SMTP response:.
        /// </summary>
        public static string SmtRresponse {
            get {
                return ResourceManager.GetString("SmtRresponse", resourceCulture);
            }
        }
    }
}
