using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using AForge.Video;                 // hem instal·lat els paquets (Nuget) Aforge, Aforge.Video i Aforge.Video.DirectShow
using AForge.Video.DirectShow;
using Amazon.Rekognition;
using Amazon.Rekognition.Model;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;

namespace AWSRekognitionLoginCares
{
    public partial class fmAWSRekognition : Form
    {
        //Aqui declarem les llistes de emocions i paraules aleatòries
        //JO FAIG SERVIR LES LLISTES PER TENIR TOTES LES EMOCIONS LLISTADES, ES PODRIA FER AMB UNA ARRAY PERO TAMBE ES POT FER AIXI
        List<string> LlistaEmocions = new List<string>();
        List<string> ParaulesAleatories = new List<string>();
        List<string> llTextos = new List<string>();      

        //Aqui declarem la camara web
        FilterInfoCollection filtreCameres;
        VideoCaptureDevice kam = null;

        //Aqui declarem el bitmap que farem servir per mostrar la imatge de la camara web
        System.Drawing.Bitmap bmp = null;

        //Aqui estem declarant el nom de la camara, com que encara no saben quin és el deixem en null
        string nomcamara = null;

        //Aqui declarem la paraula que triarem aleatòriament
        string paraulaaleatoriatriada = null;
        string paraulatrobada = null;

        //Aqui declarem la emocio que haurem de fer
        string emocioaleatoriatriada = null;
        string emociotrobada = null;

        //Aqui declararem el bool que farem servir per saber si la paraula que ha posat l'usuari a la camara és la correcte o no
        bool shatrobatparaula = false;
        bool shatrobatemocio = false;
        
        //Aqui declarem la classe
        ClAmazon classeAmazon = null;


        public fmAWSRekognition()
        {
            InitializeComponent();
        }

        private void fmAWSRekognition_Load(object sender, EventArgs e)
        {
            //Aquesta es la funcio que s'executa quan s'obre el programa

            //Com que hem iniciat les llistes en valor null/buides les omplirem amb aquesta funcio
            afegirvalorllistes();

            //Aqui farem la tria de les paraules i les emocions randoms que haurem de fer per que s'executi el programa
            pillarvalorrandomparaules();
            pillarvalorrandomemocions();
            
            //Aquesta es la funcio que farem per executar les cameres i mostrar la camara web a dins del Form
            detectariobrircams();

            //Com que no tinc la imatge de la camara en gran , posare la imagen de la camara en un picturebox i la fare gran
            posargranimatgecaamara();
        }

        private void pillarvalorrandomemocions()
        {
            //Fem un simple random per triar una emocio de la llista
            emocioaleatoriatriada = LlistaEmocions[new Random().Next(0, LlistaEmocions.Count)];
            //I la mostrarem per pantalla a un label que tinc a dins del form
            lbEmocioTriada.Text = "La emocio que has de posar és: " + emocioaleatoriatriada;
        }

        private void pillarvalorrandomparaules()
        {
            //Fem un simple random per triar una paraula de la llista
            paraulaaleatoriatriada = ParaulesAleatories[new Random().Next(0, ParaulesAleatories.Count)];
            //I la mostrare per pantalla en una label que tinc a dins del form
            lbParaulaTriada.Text = "La paraula que has de posar és: " + paraulaaleatoriatriada;
        }

