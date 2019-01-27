using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using BCILib.App;
using BCILib.Util;
using MonoBrick.NXT;

namespace LegoControllerHCI
{
    public partial class openconnection : Form
    {
        public Brick<TouchSensor, Sonar, NXTLightSensor, Sensor> brick; 
        //private sbyte speed = 0;
        public openconnection()
        {
            InitializeComponent();
            InitBCI();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            findOpenCommPorts();
        }

       //BCI stuff added here
        //variables required by the BCI
        public const string ID_Game = "UnityPegMoving";
        private bool copyData;

        //// sendAckToServer check
        private enum AckStage { None, Send, Wait };
        private AckStage SendAckStage = AckStage.None;
        private float timeSinceAck = 0;
        private bool gotAckRespose = true;
        private float timeThreshold = 4f;

        private void InitBCI()
        {
            WMHelper.Register(this.Handle, ID_Game);
            WMHelper.SetRecvCmdHandler(new WMCopyData_Process(wmHelper_OnWMCopyData));
            copyData = false;
        }

        protected void wmHelper_OnWMCopyData(ctrl_msg cmsg)
        {
            copyData = true;
            switch (cmsg.cmd)
            {
                case (int)GameCommand.StartGame:

                    break;
                case (int)GameCommand.StopGame:
                    this.Activate();
                    WMHelper.TimerStop();
                    break;
                case (int)GameCommand.CloseGame:
                    copyData = false; // tricky! this one will call OnClosing
                    this.Close();
                    return;
                case GameCommand.Pause:
                    WMHelper.TimerPause();
                    break;
                case GameCommand.Resume:
                    WMHelper.TimerResume();
                    break;
                case GameCommand.SendCmdString:
                    string[] pvl = cmsg.strdata.Split(',');
                    if (pvl.Length > 0)
                    {
                        string cmd = pvl[0].Trim();
                        switch (cmd)
                        {
                            case "StartGame":
                                //to send back acknowledgement to the client that game can be started
                                break;
                            case "StimCode":
                                if (pvl.Length > 1)
                                {
                                    int stimcode = int.Parse(pvl[1]);
                                    StimCodeTextBox.Text = stimcode.ToString();
                                    trialprogressfunction(StimCodeTextBox.Text);
                                }
                                break;
                            case "PredictClass":
                                if (pvl.Length > 1)
                                {
                                    int pc = int.Parse(pvl[1]);

                                    stimcodeaction(pc); //pc = 121,122,123 etc

                                }
                                break;
                            case "ConfidenceScore":
                                if (pvl.Length > 1)
                                {
                                    double conf = double.Parse(pvl[1]);
                                }
                                break;
                        }
                    }
                    break;
                default:
                    break;
            }
            copyData = false;
            return;
        }

        private void stimcodeaction(int pc)
        {
            int caseSwitch = pc;
            switch (caseSwitch)
            {
                case 121:
                    rotaterobotanticlockwise();
                    break;
                case 122:
                    rotaterobotclockwise();
                    break;
                case 123:
                    moverobotbackward();
                    break;
                case 124:
                    moverobotforward();
                    break;
                default:
                    defaultaction();
                    break;
            }
        }
        
        private static void ReportGameStatistics(string statistics)
        {
            WMHelper.ReportEventMsg(statistics);
            WMHelper.LogStatisticsData(statistics);
        }

        private void ReportGameEvent(string GameMessage)
        {
            WMHelper.ReportEventMsg(GameMessage);
        }

        
        
        //BCI stuff ends here




        //multithreading part
        
        //Experiments related to delegates, threading and UI

        //1. declare a delegate
        private delegate void theFunction();
        private theFunction myDelegate;
        private Thread myThread;

        private delegate void SetTextCallback(string txt);
        private bool Ack = true;

        private void runButton_Click_1(object sender, EventArgs e)
        {



            //MethodInvoker is a delegate that can take functions which are void and no parameters
            //Experiment 1
            //MethodInvoker myProcessStarter = new MethodInvoker(CheckAcknowledgement);
            //myProcessStarter.BeginInvoke(null, null);


            //Experiment 2
            //working

            if (Ack)
            {
                myThread = new Thread(new ThreadStart(CheckAcknowledgement));
                myThread.IsBackground = true;
                myThread.Start();
                Ack = false;
            }
            else
            {
                myThread.Abort();
                Ack = true;
            }


            //Experiment 3
            //myDelegate = new theFunction(CheckAcknowledgement);
            //myDelegate.BeginInvoke(null,null);
        }

        //826, 997
        //2. declare a method for the delegate
        private void CheckAcknowledgement()
        {
            int i = 0;
            while (true)
            {
                i++;
                //trace(i.ToString()); //this is not thread-safe

                //continually read the nxt brick sensor value
                string val = brick.Sensor1.ReadAsString();
                trace(val);
                multithreadaction();

                /*
                if (i > 50000)
                {
                    Ack = false;
                    return;
                }
                 * */
            }
        }

