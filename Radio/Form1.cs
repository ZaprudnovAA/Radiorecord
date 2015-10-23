using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WMPLib;

namespace Radio
{
    public partial class Form1 : Form
    {
        private Hook _hook;

        [DllImport("user32.dll")]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);

        public Form1()
        {
            InitializeComponent();
            this.Show();
            this.WindowState = FormWindowState.Normal;

            wmPlayer.settings.volume = 80;
            label_volume.Text = "Volume " + wmPlayer.settings.volume.ToString();
            trackBar1.Value = (wmPlayer.settings.volume / 10);

            this.KeyPreview = true;

            //keybd_event(0x7a, 0x45, 0x1, (UIntPtr)0); //F11
            keybd_event(0x91, 0x45, 0x1, (UIntPtr)0); //Scroll Lock

            //_hook = new Hook(0x7a); //F11
            _hook = new Hook(0x91); //Scroll Lock
            _hook.KeyPressed += new KeyPressEventHandler(_hook_KeyPressed);
            _hook.SetHook();
        }
        void _hook_KeyPressed(object sender, KeyPressEventArgs e) //Событие нажатия клавиш
        {
            if (this.Visible)
            {
                if (vars.WhoIsPlaying == 0)
                {
                    button1.PerformClick();
                    vars.WhoIsPlaying = 1;
                }
                else
                {
                    (Controls["button" + vars.WhoIsPlaying.ToString()] as Button).PerformClick();
                }
            }
            else
            {
                if (vars.WhoIsPlaying == 0) vars.WhoIsPlaying = 1;
                vars.WhoIsPlaying_str = vars.WhoIsPlaying_str == "Play" ? "Stop" : "Play";
                StartPlay(vars.WhoIsPlaying, vars.WhoIsPlaying_str);
            }
        }

        WindowsMediaPlayer wmPlayer = new WindowsMediaPlayer();

        private void StartPlay(int id, string pp)
        {
            if (id != 2)
            {
                if (pp == "Stop")
                {
                    int bitrate = 128;
                    for (int i = 1; i <= 3; i++)
                    {
                        if ((Controls["radioButton" + i.ToString()] as RadioButton).Checked)
                            bitrate = Convert.ToInt32((Controls["radioButton" + i.ToString()] as RadioButton).Text);

                        (Controls["radioButton" + i.ToString()] as RadioButton).Enabled = false;
                    }

                    wmPlayer.controls.stop();
                    wmPlayer.URL = @vars.radio_url_bitrate(bitrate, id);
                    wmPlayer.controls.play();
                    ChangeButtonText(id);
                    vars.WhoIsPlaying = id;
                }
                else
                {
                    wmPlayer.controls.stop();
                    for (int i = 1; i <= 3; i++)
                    {
                        (Controls["radioButton" + i.ToString()] as RadioButton).Enabled = true;
                    }
                }
            }
        }
        private void PausePlay(int id)
        {
            wmPlayer.controls.pause();
        }

        private void ChangeButtonText(int id = 0)
        {
            if (id != 0)
            {
                for (int i = 1; i <= vars.radio_label.Length; i++)
                {
                    if (i != 2)
                    {
                        (Controls["button" + i.ToString()] as Button).Text = i != id ? "Play" : "Stop";
                    }
                }
            }
            else
            {
                for (int i = 1; i <= vars.radio_label.Length; i++)
                {
                    if (i != 2)
                        (Controls["button" + i.ToString()] as Button).Text = "Play";
                }
            }

        }

        private void StartPlayer(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.Text = btn.Text == "Play" ? "Stop" : "Play";

            StartPlay(Convert.ToInt32(btn.TabIndex), btn.Text);

            /*if (btn.Name == "button1") StartPlay(1, btn.Text);
            if (btn.Name == "button3") StartPlay(3, btn.Text);
            if (btn.Name == "button4") StartPlay(4, btn.Text);
            if (btn.Name == "button5") StartPlay(5, btn.Text);
            if (btn.Name == "button6") StartPlay(6, btn.Text);
            if (btn.Name == "button7") StartPlay(7, btn.Text);
            if (btn.Name == "button8") StartPlay(8, btn.Text);
            if (btn.Name == "button9") StartPlay(9, btn.Text);
            if (btn.Name == "button10") StartPlay(10, btn.Text);
            if (btn.Name == "button11") StartPlay(11, btn.Text);
            if (btn.Name == "button12") StartPlay(12, btn.Text);
            if (btn.Name == "button13") StartPlay(13, btn.Text);
            if (btn.Name == "button14") StartPlay(14, btn.Text);
            if (btn.Name == "button15") StartPlay(15, btn.Text);
            if (btn.Name == "button16") StartPlay(16, btn.Text);
            if (btn.Name == "button17") StartPlay(17, btn.Text);
            if (btn.Name == "button18") StartPlay(18, btn.Text);
            if (btn.Name == "button19") StartPlay(19, btn.Text);
            if (btn.Name == "button20") StartPlay(20, btn.Text);
            if (btn.Name == "button21") StartPlay(21, btn.Text);*/
        }

        private void StopPlayer(object sender, EventArgs e)
        {
            wmPlayer.controls.stop();
            ChangeButtonText();
            for (int i = 1; i <= 3; i++)
            {
                (Controls["radioButton" + i.ToString()] as RadioButton).Enabled = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Panel panel1 = new Panel() { Dock = DockStyle.Fill, Name = "panel1" };
            this.Controls.Add(panel1);

            Label[] labels = new Label[vars.radio_label.Length - 1];
            for (int i = 0, j = 16; i < labels.Length; i++)
            {
                if (i == 0)
                    labels[i] = new Label() { Text = vars.radio_label[i], Name = "label " + i.ToString(), Location = new Point(13, j), Size = new Size(200, 23) };
                else
                    labels[i] = new Label() { Text = vars.radio_label[i], Name = "label " + i.ToString(), Location = new Point(13, i * 28 + j), Size = new Size(200, 23) };
            }
            this.Controls["panel1"].SuspendLayout();
            this.Controls["panel1"].Controls.AddRange(labels);
            this.Controls["panel1"].ResumeLayout();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                notifyIcon1.Visible = true;
                new Radio.funks.AddNotifyUserDelegate(new Radio.funks().NotifyUser).BeginInvoke("Радио продолжит работать в трее.\n\rСтарт/Стоп проигрывания - Scroll Lock", new vars().tNotifInfo, null, null);
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
            {
                MessageBox.Show("Пользоваться программой очень просто!\r\n- Выбрать понравившуюся станцию\r\n- Нажать кнопку проигрывания у выбранной станции\r\n- PROFIT\r\n\r\nГорячие клавиши:\r\nENTER\t- запуск первой станции\r\nESC\t- остановить проигрывание\r\nScrollLock\t- остановить/воспроизвести выбранную станцию", "микроHelp", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //button1.PerformClick();// имитируем нажатие button1
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label_volume.Text = "Volume " + (trackBar1.Value * 10).ToString();
            wmPlayer.settings.volume = (trackBar1.Value * 10);
        }

    }

}
