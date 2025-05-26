using MailKit.Net.Smtp;
using MimeKit;

namespace EmailService
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfiguration _emailConfiguration;

        public EmailSender(EmailConfiguration emailConfiguration)
        {
            _emailConfiguration = emailConfiguration;
        }


        public async Task SendEmailAsync(Message message)
        {
            var mailMessage = CreateEmailMessage(message); //to samo co było 
            await SendEmailAsync(mailMessage); //nowa metoda asynchroniczna
        }

        private MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailConfiguration.From, _emailConfiguration.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            var bodyBuilder = new BodyBuilder
            {
                TextBody = message.Content
            };

            emailMessage.Body = bodyBuilder.ToMessageBody();
            return emailMessage;
        }


        private async Task SendEmailAsync(MimeMessage message)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(_emailConfiguration.SmtpServer, _emailConfiguration.Port, true);//tu zmieniamy na metodę asynchroniczną
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    await client.AuthenticateAsync(_emailConfiguration.Username, _emailConfiguration.Password);//tu zmieniamy na metodę asynchroniczną

                    await client.SendAsync(message);//tu zmieniamy na metodę asynchroniczną
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("Wystąpił błąd podczas wysyłania wiadomości e-mail", ex);
                }
                finally
                {
                    await client.DisconnectAsync(true);//tu zmieniamy na metodę asynchronicznąS
                    client.Dispose();
                }
            }
        }
    }
}
