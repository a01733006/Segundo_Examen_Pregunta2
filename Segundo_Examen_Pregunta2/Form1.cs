using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace Segundo_Examen_Pregunta2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int dim = int.Parse(textBox1.Text);
            if (dim != 1 && dim != 2)
            {
                MessageBox.Show("La dimension solo puede ser de 1 o 2, por favor intente de nuevo");
                return;
            }

            int pasos = int.Parse(textBox2.Text);
            int rep = int.Parse(textBox3.Text);

            double[] distmax = new double[rep];
            TimeSpan[] tiempos = new TimeSpan[rep];
            Random random = new Random();

            DataTable dt = new DataTable();
            dt.Columns.Add("Iteración", typeof(int));
            dt.Columns.Add("Posición Inicial", typeof(string));
            dt.Columns.Add("Posición Final", typeof(string));
            dt.Columns.Add("Distancia Máxima", typeof(double));
            dt.Columns.Add("Tiempo de Iteración", typeof(TimeSpan));

            for (int i = 0; i < rep; i++)
            {
                Stopwatch stopwatch = Stopwatch.StartNew();

                double[] posicionInicial = new double[dim];
                double[] posicion = new double[dim];
                double maxDistance = 0;

                for (int j = 0; j < pasos; j++)
                {
                    for (int k = 0; k < dim; k++)
                    {
                        posicion[k] += random.Next(2) * 2 - 1;
                    }

                    double dist = Math.Sqrt(posicion.Sum(x => x * x));

                    if (dist > maxDistance)
                    {
                        maxDistance = dist;
                    }
                }

                distmax[i] = maxDistance;

                stopwatch.Stop();
                tiempos[i] = stopwatch.Elapsed;

                string posicionInicialStr = string.Join(", ", posicionInicial.Select(x => x.ToString()));
                string posicionFinalStr = string.Join(", ", posicion.Select(x => x.ToString()));

                dt.Rows.Add(i + 1, posicionInicialStr, posicionFinalStr, distmax[i], tiempos[i]);
            }

            double promediodistmax = distmax.Average();

            TimeSpan promediotiempo = new TimeSpan((long)tiempos.Average(time => time.Ticks));

            label6.Text = "Distancia Máxima Promedio: " + promediodistmax.ToString();

            label7.Text = "Tiempo Promedio: " + promediotiempo.ToString();

            dataGridView1.DataSource = dt;
        }
    }
}
