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
using System.Collections.Generic;
using System.Globalization;
using System.Net.Mail;
using System.Runtime.Serialization;
using ActiveUp.Net.Mail;
using ASC.Mail.Aggregator.Common.Imap;
using ASC.Mail.Aggregator.Common.Utils;
using Newtonsoft.Json;

namespace ASC.Mail.Aggregator.Common
{
    [DataContract(Namespace = "")]
    public class MailBox : IEquatable<MailBox>
    {
        public enum AuthProblemType
        {
            NoProblems = 0,
            ConnectError = 1,
            TooManyErrors = 2
        }

        public int TenantId { get; set; }

        [DataMember(Name = "id")]
        public int MailBoxId { get; set; }

        private string _userId;

        public string UserId
        {
            get { return _userId; }
            set { _userId = value.ToLowerInvariant(); }
        }

        [IgnoreDataMember]
        public MailAddress EMail { get; set; }

        [DataMember(Name = "email")]
        public string EMailView
        {
            get { return EMail == null ? string.Empty : EMail.ToString(); }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    EMail = new MailAddress(value);
                }
            }
        }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "account")]
        public string Account { get; set; }

        [DataMember(Name = "password")]
        public string Password { get; set; }

        [DataMember(Name = "server")]
        public string Server { get; set; }

        [DataMember(Name = "smtp_server")]
        public string SmtpServer { get; set; }

        public int SmtpPort
        {
            get
            {
                int port;
                if (!string.IsNullOrEmpty(SmtpPortStr) && int.TryParse(SmtpPortStr, out port))
                {
                    return port;
                }
                return OutcomingEncryptionType != EncryptionType.None ? WellKnownPorts.SMTP_SSL : WellKnownPorts.SMTP;
            }
            set { SmtpPortStr = value.ToString(CultureInfo.InvariantCulture); }
        }

        [DataMember(Name = "smtp_port")]
        public string SmtpPortStr { get; set; }

        [DataMember(Name = "smtp_account")]
        public string SmtpAccount { get; set; }

        [DataMember(Name = "smtp_password")]
        public string SmtpPassword { get; set; }

        [DataMember(Name = "smtp_auth")]
        public bool SmtpAuth { get; set; }

        public int Port
        {
            get
            {
                int port;
                if (!string.IsNullOrEmpty(PortStr) && int.TryParse(PortStr, out port))
                {
                    return port;
                }
                return IncomingEncryptionType != EncryptionType.None ? WellKnownPorts.POP3_SSL : WellKnownPorts.POP3;
            }
            set { PortStr = value.ToString(CultureInfo.InvariantCulture); }
        }

        [DataMember(Name = "port")]
        public string PortStr { get; set; }

        [DataMember(Name = "incoming_encryption_type")]
        public EncryptionType IncomingEncryptionType { get; set;}

        [DataMember(Name = "outcoming_encryption_type")]
        public EncryptionType OutcomingEncryptionType { get; set; }

        [DataMember(Name = "auth_type_in")]
        public SaslMechanism AuthenticationTypeIn { get; set; }

        [DataMember(Name = "auth_type_smtp")]
        public SaslMechanism AuthenticationTypeSmtp { get; set; }

        public long Size { get; set; }

        public int MessagesCount { get; set; }

        public bool Enabled { get; set; }
        public bool Active { get; set; }

        public bool QuotaError { get; set; }
        public bool QuotaErrorChanged { get; set; }

        public DateTime? AuthErrorDate { get; set; }

        /// <summary>
        /// This value is the initialized while testing a created mail account. 
        /// It is obtained from "LOGIN-DELAY" tag of CAPA command
        /// Measured in seconds
        /// </summary>
        public int ServerLoginDelay { get; set; }

        /// <summary>
        /// Limiting the period of time when limiting download emails
        /// Measured in seconds
        /// </summary>
        public Int64 MailLimitedTimeDelta { get; set; }

        /// <summary>
        /// Begin timestamp loading mails
        /// </summary>
        public DateTime MailBeginTimestamp { get; set; }

        /// <summary>
        /// This default value is assumed based on experiments on the following mail servers: qip.ru, 
        /// </summary>
// ReSharper disable InconsistentNaming
        public const int DefaultServerLoginDelay = 30;
// ReSharper restore InconsistentNaming

        /// <summary>
        /// Limiting the period of time when limiting download emails
        /// </summary>
// ReSharper disable InconsistentNaming
        public const Int64 DefaultMailLimitedTimeDelta = 25920000000000; // 30 days;