        private void posargranimatgecaamara()
        {
            //Cnavio la localitzacio de la imatge de la camara
            pbImatgeCamara.Location = new System.Drawing.Point(100, 100);
            //Canvio la mida de la camara
            pbImatgeCamara.Size = new System.Drawing.Size(this.Width-400, this.Height-200);
            //Poso la imatge en stretched, com que la camara no fa 300x300 faig que s'adapti a l'espai que te
            pbImatgeCamara.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void getCameres()
        {
            //Pillo totes les camares que tinc connectares al ordinador
            //De normal hauriem de tenir dos: -Una que es la nostre portatil i l'altre que és de OBS
            filtreCameres = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            //Per cada camara que tinguem al nostre ordinador fara una volta al bucle
            foreach (FilterInfo p in filtreCameres)
            {
                //Si la camara NO! és la de OBS entrarem a dins del bucle
                if (p.Name != "OBS Virtual Camera")
                {
                    //Aqui el nom de camara hauria de ser algo com: -HD WebCam o alguna merda generica
                    nomcamara = (p.Name);
                }

            }
        }

        
        private void detectariobrircams()
        {
            //Primer fem la funcio de detectar les cameres
            getCameres();
            //I despres d'aixo obrirem la camara
            obrirCamera();
        }

        private void obrirCamera()
        {
            //Aqui obrim la camara
            kam = new VideoCaptureDevice(filtreCameres[0].MonikerString); //ES MOLT IMPORTANT POSAR LO DE MONIKERSTRING, SI NO LA CAMARA NO SOBRE
            kam.NewFrame += nouFotograma; //per cada frame que faci la camara cridara a la funcio de noufotograma
            kam.Start(); 
        }

        private void nouFotograma(object sender, NewFrameEventArgs eventArgs)
        {
            //Com que nomes volem que a cada frame es canvii la imatge del picture box nomes posarem la seguent comanda
            pbImatgeCamara.Image = (Bitmap)(System.Drawing.Image)eventArgs.Frame.Clone();
        }

        private void afegirvalorllistes()
        {
            //Aqui afegirem les emocions a la llista de emocions
            afegirEmocions();
            //Aqui afegirem les paraules a la llista de paraules
            afegirParaulesAleatories();
        }

        private void afegirEmocions()
        {
            //Declaro la array de les emocions i faig un bucle per omplir la llista de emocions
            string[] llistaemocions = new string[] { "HAPPY", "ANGRY", "FEAR", "DISGUSTED", "SURPRISED", "SAD", "CONFUSED", "CALM" };
            for (int i = 0; i < llistaemocions.Length - 1; i++)
            {
                LlistaEmocions.Add(llistaemocions[i]);
            }
        }

        private void afegirParaulesAleatories()
        {
            //Declaro la arrau de les paraules i faig un bucle per omplir la llista de paraules
            string[] llistaparaulesrandom = new string[] { "Narguil", "Cotxe", "Gat", "Gos", "Ordinador", "Ratoli", "Pantalla", "Botella", "Aigua", "Verd", "Patata", "Estiu", "Teclat"};
            for(int i=0;i < llistaparaulesrandom.Length - 1; i++)
            {
                ParaulesAleatories.Add(llistaparaulesrandom[i]);
            }
        }
        //Aquesta sera la funcio que executarem sempre que apaguem el programa
        private void aturarKam()
        {
            //Si la camara esta encesa la apagarem
            if ((kam != null) && (kam.IsRunning))
            {
                kam.Stop();
                kam = null;
            }
            pbImatgeCamara.Image = Properties.Resources.camera_off;
        }

        private void fmAWSRekognition_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Aqui executarem la funcio de aturar la camara pasi el que pasi
            aturarKam();
        }

        private void btFerFoto_Click(object sender, EventArgs e)
        {
            //Cada cop que cliquem el boto de fer foto, la camara es guardara en un bitmap
            funcioFerFoto();
            //comprovardoscoses();
            comprovarlogin();
            
        }

        private void comprovarlogin()
        {
            //Si el login es correcte entrarem a dins del bucle
            if (classeAmazon.comprovardoscoses())
            {
                MessageBox.Show("Login correcte fent servir la classe");
            }
            else
            {
                MessageBox.Show("Login incorrecte fent servir la classe");            
            }
        }

        

        private void funcioFerFoto()
        {
            //Si la camara esta encesa farem la foto, si no no farem la foto
            if (kam.IsRunning)
            {
                //Aqui guardarem la imatge en bitmap
                bmp = (Bitmap)pbImatgeCamara.Image.Clone();

                //Aqui donarem la classe
                classeAmazon = new ClAmazon(bmp,paraulaaleatoriatriada, emocioaleatoriatriada);
                
            }
            
        }

        

        

        
    }

       

        
 }

