﻿using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MimeKit;
using MailKit.Net.Smtp;

namespace DiscussionMVCAppDuffield.Services
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                //From Address    
                string FromAddress = "duffieldjr96@gmail.com";
                string FromAddressTitle = "WVU Parking App Admin";
                //To Address    
                string ToAddress = email;
                string ToAddressTitle = "WVU Parking App User";
                string Subject = subject;
                string BodyContent = message;

                //Smtp Server    
                string SmtpServer = "smtp.gmail.com";
                //Smtp Port Number    
                int SmtpPortNumber = 587;

                var mimeMessage = new MimeMessage();
                mimeMessage.From.Add(new MailboxAddress
                                        (FromAddressTitle,
                                         FromAddress
                                         ));
                mimeMessage.To.Add(new MailboxAddress
                                         (ToAddressTitle,
                                         ToAddress
                                         ));
                mimeMessage.Subject = Subject; //Subject  
                mimeMessage.Body = new TextPart("html")
                {
                    Text = BodyContent
                };

                using (var client = new SmtpClient())
                {
                    client.Connect(SmtpServer, SmtpPortNumber, false);
                    client.Authenticate(
                        "duffieldjr96@gmail.com",
                        "Bp081696d!"
                        );
                    await client.SendAsync(mimeMessage);
                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
