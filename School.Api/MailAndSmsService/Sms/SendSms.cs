using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MailAndSmsService.Sms
{
    public class SendSms
    {
        public static string Send(string MobileNumber, string Message)
        {
            //Your authentication key  
            string authKey = "O7rPdd4c2k69pQ4auuHGiw";

            //Multiple mobiles numbers separated by comma  
            string mobileNumber = MobileNumber;
            //Sender ID,While using route4 sender id should be 6 characters long.  
            string senderId = "WEBSMS";
            //Your message to send, Add URL encoding here.  
            string channel = "Promo";
            string message = HttpUtility.UrlEncode(Message);
            string route = "4";
            //Prepare you post parameters  
            StringBuilder sbPostData = new StringBuilder();
            sbPostData.AppendFormat("user={0}", authKey);
            sbPostData.AppendFormat("&password={0}", "306991");
            sbPostData.AppendFormat("&senderid={0}", "INFOSM");
            sbPostData.AppendFormat("&sender={0}", senderId);
            sbPostData.AppendFormat("&channel={0}", "PARANSE");
            sbPostData.AppendFormat("&DCS={0}", 0);
            sbPostData.AppendFormat("&flashsms={0}", 0);
            sbPostData.AppendFormat("&number={0}", "");
            sbPostData.AppendFormat("&text={0}", "test sms");
            sbPostData.AppendFormat("&route={0}", "##");
            //Call Send SMS API  ?user=demo&password=demo123&senderid=WEBSMS
            //&channel=Promo
            //&DCS=0&flashsms=0&number=91989xxxxxxx&text=test message&route=##

            string sendSMSUri = "http://smslogin.pcexpert.in/api/mt/SendSMS";
            //Create HTTPWebrequest  
            HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(sendSMSUri);
            //Prepare and Add URL Encoded data  
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] data = encoding.GetBytes(sbPostData.ToString());
            //Specify post method  
            httpWReq.Method = "POST";
            httpWReq.ContentType = "application/x-www-form-urlencoded";
            httpWReq.ContentLength = data.Length;
            using (Stream stream = httpWReq.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            //Get the response  
            HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string responseString = reader.ReadToEnd();

            //Close the response  
            reader.Close();

            response.Close();
            return responseString;
        }
        public static async Task<string> SendOtp(string MobileNumber, string Message)
        {

            string baseurl = "http://smslogin.pcexpert.in/api/mt/SendSMS?user=SALUJA&password=trust&senderid=INFOSM&channel=Trans&DCS=0&flashsms=0&number=" + MobileNumber + "&text=" + Message + "&route=46";
            using (var client = new HttpClient())
            {


                var response = await client.GetStringAsync(baseurl);
                // Parse JSON response.
                return response;

            }
        }
    }
}
