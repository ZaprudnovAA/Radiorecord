using System;
using System.Windows.Forms;
using WMPLib;

namespace Radio
{
    public partial class Form1 : Form
    {
        private Hook _hook;
        private WindowsMediaPlayer wmPlayer = new WindowsMediaPlayer();

        public Form1()
        {
            InitializeComponent();
            this.Show();
            this.WindowState = FormWindowState.Normal;

            wmPlayer.settings.volume = 80;
            label_volume.Text = "Volume " + wmPlayer.settings.volume.ToString();
            trackBar1.Value = (wmPlayer.settings.volume / 10);

            this.KeyPreview = true;

            _hook = new Hook(0x91); //Scroll Lock
            _hook.KeyPressed += new KeyPressEventHandler(_hook_KeyPressed);
            _hook.SetHook();
        }

        private void _hook_KeyPressed(object sender, KeyPressEventArgs e)
        {
            if (this.Visible)
            {
                if (vars.WhoIsPlaying == 0)
                {
                    button1.PerformClick();
                    vars.WhoIsPlaying = 1;
                }
                else
                    (Controls["button" + vars.WhoIsPlaying.ToString()] as Button).PerformClick();
            }
            else
            {
                if (vars.WhoIsPlaying == 0) vars.WhoIsPlaying = 1;
                StartPlay(vars.WhoIsPlaying);
            }
        }

        private void StartPlay(int id)
        {
            if (id != 25)
            {
                if (vars.WhoIsPlaying != id)
                {
                    int bitrate = 128;
                    for (int i = 1; i <= 3; i++)
                    {
                        if ((Controls["radioButton" + i.ToString()] as RadioButton).Checked)
                            bitrate = Convert.ToInt32((Controls["radioButton" + i.ToString()] as RadioButton).Text);

                        (Controls["radioButton" + i.ToString()] as RadioButton).Enabled = false;
                    }
                    wmPlayer.controls.stop();
                    wmPlayer.URL = vars.radio_url_bitrate(bitrate, id);
                    wmPlayer.controls.play();
                    vars.WhoIsPlaying = id;
                    this.Text = string.Format("{0} - {1} now playing", vars.aName, vars._names[id]);
                }
                else
                    PausePlay(id);
            }
        }

        private void PausePlay(int id)
        {
            if (wmPlayer.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                wmPlayer.controls.pause();
                this.Text = string.Format("{0} - {1} is paused", vars.aName, vars._names[id]);
                for (int i = 1; i <= 3; i++)
                {
                    (Controls["radioButton" + i.ToString()] as RadioButton).Enabled = false;
                }
            }
            else
            {
                wmPlayer.controls.play();
                this.Text = string.Format("{0} - {1} now playing", vars.aName, vars._names[id]);
                for (int i = 1; i <= 3; i++)
                {
                    (Controls["radioButton" + i.ToString()] as RadioButton).Enabled = false;
                }
            }
        }

        private void StartPlayer(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            StartPlay(Convert.ToInt32(btn.TabIndex));
        }

        private void StopPlayer(object sender, EventArgs e)
        {
            wmPlayer.controls.stop();
            this.Text = string.Format("{0}", vars.aName);
            for (int i = 1; i <= 3; i++)
            {
                (Controls["radioButton" + i.ToString()] as RadioButton).Enabled = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Panel panel1 = new Panel() { Dock = DockStyle.Fill, Name = "panel1", BackColor = System.Drawing.Color.Transparent };
            this.Controls.Add(panel1);
            this.Controls["panel1"].SuspendLayout();
            this.Controls["panel1"].ResumeLayout();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                notifyIcon1.Visible = true;
                new Radio.funks.AddNotifyUserDelegate(new Radio.funks().NotifyUser).BeginInvoke("Радио продолжит работать в трее.\n\rСтарт/Стоп проигрывания - Scroll Lock", vars.tNotifInfo, null, null);
            }
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
                MessageBox.Show("Пользоваться программой очень просто!\r\n- Выбрать понравившуюся станцию\r\n- Нажать кнопку проигрывания у выбранной станции\r\n- PROFIT\r\n\r\nГорячие клавиши:\r\nENTER\t- запуск первой станции\r\nESC\t- остановить проигрывание\r\nScrollLock\t- остановить/воспроизвести выбранную станцию", "микроHelp", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label_volume.Text = "Volume " + (trackBar1.Value * 10).ToString();
            wmPlayer.settings.volume = (trackBar1.Value * 10);
        }
    }
}
