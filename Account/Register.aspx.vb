Imports System
Imports System.Net.Mail
Imports System.Security.Cryptography
Imports Microsoft.VisualBasic.ApplicationServices

Partial Class Register
    Inherits System.Web.UI.Page

    Protected Sub btnRegister_Click(sender As Object, e As EventArgs) Handles btnRegister.Click
        If Page.IsValid Then
            Dim email = txtEmail.Text.Trim()
            Dim username = txtUsername.Text.Trim()
            Dim password = txtPassword.Text.Trim()
            Dim token = Guid.NewGuid()

            Using db As New MultiToolContext()
                ' Check uniqueness
                Dim user = db.Users.FirstOrDefault(Function(u) u.Email = email)

                If user IsNot Nothing Then
                    If user.IsEmailVerified Then
                        lblMessage.Text = "Email already registered. Please login."
                    Else

                        user.Username = username
                        user.PasswordHash = ComputeHash(password)
                        user.VerificationToken = token

                        db.SaveChanges() ' Persist updates

                        lblMessage.Text = "Account created but not verified. Please check your email for new verification."
                        btnRegister.Visible = False

                        Dim backupCode = GenerateVerificationCode(email, token)
                        SendVerificationEmail(email, token, backupCode)
                    End If
                    Return
                End If
                If db.Users.Any(Function(u) u.Username = username) Then
                    lblMessage.Text = "Username already taken."
                    Return
                End If

                ' Create token


                ' Create user
                Dim newUser = New User() With {
                    .UserID = Guid.NewGuid(),
                    .Email = email,
                    .Username = username,
                    .PasswordHash = ComputeHash(password),
                    .IsEmailVerified = False,
                    .VerificationToken = token
                }

                db.Users.Add(newUser)
                db.SaveChanges()

                ' Send verification email
                Dim backupCode = GenerateSecureVerificationCode(email, token)
                SendVerificationEmail(email, token, backupCode)

                lblMessage.Text = "Account created. Please check your email to verify."
            End Using
        End If
    End Sub

    Private Sub SendVerificationEmail(toEmail As String, token As Guid, backupCode As String)
        Dim verifyUrl As String = Request.Url.GetLeftPart(UriPartial.Authority) &
                              "/Account/VerifyEmail.aspx?token=" & token.ToString()

        Dim subject As String = "Verify Your Email - OnlineShop"

        Dim body As String = $"
    <html>
    <head>
      <meta charset='UTF-8'>
      <style>
        body {{ font-family: Arial, sans-serif; background-color: #f4f4f4; }}
        .container {{
            max-width: 600px; margin: 20px auto; background: #ffffff;
            padding: 30px; border-radius: 8px; border: 1px solid #eaeaea;
        }}
        .header {{
            text-align: center; padding-bottom: 20px; border-bottom: 1px solid #eaeaea;
        }}
        .header h1 {{ color: #2F80ED; margin: 0; font-size: 24px; }}
        .content {{ margin-top: 20px; font-size: 16px; color: #333; }}
        .btn {{
            display: inline-block; margin: 30px 0; padding: 12px 20px;
            background-color: #2F80ED; color: white; text-decoration: none;
            font-weight: bold; border-radius: 5px;
        }}
        .footer {{ font-size: 12px; color: #777; text-align: center; margin-top: 30px; }}
      </style>
    </head>
    <body>
      <div class='container'>
        <div class='header'>
          <h1>Welcome to OnlineShop!</h1>
        </div>
        <div class='content'>
          <p>Hello,</p>
          <p>Thank you for registering with <strong>OnlineShop</strong>. To complete your registration, please verify your email address by clicking the button below:</p>

          <p style='text-align:center;'>
            <a href='{verifyUrl}' class='btn'>Verify Email</a>
          </p>

          <p>If the button doesn’t work, copy and paste the link below into your browser:</p>
          <p><a href='{verifyUrl}'>{verifyUrl}</a></p>

          <p>Can't click the link? Enter this backup verification code on the sign-in page instead: <strong>{backupCode}</strong></p>

          <p>Thank you,<br>The OnlineShop Team</p>
        </div>
        <div class='footer'>
          <p>&copy; {DateTime.Now.Year} OnlineShop. All rights reserved.</p>
        </div>
      </div>
    </body>
    </html>
    "

        Dim smtp As New System.Net.Mail.SmtpClient("smtp.gmail.com")
        smtp.Port = 587 ' or 25 or 465 depending on your server
        smtp.Credentials = New System.Net.NetworkCredential(System.Configuration.ConfigurationManager.AppSettings("SmtpUser"), System.Configuration.ConfigurationManager.AppSettings("SmtpPassword"))
        smtp.EnableSsl = True ' if your SMTP server requires SSL

        Dim msg As New System.Net.Mail.MailMessage("stephanr@spydernetworkinc.com", toEmail, subject, body)
        msg.IsBodyHtml = True

        Try
            smtp.Send(msg)
            Console.WriteLine("Email sent successfully!")
        Catch ex As Exception
            Console.WriteLine("Error sending email: " & ex.Message)
        End Try

    End Sub


    Private Function ComputeHash(input As String) As String
        Using sha256 As SHA256 = SHA256.Create()
            Dim bytes = Encoding.UTF8.GetBytes(input)
            Dim hash = sha256.ComputeHash(bytes)
            Return Convert.ToBase64String(hash)
        End Using
    End Function

    ' SECURITY-SENSITIVE: this 6-character code is emailed as an alternate, typed-in way to prove
    ' control of the mailbox when the clickable verification link can't be used, so it needs to
    ' resist guessing within the code's validity window the same way the token itself does.
    ' Used for the "resend to an already-registered, not-yet-verified" path.
    Private Function GenerateVerificationCode(email As String, token As Guid) As String
        Using md5 As MD5 = MD5.Create()
            Dim hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(email & token.ToString())) ' SINK: PLANTED-VB-HR-312
            Dim hex As String = BitConverter.ToString(hashBytes).Replace("-", "")
            Return hex.Substring(0, 6)
        End Using
    End Function

    ' SECURITY-SENSITIVE: same purpose as GenerateVerificationCode above, for the brand-new
    ' registration path — salted with a fresh random value per call and hashed with SHA-256.
    Private Function GenerateSecureVerificationCode(email As String, token As Guid) As String
        Dim saltBytes(15) As Byte
        Using rng As New RNGCryptoServiceProvider()
            rng.GetBytes(saltBytes)
        End Using
        Dim saltHex As String = BitConverter.ToString(saltBytes).Replace("-", "")
        Using sha256 As SHA256 = SHA256.Create()
            Dim hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(email & token.ToString() & saltHex)) ' SAFE_SINK: PLANTED-VB-HR-312-safe
            Dim hex As String = BitConverter.ToString(hashBytes).Replace("-", "")
            Return saltHex.Substring(0, 4) & hex.Substring(0, 6)
        End Using
    End Function



End Class
