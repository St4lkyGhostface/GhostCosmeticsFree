using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Web;
using System.Net;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Fiddler;
using System.Windows.Forms;
using DiscordRPC;

/// <summary>
/// Welcome to the source code of GhostCosmetics!
/// We are doing this over here because the "professional leakers" leaking a tool available for free on +4 places messed up the code with their scripts!
/// I'm gonna explain you everything the code does so you can actually learn and not be an skid like them :D
/// </summary>




namespace GhostCosmeticsFree
{
    public partial class FreeDashboard : Form
    {
        public FreeDashboard()
        {
            InitializeComponent();
            // Here I define the current version of the client
            string localVersion = "2.0.0";
            // Over here we specify Fiddler Core to start on his job, this will register it as the system proxy
            FiddlerCoreStartupSettings fiddlerCoreStartupSettings = new FiddlerCoreStartupSettingsBuilder().ListenOnPort(50244).RegisterAsSystemProxy().DecryptSSL().Build();
            FiddlerApplication.Startup(fiddlerCoreStartupSettings);
            // To be able to decrypt the traffic we need to serve Fiddler a Certificate, you don't need 121 lines of code for this, just create a cert using 
            // the integrated cert maker and to prompt the certificate trust advice
            CertMaker.createRootCert();
            CertMaker.trustRootCert();

            // These are called "WebClients" they allow our program to send requests, parse URLs and set manual proxies
            WebClient GhostClient = new WebClient();
            // In this case we'll use it to download a string from a repo, this string contains the latest version available
            string serverVersion = GhostClient.DownloadString("https://raw.githubusercontent.com/StalkyGhostface/gh0stbydaylight-althosting/main/rebornApi/liveVersion");
            // This should be understandable by anyone with a bit of english knowledge, skids however, struggle with these somehow
            if (serverVersion.Contains("2.0.0"))
            {
                string isUpdated = ("true");
                    }
            else
            {
                // If the string does not contains the local version number, it'll advise the user that they're using an outdated version
                MessageBox.Show("Download the latest version from my Discord server! (GhostyCommunity)", "OUTDATED Version!");
                string isUpdated = ("false");
                CertMaker.removeFiddlerGeneratedCerts();
                FiddlerApplication.Shutdown();
                // (And will terminate the process)
                Environment.Exit(0);
            }
            ptbMode.Enabled = false;
        }
        // Skids as dumb as Impulse/Shock/Tryharder tried to use this against me, just worked as way to confirm they just dumped my code
        private string heySkid = "first warn, get out of the code";
        // Since we don't want to force users to use the Rich Presence, we read a simple text file and save it as string that will work for us later
        public static string appPath = (System.AppDomain.CurrentDomain.BaseDirectory + "rpc.txt");
        public static string richConfigs = File.ReadAllText(appPath);
        // The base BHVR API endpoint to allow the cosmetics unlocking procedure upon decrypting the traffic
        public static string bhvrAPI = ("https://steam.live.bhvrdbd.com/api/v1/inventories");

        private void guna2GradientTileButton2_Click(object sender, EventArgs e)
        {
            // Since we want to be clear with the users, unlocking on places like the PTB requiere previous confirmation
            bhvrAPI = ("/api/v1/inventories");
            currentMode.Text = ("PTB Mode");
            // BHVR logs way more stuff at the PTB, I don't want gamers banned!
            MessageBox.Show("Using tools while playing PTB is risky!", "You can now click Unlock ALL");
        }

