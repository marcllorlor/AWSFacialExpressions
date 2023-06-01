using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.Rekognition.Model;
using Amazon.Rekognition;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace AWSRekognitionLoginCares
{
    internal class ClAmazon
    {
        //Aqui declarem les claus del aws
        string awsKey = "INSERT KEY";
        string awsSecret = "INSERT SECRET KEY";
        string awsToken = "INSERT AWS TOKEN";

        //Aqui declarem la paraula que triarem aleatòriament
        string paraulaaleatoriatriada = null;
        string paraulatrobada = null;

        //Aqui declarem la emocio que haurem de fer
        string emocioaleatoriatriada = null;
        string emociotrobada = null;

        //Aqui declarem els bools de sha trobat emocio i sha trobat paraula
        bool shatrobatparaula = false;
        bool shatrobatemocio = false;

        //Aqui declararem el bool que fa que detecti les dos coses
        bool loginconseguit = false;

        //Aqui farem el constructor de la classe de deteccio de textos
        public ClAmazon(Bitmap bmp, string paraulaaleatoriatriada, string emocioaleatoriatriada)
        {
            //Aqui cridarem les dos funcions que farem servir per detectar el text i les emocions
            //A les dos els hi pasarem el bmp (És la imatge de la camara en forma de bitmap) i la seva paraula i emocio corresponent
            detectartextos(bmp, paraulaaleatoriatriada);
            detectaremocions(bmp, emocioaleatoriatriada);

        }

        

        private void detectaremocions(Bitmap bmp, string emocioaleatoriatriada)
        {
            try
            {
                //Declarem el convertor de imatges
                ImageConverter convertidor = new ImageConverter();
                //Declarem la imatge en bytes que tindra el valor del bitmap que ha capturat quan la camara ha apretat a fer la foto
                Byte[] imatgeEnBytes = (Byte[])convertidor.ConvertTo(bmp, typeof(byte[]));

                //Ni putissima idea de per que son aquestes tres merdes
                Amazon.Rekognition.Model.Image imatgeAWS = new Amazon.Rekognition.Model.Image();
                //Estem declarant el client de amazon amb les nostres credencials
                AmazonRekognitionClient clientAWSRekognition = new AmazonRekognitionClient(awsKey, awsSecret, awsToken, Amazon.RegionEndpoint.USEast1);
                RecognizeCelebritiesRequest peticioReconeixementCelebritats = new RecognizeCelebritiesRequest();

                //Tampoc tinc idea de que fa aquesta merda
                CelebrityRecognition celebritatsDetectades = new CelebrityRecognition();
                imatgeAWS.Bytes = new MemoryStream(imatgeEnBytes);
                peticioReconeixementCelebritats.Image = imatgeAWS;
                RecognizeCelebritiesResponse respostaReconeixementCelebritats = clientAWSRekognition.RecognizeCelebrities(peticioReconeixementCelebritats);

                //Per cada cara que detecti a dins de la imatge fara una volta al bucle: 2 cara, 2 cops fara el bucle
                foreach (ComparedFace c in respostaReconeixementCelebritats.UnrecognizedFaces)
                {
                    //Per cada emocio que presenti la persona fara una volta al bucle
                    //Com que cada cara té minim i maxim 6 emocions, el bucle sempre fara 6 voltes
                    foreach (Emotion e in c.Emotions)
                    {
                        //Si la emocio que ha detectat es igual a la emocio que hem triat aleatòriament
                        if (e.Type.ToString().ToLower() == emocioaleatoriatriada.ToLower())
                        {
                            //I el valor de la emocio es superior a 80
                            if (e.Confidence > 80)
                            {
                                //Direm que ha trobat la emocio
                                shatrobatemocio = true;
                            }
                            //Com que ja ha trobat la emocio, no cal que faci mes voltes al bucle
                            
                            break;
                        }
                    }
                }



            }
            catch (Exception excp)
            {
                //Si hi ha un error amb la merda de amazon, ens ho dira aqui. Aqui es a on cau quan les claus del Amazon estan outdateades o no son valides
                MessageBox.Show(excp.Message);
            }
        }

        private void detectartextos(Bitmap bmp, string paraulaaleatoriatriada)
        {
            //Pensa que aixo es la mateixa merda que les cares pero amb la deteccio de textos
            try
            {
                //La funcio aquesta esta mal feta ja que nomes pot detectar quan hi ha UNA SOLA paraula a la camara, si hi han mes paraules es molt provable que falli
                //La FUNCIO ESTA ARREGLADA ARA SI
                
                ImageConverter convertidor = new ImageConverter();
                Byte[] imatgeEnBytes = (Byte[])convertidor.ConvertTo(bmp, typeof(byte[]));

                Amazon.Rekognition.Model.Image imatgeAWS = new Amazon.Rekognition.Model.Image();
                AmazonRekognitionClient clientAWSRekognition = new AmazonRekognitionClient(awsKey, awsSecret, awsToken, Amazon.RegionEndpoint.USEast1);

                DetectTextRequest textosDetectats = new DetectTextRequest();

                imatgeAWS.Bytes = new MemoryStream(imatgeEnBytes);

                textosDetectats.Image = imatgeAWS;

                //Aqui es a on fem la deteccio de textos
                var deteccions = clientAWSRekognition.DetectText(textosDetectats);
                
                //Si la cantitat de paraules supera 0 entrara a dins del if , si no no
                if (deteccions.TextDetections.Count > 0)
                {
                    //Com que ens retorna les paraules en Json, haurem de fer desconvertir el json a una cosa que nosaltres entenguem
                    var jsonDeteccions = JsonConvert.SerializeObject(deteccions.TextDetections);

                    //Per cada paraula que trobi farem una volta al bucle
                    foreach (TextDetection t in deteccions.TextDetections)
                    {
                        //Si la paraula que ha trobat coincideix amb la paraula que hem triat aleatoriament entrara a dins del bucle
                        if (t.DetectedText.ToLower() == paraulaaleatoriatriada.ToLower())
                        {
                            //Posarem que hem trobat la paraula i sortirem del bucle/if de tot
                            shatrobatparaula = true;
                            break;
                        }
                        
                    }
                    
                }
            }
            catch (Exception excp)
            {
                //Si pasa algun error mentre s'executa el programa ens ho dira aqui
                MessageBox.Show(excp.Message);
            }
        }

        

        public bool comprovardoscoses()
        {
            //Aquesta funcio la cridarem desde l'arxiu MAIN del PROGAMA
            //Aquesta es la funcio que cridarem quan tinguem les dos coses fetes, FETES no vol dir que les dos estiguin be, vol dir que ha acabat el proces de les dos coses
            if (shatrobatemocio && shatrobatparaula)
            {
                loginconseguit = true;
            }
            return loginconseguit;
        }
    }
}
