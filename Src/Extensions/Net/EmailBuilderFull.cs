using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Mail;
using GoodToCode.Extensions;
using GoodToCode.Extensions.Text;

namespace GoodToCode.Extensions.Net
{
    /// <summary>
    /// Forms and sends emails. Supports HTML templates.
    /// </summary>
    public class EmailBuilder
    {
        /// <summary>
        /// Legal footer required on every email
        /// </summary>
        public class FooterLegal
        {
            /// <summary>
            /// ApplicationID
            /// </summary>
            public Guid ApplicationId { get; set; } = Defaults.Guid;
            /// <summary>
            /// PublishFooter
            /// </summary>
            public bool PublishFooter { get; set; } = Defaults.Boolean;
            /// <summary>
            /// ToEmailAddress
            /// </summary>
            public string ToEmailAddress { get; set; } = Defaults.String; // 0
            /// <summary>
            /// CompanyFriendlyName
            /// </summary>
            public string CompanyFriendlyName { get; set; } = Defaults.String; // 1
            /// <summary>
            /// UnsubscribeURL
            /// </summary>
            public string UnsubscribeURL { get; set; } = Defaults.String; // 2
            /// <summary>
            /// CompanyLegalName
            /// </summary>
            public string CompanyLegalName { get; set; } = Defaults.String; // 3
            /// <summary>
            /// Address
            /// </summary>
            public string Address { get; set; } = Defaults.String; // 4
            
            /// <summary>
            /// Constructor
            /// </summary>
            public FooterLegal() : base() { }

            /// <summary>
            /// Constructor
            /// </summary>
            public FooterLegal(string toEmailAddress, string companyFriendlyName, string unsubscribeURL, string companyLegalName, string address, Guid applicationId)
            {
                PublishFooter = true;
                ToEmailAddress = toEmailAddress;
                CompanyFriendlyName = companyFriendlyName;
                UnsubscribeURL = unsubscribeURL;
                CompanyLegalName = companyLegalName;
                Address = address;
                ApplicationId = applicationId;
            }
            
            /// <summary>
            /// Returns as array that fills data in the footer
            /// </summary>
            /// <returns>List of strings with fully formed email Html</returns>
            public List<string> ToList()
            {
                var returnValue = new List<string>();
                PublishFooter = true;
                returnValue.Add(this.ToEmailAddress);
                returnValue.Add(this.CompanyFriendlyName);
                returnValue.Add(this.UnsubscribeURL);
                returnValue.Add(this.CompanyLegalName);
                returnValue.Add(this.Address);
                return returnValue;
            }

            /// <summary>
            /// Returns HTML footer with all data
            /// </summary>
            /// <returns>Footer portion of email</returns>
            public string ToFooter()
            {
                var legalFooter = @"<br /><hr /><p style=""font-size:11px; color: lightgray; "">This message was sent to {0}. If you don't want to receive these emails from {1} in the future, please <a href=""{2}"">unsubscribe</a>.{3}, {4}</p>";
                var builder = new TemplateBuilder(legalFooter, this.ToList());
                PublishFooter = true;
                string returnValue = builder.ToString();
                return returnValue;
            }
            
        }
        
        /// <summary>
        /// SendCompletedCallbackDelegate
        /// </summary>
        /// <param name="sender">Sender information</param>
        /// <param name="e">Event argument data</param>
        public delegate void SendCompletedCallbackDelegate(object sender, AsyncCompletedEventArgs e);
        private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            // Get the unique identifier for this asynchronous operation. 
            var token = Convert.ToString(e.UserState);

            if (e.Cancelled)
            {
                Console.WriteLine("[{0}] Send canceled.", token);
            }
            if (e.Error != null)
            {
                Console.WriteLine("[{0}] {1}", token, e.Error.ToString());
            }
            else
            {
                Console.WriteLine("Message sent.");
            }
        }
        
        /// <summary>
        /// Constructor
        /// </summary>
        public EmailBuilder() : base()
        {
        }
        
