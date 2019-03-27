using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Wox.Plugin;

namespace Random.Password.Generator
{
    class Main : IPlugin
    {
        private PluginInitContext context;
        public void Init(PluginInitContext context)
        {
            this.context = context;
            random = new System.Random();
        }

        public List<Result> Query(Query query)
        {
            var results = new List<Result>();
            if (query.ActionParameters.Count == 1)
            {
                string r = CreatePassword(Convert.ToInt32(query.ActionParameters[0]));
                results.Add(new Result
                {
                    Title =r,
                    IcoPath = "Images\\password.png",
                    Action = c =>
                    {
                        context.API.HideApp();
                        Clipboard.SetText(r);
                        return true;
                    }
                });
            }
            else
            {
                for(int i=0;i< Convert.ToInt32(query.ActionParameters[1]); i++)
                {
                    string r = CreatePassword(Convert.ToInt32(query.ActionParameters[0]));
                    results.Add(new Result
                    {
                        Title = r,
                        IcoPath = "Images\\password.png",
                        Action = c =>
                        {
                            context.API.HideApp();
                            Clipboard.SetText(r);
                            ShowBalloon("Random Password Generator", "Copy to clypbord Password" );
                            return true;
                        }
                    });
                }
            }
            return results;
        }
        System.Random random ;
        private string CreatePassword(int length = 5)
        {
            // Create a string of characters, numbers, special characters that allowed in the password  
            //string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*?_-";

            string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789@#$%^*?_-";
            // Select one random character at a time from the string  
            // and create an array of chars  
            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }
            return new string(chars);
        }
        private void ShowBalloon(string title, string body)
        {
            NotifyIcon notifyIcon = new NotifyIcon();
            notifyIcon.Visible = true;

            if (title != null)
            {
                notifyIcon.BalloonTipTitle = title;
            }

            if (body != null)
            {
                notifyIcon.BalloonTipText = body;
            }

            notifyIcon.ShowBalloonTip(30000);
            // The notification should be disposed when you don't need it anymore,
            // but doing so will immediately close the balloon if it's visible.
            notifyIcon.Dispose();

        }
    }
}
