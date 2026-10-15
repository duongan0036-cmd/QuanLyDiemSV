using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace quanlydiem;

public static class mail
{
    private const string host = "smtp.gmail.com";
    private const int cong = 587;
    private const string tk = "thuyst2006374@gmail.com";
    private const string mk = "wrtb hpgf xxei gula";

    public static Task guiasync(string den, string tieude, string noidung)
    {
        return Task.Run(() =>
        {
            using var m = new MailMessage();
            m.From = new MailAddress(tk, "Quản lý điểm sinh viên", Encoding.UTF8);
            m.To.Add(den);
            m.Subject = tieude;
            m.SubjectEncoding = Encoding.UTF8;
            m.Body = noidung;
            m.BodyEncoding = Encoding.UTF8;
            using var c = new SmtpClient(host, cong)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(tk, mk)
            };
            c.Send(m);
        });
    }
}