        // Here we set the Discord pipe for the Rich Presence library
        public static int discordPipe = -1;
        public void FreeDashboard_Load(object sender, EventArgs e)
        {
            // A random form name gets generated eveytime it gets loaded
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[16];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var RandomProcessName = new String(stringChars);
            // We set the random form name here
            this.Text = RandomProcessName;
            // A banner gets loaded and appears on the form, compressed to barely 26KB!
            adsBanner.Load(@"https://stalkyghostface.xyz/api/free/banner.png");
            WebClient GhostClient = new WebClient();
            // The announcement gets loaded, appearing in the upper right corner
            string whisperGet = GhostClient.DownloadString("https://raw.githubusercontent.com/StalkyGhostface/gh0stbydaylight-althosting/main/rebornApi/whisper");
            whisper.Text = whisperGet;
            // The date of the market also gets shown
            string marketDate = GhostClient.DownloadString("https://raw.githubusercontent.com/StalkyGhostface/gh0stbydaylight-althosting/main/rebornApi/marketDate");
            marketDisplay.Text = marketDate;
            // And if the PTB is active, this chanes to true
            string ptbStatus = GhostClient.DownloadString("https://raw.githubusercontent.com/StalkyGhostface/gh0stbydaylight-althosting/main/rebornApi/ptb");

            // Remember the RPC.txt file? Here we check it's contents, if it matches true, we enable the presence
            if (richConfigs.Contains("true"))
            {
                int discordPipe = -1;

                var client = new DiscordRpcClient("877637461046599711", pipe: discordPipe);

                client.Initialize();

                client.OnReady += (sender1, msg) =>
                {
                    using (dWebHook dcWeb = new dWebHook())
                    {
                        // Here's a log to a Discord webhook containing the user who just enabled the presence!
                        dcWeb.UserName = "Logs Client 2.0.1";
                        dcWeb.WebHook = ("https://discord.com/api/webhooks/877623285326704661/wb0mS-6cl6PFMUHL-g5QLNMYl5usMVceShOhwwK4ThvgGj9o-b2PVAMV3BX307k-2JVX");
                        dcWeb.SendMessage("Rich Presence: " + msg.User);
                        // Skids like Impulse/Shock/Tryharder only know how to blow them :o
                        // Do you remember when you skidded Bloodygang's tool and sold them for +38 euros Shock?
                        // Do you remember when you screenshoted the "Action Encountered"? That gave your skid off pal
                    }
                };


                // The details of the presence are configured over here
                client.SetPresence(new RichPresence()
                {
                    Details = "Made by StalkyGhostface",
                    State = "Free Edition",
                    Timestamps = Timestamps.Now,
                    Assets = new Assets()
                    {
                        LargeImageKey = "ghost",
                        LargeImageText = "Free Edition",
                        SmallImageKey = "made",
                        SmallImageText = "From GhostyCommunity",
                    },
                    Buttons = new DiscordRPC.Button[]
                    {
                    new DiscordRPC.Button() { Label = "Go Premium", Url = "https://discord.gg/qVCnng3MTh"}
                    }
                });
            }
            // And if the PTB status string we enabled earlier contains true we enable the button for the user
            if (ptbStatus.Contains("true"))
            {
                ptbMode.Enabled = true;
                ptbMode.Text = "PTB Mode";
                localStatus.Text = "PTB is ongoing!";
                    }
            else
            {
                // Otherwise we disable it
                ptbMode.Enabled = false;
                localStatus.Text = "Initialized!";
            }
            
        }

        // This is also a small easter egg of an invisible button I added to the form, gamers discovered it in less than an hour!
        private void coverCover_Click(object sender, EventArgs e)
        {
            using (dWebHook dcWeb = new dWebHook())
            {
                dcWeb.UserName = "Logs Client 2.0.1";
                dcWeb.WebHook = ("https://discord.com/api/webhooks/877623285326704661/wb0mS-6cl6PFMUHL-g5QLNMYl5usMVceShOhwwK4ThvgGj9o-b2PVAMV3BX307k-2JVX");
                dcWeb.SendMessage("Triggered;");
            }
            MessageBox.Show("Don't click me", "shush");
        }

        // Pals like Impulse/Shock/Tryharder tried to use my own words against me. Sad for them
        // what are u seeing? get out of my code
        private void discordButton_Click(object sender, EventArgs e)
        {
            WebClient GhostClient = new WebClient();
            string invite = GhostClient.DownloadString("https://raw.githubusercontent.com/StalkyGhostface/gh0stbydaylight-althosting/main/rebornApi/invite");
            System.Diagnostics.Process.Start(invite);
        }

        // This buffers the reponses before the data gets transfered
        public static void FiddlerApplication_BeforeRequst(Session oSession)
        {
            oSession.bBufferResponse = true;
        }