        private void trace(string p)
        {
            if (InvokeRequired)
            {

                SetTextCallback d = new SetTextCallback(trace);
                this.Invoke(d, new object[] { p });
                //do not use BeginInvoke as you will not see the trace output

            }
            else
            {
                this.textBox1.Text = p;
            }

        }


        // e Contains e.Argument and e.Result
        //e.Argument = Used to get the parameter reference received by RunWorkerAsync.
        //e.Result = Check to see what the BackgroundWorker processing did.

        //backgroundWorker1.RunWorkerAsync
        //Called to start a process on the worker thread.

        //http://www.dreamincode.net/forums/topic/112547-using-the-backgroundworker-in-c%23/

        //multithreading part ends


        //Functions
        private void moveMotorB(sbyte spd)
        {
            if (checkBoxMotorBSpeedEnable.Checked == true)
            {
                sbyte sp = (sbyte)motorBSpeed.Value;
                brick.MotorB.On(sp);
            }
            else
            {
                brick.MotorB.On(spd);
            }
        }

        private void moveMotorA(sbyte spd)
        {
            if (checkBoxMotorASpeedEnable.Checked == true)
            {
                sbyte sp = (sbyte) motorASpeed.Value;
                brick.MotorA.On(sp);
            }
            else
            {
                brick.MotorA.On(spd);
            }
        }

        private void moverobotforward()
        {
            //brick.MotorB.On(20);
            //brick.MotorA.On(20);
            moveMotorB(20);
            moveMotorA(20);

            for (int i = 0; i < 12; i++)
            {
                multithreadaction();
                Thread.Sleep(200);
                }
            
      
            brick.MotorA.Brake();
            brick.MotorB.Brake();
        }
        private void rotaterobotclockwise()
        {
            //brick.MotorB.On(20,2*360,true);
            //brick.MotorA.On(-20, 2*360, true);
            moveMotorB(20);
            moveMotorA(-20);
            
            for (int i = 0; i < 17; i++)
            {
                int caseSwitch = brick.Sensor1.Read();
                switch (caseSwitch)
                {
                    case 0:
                        break;
                    case 1:
                        int time = i * 75;
                        //brick.MotorB.On(-20);
                        //brick.MotorA.On(20);
                        moveMotorB(-20);
                        moveMotorA(20);
                        Thread.Sleep(time);
                        brick.MotorA.Brake();
                        brick.MotorB.Brake();
                        break;

                }
                Thread.Sleep(75);
            }
             
                brick.MotorA.Brake();
                brick.MotorB.Brake();
                brick.MotorA.Off();
                brick.MotorB.Off();

            
        }

        private void moverobotbackward()
        {
            //brick.MotorB.On(-20);
            //brick.MotorA.On(-20);
            moveMotorB(-20);
            moveMotorA(-20);
            Thread.Sleep(2400);
            brick.MotorA.Brake();
            brick.MotorB.Brake();
        }
        private void rotaterobotanticlockwise()
        {
            //brick.MotorB.On(-20);
            //brick.MotorA.On(20);
            moveMotorB(-20);
            moveMotorA(20);
            for (int i = 0; i < 17; i++)
            {
                int caseSwitch = brick.Sensor1.Read();
                switch (caseSwitch)
                {
                    case 0:
                        break;
                    case 1:
                        int time = i * 75;
                        //brick.MotorB.On(20);
                        //brick.MotorA.On(-20);
                        moveMotorB(20);
                        moveMotorA(-20);
                        Thread.Sleep(time);
                        brick.MotorA.Brake();
                        brick.MotorB.Brake();
                        break;

                }
                Thread.Sleep(75);
            }
            brick.MotorA.Brake();
            brick.MotorB.Brake();
            brick.MotorA.Off();
            brick.MotorB.Off();
        }
        private void defaultaction(){
            //speed = 0;
            brick.MotorA.Brake();
            brick.MotorB.Brake();
        }
        private void multithreadaction()
        {
             int caseSwitch = brick.Sensor1.Read();
             switch (caseSwitch)
             {
                 case 0:
                     break;
                 case 1:
                     //brick.MotorB.On(-20);
                     //brick.MotorA.On(-20);
                     moveMotorB(-20);
                     moveMotorA(-20);
                     Thread.Sleep(500);
                     brick.MotorA.Brake();
                     brick.MotorB.Brake();
                     break;
             }
        }

