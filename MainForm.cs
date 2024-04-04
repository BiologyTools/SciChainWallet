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
        private Button copyBut;
        [Builder.Object]
        private Label statusLabel;
#pragma warning restore 649

        #endregion

        #region Constructors / Destructors

        
        /// <summary>
        /// The function creates a MainForm object using a Builder to load a Glade file.
        /// </summary>
        /// <returns>
        /// An instance of the `MainForm` class is being returned.
        /// </returns>
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
            copyBut.Clicked += CopyBut_Clicked;
            this.Destroyed += MainForm_Destroyed;
        }

        private void MainForm_Destroyed(object? sender, EventArgs e)
        {
            Save();
            wallet.Save(passwordBox.Buffer.Text);
            Application.Quit();
        }

        /// <summary>
        /// The function `CopyBut_Clicked` copies the value of the `GUID` variable to the clipboard when
        /// a button is clicked.
        /// </summary>
        /// <param name="sender">The `sender` parameter in the `CopyBut_Clicked` method refers to the
        /// object that raised the event. In this case, it would be the button that was clicked to
        /// trigger the event.</param>
        /// <param name="EventArgs">The `EventArgs` parameter in the `CopyBut_Clicked` event handler
        /// method is a base class for classes containing event data. It is often used when the event
        /// handler does not need to pass any additional information about the event.</param>
        private void CopyBut_Clicked(object? sender, EventArgs e)
        {
            TextCopy.ClipboardService.SetText(GUID);
        }
        
        #endregion
        private StringWriter _writer;
        private Blockchain.Wallet wallet;
        private string GUID;
        string token;
        public string peer = "92.205.238.105";
       /// <summary>
       /// The loginBut_Click function initializes a wallet, connects to a chat client, loads blockchain
       /// data, starts a timer, and updates status and balance labels.
       /// </summary>
       /// <param name="sender">The `sender` parameter in the `loginBut_Click` method is an object that
       /// represents the control that raised the event. In this case, it would most likely be the
       /// button that was clicked to trigger the login process.</param>
       /// <param name="EventArgs">EventArgs is a base class for classes containing event data. It
       /// represents the arguments that are passed to event handler methods when an event is raised. In
       /// the context of your code snippet, the EventArgs parameter in the loginBut_Click method is
       /// used to handle event data related to the button click event.</param>
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
        /// <summary>
        /// The Timer function updates status, balance, and reputation labels on a form at regular
        /// intervals.
        /// </summary>
        private static void Timer()
        {
            do
            {
                try
                {
                    form.statusLabel.Text = "Logged In:" + form.GUID + "Connections:" + Peers.Count.ToString() + " Height: " + Chain.Count + " Treasury:" + GetTreasury();
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
        /// <summary>
        /// The StartTimer function creates a new thread to run the Timer method.
        /// </summary>
        private void StartTimer()
        {
            Thread th = new Thread(Timer);
            th.Start();
        }

        /// <summary>
        /// The function `AddItem` adds a new item to a ComboBox and updates its model accordingly.
        /// </summary>
        /// <param name="ComboBox">The `ComboBox` parameter in the `AddItem` method represents the
        /// ComboBox widget to which you want to add an item. This method is used to add a new item
        /// (specified by the `st` parameter) to the ComboBox's list of items.</param>
        /// <param name="st">The `st` parameter in the `AddItem` method represents the string value that
        /// you want to add to the ComboBox as a new item. This method adds the string `st` to the
        /// ComboBox's list of items.</param>
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

        /// <summary>
        /// The function creates and signs a transaction, verifies it, and adds it to a list of
        /// transactions.
        /// </summary>
        /// <param name="sender">The `sender` parameter in the `sendBut_Click` method is of type
        /// `object?`, which means it can accept any object or `null`. It typically represents the
        /// object that raised the event, in this case, the button that was clicked to trigger the
        /// event.</param>
        /// <param name="EventArgs">The `EventArgs` parameter in the `sendBut_Click` method is an object
        /// that contains event data specific to the `Click` event. It provides information about the
        /// event that occurred, such as the sender of the event and any additional event-specific data.
        /// In this case, the `EventArgs` parameter</param>
        private void sendBut_Click(object? sender, EventArgs e)
        {
            Block.Transaction tr = new Block.Transaction(Block.Transaction.Type.transaction, GUID, wallet.PublicKey, addressBox.Buffer.Text, (decimal)amountBox.Value);
            tr.SignTransaction(wallet.PrivateKey);
            bool res = VerifyTransaction(tr);

            AddTransaction(tr);
        }

        /// <summary>
        /// The updateBut_Click function updates the balance and reputation labels with values retrieved
        /// using the GetBalance and GetReputation functions.
        /// </summary>
        /// <param name="sender">The `sender` parameter in the `updateBut_Click` method refers to the
        /// object that raised the event. In this case, it would typically be the button that was
        /// clicked to trigger the event.</param>
        /// <param name="EventArgs">The `EventArgs` parameter in the `updateBut_Click` method is an
        /// object that contains information about the event that triggered the click event. It provides
        /// data about the event, such as the source of the event and any additional event-specific
        /// information. In this case, it is used to handle the click</param>
        private void updateBut_Click(object? sender, EventArgs e)
        {
            balanceLabel.Text = "Balance: " + GetBalance(GUID).ToString();
            reputationLabel.Text = "Reputation: " + GetReputation(GUID).ToString();
        }

        /// <summary>
        /// This C# function asynchronously searches for an ORCID using a specified name and updates the
        /// address box with the result.
        /// </summary>
        /// <param name="sender">The `sender` parameter in the `getAddrBut_Click` method is of type
        /// `object?`. It represents the object that raised the event, in this case, the button that was
        /// clicked.</param>
        /// <param name="EventArgs">EventArgs is a base class that provides data for event handlers in
        /// C#. It contains no data but is used as a base class for classes that contain event data. It
        /// is typically used as a parameter in event handler methods to provide information about the
        /// event that occurred.</param>
        private async void getAddrBut_Click(object? sender, EventArgs e)
        {
            addressBox.Buffer.Text = await Orcid.SearchForORCID(sendToNameBox.Text);
        }
    }
}