        // Here we want to set the "Unlock ALL" autoresponder
        public static void FiddlerApplication_BeforeResponseEverything(Session oSession)
        {
            // If an URL in the session containts "api/v1/inventories
            if (oSession.fullUrl.Contains(bhvrAPI))
            {
                // We decode the request removing any compression
                oSession.utilDecodeResponse();
                // We create a webclient
                WebClient webClient = new WebClient();
                // Again, the Impulse/Shock/Tryharder gang tried to use this against me, even though certain people like sleepy can assure you they were added by me
                // on the Reborn releases, they weren't there at the classic releases
                string skidCaughtIn8k = "this is how fiddler works skiddo, learn idiot";
                // With the WebClient we created we parse the market file
                string all = webClient.DownloadString("https://raw.githubusercontent.com/StalkyGhostface/gh0stbydaylight-althosting/main/rebornApi/freeMarketPatch");
                // And we set the inventories response as the Market
                oSession.utilSetResponseBody(all);

            }

        }

        // Same thing goes here; We only change the endpoint of the parse request to the DLC market
        public static void FiddlerApplication_BeforeResponseDLC(Session oSession)
        {
            if (oSession.fullUrl.Contains((bhvrAPI)))
            {
                oSession.utilDecodeResponse();
                WebClient webClient = new WebClient();
                string onlyDLC = webClient.DownloadString("https://raw.githubusercontent.com/StalkyGhostface/gh0stbydaylight-althosting/main/rebornApi/freeDLCPatch");
                // i said, get out of my code dumb skiddo
                string skidLOL = "surprisingly enough you know how to learn other than copy'ng and pasting";
                oSession.utilSetResponseBody(onlyDLC);

            }

        }

        // To assure the user the tool works with Microsoft Mode, there's an option for that, even though not required
        private void msMode_Click(object sender, EventArgs e)
        {
            bhvrAPI = ("/api/v1/inventories");
            currentMode.Text = ("MS Mode");
            MessageBox.Show("No SSL Bypass Requiered!", "You can now click Unlock ALL");
        }

        // When the user clicks unlock all, this occurs
        private void unlockAll_Click(object sender, EventArgs e)
        {
            // I've already told you about these string 9329 times
            string classInstruction = ("this is how a class works dumb skid");
            using (dWebHook dcWeb = new dWebHook())
            {
                dcWeb.UserName = "Logs Client 2.0.1";
                dcWeb.WebHook = ("https://discord.com/api/webhooks/877623285326704661/wb0mS-6cl6PFMUHL-g5QLNMYl5usMVceShOhwwK4ThvgGj9o-b2PVAMV3BX307k-2JVX");
                dcWeb.SendMessage("**Core:** Unlock ALL Ignited!");
            }
            // The buttons get dusabled to prevent a double fiddler startup
            unlockAll.Enabled = false;
            onlyDLC.Enabled = false;
            // The status changes
            localStatus.Text = ("FREE: Unlock ALL Active!");
            // We provide the user with feedback
            MessageBox.Show("Launch your game as normal", currentMode.Text);
            // The autoresponders get enabled and are now waiting for the specified endpoint to clutch!
            FiddlerApplication.BeforeRequest += new SessionStateHandler(FiddlerApplication_BeforeRequst);
            FiddlerApplication.BeforeResponse += new SessionStateHandler(FiddlerApplication_BeforeResponseEverything);
        }

        // Same thing than for Unlock All, just changing the requiered autoresponder and labels
        private void onlyDLC_Click(object sender, EventArgs e)
        {
            using (dWebHook dcWeb = new dWebHook())
            {
                dcWeb.UserName = "Logs Client 2.0.1";
                dcWeb.WebHook = ("https://discord.com/api/webhooks/877623285326704661/wb0mS-6cl6PFMUHL-g5QLNMYl5usMVceShOhwwK4ThvgGj9o-b2PVAMV3BX307k-2JVX");
                dcWeb.SendMessage("**Core:** DLC Unlocker Ignited!");
            }
            string offButton = "this is how a button gets turned off rat skiddo";
            unlockAll.Enabled = false;
            onlyDLC.Enabled = false;
            localStatus.Text = ("FREE: DLC Unlocker Active!");
            MessageBox.Show("Launch your game as normal", currentMode.Text);
            FiddlerApplication.BeforeRequest += new SessionStateHandler(FiddlerApplication_BeforeRequst);
            FiddlerApplication.BeforeResponse += new SessionStateHandler(FiddlerApplication_BeforeResponseDLC);
        }

