//using CoursesAssignviews.Models;
//using MailKit.Security;
//using Microsoft.Extensions.Options;
//using MimeKit;

//namespace CoursesAssignviews.Repositories
//{
//    public class Emailservice : IemailService
//    {
//        private readonly EmailSettings emailSettings;
//        public Emailservice(IOptions<EmailSettings> options)
//        {
//            this.emailSettings = options.Value;
//        }

//        public async Task SendEmail(MailRequest mailrequest)
//        {
//            var email = new MimeMessage();
//            email.Sender = MailboxAddress.Parse(emailSettings.Email);
//            email.To.Add(MailboxAddress.Parse(mailrequest.email));
//            email.Subject = mailrequest.subject;

//            var builder = new BodyBuilder();
//            builder.HtmlBody = mailrequest.body;
//            email.Body = builder.ToMessageBody();

//            using var smtp = new MailKit.Net.Smtp.SmtpClient();


//            smtp.Connect(emailSettings.Host, emailSettings.port, SecureSocketOptions.StartTls);
//            smtp.Authenticate(emailSettings.Email, emailSettings.Password);

//            await smtp.SendAsync(email);
//            smtp.Disconnect(true);


//        }
//    }
//}
