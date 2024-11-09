using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace WeddingPlanningReport
{
    public class ViolationNoticeMailService
    {
        private readonly IConfiguration _configuration;

        public ViolationNoticeMailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string MemberEmail, string MemberName, string MailContent,DateTime? CreatedTime,string shopName)
        {
            try
            {
                var email = new MimeMessage();
                email.From.Add(new MailboxAddress("AuroraBliss官方團隊", _configuration["MailSettings:SenderEmail"]));
                email.To.Add(MailboxAddress.Parse(MemberEmail));
                email.Subject = "關於您在AuroraBliss商家的評價暫時下架的通知";


                var bodyBuilder = new BodyBuilder
                {
                    HtmlBody = $@"
                    <html>
                    <body style=""font-family: Arial, sans-serif; line-height: 1.6; margin: 0; padding: 20px; background-color: #f9f9f9;"">
                        <div style=""max-width: 600px; margin: auto; background: white; padding: 20px; border-radius: 8px; box-shadow: 0 2px 5px rgba(0,0,0,0.1);"">
                            <h1 style=""color: #333;"">親愛的 {MemberName}</h1> <!-- 插入會員名稱 -->
                            <h3 style=""color: #555;"">感謝您使用我們網站的服務！</h3>
                            <h3 style=""color: #555;"">在此要遺憾地通知您，{CreatedTime}對商家{shopName}的評價可能存在違規行爲，因此該條評價已經被暫時下架，等待進一步的審核。</h3>
                            <p>該條評價内容如下：</p>
                            <div style=""border-left: 4px solid #555555; background-color: #f1f1f1; padding: 15px; margin-bottom: 20px;"">
                                <p style=""color: #555;"">{MailContent}</p> 
                            </div>

                            <h4 style=""color: #555;"">如有任何疑問或需要進一步的幫助，請隨時與我們聯繫。</h4>
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
