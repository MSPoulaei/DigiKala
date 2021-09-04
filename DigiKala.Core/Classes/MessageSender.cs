using System;
using System.Collections.Generic;
using System.Text;

namespace DigiKala.Core.Classes
{
    public class MessageSender
    {
        public bool SMS(string ReceiverNumber, string MessageBody)
        {
            var sender = "1000596446";
            //var receptor = ReceiverNumber;//"09116863556"
            //var message = MessageBody;// ".وب سرویس پیام کوتاه کاوه نگار"
            try
            {
                var api = new Kavenegar.KavenegarApi("334538713273646B384B6C5965574A574652554844794E6539644C4C4451352F4B776D66676C69657745383D");
                api.Send(sender, ReceiverNumber, MessageBody);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
