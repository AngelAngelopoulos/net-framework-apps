﻿using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;
//using System.Drawing;
//using System.Threading;

namespace Drive_Thru
{
    public partial class Form1 : Form
    {
        Timer t;
        Automovil[] autos;
        AutoSprite[] autoSprites;
        Image[] imgAutos;
        Pen pen;
        //int[] numAuto;
        public Form1()
        {
            InitializeComponent();
            //Inicializamos el timer
            t = new Timer();
            //Elegimos el intervalo de las funciones 1000ms / 60hz = 16
            t.Interval = 16;
            //Elegimos la funcion que se llamara con cada tick del Timer
            t.Tick += T_Tick;
            //Color de la pluma del rectangulo (cuando se cambie por imagen no se necesitara)
            pen = new Pen(Color.Red);

            /* numAuto = new int[]
             {
                 new Random().Next(1,5),
                 new Random().Next(1,5),
                 new Random().Next(1,5)
             };*/

            imgAutos = new Image[]
            {
                //Image.FromFile(@"C:\Users\angelalvarado\source\repos\AngelAngelopoulos\Proyecto-Interfaces\Drive Thru\auto1.png"),
                Image.FromFile("auto1.png"),
                Image.FromFile("auto2.png"),
                Image.FromFile("auto3.png"),
                Image.FromFile("auto4.png"),
                Image.FromFile("auto5.png")
            };

            //Decalaramos los sprites (imagenes) de los autos que van a pasar
            autoSprites = new AutoSprite[]
            {
                //Se declaran tres, con estas separaciones, debido al ancho y largo de la ventana
                new AutoSprite(new Random().Next(5), new Rectangle(-100, 75, 200, 100)),
                new AutoSprite(new Random().Next(5), new Rectangle(-100, 250, 200, 100)),
                new AutoSprite(new Random().Next(5), new Rectangle(-100, 425, 200, 100))
            };


            //Declaramos el arreglo de Autos (hilos) que van a pasar
            autos = new Automovil[]
            {
                new Automovil(),
                new Automovil(),
                new Automovil()
            };

            //Inicializamos los hilos de los autos
            for (int i = 0; i < autos.Length; i++)
            {
                autos[i].initHilo();
            }

            //Inicializamos el Timer y con ello los Ticks 
            t.Start();
        }

        private void T_Tick(object sender, EventArgs e)
        {
            //Recorremos el arreglo de hilos que tenemos
            for (int i = 0; i < autos.Length; i++)
            {
                //si el estado de los autos esta activado
                if (autos[i].estado)
                {
                    //movemos las coordenadas en x del auto
                    autoSprites[i].fitImg.X += autos[i].vel;

                    //Si llega a la caseta
                    if (autoSprites[i].fitImg.X >= 290)
                    {
                        //se detiene el auto
                        autos[i].move = false;
                    }
                }
                //si el auto ya no se mueve
                else if (!autos[i].move)
                {
                    //manten el auto donde esta
                    autoSprites[i].fitImg.X = autoSprites[i].fitImg.X;
                }
                //si el auto llega al final de la carretera
                if (autoSprites[i].fitImg.X > 1200)
                {
                    //termina el hilo del ese auto
                    autos[i].termina = true;
                }

                //si el hilo del auto termina
                if (autos[i].termina)
                {
                    //instancia un nuevo auto
                    autos[i] = new Automovil();
                    //inciia el hilo de ese nuevo auto
                    autos[i].initHilo();

                    /*Aqui se instancian los sprites, segun que auto(hilo) se halla termnado*/
                    if (i == 0)
                    {
                        autoSprites[i] = new AutoSprite(new Random().Next(5), new Rectangle(-100, 75, 200, 100));
                    }
                    else if (i == 1)
                    {
                        autoSprites[i] = new AutoSprite(new Random().Next(5), new Rectangle(-100, 250, 200, 100));
                    }
                    else if (i == 2)
                    {
                        autoSprites[i] = new AutoSprite(new Random().Next(5), new Rectangle(-100, 425, 200, 100));
                    }
                }
                
            }
            /*if (automovil.estado)
            {
                rect.X += 5;
                //rect.Y += 5;
                //i++;

                if (rect.X > 200)
                {
                    automovil.move = false;
                }
                if (rect.X > 1100)
                {

                }
            }
               
            else if (!automovil.move)
                rect.X = rect.X;

            if (rect.X > 1200)
            {
                automovil.termina = true;
            }*/

            Invalidate();
  
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //e.Graphics.DrawImageUnscaled(background, 0, 0);
            //Recorre y dibuja cada auto (sprite)
            for (int i = 0; i < autoSprites.Length; i++)
            {
                //e.Graphics.DrawRectangle(pen, autoSprites[i]);
                if (autoSprites[i].noImg == 4)
                {
                    e.Graphics.DrawImageUnscaledAndClipped(imgAutos[autoSprites[i].noImg],
                        new Rectangle(autoSprites[i].fitImg.X,
                        autoSprites[i].fitImg.Y,
                        300, 100));
                }
                else
                {
                    e.Graphics.DrawImageUnscaledAndClipped(imgAutos[autoSprites[i].noImg],
                   autoSprites[i].fitImg);
                }
            }
        }
    }
}