        private void trialprogressfunction(string caseSwitch)
        {
            //string caseSwitch = StimCodeTextBox.Text;
            switch (caseSwitch)
            {
                case "":
                    trialprogress.Text = "Trial has not started!";
                    break;
                case "100":
                    trialprogress.Text = "Prepare";
                    break;
                case "121":
                    trialprogress.Text = "Start!";
                    break;
                case "122":
                    trialprogress.Text = "Start!";
                    break;
                case "123":
                    trialprogress.Text = "Start!";
                    break;
                case "124":
                    trialprogress.Text = "Start!";
                    break;
                case "199":
                    trialprogress.Text = "End!";
                    break;
                default:
                    trialprogress.Text = "Weird values!";
                    break;
            }
        }
        //End of functions

        private void buttonleft_MouseClick(object sender, MouseEventArgs e)
        {
            rotaterobotanticlockwise();
        }

        private void buttonup_Click(object sender, EventArgs e)
        {
            moverobotforward();
        }

        private void buttonright_Click(object sender, EventArgs e)
        {
            rotaterobotclockwise();
        }

        private void buttondown_Click(object sender, EventArgs e)
        {
            moverobotbackward();
        }

        private void buttonup_MouseUp(object sender, MouseEventArgs e)
        {

        }

 /*       private void touchsensor_Click(object sender, EventArgs e)
        {
            textBox1.Text = brick.Sensor1.ReadAsString();
             if (brick.Sensor1.Read() == 0)
            {
                brick.MotorA.On(50,1*180);
                brick.MotorB.On(50,1*180);
            }
            else
            {
                brick.MotorA.On(-50,1*180);
                brick.MotorB.On(-50,1*180);
            } 
        }
        */
       /* private void sonarsensor_Click(object sender, EventArgs e)
        {

            int sonarvalue = brick.Sensor2.ReadDistance();
            if (sonarvalue <= 2)
            {
            string sonartextboxtext = brick.Sensor2.ReadAsString();
            sonartextbox.Text = sonartextboxtext; 
                brick.MotorA.On(50,1*180);
                brick.MotorB.On(50,1*180);
            }
            else
            {
                //sonartextbox.Text = brick.Sensor2.ReadAsString();
                brick.MotorA.On(-50,1*360);
                brick.MotorB.On(-50,1*360);
            } 
        } 
        */
       /*private void lightsensor_Click(object sender, EventArgs e)
        {
            int lightvalue = brick.Sensor3.ReadLightLevel();
            sonartextbox.Text = brick.Sensor3.ReadAsString();
            i also switched the motors so remember to switch the codes for light sensor
            int blackvalue = 400;
            sbyte motoraspeed =(sbyte)(blackvalue - lightvalue);
            sbyte sbyte1 = motoraspeed;
            int whitevalue = 650;
            sbyte motorbspeed = (sbyte)(whitevalue - lightvalue);
            brick.MotorA.On(motoraspeed);
            brick.MotorB.On(motorbspeed); 
        } */



        private void GUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (myThread != null)
            {
                if (myThread.IsAlive) myThread.Abort();
            }
            while (copyData)
            {
                Application.DoEvents();
            }

            WMHelper.Deregister();

            closeBrick();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string comPort = cBoxCommPort.Text;

            if (comPort == "")
            {
                MessageBox.Show("Communications Port is not specified");
            }
            else
            {
                initBrick(comPort);
            }
        }

        private void closeBrick()
        {
            if (brick != null && brick.Connection.IsConnected)
            {
                brick.MotorA.Off();
                brick.MotorB.Off();
                brick.Connection.Close();
            }
        }

        private void initBrick(string connectionType)
        {
            if (brick == null) 
            {
                brick = new Brick<TouchSensor, Sonar, NXTLightSensor, Sensor>(connectionType);
                brick.Sensor1 = new TouchSensor();
                brick.Sensor2 = new Sonar();
                brick.Sensor3 = new NXTLightSensor();
            }

            if (brick.Connection.IsConnected)
            {
                MessageBox.Show("already connected");
                return;
            }

            try
            {
                brick.Connection.Open();
                Cursor.Current = Cursors.WaitCursor;
                ushort battLevel = brick.GetBatteryLevel();
                MessageBox.Show("Connected to Brick. Battery Level = " + battLevel.ToString());
                cBoxCommPort.Enabled = false;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message);
            }
            Cursor.Current = Cursors.Default;
        }

        private void comport_Click(object sender, EventArgs e)
        {
           string comportno = comporttextbox.Text;
        }

        private void findOpenCommPorts()
        {
            string[] ports = SerialPort.GetPortNames();
            Array.Sort<String>(ports); //sort the array port names
            List<string> list = new List<string>(ports);
            list.Add("usb");
            cBoxCommPort.DataSource = list;
        }

        private void checkBoxMotorASpeedEnable_CheckedChanged(object sender, EventArgs e)
        {
            motorASpeed.Enabled = checkBoxMotorASpeedEnable.Checked;
        }

        private void checkBoxMotorBSpeedEnable_CheckedChanged(object sender, EventArgs e)
        {
            motorBSpeed.Enabled = checkBoxMotorBSpeedEnable.Checked;
        }
    }  
}
