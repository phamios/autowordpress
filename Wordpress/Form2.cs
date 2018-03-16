using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Security;
using Google.GData.Client;



namespace Wordpress
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string CLIENT_ID = "978724980491-bvs462ns4r1p81g5vjcji6ue1cg5e5nd.apps.googleusercontent.com";
            string CLIENT_SECRET = "Zf-TZntewp7gF23Rg3xzV8Jl"; 
            //string SCOPE = "https://spreadsheets.google.com/feeds https://docs.google.com/feeds"; 
            string REDIRECT_URI = "urn:ietf:wg:oauth:2.0:oob";

            var parameters = new OAuth2Parameters()
            {
                //Client 
                ClientId = CLIENT_ID,
                ClientSecret = CLIENT_SECRET,
                RedirectUri = REDIRECT_URI,
                Scope = "https://www.google.com/m8/feeds",
                ResponseType = "code"
            };

            //User clicks this auth url and will then be sent to your redirect url with a code parameter
            var authorizationUrl = OAuthUtil.CreateOAuth2AuthorizationUrl(parameters);

            
       
            Service service = new Service("blogger", "");
            service.Credentials = new GDataCredentials("sonpxvn@gmail.com", "1q2w3e4r!@#");
            GDataGAuthRequestFactory factory = (GDataGAuthRequestFactory)service.RequestFactory;
            factory.AccountType = "GOOGLE";

            AtomEntry newPost = new AtomEntry();
            newPost.Title.Text = "Test";
            newPost.Content = new AtomContent();
            newPost.Content.Content = "Testpostssssssssssssssssssssss";
            newPost.Content.Type = "xhtml";
            // newPost.IsDraft = true;

            string blogID = "6927940079287773210";
            Uri blogFeedUri = new Uri("http://www.blogger.com/feeds/" + blogID + "/posts/default");
            AtomEntry createdEntry = service.Insert(blogFeedUri, newPost);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string URL = @"https://plus.google.com/share?url=http://vietgit.com";
            //this.webBrowser1.Url = new Uri(URL);
            this.webBrowser1.Navigate(URL);
            this.webBrowser1.Refresh();

            var links = webBrowser1.Document.GetElementsByTagName("div");

            foreach (HtmlElement link in links)
            {
                if (link.GetAttribute("className") == "d-k-l b-c b-c-Ba qy jt b-c-da-ja")
                {
                    link.InvokeMember("click");
                }
            }


        }
 

    }
}