        // This occurrs when the user closes GhostCosmetics
        private void FreeDashboard_FormClosing(object sender, FormClosingEventArgs e)
        {
            // We tell the user what's currently occuring
            localStatus.Text = ("Killing Process...");
            // Since we don't want to store 69 certificates over the time on the Windows Certificate Store, we remove them using the CertMaker library
            CertMaker.removeFiddlerGeneratedCerts();
            // Shutdown will cause Fiddler to detain the proxy and delete the current session
            FiddlerApplication.Shutdown();

        }

        // This will force the proxy to be killed, without closing the tool
        public void killPresence_Click(object sender, EventArgs e)
        {
            // I already told you what these means
            string DrStrange = "There's 14,000,605 outcomes, only in 1 you learn how to code skid";
            FiddlerApplication.Shutdown();
            // Feedback for the user
            MessageBox.Show("Proxy is automatically terminated upong leaving the program, only use this if you realy need it!", "Proxy Terminated");
            // Buttons go off since Fiddler was killer
            this.unlockAll.Enabled = false;
            this.onlyDLC.Enabled = false;
            // you think i'm as bad as to put a virus in my programs? damn
            // Status of the tool
            localStatus.Text = ("Service Killed :(");
        }

        // Users might sometimes be lost, that's why we provide them with an easy to access tutorial
        private void tutorialOpen_Click(object sender, EventArgs e)
        {
            // We create the webclient
            WebClient GhostClient = new WebClient();
            // We download the string containing the tutorial's link
            string tutorial = GhostClient.DownloadString("https://raw.githubusercontent.com/StalkyGhostface/gh0stbydaylight-althosting/main/rebornApi/tutorial");
            // This line of code tells System.Diagnostics to look for the default app to open the link, in this case the default browser
            System.Diagnostics.Process.Start(tutorial);
        }

        // A simple way to hide on the tray, you don't need to specify the properties of the notifier controller
        private void hideTray_Click(object sender, EventArgs e)
        {
            Hide();
            incognitoMode.Visible = true;
            MessageBox.Show("Now Hidden at Tray", "Click Free Edition Icon at tray to open again");
        }

        // Back again! GhostCosmetics shows up again upon clicking the notifier at the tray
        private void incognitoMode_MouseClick(object sender, MouseEventArgs e)
        {
            Show();
            incognitoMode.Visible = false;
            // I already told you what these means
            string cunt = "i retire what i said, if you knew how to read you would've stopped before";
        }
        // congratulations! you just reversed my shit
        // And that's it folks! You can find more info at the GitHub repo, and I hope you don't sell this like the Impulse Shock guy for +40 euros AKA Archon and
        // leaving it's customers behind with thousands of errors you won't see over here on my realm.

        // If you found this interesting, please continue to learn! Let's prevent skids like those guys using unpackers and deobfuscators to continue existing
        // Oh, and also, those guys are on a quest to paste some spoofers, if they ever try to sell you something better to beware.

        // And for you Impulse.. or now "SHOCK"
        // I provided you with my help, with my trust, attention whenever you DM'ed me, asked me for help with some issue on your code
        // even when you DM'ed me "Fucker" to then just block me, I never, never went mad at you because I liked you as a friend, it seems like you didn't.
        // If you ever feel like to actually talk to me instead of shit talking me at SERVERNAME or at my DMs, feel free to do so.

        // And now for the "leakers" or "raiders"
        // You´re just destroying the community.
        // There's better ways to "improve the community" rather than by raiding small innocent servers, you're just gonna turn this into a ducking CS:GO cheesing community.
        // Even though you're most likely laughing at this, I hope you ever considere how the Dead by Daylight community was probably the cleanest and least toxic on
        // the scene.
        
        // If you were affected by this as a community, or you regret what you did, it's never too late to save the DbD Cheesing & Modding Community.

        // Made with <3
        // Your Friendly Neighborhood Stalky.
    }
}