// ReSharper restore InconsistentNaming

        /// <summary>
        /// Default begin timestamp loading emails
        /// </summary>
// ReSharper disable InconsistentNaming
        public const Int64 DefaultMailBeginTimestamp = 622933632000000000; // 01/01/1975 00:00:00
// ReSharper restore InconsistentNaming

        public int AuthorizeTimeoutInMilliseconds
        {
            get { return 10000; }
        }

        [DataMember(Name = "imap")]
        public bool Imap { get; set; }

        [DataMember(Name = "begin_date")]
        public DateTime BeginDate { get; set; }

        [DataMember(Name = "service_type")]
        public byte ServiceType { get; set; }

        [DataMember(Name = "refresh_token")]
        public string RefreshToken { get; set; }

        [DataMember(Name = "restrict")]
        public bool Restrict { get; set; }

        public bool ImapFolderChanged { get; set; }

        public bool BeginDateChanged { get; set; }

        public Dictionary<string, ImapFolderUids> ImapIntervals { get; set; }

        public int SmtpServerId { get; set; }

        public int InServerId { get; set; }

        public string ImapIntervalsJson
        {
            get
            {
                var contentJson = JsonConvert.SerializeObject(ImapIntervals);
                return contentJson;
            }
            set
            {
                if (string.IsNullOrEmpty(value)) return;
                ImapIntervals = MailUtil.ParseImapIntervals(value);
            }
        }

        [DataMember(Name = "email_in_folder")]
        public string EMailInFolder { get; set; }

        [DataMember(Name = "is_teamlab")]
        public bool IsTeamlab { get; set; }

        public bool IsRemoved { get; set; }

        public MailBox()
        {
            ServerLoginDelay = DefaultServerLoginDelay; //This value can be changed in test mailbox connection
            ImapIntervals = new Dictionary<string, ImapFolderUids>();
        }

        public MailBox(int tenant, string user, string name,
            MailAddress email, string account, string password, string server, bool imap,
            string smtpServer, string smtpPassword, bool smtpauth, int mailboxId, DateTime beginDate,
            EncryptionType incomingEncryptionType, EncryptionType outcomingEncryptionType, byte service,
            string refreshToken, string emailInFolder, bool isRemoved)
        {
            if (string.IsNullOrEmpty(user)) throw new ArgumentNullException("user");
            if (email == null) throw new ArgumentNullException("email");
            if (string.IsNullOrEmpty(account)) throw new ArgumentNullException("account");
            if (string.IsNullOrEmpty(server)) throw new ArgumentNullException("server");
            if (!server.Contains(":")) throw new FormatException("Valid server string format is <server:port>");
            if (!smtpServer.Contains(":")) throw new FormatException("Valid server string format is <server:port>");

            MailLimitedTimeDelta = DefaultMailLimitedTimeDelta;
            MailBeginTimestamp = new DateTime(DefaultMailBeginTimestamp);

            MailBoxId = mailboxId;
            TenantId = tenant;
            UserId = user;
            EMail = email;
            Account = account;
            Name = name;
            Server = server.Split(':')[0];
            Port = int.Parse(server.Split(':')[1]);
            SmtpServer = smtpServer.Split(':')[0];
            SmtpPort = int.Parse(smtpServer.Split(':')[1]);
            SmtpAuth = smtpauth;
            Imap = imap;

            Password = password;
            SmtpPassword = smtpPassword;
            IncomingEncryptionType = incomingEncryptionType;
            OutcomingEncryptionType = outcomingEncryptionType;
            ServerLoginDelay = DefaultServerLoginDelay;
            BeginDate = beginDate;
            Restrict = !(BeginDate.Equals(MailBeginTimestamp));
            ServiceType = service;
            RefreshToken = refreshToken;
            ImapIntervals = new Dictionary<string, ImapFolderUids>();
            EMailInFolder = emailInFolder;
            IsRemoved = isRemoved;
        }

        public override string ToString()
        {
            return EMail.ToString();
        }

        public override int GetHashCode()
        {
            return (TenantId.GetHashCode() + UserId.GetHashCode()) ^ EMail.Address.ToLowerInvariant().GetHashCode();
        }

        public bool Equals(MailBox other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (ReferenceEquals(null, other)) return false;

            return
                TenantId == other.TenantId &&
                string.Compare(UserId, other.UserId, StringComparison.InvariantCultureIgnoreCase) == 0 &&
                string.Compare(EMail.Address, other.EMail.Address, StringComparison.InvariantCultureIgnoreCase) == 0;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || Equals(obj as MailBox);
        }
    }
}
