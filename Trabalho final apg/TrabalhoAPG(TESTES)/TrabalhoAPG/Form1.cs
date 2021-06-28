using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Threading;
using System.Diagnostics;

namespace TrabalhoAPG
{
    public partial class Form1 : Form
    {
        private Thread GameThread;

        // Cria uma lista de Particulas
        private Conj_Particulas cpart = new Conj_Particulas();

        


        // Desonvolvimento das particulas até serem removidas
        public void Proced()
        {

            try
            {
                for (; ; )
                {
                    // Delay de 50 Milisegundos
                    Thread.Sleep(50);

                    // Cria uma lista para as particulas removidas
                    List<Particula> removeParticulas = new List<Particula>();

                    // 
                    lock (cpart.Particulas)
                    {
                        // Para cada Particula na Lista de Particulas
                        foreach (Particula p in cpart.Particulas)
                        {
                            // Verifica se a particula em questao ainda está 'viva'
                            if (p.PerformFrame(cpart))
                            {
                                // Adiciona as particulas removidas à lista
                                removeParticulas.Add(p);
                            }



                        }
                    }
                    // Para cada particula na lista das Particulas Removidas
                    foreach (var part_remove in removeParticulas)
                    {
                        // Remove a Particula da Lista de Particulas
                        cpart.Particulas.Remove(part_remove);

                    }

                    // Faz com que a pictureBox seja atualizada
                    Invoke((MethodInvoker)(() =>
                    {
                        pictureBox1.Invalidate();
                        pictureBox1.Update();
                    }));


                }
            }
            catch {
                
            }

        }

        public Form1()
        {
            InitializeComponent();
            pictureBox1.Cursor = new Cursor(GetType(), "scope.cur");
            
        }

        public void Form1_Load(object sender, EventArgs e)
        {
            // Cria e inicia um Thread para a função GameProc
            GameThread = new Thread(Proced);
            GameThread.Start();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            // Desenha as Particulas no ecrã
            foreach (Particula p in cpart.Particulas)
            {
                p.Draw(e.Graphics);
            }
        }



        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            // Guarda a posição do click do rato
            PointF clicklocation = new PointF(e.X,e.Y);

            Random rnd1 = new Random();

            // Numero Aleatorio de Particulas que vão ser geradas no click
            int NrParticulas = rnd1.Next(10, 30);

            // Como podemos ter vários cliques no memso intervalo de tempo,
            // Lock permite que o código só avança quando acabar de fazer o que está estava a fazer
            lock (cpart.Particulas)
            {
                for (int i = 0; i < NrParticulas; i++)
                {
                    // Cria as particulas no clique do rato, com uma velocidade aleatoria entre 1 e 5
                    Particula createparticle = new Particula(clicklocation, rnd1.Next(1, 5));
                    // Coloca as particulas numa lista de Particulas
                    cpart.Particulas.Add(createparticle);
                }
           }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Fecha o Thread
            GameThread.Abort();
            GameThread = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
