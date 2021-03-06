using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TrabalhoAPG
{
    class Particula
    {
        // Cor da Particula
        private Color imageicula = Color.Red;
        // Tempo de Vida Esperado Aleatório entre 3 e 8 segundos
        TimeSpan TempoVida = new TimeSpan(0, 0, 0, rg.Next(2, 8));
        // Data de Nascimento de uma particula
        DateTime? DataNasc;


        

            
            

        // Posição
        private PointF _Location;
        public PointF Location { get { return _Location; } set { _Location = value; } }

        // Velocidade
        private PointF _Velocity;
        public PointF Velocity { get { return _Velocity; } set { _Velocity = value; } }

        public static Random rg = new Random();

        // Calcula o angulo em que sai do ponto (clique do rato)
        public static PointF RandomVector(float speed)
        {
            // Calcula um angulo aleatorio para a particula sair
            // Sendo Pi*2 correspondente a 360 graus, multiplicamos um valor aleatorio de 0 a 1 para obter um angulo aleatorio
            float rangle = (float)((rg.NextDouble() * (Math.PI * 2)));

            // A velocidade vai ser multiplicada por um valor entre 1 e 0
            speed *= (float)rg.NextDouble();

            // Vai retornar um PointF com o Cos e um sen do angulo multiplicado pela velocidade
            return new PointF((float)Math.Cos(rangle) * speed, (float)Math.Sin(rangle) * speed);
        }


        // CONSTRUTOR
        public Particula(PointF Location, float Speed) : this(Location, RandomVector(Speed))
        {


        }

        // CONSTRUTOR
        public Particula(PointF Location, PointF Velocity)
        {
            // Define a posição inicial e Velocidade da Particula
            _Location = Location;
            _Velocity = Velocity;


        }

        // Calcula a Posição e a Velocidade de cada Particula a cada Frame
        public bool PerformFrame(Conj_Particulas part)
        {
            //Verifica se a Data de Nascimento da Particula é Null, se for, foca com a Data Atual
            if (DataNasc == null) DataNasc = DateTime.Now;

            // Calcula a Ñova posição da Particula
            // Somando a Antiga posição com a Velocidade
            _Location = new PointF(_Location.X + _Velocity.X, _Location.Y + _Velocity.Y);
            //Calcula a nova velocidade da Particula
            // Multiplicando a Velocidade anterior por 0.99
            _Velocity = new PointF(_Velocity.X * 0.99f, _Velocity.Y * 0.99f);


            // Ricochete nas paredes 
            if (_Location.X > 640 || _Location.X < 0) _Velocity.X = -Velocity.X;
            if (_Location.Y > 310 || _Location.Y < 0) _Velocity.Y = -Velocity.Y;

            

            // Verifica se a Data Atual menos a Data de Nascimento é maior que o tempo de Vida
            // Caso seja, significa que a Particula morreu e será removida posteriormente
            return DateTime.Now - DataNasc > TempoVida;

        }
        private int i = 255;
        // Deseja as Particulas no ecrã
        public void Draw(Graphics g)
        {
            Image image;     
            image = TrabalhoAPG.Properties.Resources.tennis_ball;
           
            // Desenha a Particula no Ecrã com a imagem selecionada na posição da Particula com o tamanho de 16 por 16 
            g.DrawImage(image, Location.X, Location.Y,16,16);
        } 


    }
}

