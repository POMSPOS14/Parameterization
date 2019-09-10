using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kursovaya2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //Инициализация значений
        double lam = Math.Pow(10, 2);
        double k1, k2, k3, k4, k5, m1, m2, m3, m4, m5,t;//для методов  
        double h, eps;//шаг и точность
        double u0 = 1, x0 = 0;// начальные значения
        double u, x1, sig, u2, x2, sig2,x,y;// konechnie znacheniya
        double gr1, gr2;//доп переменные для отрисовки 
        double ur1, ur2;//доп переменные для проверки 

      

        double[] mass = new double[2];//вспомогательный массив для функции step2
        double[] massMerson = new double[2];//вспомогательный массив для функции step2
        double[] masstest = new double[4];//вспомогательный массив для функции StepTestMerson
        double temp, bigStep, smallStep, big2Step, superStep;

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

       
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        #region функции
        double f1(double u) //начальная функция
        {
            return -lam * u;
        }
        double f2(double u)
        {
            return -lam * u / Math.Sqrt(1 + lam * lam * u * u);
        }//u для метода DD
        double f3(double u)
        {
            return 1 / Math.Sqrt(1 + lam * lam * u * u);
        }//t для метода DD
        double ftest1(double x,double y)
        {
            return -2*x+4*y;
        }
        double ftest2(double x, double y)
        {
            return -x+3*y;
        }
        double ff(double t)
        {
            return Math.Exp(-lam * t);
        }
        #endregion

        #region методы
        double Step(double u0, double x0, double h)
        {
            x1 = x0 + h;
            k1 = h * f1(u0);
            k2 = h * f1(u0 + k1 / 2);
            k3 = h * f1(u0 + k2 / 2);
            k4 = h * f1(u0 + k3);
            u = u0 + (k1 + 2 * k2 + 2 * k3 + k4) / 6;
            return u;
        }

        double[] Step2(double u0, double x0, double t0, double h)
        {

            x1 = x0 + h;
            m1 = h * f2(u0);
            k1 = h * f3(u0);
            m2 = h * f2(u0 + m1 / 2);
            k2 = h * f3(u0 + k1 / 2);
            m3 = h * f2(u0 + m2 / 2);
            k3 = h * f3(u0 + k2 / 2);
            m4 = h * f2(u0 + m3);
            k4 = h * f3(u0 + k3);
            u = u0 + (m1 + 2 * m2 + 2 * m3 + m4) / 6;
            t = t0 + (k1 + 2 * k2 + 2 * k3 + k4) / 6;
            mass[0] = u;
            mass[1] = t;
            return mass;
        }

        double[] StepTestMerson(double u0, double x0, double h)
        {
            double sigM1, sigM2;

            x1 = x0 + h;
            k1 = 1 / 3.0 * h * ftest1(x0, u0);
            k2 = 1 / 3.0 * h * ftest1(x0 + 1 / 3.0 * h, u0 + k1);
            k3 = 1 / 3.0 * h * ftest1(x0 + 1 / 3.0 * h, u0 + 1 / 2.0 * k1 + 1 / 2.0 * k2);
            k4 = 1 / 3.0 * h * ftest1(x0 + 1 / 2.0 * h, u0 + 3 / 8.0 * k1 + 9 / 8.0 * k3);
            k5 = 1 / 3.0 * h * ftest1(x0 + h, u0 + 3 / 2.0 * k1 - 9 / 2.0 * k3 + 6 * k4);
            sigM1 = k1 - 9 / 2.0 * k3 + 4 * k4 - 1 / 2.0 * k5;

            m1 = 1 / 3.0 * h * ftest2(x0, u0);
            m2 = 1 / 3.0 * h * ftest2(x0 + 1 / 3.0 * h, u0 + m1);
            m3 = 1 / 3.0 * h * ftest2(x0 + 1 / 3.0 * h, u0 + 1 / 2.0 * m1 + 1 / 2.0 * m2);
            m4 = 1 / 3.0 * h * ftest2(x0 + 1 / 2.0 * h, u0 + 3 / 8.0 * m1 + 9 / 8.0 * m3);
            m5 = 1 / 3.0 * h * ftest2(x0 + h, u0 + 3 / 2.0 * m1 - 9 / 2.0 * m3 + 6 * m4);
            sigM2 = m1 - 9 / 2.0 * m3 + 4 * m4 - 1 / 2.0 * m5;

            x = x0 + (1 / 2.0 * (k1 + 4 * k4 + k5));
            u = u0 + (1 / 2.0 * (m1 + 4 * m4 + m5));
            
            

            masstest[0] = x;
            masstest[1] = u;
            masstest[2] = sigM1;
            masstest[3] = sigM2;


            return masstest;
        }

        double[] StepMersonDD(double u0, double x0, double t0, double h)
        {
            x1 = x0 + h;
            k1 = 1 / 3.0 * h * f2(u0);
            k2 = 1 / 3.0 * h * f2(u0 + k1);
            k3 = 1 / 3.0 * h * f2(u0 + 1 / 2.0 * k1 + 1 / 2.0 * k2);
            k4 = 1 / 3.0 * h * f2(u0 + 3 / 8.0 * k1 + 9 / 8.0 * k3);
            k5 = 1 / 3.0 * h * f2(u0 + 3 / 2.0 * k1 - 9 / 2.0 * k3 + 6 * k4);


            m1 = 1 / 3.0 * h * f3(u0);
            m2 = 1 / 3.0 * h * f3(u0 + m1);
            m3 = 1 / 3.0 * h * f3(u0 + 1 / 2.0 * m1 + 1 / 2.0 * m2);
            m4 = 1 / 3.0 * h * f3(u0 + 3 / 8.0 * m1 + 9 / 8.0 * m3);
            m5 = 1 / 3.0 * h * f3(u0 + 3 / 2.0 * m1 - 9 / 2.0 * m3 + 6 * m4);
            

            u = u0 + (1 / 2.0 * (k1 + 4 * k4 + k5));
            t = t0 + (1 / 2.0 * (m1 + 4 * m4 + m5));


            massMerson[0] = u;
            massMerson[1] = t;

            return massMerson;
        }

        double MersonTest(double y, double x, double h)
        {
            double htemp1, htemp2, t = 0, realx, realy;
            double absx, absy, mersonx, mersony;
            do
            {
                htemp1 = StepTestMerson(y, x, h)[0];//x
                htemp2 = StepTestMerson(y, x, h)[1];//y

                x = htemp1;
                y = htemp2;
                
                t += h;

                realx = 4 * Math.Exp(-t) - Math.Exp(2 * t);
                realy = Math.Exp(-t) - Math.Exp(2 * t);

                absx = Math.Abs(x - realx);
                absy = Math.Abs(y - realy);
                mersonx = StepTestMerson(y, x, h)[2];
                mersony = StepTestMerson(y, x, h)[3];

                dataGridView5.Rows.Add(x,y,realx,realy,absx,absy,mersonx,mersony);
                chart5.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
                chart5.Series[0].Color = Color.Green;
                chart5.Series[0].BorderWidth = 3;// толщина линии
                chart5.Series[0].Points.AddXY(x, y);

            } while (t < 1);
            return 1;
        }

        double Merson(double u0, double x0, double h, double eps)
        {
            int i = 0;
            do
            {
                i++;
                x1 = x0 + h;
                k1 = 1 / 3.0 * h * f1(u0);
                k2 = 1 / 3.0 * h * f1(u0 + k1);
                k3 = 1 / 3.0 * h * f1(u0 + 1 / 2.0 * k1 + 1 / 2.0 * k2);
                k4 = 1 / 3.0 * h * f1(u0 + 3 / 8.0 * k1 + 9 / 8.0 * k3);
                k5 = 1 / 3.0 * h * f1(u0 + 3 / 2.0 * k1 - 9 / 2.0 * k3 + 6 * k4);
                sig = Math.Abs(k1 - 9 / 2.0 * k3 + 4 * k4 - 1 / 2.0 * k5)/5;
                u = u0 + (1 / 2.0 * (k1 + 4 * k4 + k5));
                if (sig >  eps)
                {
                    h = h / 2;
                    continue;
                }
                if (sig <  eps/32)
                {
                    h = h * 2;
                }
                u0 = u;
                x0 = x1;

                dataGridView1.Rows.Add(x1, u, h,sig);
                chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
                chart1.Series[0].Color = Color.Green;
                chart1.Series[0].BorderWidth = 3;// толщина линии
                chart1.Series[0].Points.AddXY(x1, u);

            } while (x1 < 1);
            label4.Text = Convert.ToString(i*5);
            return 1;
        }

        double Runge2(double u0, double x0, double h, double eps)
        {
            int i = 0;
            do
            {
                i++;
                bigStep = Step(u0, x0, h);
                temp = Step(u0, x0, h / 2);
                smallStep = Step(temp, x0 + h / 2, h / 2);
                sig2 = Math.Abs(bigStep - smallStep);

                if (sig2 > eps)
                {
                    h = h / 2;
                    continue;
                }

                big2Step = Step(bigStep, x0 + h, h);
                superStep= Step(u0, x0, 2*h);

                if (Math.Abs(big2Step-superStep)<eps/2)
                {
                    h = h * 2;
                    continue;
                }
                
                u0 = bigStep;
                x0 +=h;

                dataGridView3.Rows.Add(x0, u0, h, sig2);
                chart2.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
                chart2.Series[0].Color = Color.Green;
                chart2.Series[0].BorderWidth = 3;// толщина линии
                chart2.Series[0].Points.AddXY(x0, u0);


            } while (x1 < 1);
            label5.Text = Convert.ToString(i * 4 * 3);
            return 1;
        }

        double RungeDD(double u0, double x0, double h, double eps)
        {
            int i = 0;
            double htemp1,htemp2,smallStepDD1, smallStepDD2,sigRDD=0, bigStep1, bigStep2, big2Step;
            double t0 = 0;
            do
            {
                i++;
                bigStep1 = Step2(u0, x0, t0, h)[0];//u
                bigStep2 = Step2(u0, x0, t0, h)[1];//t

                htemp1 = Step2(u0, x0, t0, h / 2)[0];
                smallStepDD1 = Step2(htemp1, x0 + h / 2, t0, h / 2)[0];//u

                htemp2 = Step2(u0, x0, t0, h / 2)[1];
                smallStepDD2 = Step2(htemp2, x0 + h / 2, t0, h / 2)[1];//t

                //sigRDD += (Math.Pow((bigStep1 - smallStepDD1), 2) / Math.Pow(smallStepDD1, 2));
                sig2 = Math.Abs(bigStep1 - smallStepDD1);
                sigRDD = sig2;
                if (sig2 > eps)
                {
                    h = h / 2;
                    continue;
                }

                big2Step = Step2(bigStep1, x0 + h,t0, h)[0];
                superStep = Step2(u0, x0,t0, 2 * h)[0];
                
                 if (Math.Abs(big2Step - superStep) < eps / 2)
                //if (sig2 < (eps / 15))
                {
                    h = h * 2;
                    continue;
                }


                u0 = bigStep1;
                t0 = bigStep2;
                x0 += h;

                dataGridView2.Rows.Add(t0,u0,h,sig2);
                chart3.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
                chart3.Series[0].Color = Color.Green;
                chart3.Series[0].BorderWidth = 3;// толщина линии
                chart3.Series[0].Points.AddXY(t0, u0);
               // chart3.Series[0].Points.AddXY(smallStepDD2, smallStepDD1);

            } while (t0 < 1);
            label6.Text = Convert.ToString(i*8*3);
            return 1;
        }

        double MersonDD(double u0, double x0, double h, double eps)
        {
            int i = 0;
            double htemp1, htemp2, smallStepDD1, smallStepDD2, sigRDD = 0, bigStep1, bigStep2, big2Step,abspogr;
            double t0 = 0;
            do
            {
                i++;
                bigStep1 = StepMersonDD(u0, x0, t0, h)[0];//u
                bigStep2 = StepMersonDD(u0, x0, t0, h)[1];//t

                htemp1 = StepMersonDD(u0, x0, t0, h / 2)[0];
                smallStepDD1 = StepMersonDD(htemp1, x0 + h / 2, t0, h / 2)[0];//u

                htemp2 = StepMersonDD(u0, x0, t0, h / 2)[1];
                smallStepDD2 = StepMersonDD(htemp2, x0 + h / 2, t0, h / 2)[1];//t

                //sigRDD += (Math.Pow((bigStep1 - smallStepDD1), 2) / Math.Pow(smallStepDD1, 2));
                sig2 = Math.Abs(bigStep1 - smallStepDD1);

                if (sig2 > eps)
                {
                    h = h / 2;
                    continue;
                }

                big2Step = StepMersonDD(bigStep1, x0 + h, t0, h)[0];
                superStep = StepMersonDD(u0, x0, t0, 2 * h)[0];

                if (Math.Abs(big2Step - superStep) < eps / 2)
                {
                    h = h * 2;
                    continue;
                }

               
                u0 = bigStep1;
                t0 = bigStep2;
                x0 += h;
                abspogr = (Math.Exp(-lam * t0)-u0);
                dataGridView4.Rows.Add(t0, u0, h, sig2);
                chart4.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
                chart4.Series[0].Color = Color.Green;
                chart4.Series[0].BorderWidth = 3;// толщина линии
                chart4.Series[0].Points.AddXY(t0, u0);
                // chart3.Series[0].Points.AddXY(smallStepDD2, smallStepDD1);

            } while (t0 < 1);
            label7.Text = Convert.ToString(i*10);
            return 1;
        }
        #endregion

        #region графики шаг от времени
        void ShagOtVremeniMersonDD()
        {
            foreach (DataGridViewRow row in this.dataGridView4.Rows)
            {
               
                chart6.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
                chart6.Series[0].Color = Color.Green;
                chart6.Series[0].BorderWidth = 3;// толщина линии
                if (Convert.ToDouble(row.Cells[2].Value)!=0)
                {
                    chart6.Series[0].Points.AddXY(Convert.ToDouble(row.Cells[0].Value), Convert.ToDouble(row.Cells[2].Value));
                }                                
            }

        }
        void ShagOtVremeniMerson()
        {
            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                
                chart7.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
                chart7.Series[0].Color = Color.Green;
                chart7.Series[0].BorderWidth = 3;// толщина линии
                if (Convert.ToDouble(row.Cells[2].Value) != 0)
                {
                    chart7.Series[0].Points.AddXY(Convert.ToDouble(row.Cells[0].Value), Convert.ToDouble(row.Cells[2].Value));
                }
            }

        }
        void ShagOtVremeniRunge()
        {
            foreach (DataGridViewRow row in this.dataGridView3.Rows)
            {
                dataGridView5.Rows.Add(Convert.ToDouble(row.Cells[0].Value), row.Cells[2].Value);
                chart8.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
                chart8.Series[0].Color = Color.Green;
                chart8.Series[0].BorderWidth = 3;// толщина линии
                if (Convert.ToDouble(row.Cells[2].Value) != 0)
                {
                    chart8.Series[0].Points.AddXY(Convert.ToDouble(row.Cells[0].Value), Convert.ToDouble(row.Cells[2].Value));
                }
            }

        }
        void ShagOtVremeniRungeDD()
        {
            foreach (DataGridViewRow row in this.dataGridView2.Rows)
            {
                dataGridView5.Rows.Add(Convert.ToDouble(row.Cells[0].Value), row.Cells[2].Value);
                chart9.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
                chart9.Series[0].Color = Color.Green;
                chart9.Series[0].BorderWidth = 3;// толщина линии
                if (Convert.ToDouble(row.Cells[2].Value) != 0)
                {
                    chart9.Series[0].Points.AddXY(Convert.ToDouble(row.Cells[0].Value), Convert.ToDouble(row.Cells[2].Value));
                }
            }

        }
        #endregion

        void PogrOtVremeniMersonDD()
        {
            double t, sig, u, abspog ;
            
            foreach (DataGridViewRow row in this.dataGridView4.Rows)
            {
                t = Convert.ToDouble(row.Cells[0].Value);
                sig = Convert.ToDouble(row.Cells[3].Value);
                u = Convert.ToDouble(row.Cells[1].Value);
                abspog = Math.Abs(u-ff(t));
                chart10.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
                chart10.Series[0].Color = Color.Green;
                chart10.Series[0].BorderWidth = 4;// толщина линии
                if (Convert.ToDouble(row.Cells[3].Value) != 0)
                {
                    chart10.Series[0].Points.AddXY(t, sig);
                }
                chart10.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
                chart10.Series[1].Color = Color.Red;
                chart10.Series[1].BorderWidth = 1;// толщина линии
                if (Convert.ToDouble(row.Cells[3].Value) != 0)
                {
                    chart10.Series[1].Points.AddXY(t, abspog);
                }
            }

        }

        void PogrOtVremeniRungeDD()
        {
            double t, sig, u, abspog;

            foreach (DataGridViewRow row in this.dataGridView2.Rows)
            {
                t = Convert.ToDouble(row.Cells[0].Value);
                sig = Convert.ToDouble(row.Cells[3].Value);
                u = Convert.ToDouble(row.Cells[1].Value);
                abspog = Math.Abs(u - ff(t));
                chart11.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
                chart11.Series[0].Color = Color.Green;
                chart11.Series[0].BorderWidth = 4;// толщина линии
                if (Convert.ToDouble(row.Cells[3].Value) != 0)
                {
                    chart11.Series[0].Points.AddXY(t, sig);
                }
                chart11.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
                chart11.Series[1].Color = Color.Red;
                chart11.Series[1].BorderWidth = 1;// толщина линии
                if (Convert.ToDouble(row.Cells[3].Value) != 0)
                {
                    chart11.Series[1].Points.AddXY(t, abspog);
                }
            }

        }
        void PogrOtVremeniRunge()
        {
            double t, sig, u, abspog;

            foreach (DataGridViewRow row in this.dataGridView3.Rows)
            {
                t = Convert.ToDouble(row.Cells[0].Value);
                sig = Convert.ToDouble(row.Cells[3].Value);
                u = Convert.ToDouble(row.Cells[1].Value);
                abspog = Math.Abs(u - ff(t));
                chart12.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
                chart12.Series[0].Color = Color.Green;
                chart12.Series[0].BorderWidth = 4;// толщина линии
                if (Convert.ToDouble(row.Cells[3].Value) != 0)
                {
                    chart12.Series[0].Points.AddXY(t, sig);
                }
                chart12.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
                chart12.Series[1].Color = Color.Red;
                chart12.Series[1].BorderWidth = 1;// толщина линии
                if (Convert.ToDouble(row.Cells[3].Value) != 0)
                {
                    chart12.Series[1].Points.AddXY(t, abspog);
                }
            }

        }

        void PogrOtVremeniMerson()
        {
            double t, sig, u, abspog;

            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                t = Convert.ToDouble(row.Cells[0].Value);
                sig = Convert.ToDouble(row.Cells[3].Value);
                u = Convert.ToDouble(row.Cells[1].Value);
                abspog = Math.Abs(u - ff(t));
                chart13.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
                chart13.Series[0].Color = Color.Green;
                chart13.Series[0].BorderWidth = 4;// толщина линии
                if (Convert.ToDouble(row.Cells[3].Value) != 0)
                {
                    chart13.Series[0].Points.AddXY(t, sig);
                }
                chart13.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
                chart13.Series[1].Color = Color.Red;
                chart13.Series[1].BorderWidth = 1;// толщина линии
                if (Convert.ToDouble(row.Cells[3].Value) != 0)
                {
                    chart13.Series[1].Points.AddXY(t, abspog);
                }
            }

        }

        
        private void button1_Click(object sender, EventArgs e)
        {
            #region Добавление серий для графиков
            chart1.Series.Clear();
            chart1.Series.Add("");//серия для 1 графика
            chart2.Series.Clear();
            chart2.Series.Add("");//серия для 2 графика
            chart3.Series.Clear();
            chart3.Series.Add("");
            chart4.Series.Clear();
            chart4.Series.Add("");
            chart5.Series.Clear();
            chart5.Series.Add("");
            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();
            dataGridView3.Rows.Clear();
            dataGridView4.Rows.Clear();
            dataGridView5.Rows.Clear();
            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "{0.0}";
            chart2.ChartAreas[0].AxisX.LabelStyle.Format = "{0.0}";
            chart3.ChartAreas[0].AxisX.LabelStyle.Format = "{0.0}";
            chart4.ChartAreas[0].AxisX.LabelStyle.Format = "{0.0}";
            chart5.ChartAreas[0].AxisX.LabelStyle.Format = "{0.0}";
            chart6.Series.Clear();
            chart6.Series.Add("");
            chart6.ChartAreas[0].AxisX.LabelStyle.Format = "{0.0}";
            chart7.Series.Clear();
            chart7.Series.Add("");
            chart7.ChartAreas[0].AxisX.LabelStyle.Format = "{0.0}";
            chart8.Series.Clear();
            chart8.Series.Add("");
            chart8.ChartAreas[0].AxisX.LabelStyle.Format = "{0.0}";
            chart9.Series.Clear();
            chart9.Series.Add("");
            chart9.ChartAreas[0].AxisX.LabelStyle.Format = "{0.0}";
            chart10.Series.Clear();
            chart10.Series.Add("");
            chart10.Series.Add("");
            chart10.ChartAreas[0].AxisX.LabelStyle.Format = "{0.0}";
            chart11.Series.Clear();
            chart11.Series.Add("");
            chart11.Series.Add("");
            chart11.ChartAreas[0].AxisX.LabelStyle.Format = "{0.0}";
            chart12.Series.Clear();
            chart12.Series.Add("");
            chart12.Series.Add("");
            chart12.ChartAreas[0].AxisX.LabelStyle.Format = "{0.0}";
            chart13.Series.Clear();
            chart13.Series.Add("");
            chart13.Series.Add("");
            chart13.ChartAreas[0].AxisX.LabelStyle.Format = "{0.0}";
            #endregion

            
            h = Convert.ToDouble(textBox1.Text);
            eps = Convert.ToDouble(textBox2.Text);
            lam = Math.Pow(10, Convert.ToDouble(textBox3.Text));

            chart10.ChartAreas[0].AxisY.Maximum = eps + eps / 2;
            chart11.ChartAreas[0].AxisY.Maximum = eps + eps / 2;

            Merson(u0, x0, h, eps);
            Runge2(u0, x0, h, eps);
            RungeDD(u0, x0, h, eps);
            MersonDD(u0, x0, h, eps);

            ShagOtVremeniMersonDD();
            ShagOtVremeniMerson();
            ShagOtVremeniRunge();
            ShagOtVremeniRungeDD();

            PogrOtVremeniMersonDD();
            PogrOtVremeniRungeDD();
            PogrOtVremeniRunge();
            PogrOtVremeniMerson();

           // MersonTest(0, 3, h);
        }
    }
}
