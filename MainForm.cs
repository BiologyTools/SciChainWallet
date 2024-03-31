using static SciChain.Orcid;
using static SciChain.Blockchain;
using NetCoreServer;
using System.Transactions;
using Newtonsoft.Json;
using static SciChain.Block;
using System.Text;
using System.Security.Cryptography;
namespace SciChain
{
    public partial class MainForm : Form
    {
        private StringWriter _writer;
        private Blockchain.Wallet wallet;
        string token;
        public string peer = "92.205.238.105";
        public string GUID;
        public MainForm()
        {
            InitializeComponent();
        }
        public string CalculateHash(string st)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(st));
                return BitConverter.ToString(bytes).Replace("-", "").ToLowerInvariant();
            }
        }
        private async void loginBut_Click(object sender, EventArgs e)
        {
            _writer = new StringWriter();
            Console.SetOut(_writer);
            wallet = new Blockchain.Wallet();
            wallet.Load(maskedTextBox.Text);
            string st = RSA.RSAParametersToStringAll(wallet.PrivateKey);
            GUID = CalculateHash(st);
            Initialize(wallet);
            ChatClient cl = new ChatClient(peer, 8333);
            cl.ConnectAsync();
            ConnectToPeer(peer, cl, 8333);
            Blockchain.Load();
            timer.Start();
            toolStripStatusLabel.Text = "Logged In:" + GUID;
            balanceLabel.Text = "Balance: " + GetBalance(GUID).ToString();
            sendBut.Enabled = true;
            updateBut.Enabled = true;
            getAddrBut.Enabled = true;
            GetPending(Peers.First().Value, PendingBlocks.Count);
        }

        private void sendBut_Click(object sender, EventArgs e)
        {
            Block.Transaction tr = new Block.Transaction(Block.Transaction.Type.transaction, GUID, wallet.PublicKey, addrBox.Text, amountBox.Value);
            tr.SignTransaction(wallet.PrivateKey);
            AddTransaction(tr);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Save();
            wallet.Save(maskedTextBox.Text);
        }

        private void updateBut_Click(object sender, EventArgs e)
        {
            balanceLabel.Text = "Balance: " + GetBalance(GUID).ToString();
        }

        private async void getAddrBut_Click(object sender, EventArgs e)
        {
            addrBox.Text = await Orcid.SearchForORCID(sendToNameBox.Text);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "Connections:" + Peers.Count.ToString() + " Height: " + Chain.Count + " Treasury:" + GetTreasury();
            updateBut.PerformClick();
            textBox1.Text = _writer.ToString();
            textBox1.SelectionStart = textBox1.Text.Length;
            textBox1.ScrollToCaret();
        }
       
        private void toolStripStatusLabel_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(GUID);
        }

    }
}