        /// <summary>
        /// Sends a mail based on off SMTP settings in .config
        /// </summary>
        /// <param name="mailToAddresses"></param>
        /// <param name="title"></param>
        /// <param name="contents"></param>
        /// <param name="legal"></param>
        /// <param name="addressseparator"></param>
        /// <returns></returns>
        public KeyValuePair<string, bool> Send(string mailToAddresses, string title, string contents, FooterLegal legal, char addressseparator = ';')
        {

            var returnValue = new KeyValuePair<string, bool>();
            System.Collections.Generic.List<string> mailTo = new System.Collections.Generic.List<string>();

            // The Mail To Addresses will be separated by ;
            mailTo = new List<string>(mailToAddresses.Trim().Split(new char[] { addressseparator }));
            returnValue = Send(mailTo, title, contents, legal, SendCompletedCallback, Defaults.String);

            // Return success/failure
            return returnValue;
        }

        /// <summary>
        /// Sends a mail based on off SMTP settings in .config
        /// </summary>
        /// <param name="mailToAddresses"></param>
        /// <param name="title"></param>
        /// <param name="contents"></param>
        /// <param name="legal"></param>
        /// <param name="callback"></param>
        /// <param name="callbackData"></param>
        /// <param name="addressseparator"></param>
        /// <returns>Email addresses and their send status (true or false)</returns>
        public KeyValuePair<String, Boolean> Send(System.Collections.Generic.List<string> mailToAddresses, string title, string contents, FooterLegal legal, 
            SendCompletedCallbackDelegate callback, string callbackData, char addressseparator = ';')
        {
            var returnValue = new KeyValuePair<String, bool>();
            var client = new SmtpClient();
            var footer = Defaults.String;
            var toAddresses = new List<string>();

            try
            {
                // Never batch send for legal reasons. Have to put email in the footer of every email
                foreach (var emailAddress in mailToAddresses)
                {
                    if (emailAddress.IsEmail(false))
                    {
                        // Will get 'from, etc' from .config file
                        var OutgoingMail = new MailMessage();
                        OutgoingMail.To.Add(new MailAddress(emailAddress));
                        OutgoingMail.Subject = title;
                        OutgoingMail.Body = contents + (legal.PublishFooter ? legal.ToFooter() : Defaults.String);
                        OutgoingMail.IsBodyHtml = true;
                        OutgoingMail.Priority = MailPriority.Normal;
                        client.SendCompleted += SendCompletedCallback;
                        client.Send(OutgoingMail);
                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                client.Dispose();
            }

            // Return success/failre
            return returnValue;
        }
        
        /// <summary>
        /// Sends an email one at a time, with variable data for each message body
        /// Uses String.Format to handle a template.
        ///    Dim Template As string = "Confirmation email for {0}. " + _
        ///          "Date: {1}. Weeks: {2}\n" + _
        ///          "Name: {3}\n" + _
        ///          "{4}"
        ///    String.Format(Template, "Spot1", Date.UtcNow, "Spot3", "Spot4")
        /// </summary>
        /// <param name="mailToAddressesAndData">email address and all the data for that email message</param>
        /// <param name="title"></param>
        /// <param name="contents"></param>
        /// <param name="legal"></param>
        /// <param name="callback"></param>
        /// <param name="callbackData"></param>
        /// <param name="addressseparator"></param>
        /// <returns></returns>
        public Dictionary<string, bool> SendTemplate(List<KeyValuePair<string, List<string>>> mailToAddressesAndData, string title, string contents,
                                            FooterLegal legal, SendCompletedCallbackDelegate callback, string callbackData, char addressseparator = ';')
        {
            var returnValue = new Dictionary<string, bool>();
            var mailResult = new KeyValuePair<string, bool>();
            var mailTo = new List<string>();
            var titleFilled = Defaults.String;

            foreach (var Item_loopVariable in mailToAddressesAndData)
            {
                mailTo.Clear();
                mailTo.Add(Item_loopVariable.Key);
                contents = new TemplateBuilder(contents, Item_loopVariable.Value).ToString();
                mailResult = Send(mailTo, title, contents, legal, callback, callbackData);
                returnValue.Add(mailResult.Key, mailResult.Value);
            }

            return returnValue;
        }
    }
}
