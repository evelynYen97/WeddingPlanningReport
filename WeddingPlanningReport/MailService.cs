using MailKit.Security;
using MimeKit;
using MailKit.Net.Smtp;
//using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace WeddingPlanningReport
{
    public class MailService
    {
        private readonly IConfiguration _configuration;

        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string MemberEmail, string MailTitle, string MemberName, string MailContent, string ReplyContent)
        {
            try
            {
                var email = new MimeMessage();
                email.From.Add(new MailboxAddress("AuroraBliss官方團隊", _configuration["MailSettings:SenderEmail"]));
                email.To.Add(MailboxAddress.Parse(MemberEmail));
                email.Subject = MailTitle;


                var bodyBuilder = new BodyBuilder
                {
                    HtmlBody = $@"
                    <html>
                    <body style=""font-family: Arial, sans-serif; line-height: 1.6; margin: 0; padding: 20px; background-color: #f9f9f9;"">
                        <div style=""max-width: 600px; margin: auto; background: white; padding: 20px; border-radius: 8px; box-shadow: 0 2px 5px rgba(0,0,0,0.1);"">
                            <h1 style=""color: #333;"">親愛的 {MemberName}</h1> <!-- 插入會員名稱 -->
                            <p style=""color: #555;"">感謝您使用我們的服務！我們很高興能夠為您提供協助。</p>
                            <p style=""color: #555;"">以下是您之前寄給我們的訊息：</p>

                            <!-- 用戶信件內容 -->
                            <div style=""border-left: 4px solid #555555; background-color: #f1f1f1; padding: 15px; margin-bottom: 20px;"">
                                <p style=""font-weight: bold; color: #333;"">標題：{MailTitle}</p> <!-- 用戶信件標題 -->
                                <p style=""color: #555;"">{MailContent}</p> <!-- 用戶信件內容 -->
                            </div>

                            <!-- 回覆內容 -->
                            <p style=""color: #555;"">以下是我們對您的回覆：</p>
                            <div style=""border-left: 4px solid #555555; background-color: #f1f1f1; padding: 15px;"">
                                <p style=""color: #555;"">{ReplyContent}</p> <!-- 回覆內容 -->
                            </div>

                            <p style=""color: #555;"">如有任何疑問或需要進一步的幫助，請隨時與我們聯繫。</p>
                            <p style=""color: #555;"">此致，</p>
                            <p style=""color: #333; font-weight: bold;"">AuroraBliss官方團隊</p>
                            <img src='cid:logo' alt='公司標誌' style='display: none;' />
                        </div>
                    </body>
                    </html>"
                };




                // 添加嵌入的公司標誌
                var logoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "logo.png"); // 本地路徑
                byte[] logoData = await File.ReadAllBytesAsync(logoPath); // 讀取圖片為 byte[]

                // 使用 MimeKit.ContentType 來指定 MIME 類型
                var contentType = new MimeKit.ContentType("image", "png");
                bodyBuilder.LinkedResources.Add("AuroraBliss", logoData, contentType); // 添加圖片數據和 MIME 類型

                email.Body = bodyBuilder.ToMessageBody();



                //email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = ReplyContent };

                using var smtp = new SmtpClient();
                await smtp.ConnectAsync(_configuration["MailSettings:SmtpServer"],
                int.Parse(_configuration["MailSettings:SmtpPort"]), SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(_configuration["MailSettings:Username"], _configuration["MailSettings:Password"]);
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
