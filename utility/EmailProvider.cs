
using System.Net;
using System.Net.Mail;

namespace sample_one.utility
{



    public class EmailProvider : IEmailProvider
    {
        public async Task<string> SendVerificationEmail(string email)
        {
            

string htmlContent = @"
<!DOCTYPE html>
<html>
<head>
<title>Your Email Title</title>
<style>
  body {
    font-family: Arial, sans-serif;
    font-size: 16px;
    color: #333;
  }

  h1 {
    font-size: 24px;
    margin: 0;
  }

  p {
    margin: 10px 0;
  }

  .otp-container {
    background-color: #f5f5f5;
    padding: 10px;
    border-radius: 5px;
    text-align: center;
  }

  .otp-code {
    font-size: 20px;
    font-weight: bold;
    font-style: italic;
    color: blue;
  }
</style>
</head>
<body>
<h1>Welcome, <b>@Name</b>!</h1>

<p>Your one-time password (OTP) is:</p>
<div class='otp-container'>
  <span class='otp-code'>@Otp</span>
</div>
<p>Please enter this code to verify your account.</p>
<p>Thanks,<br>Your Company Name</p>
</body>
</html>
";



htmlContent = htmlContent.Replace("@Name", email);
 Random random = new Random();

        // Generate a random integer between 100000 and 999999 (inclusive)
int randomNumber = random.Next(100000, 1000000);
htmlContent = htmlContent.Replace("@Otp",randomNumber.ToString());





            using (SmtpClient client = new SmtpClient("smtp-relay.brevo.com", 587))
            {
                client.Credentials = new NetworkCredential("arijitworkofc@gmail.com", "VQSvzWbXxML09jmZ");
                client.EnableSsl = true;

                MailMessage message = new MailMessage();
                message.From = new MailAddress("arijitworkofc@gmail.com");
                message.To.Add(email);
                message.Subject = "the verification code is ";
                  message.IsBodyHtml = true; // Set this to true for HTML content
                    message.Body = htmlContent;

                 client.Send(message);
            }

            return "test message";

            
        }
    }



}