using static SciChain.Orcid;
using static SciChain.Blockchain;
using NetCoreServer;
using System.Transactions;
using Gtk;
using Pango;
using System.Reflection;
using System;
namespace SciChain
{
    public partial class MainForm : Window
    {
        #region Properties

        /// <summary> Used to load in the glade file resource as a window. </summary>
        private Builder _builder;
        private static MainForm form;
        int line = 0;
#pragma warning disable 649
        [Builder.Object]
        private Label balanceLabel;
        [Builder.Object]
        private Label reputationLabel;
        [Builder.Object]
        private Entry addressBox;
        [Builder.Object]
        private SpinButton amountBox;
        [Builder.Object]
        private Entry sendToNameBox;
        [Builder.Object]
        private Button sendBut;
        [Builder.Object]
        private Button getAddressBut;
        [Builder.Object]
        private Entry passwordBox;
        [Builder.Object]
        private Button loginBut;
        [Builder.Object]
        private Label statusLabel;
#pragma warning restore 649

        #endregion

        #region Constructors / Destructors

        /// Create a new BioConsole object using the Glade file "BioGTK.Glade.BioConsole.glade"
        /// @return A new instance of the BioConsole class.
        public static MainForm Create()
        {
            Builder builder = new Builder(new FileStream(System.IO.Path.GetDirectoryName(Environment.ProcessPath) + "/" + "Glade/MainForm.glade", FileMode.Open));
            return new MainForm(builder, builder.GetObject("mainform").Handle);
        }

        /// <summary> Specialised constructor for use only by derived class. </summary>
        /// <param name="builder"> The builder. </param>
        /// <param name="handle">  The handle. </param>
        protected MainForm(Builder builder, IntPtr handle) : base(handle)
        {
            _builder = builder;
            builder.Autoconnect(this);
            SetupHandlers();
            form = this;
        }

        #endregion

        #region Handlers

        /// <summary> Sets up the handlers. </summary>
        protected void SetupHandlers()
        {
            getAddressBut.Clicked += getAddrBut_Click;
            sendBut.Clicked += sendBut_Click;
            loginBut.Clicked += loginBut_Click;
            statusLabel.ButtonPressEvent += StatusLabel_ButtonPressEvent;
            this.DestroyEvent += MainForm_DestroyEvent;
        }

        private void StatusLabel_ButtonPressEvent(object o, ButtonPressEventArgs args)
        {
            Clipboard clipboard = Clipboard.Get(Gdk.Selection.Clipboard);
            // Set text to the clipboard
            clipboard.Text = GUID;
        }

        private void MainForm_DestroyEvent(object o, DestroyEventArgs args)
        {
            Save();
            wallet.Save(passwordBox.Buffer.Text);
        }

        #endregion
        private StringWriter _writer;
        private Blockchain.Wallet wallet;
        private string GUID;
        string token;
        public string peer = "92.205.238.105";
        private async void loginBut_Click(object? sender, EventArgs e)
        {
            _writer = new StringWriter();
            Console.SetOut(_writer);
            wallet = new Blockchain.Wallet();
            wallet.Load(passwordBox.Buffer.Text);
            string st = RSA.RSAParametersToStringAll(wallet.PrivateKey);
            GUID = CalculateHash(st);
            Initialize(wallet);
            ChatClient cl = new ChatClient(peer, 8333);
            cl.ConnectAsync();
            ConnectToPeer(peer, cl, 8333);
            Blockchain.Load();
            StartTimer();
            statusLabel.Text = "Logged In:" + GUID;
            balanceLabel.Text = "Balance: " + GetBalance(GUID).ToString();
        }
        private static void Timer()
        {
            do
            {
                try
                {
                    form.statusLabel.Text = "Connections:" + Peers.Count.ToString() + " Height: " + Chain.Count + " Treasury:" + GetTreasury();
                    form.balanceLabel.Text = "Balance: " + GetBalance(form.GUID).ToString();
                    form.reputationLabel.Text = "Reputation: " + GetReputation(form.GUID).ToString();
                    Thread.Sleep(1000);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            } while (true);
            
        }
        private void StartTimer()
        {
            Thread th = new Thread(Timer);
            th.Start();
        }

        private void AddItem(ComboBox box, string st)
        {
            ListStore ls = new ListStore(typeof(string));
            TreeIter iter;
            if(box.Model == null)
            {
                box.Model = new ListStore(typeof(string));
            }
            // Check if the model has a first row
            if (box.Model.GetIterFirst(out iter))
            {
                do
                {
                    // Retrieve the value in the first column of the current row
                    string item = (string)box.Model.GetValue(iter, 0);
                    ls.AppendValues(item);
                }
                while (box.Model.IterNext(ref iter)); // Move to the next row
            }
            ls.AppendValues(st);
            // Create a CellRendererText and pack it into the ComboBox
            CellRendererText cell = new CellRendererText();
            box.PackStart(cell, false);
            box.AddAttribute(cell, "text", 0); // Associate the renderer with the first column of the model
            box.Model = ls;
        }

        private void sendBut_Click(object? sender, EventArgs e)
        {
            Block.Transaction tr = new Block.Transaction(Block.Transaction.Type.transaction, GUID, wallet.PublicKey, addressBox.Buffer.Text, (decimal)amountBox.Value);
            tr.SignTransaction(wallet.PrivateKey);
            bool res = VerifyTransaction(tr);

            AddTransaction(tr);
        }

        private void updateBut_Click(object? sender, EventArgs e)
        {
            balanceLabel.Text = "Balance: " + GetBalance(GUID).ToString();
            reputationLabel.Text = "Reputation: " + GetReputation(GUID).ToString();
        }

        private async void getAddrBut_Click(object? sender, EventArgs e)
        {
            addressBox.Buffer.Text = await Orcid.SearchForORCID(sendToNameBox.Text);
        }
    }
}
