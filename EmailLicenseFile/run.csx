#r "SendGrid"

using SendGrid.Helpers.Mail;
using System.Text.RegularExpressions;

public static void Run(string myBlob, string filename, TraceWriter log, out Mail message)
{
    var email = Regex.Match(myBlob, @"^Email\:\ (.+)$", RegexOptions.Multiline).Groups[1].Value;
    log.Info($"Got order from {email}\n License file name:{filename}");

    message = new Mail();
    var personalization = new Personalization();
    personalization.AddTo(new Email(email));
    message.AddPersonalization(personalization);

    Attachment attachment = new Attachment();
    var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(myBlob);
    attachment.Content = System.Convert.ToBase64String(plainTextBytes);
    attachment.Type = "text/plain";
    attachment.Filename = "license.lic";
    attachment.Disposition = "attachment";
    attachment.ContentId = "License File";
    message.AddAttachment(attachment);

    var messageContent = new Content("text/html","Your license file is attached.");

    log.Info($"messageContent: {messageContent.ToString()}");

    message.AddContent(messageContent);
    message.Subject = "Thanks for your order";
    message.From = new Email("esbjorn.larsen@dqc.se");
}